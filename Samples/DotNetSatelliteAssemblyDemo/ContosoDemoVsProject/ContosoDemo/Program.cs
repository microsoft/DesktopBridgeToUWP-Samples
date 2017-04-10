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
using System.Globalization;
using System.Reflection;
using System.Resources;
using System.Threading;

namespace ContosoDemo
{
    static class Program
    {
        /// <summary>
        /// This is a trivial test program that simply tries to resolve various resource
        /// strings using different languages, including one for which we have not localized.
        /// See 'readme.md' for more information.
        /// </summary>
        /// <param name="commandLine">Unused; command-line arguments</param>
        static void Main(string[] commandLine)
        {
            // If you are experimenting with Desktop App Converter and AppX packaging, the app
            // is launched by the Windows shell and not VS. If you want to debug you need
            // to attach the debugger manually. 
            // Obviously this would not appear in a real app.
            if (!Debugger.IsAttached)
                Console.WriteLine("Attach debugger now if needed.");

            // If the app is not running in a packaged context, we don't need to provide
            // a resolver.
            if (PriResourceResolver.IsSupported())
            {
                // Similarly, this would not appear in a real app, but is helpful for diagnostic
                // purposes as you experiment with desktop packaging.
                Console.WriteLine("{0}Attempt to resolve assemblies via MRT? [ Y / N ]", Environment.NewLine);
                var key = Console.ReadKey();

                if (key.KeyChar != 'n' && key.KeyChar != 'N')
                {
                    // This is the important part of this sample!
                    PriResourceResolver.Enable();
                    Console.WriteLine("{0}Resolving assemblies via MRT . . .", Environment.NewLine);
                }
                else
                    Console.WriteLine("{0}Ignoring assembly resolution failures . . .", Environment.NewLine);
            }
            else
            {
                Console.WriteLine("Press any key to continue . . .{0}", Environment.NewLine);
                Console.ReadKey();
            }

            // Load the same localized string for several different languages
            RunTest();

            Console.WriteLine("Press any key to continue . . .");
            Console.ReadKey();
        }

        /// <summary>
        /// Simple code to display text in a variety of localized languages.
        /// Requires the caller to have provided a handler for the TextAvailable event
        /// before calling. The Desktop app sends the text to the console, and the
        /// UWP app sends it to a ListView.
        /// </summary>
        static void RunTest()
        {
            // Attempt to display a resource string using whatever the user's current
            // language is (the sample assumes it is en-US, but that doesn't really matter;
            // it will fall back to the default language if it's not one of the ones
            // that have been provided)
            Console.WriteLine(GetLocalizedGreeting());

            // Repeat the test, but in German (there is a resource for this, so it should
            // succeed)
            CultureInfo newCulture = new CultureInfo("de-DE");
            Thread.CurrentThread.CurrentUICulture = newCulture;
            Console.WriteLine(GetLocalizedGreeting());

            // Repeat the test, but in French (there is a resource for this, so it should
            // succeed)
            newCulture = new CultureInfo("fr-FR");
            Thread.CurrentThread.CurrentUICulture = newCulture;
            Console.WriteLine(GetLocalizedGreeting());

            // Repeat the test for Spanish (there IS NOT a resource for this, so it will
            // fall-back to the default language)
            newCulture = new CultureInfo("es-MX");
            Thread.CurrentThread.CurrentUICulture = newCulture;
            Console.WriteLine(GetLocalizedGreeting());
        }

        /// <summary>
        /// Return a resource string that is potentially localized.
        /// This is standard .NET resource code; it has nothing to do with MRT or UWP packaged apps.
        /// 
        /// The .NET resource loading infrastructure will indirectly call into the MRT code via 
        /// the AssemblyResolveHandler if it is set by calling UseMrtForResourceLoading
        /// </summary>
        static string GetLocalizedGreeting()
        {
            ResourceManager resourceManager = new ResourceManager("ContosoDemo.Strings.greetings", Assembly.GetExecutingAssembly());
            string text = resourceManager.GetString("Greeting");
            return String.Format("{0}: {1}", CultureInfo.CurrentUICulture, text);
        }
    }
}
