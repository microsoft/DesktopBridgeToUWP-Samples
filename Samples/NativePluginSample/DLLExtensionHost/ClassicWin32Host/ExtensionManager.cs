//*********************************************************
//
// Copyright (c) Microsoft. All rights reserved.
// This code is licensed under the MIT License (MIT).
// THIS CODE IS PROVIDED *AS IS* WITHOUT WARRANTY OF
// ANY KIND, EITHER EXPRESS OR IMPLIED, INCLUDING ANY
// IMPLIED WARRANTIES OF FITNESS FOR A PARTICULAR
// PURPOSE, MERCHANTABILITY, OR NON-INFRINGEMENT.
//
//*********************************************************

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Threading;
using System.Threading.Tasks;
using Windows.ApplicationModel.Core;
using Windows.ApplicationModel;
using Windows.ApplicationModel.AppExtensions;  // App Extensions!!
using Windows.ApplicationModel.AppService;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.UI.Core;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Imaging;
using Windows.System.Threading;

namespace ClassicWin32Host
{
    class ExtensionManager
    {
        private ObservableCollection<Extension> _extensions;
        private string _contract;
        private Dispatcher _dispatcher;
        private AppExtensionCatalog _catalog;
        private StorageFolder _folder;

        public ExtensionManager(string contract, Dispatcher dispatcher)
        {
            // extensions list   
            _extensions = new ObservableCollection<Extension>();

            // thread which creates the observable collection must be the thread to modify it
            _dispatcher = dispatcher;

            // catalog & contract
            _contract = contract;
            _catalog = AppExtensionCatalog.Open(_contract);
            _folder = null;

            // set up extension management events
            _catalog.PackageInstalled += Catalog_PackageInstalled;
            _catalog.PackageUpdated += Catalog_PackageUpdated;
            _catalog.PackageUninstalling += Catalog_PackageUninstalling;
            _catalog.PackageUpdating += Catalog_PackageUpdating;
            _catalog.PackageStatusChanged += Catalog_PackageStatusChanged;

            // Scan all extensions
            FindAllExtensions();
        }

        public ObservableCollection<Extension> Extensions
        {
            get { return _extensions; }
        }

        public string Contract
        {
            get { return _contract; }
        }

        public StorageFolder Folder
        {
            get { return _folder; }
        }

        public async void FindAllExtensions()
        {    
            // load all the extensions currently installed
            IReadOnlyList<AppExtension> extensions = await _catalog.FindAllAsync();
            foreach (AppExtension ext in extensions)
            {
                // load this extension
                await LoadExtension(ext);
            }
        }

        private async void Catalog_PackageInstalled(AppExtensionCatalog sender, AppExtensionPackageInstalledEventArgs args)
        {
            await _dispatcher.BeginInvoke((Action)(async () => { 
                foreach (AppExtension ext in args.Extensions)
                {
                    await LoadExtension(ext);
                }
            }) ,DispatcherPriority.Normal, null);
        }

        private async Task LoadAllExtensions(AppExtensionPackageInstalledEventArgs args)
        {
            foreach (AppExtension ext in args.Extensions)
            {
                    await LoadExtension(ext);
            }
            return;
        }

        // package has been updated, so reload the extensions
        private async void Catalog_PackageUpdated(AppExtensionCatalog sender, AppExtensionPackageUpdatedEventArgs args)
        {
            await _dispatcher.BeginInvoke((Action)(async () => { 
                foreach (AppExtension ext in args.Extensions)
                {
                    await LoadExtension(ext);
                }
            }) ,DispatcherPriority.Normal, null);
        }

        // package is updating, so just unload the extensions
        private async void Catalog_PackageUpdating(AppExtensionCatalog sender, AppExtensionPackageUpdatingEventArgs args)
        {
            await UnloadExtensions(args.Package);
        }

        // package is removed, so unload all the extensions in the package and remove it
        private async void Catalog_PackageUninstalling(AppExtensionCatalog sender, AppExtensionPackageUninstallingEventArgs args)
        {
            await RemoveExtensions(args.Package);
        }

        // package status has changed, could be invalid, licensing issue, app was on USB and removed, etc
        private async void Catalog_PackageStatusChanged(AppExtensionCatalog sender, AppExtensionPackageStatusChangedEventArgs args)
        {
            // get package status
            if (!(args.Package.Status.VerifyIsOK()))
            {
                // if it's offline unload only
                if (args.Package.Status.PackageOffline)
                {
                    await UnloadExtensions(args.Package);
                }

                // package is being serviced or deployed
                else if (args.Package.Status.Servicing || args.Package.Status.DeploymentInProgress)
                {
                    // ignore these package status events
                }

                // package is tampered or invalid or some other issue
                // glyphing the extensions would be a good user experience
                else
                {
                    await RemoveExtensions(args.Package);
                }

            }
            // if package is now OK, attempt to load the extensions
            else
            {
                // try to load any extensions associated with this package
                await LoadExtensions(args.Package);
            }
        }
        

        // loads an extension
        public async Task LoadExtension(AppExtension ext)
        {

            // create plugins folder if it doesn't already exist
            if (_folder == null)
            {
                try
                {
                    // get or create folder for the extension that is unique
                    _folder = await ApplicationData.Current.LocalFolder.CreateFolderAsync("Plugins", CreationCollisionOption.OpenIfExists);
                }
                catch
                {
                    // Bad Things have happened
                    // But it's OK, becuase it'll be re-created later on.
                }
            }

            // get unique identifier for this extension
            string identifier = ext.AppInfo.AppUserModelId + "!" + ext.Id;

            // load the extension if the package is OK
            if (!(ext.Package.Status.VerifyIsOK()
                    // This is where we'd normally do signature verfication, but for demo purposes it isn't important
                    // Below is an example of how you'd ensure that you only load store-signed extensions :)
                    //&& ext.Package.SignatureKind == PackageSignatureKind.Store
                    ))
            {
                // if this package doesn't meet our requirements
                // ignore it and return
                return;
            }

            // if its already existing then this is an update
            Extension existingExt = _extensions.Where(e => e.UniqueId == identifier).FirstOrDefault();

            // new extension
            if (existingExt == null)
            {
                // create new extension
                Extension nExt = new Extension(ext);

                // Add it to extension list
                _extensions.Add(nExt);

                // load it
                await nExt.Load();
            }
            // update
            else
            {
                // unload the extension
                existingExt.Unload();

                // update the extension
                await existingExt.Update(ext);
            }
        }

        

        // loads all extensions associated with a package - used for when package status comes back
        public async Task LoadExtensions(Package package)
        {
            await _dispatcher.BeginInvoke((Action)(() => {
                _extensions.Where(ext => ext.AppExtension.Package.Id.FamilyName == package.Id.FamilyName).ToList().ForEach(async e => { await e.Load(); });
            }), DispatcherPriority.Normal, null);
        }

        // unloads all extensions associated with a package - used for updating and when package status goes away
        public async Task UnloadExtensions(Package package)
        {
            await _dispatcher.BeginInvoke((Action)(() => {
                _extensions.Where(ext => ext.AppExtension.Package.Id.FamilyName == package.Id.FamilyName).ToList().ForEach(e => { e.Unload(); });
            }), DispatcherPriority.Normal, null);
        }

        // removes all extensions associated with a package - used when removing a package or it becomes invalid
        public async Task RemoveExtensions(Package package)
        {
            await _dispatcher.BeginInvoke((Action)(() => {
                _extensions.Where(ext => ext.AppExtension.Package.Id.FamilyName == package.Id.FamilyName).ToList().ForEach(e => { e.Unload(); _extensions.Remove(e); });
            }), DispatcherPriority.Normal, null);
        }
        

        public void RemoveExtension(Extension ext)
        {
            // Centennial apps cannot use this method. See AppExtensionCatalog documentation for details.
            //await _catalog.RequestRemovePackageAsync(ext.AppExtension.Package.Id.FullName);
        }

        #region Extra exceptions
        // For exceptions using the Extension Manager
        public class ExtensionManagerException : Exception
        {
            public ExtensionManagerException() { }

            public ExtensionManagerException(string message) : base(message) { }

            public ExtensionManagerException(string message, Exception inner) : base(message, inner) { }
        }
        #endregion
    }

    public class Extension : INotifyPropertyChanged
    {
        #region Member Vars
        private AppExtension _extension;
        private bool _enabled;
        private bool _loaded;
        private bool _offline;
        private string _uniqueId;
        private StorageFolder _folder;
        private StorageFile _file;
        private Library _lib;
        private System.Windows.Visibility _visibility;

        private readonly object _sync = new object();

        public event PropertyChangedEventHandler PropertyChanged;
        #endregion

        public Extension(AppExtension ext)
        {
            _extension = ext;
            _enabled = false;                // default enabled/disabled
            _loaded = false;
            _offline = false;
            _folder = null;
            _file = null;
            _lib = new Library();
            _visibility = System.Windows.Visibility.Collapsed;

            //AUMID + Extension ID is the unique identifier for an extension
            _uniqueId = ext.AppInfo.AppUserModelId + "!" + ext.Id;
        }

        ~Extension()
        {
            // unload the DLL
            _lib.Free();
        }

        #region Properties
        public string UniqueId
        {
            get { return _uniqueId; }
        }

        public bool Enabled
        {
            get { return _enabled; }
        }

        public bool Offline
        {
            get { return _offline; }
        }

        public AppExtension AppExtension
        {
            get { return _extension; }
        }

        public System.Windows.Visibility Visible
        {
            get { return _visibility; }
        }
        #endregion
        
        public Library Lib
        {
            get { return _lib; }
        }

        // Test that the extension works
        public void TestExtension()
        {
            if (_enabled && _lib.Func.FunctionsLoaded)
            {
                int rnum = (new Random()).Next(1, 50);
                string msg = "Extension loaded!"
                    + "\n" + _extension.DisplayName
                    + "\n" + _uniqueId
                    + "\n ++" + rnum.ToString() + " = " + _lib.Func.Increment(rnum).ToString()
                    + "\n" + _lib.Func.EditText(@"TestStr")
                    ;
                System.Windows.MessageBox.Show(msg);
            }
        }

        public async Task Update(AppExtension ext)
        {
            // ensure this is the same uid
            string identifier = ext.AppInfo.AppUserModelId + "!" + ext.Id;
            if (identifier != this.UniqueId)
            {
                return;
            }

            // update the extension
            this._extension = ext;
            RaisePropertyChanged("AppExtension");

            // load it & signal update
            await Load(true);
        }

        // this controls loading of the extension
        public async Task Load(bool isUpdate)
        {
            #region Error Checking
            // if it's not enabled or already loaded, don't load it
            if (!_enabled || _loaded)
            {
                return;
            }

            // make sure package is OK to load
            if (!_extension.Package.Status.VerifyIsOK())
            {
                return;
            }
            #endregion

            // If we are updating we won't use existing files and will re-copy them instead.
            if (isUpdate)
            {
                _folder = null;
                _file = null;
                _lib.Free();
            }

            // load the file if we don't already have it.
            if (_file == null)
            { 
                // get extension from the other package 
                StorageFolder folder = await _extension.GetPublicFolderAsync();
                if (folder != null)
                {
                    try
                    {
                        StorageFile extensionfile = await folder.GetFileAsync(@"ClassicDLL.dll");
                        if (extensionfile != null)
                        {
                            // get or create folder for the extension that is unique
                            _folder = await AppData.ExtensionManager.Folder.CreateFolderAsync(_uniqueId, CreationCollisionOption.OpenIfExists);

                            // copy the dll
                            _file = await extensionfile.CopyAsync(_folder, extensionfile.Name, NameCollisionOption.ReplaceExisting);
                        }
                    }
                    catch (Exception)
                    {
                        // something bad happened
                        return;
                    }
                }
            }

            // load the dll
            _lib.Load(_file.Path);

            //TestExtension();

            // all went well, set state
            _loaded = true;
            _visibility = System.Windows.Visibility.Visible;
            RaisePropertyChanged("Visible");
            _offline = false;
        }
        public Task Load()
        {
            return this.Load(false);
        }

        // This enables the extension
        public async Task Enable()
        {
            // indicate desired state is enabled
            _enabled = true;

            // load the extension
            await Load();
        }

        // this is different from Disable, example: during updates where we only want to unload -> reload
        // Enable is user intention. Load respects enable, but unload doesn't care
        public void Unload()
        {
            // unload it
            lock (_sync)
            {
                if (_loaded)
                {

                    // unload the DLL
                    _lib.Free();

                    _loaded = false;
                    _visibility = System.Windows.Visibility.Collapsed;
                    RaisePropertyChanged("Visible");
                }
            }
        }

        // user-facing action to disable the extension
        public void Disable()
        {
            // only disable if it is enabled
            if (_enabled)
            {
                // set desired state to disabled
                _enabled = false;
                // unload the app
                Unload();
            }
        }
        #region PropertyChanged

        // typical property changed handler
        private void RaisePropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }
        #endregion

    }
}