//*
//
// Copyright (c) Microsoft. All rights reserved.
// This code is licensed under the MIT License (MIT).
// THIS CODE IS PROVIDED AS IS WITHOUT WARRANTY OF
// ANY KIND, EITHER EXPRESS OR IMPLIED, INCLUDING ANY
// IMPLIED WARRANTIES OF FITNESS FOR A PARTICULAR
// PURPOSE, MERCHANTABILITY, OR NON-INFRINGEMENT.
//
//*

using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Resources.Core;

namespace ContosoDemo
{
    /// <summary>
    /// Helper class that provides a bridge between .NET Satellite Assemblies and MRT
    /// Resource Packages for finding localized assemblies.
    /// </summary>
    static class PriResourceResolver
    {
        // Helper method to determine if the PRI resolver is supported (ie, is this app
        // running in a packaged context; this will be false if the app is just run
        // directly from the command line
        internal static bool IsSupported()
        {
            try
            {
                // If we have a package ID then we are running in a packaged context
                var temp = Windows.ApplicationModel.Package.Current.Id;
                return true;
            }
            catch
            {
                return false;
            }
        }

        // HRESULT failures that are expected from MRT
        private static class KnownMrtFailureCodes
        {
            // The HRESULT thrown when MRT is used in a normal desktop app
            internal const int MrtNeedsAppXPackagedApp = -2147009760;

            // The HRESULT thrown when a file is missing from the package
            internal const int MissingDefaultOrNeutralResource = -2147009780;
        }

        /// <summary>
        /// Trivial helper to use MRT resource resolving.
        /// </summary>
        internal static void Enable()
        {
            // The call to set the AssemblyResolve handler is the key part of this sample; 
            // everything else is test code or scaffolding.
            AppDomain.CurrentDomain.AssemblyResolve += AssemblyResolveHandler;
        }

        /// <summary>
        /// Provides an implementation of the AppDomain.AssemblyResolve handler that
        /// will attempt to lookup .NET Satellite Assemblies that have been re-packaged
        /// as MRT Resource Packs.
        /// Note that if you already use the AssemblyResolve handler to perform other
        /// assembly look-up tasks, you will need to incorporate this code into your
        /// existing handler
        /// </summary>
        /// <param name="sender">Unused</param>
        /// <param name="args">Information about the assembly that failed to load</param>
        /// <returns>The newly-loaded assembly, or null if it can't be found</returns>
        internal static Assembly AssemblyResolveHandler(object sender, ResolveEventArgs args)
        {
            Debug.WriteLine("---- Begin AssemblyResolve event handler ----");
            Debug.WriteLine(string.Format("Requested Assembly='{0}'", args.Name));

            // First, we need to get the base filename and language from the full assembly name string.
            // It's of the form "AssemblyName, Version=a.b.c.d, Culture=xx-yy, PublicKeyToken=xxxx, etc."
            // We don't need to parse it by hand; the AssemblyName class will do it for us
            AssemblyName fullAssemblyName = new AssemblyName(args.Name);
            string desiredAssemblyName = fullAssemblyName.Name;
            string desiredCulture = fullAssemblyName.CultureName;

            Debug.WriteLine("Requested Assembly Name='{0}', language='{1}'", desiredAssemblyName, desiredCulture);

            // The only expected exceptions are for use in a non-AppX context (when the app is sharing
            // code with (eg) a Windows 7 desktop app) or for missing fallback resources.
            try
            {
                // A ResourceContext includes things like the current language, scale factor etc.
                // In a normal UWP you would use GetForCurrentView(), but this is a Win32 app 
                // and it doesn't have 'views'. We will use the generic context instead
                ResourceContext resourceContext = ResourceContext.GetForViewIndependentUse();

                // Override whatever the context's language is with the language requested by .NET
                // This would cover the case where (eg) .NET code had explicitly set a different
                // culture from the user's preferred culture.
                resourceContext.Languages = new[] { desiredCulture };

                // Get the current WinRT ResourceManager instance. Note this would require a
                // 'using' alias if we were trying to mix and match .NET resource code and
                // WinRT resource code in the same file, since they both have a type with this name.
                ResourceManager resourceManager = ResourceManager.Current;

                // The resource manager groups together different resources types under different sub-trees.
                // All files are under the "Files" subtree, so we retrieve that
                // (This is optional - we could prepend "files/" to the resource name instead)
                ResourceMap fileResources = resourceManager.MainResourceMap.GetSubtree("Files");

                // TODO: Add your own naming conventions here, if necessary
                // This is the filename as it appeared on-disk when the package was created. At a minimum, you
                // need to add ".dll" on the end, but you might also need to prepend a folder name or do other
                // modifications to match your configuration.
                string targetFileName = string.Format(@"{0}.dll", desiredAssemblyName);

                // From all the resources available, we want to look for the one with the target filename
                NamedResource desiredResource = fileResources[targetFileName];

#if DEBUG_CANDIDATES
                // Print out the list of candidates and their qualifiers, in sorted order
                // Useful if something isn't resolving the way you expect. See MSDN for more information
                // on the properties being used.
                int i = 0;
                Debug.WriteLine(" Candidates:");
                foreach (ResourceCandidate candidate in desiredResource.ResolveAll(resourceContext))
                {
                    Debug.WriteLine("  {0}: {1}, Match={2}, Default={3}", i, 
                        candidate.ValueAsString, candidate.IsMatch, candidate.IsDefault);
                    Debug.WriteLine("   Qualifiers:");
                    foreach (var qualifier in candidate.Qualifiers)
                    {
                        Debug.WriteLine("    {0}={1}, Match={2}, Default={3}", 
                            qualifier.QualifierName, qualifier.QualifierValue, 
                            qualifier.IsMatch, qualifier.IsDefault);
                    }
                    ++i;
                }
#endif

                // Get the best-matching candidate (filename) to load as an assembly
                // NOTE: If this throws the following exception:
                // 
                //   The ResourceMap or NamedResource has an item that does not have default or neutral 
                //   resource. (Exception from HRESULT: 0x80073B0C)
                // 
                // then double-check your MakePRI command and ensure it didn't complain about missing files:
                // 
                //   MakePRI: warning 0xdef01051: No default or neutral resource given for '<filename>'. 
                //   The application may throw an exception for certain user configurations when 
                //   retrieving the resources.
                //
                ResourceCandidate bestCandidate = desiredResource.Resolve(resourceContext);

                // Candidates have various properties, but we really only need the value at this point
                string absoluteFileName = bestCandidate.ValueAsString;

                // Now we have the filename, we should be able to do this:
                // 
                //   Assembly resourceAssembly = Assembly.LoadFrom(absoluteFileName);
                //
                // but due to a limitation with .NET and Windows resource packages, we need
                // to use an "Unsafe" load operation. To make sure we don't accidentally load
                // assemblies that really are unsafe (eg, from the network) we use this helper
                // function to validate the input first.
                // In this scenario the extra protections are uneccessary, but for anyone who copies
                // this code into a broader context, it will be useful.
                Assembly resourceAssembly = LoadAssemblyFromPackageGraph(absoluteFileName, Package.Current);

                Debug.WriteLine("Resolved Assembly Name='{0}', language='{1}'", 
                    absoluteFileName, resourceAssembly.GetName().CultureName);

                // Finally, return the assembly we have loaded
                return resourceAssembly;
            }
            catch (Exception ex)
            {
                // This is expected to fail for non-AppX contexts.
                if (ex.HResult == KnownMrtFailureCodes.MrtNeedsAppXPackagedApp)
                {
                    Debug.WriteLine("Info: MRT only works for AppX packaged apps; returning null.");
                    return null;
                }

                // This happens if you are missing a fallback file, but might not be fatal.
                // See comment above about MakePRI warnings
                if (ex.HResult == KnownMrtFailureCodes.MissingDefaultOrNeutralResource)
                {
                    Debug.WriteLine("Error: No neutral resource supplied; returning null.");
                    return null;
                }

                // Otherwise, something else went wrong that needs to be looked at.
                Console.WriteLine("Excpetion while trying to find assembly: {0}\r\n{1}", ex.Message, ex.ToString());
                throw;
            }
            finally
            {
                Debug.WriteLine("---- End AssemblyResolve event handler   ----");
            }
        }

        /// <summary>
        /// Loads an assembly, by name, only if it originated from the package graph (the package plus any
        /// dependencies). This ensures we don't accidentally load a file from the Temporary Internet Files folder,
        /// for example, or some other random untrusted location
        /// </summary>
        /// <param name="absoluteAssemblyName">Name of the assembly to load</param>
        /// <param name="package">The Package that the assembly is supposed to be part of</param>
        /// <returns>The loaded assembly</returns>
        private static Assembly LoadAssemblyFromPackageGraph(string absoluteAssemblyName, Package package)
        {
            if (IsFileNameInsideDirectoryName(absoluteAssemblyName, package.InstalledLocation.Path))
            {
                return Assembly.UnsafeLoadFrom(absoluteAssemblyName);
            }

            foreach (var dependency in package.Dependencies)
            {
                if (IsFileNameInsideDirectoryName(absoluteAssemblyName, dependency.InstalledLocation.Path))
                {
                    return Assembly.UnsafeLoadFrom(absoluteAssemblyName);
                }
            }

            throw new InvalidOperationException("Tried to load an assembly from outside the package graph");
        }

        /// <summary>
        /// Checks if a filename resides inside a directory (or its subdirectories). A simple substring 
        /// match is performed to verify the file is inside the directory; note that this might give false 
        /// negatives for complex scenarios like mapped network drives, symlinkgs, etc. where the same 
        /// directory has two or more names. 
        /// </summary>
        /// <param name="absoluteFileName">Absolute filename to check</param>
        /// <param name="absoluteDirectoryName">Absolute directory name to check within</param>
        /// <returns>True if the filename is inside the directory; false otherwise</returns>
        private static bool IsFileNameInsideDirectoryName(string absoluteFileName, string absoluteDirectoryName)
        {
            var fileInfoName = new FileInfo(absoluteFileName).FullName;
            var directoryInfoName = new DirectoryInfo(absoluteDirectoryName).FullName;
            if (!directoryInfoName.EndsWith(Path.DirectorySeparatorChar.ToString()))
                directoryInfoName += Path.DirectorySeparatorChar;

            if (String.Compare(fileInfoName, 0, directoryInfoName, 0, directoryInfoName.Length, StringComparison.OrdinalIgnoreCase) == 0)
                return true;

            return false;
        }
    }
}
