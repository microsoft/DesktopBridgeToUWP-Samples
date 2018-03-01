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
using System.Threading.Tasks;
using Windows.ApplicationModel.AppService;
using Windows.ApplicationModel.Core;
using Windows.ApplicationModel.DataTransfer;
using Windows.Foundation.Collections;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.ApplicationModel;
using Windows.UI.Popups;
using Windows.UI.Core;
using Windows.System;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace NorthwindDemo
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public ProductList Products { get; set; }
        ApplicationView currentView;

        public MainPage()
        {
            this.InitializeComponent();

            // Set up reference to this window so secondary windows can find it
            (App.Current as App).MainPageInstance = this;

            // Register for accelerator key events used for button hotkeys
            Window.Current.CoreWindow.Dispatcher.AcceleratorKeyActivated += Dispatcher_AcceleratorKeyActivated;

            #region SqlBridge connection event handler registrations
            // Register to receive event when connection to desktop SqlBridge app has been built
            (App.Current as App).AppServiceConnected += MainPage_AppServiceConnected;

            // Register to receive event when this window is closing
            // Needed so we can signal the SqlBridge app to shut down too
            currentView = ApplicationView.GetForCurrentView();
            currentView.Consolidated += CurrentView_Consolidated;

            // Launch FullTrust SqlBridge app that communicates with SQL Server
            // The name of this app is found in Package.appxmanifest
            FullTrustProcessLauncher.LaunchFullTrustProcessForCurrentAppAsync();
#endregion
        }
        #region Code for hotkey handling
        private void Dispatcher_AcceleratorKeyActivated(CoreDispatcher sender, AcceleratorKeyEventArgs args)
        {
            if (args.EventType.ToString().Contains("Down"))
            {
                var ctrl = Window.Current.CoreWindow.GetKeyState(VirtualKey.Control);
                if (ctrl.HasFlag(CoreVirtualKeyStates.Down))
                {
                    switch (args.VirtualKey)
                    {
                        case VirtualKey.V:
                            ViewOrders_Tapped(this, null);
                            break;
                        case VirtualKey.N:
                            NewOrder_Tapped(this, null);
                            break;
                    }
                }
            }
        }
#endregion
        #region Load Products table from SQL Server
        private async void MainPage_AppServiceConnected(object sender, EventArgs e)
        {
            // App service is connected now so signal console app to send data
            await LoadProducts();
        }
        private async Task LoadProducts()
        {
            await LoadProductsUsingSqlBridge();
            if (Products is ProductList)
            {
                (App.Current as App).Products = Products;
                InventoryList.ItemsSource = Products;
            }
        }
        private async Task LoadProductsUsingSqlBridge()
        {
            Products = new ProductList();
            if ((App.Current as App).DatabaseServiceConnection != null)
            {
                var connString = (App.Current as App).ConnectionString;
                ValueSet valueSet = new ValueSet();
                valueSet.Add("request", "GetProducts");
                valueSet.Add("ConnectionString", connString);

                AppServiceResponse response = await 
                    (App.Current as App).DatabaseServiceConnection.SendMessageAsync(valueSet);
                if (response.Status == AppServiceResponseStatus.Success)
                {
                    var json = (string)(response.Message["response"]);
                    if (!string.IsNullOrEmpty(json))
                    {
                        Products.LoadFromJsonString(json);
                    }
                }
                else
                {
                    await new MessageDialog("Error: No response from SqlBridge!").ShowAsync();
                }
            }
            else
            {
                await new MessageDialog("Cannot access SQL Server: SqlBridge is not running!").ShowAsync();
            }
        }
        private async void CurrentView_Consolidated(ApplicationView sender, ApplicationViewConsolidatedEventArgs args)
        {
            // Clean up code to shut down the SqlBridge app as this one closes
            if ((App.Current as App).DatabaseServiceConnection != null)
            {
                // Signal app service console app to close itself
                ValueSet valueSet = new ValueSet();
                valueSet.Add("request", "Exit");
                var ignored = await (App.Current as App).DatabaseServiceConnection.SendMessageAsync(valueSet);
            }
            Application.Current.Exit();
        }

        #endregion

        private async void NewOrder_Tapped(object sender, TappedRoutedEventArgs e)
        {
            // Create New Orders window on new thread
            // Good article on threading issues with multiple windows: http://mikaelkoskinen.net/post/uwp-multi-view-communication
            var newView = CoreApplication.CreateNewView();
            int newViewId = 0;
            await newView.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
            {
                Frame frame = new Frame();
                frame.Navigate(typeof(OrderPage), null);
                Window.Current.Content = frame;
                Window.Current.Activate();

                // Save Id of Orders view
                newViewId = ApplicationView.GetForCurrentView().Id;
            });

            // Show the Orders window as standalone window
            // and switch to the new window
            if (newViewId != 0)
            {
                bool isShown = await ApplicationViewSwitcher.TryShowAsStandaloneAsync(newViewId);
                if (isShown)
                {
                    await ApplicationViewSwitcher.SwitchAsync(newViewId);
                }
            }
        }
        #region Drag handling code 
        // Event handlers are wired up in the XAML
        private void InventoryList_DragOver(object sender, DragEventArgs e)
        {
            e.AcceptedOperation = DataPackageOperation.Copy;
        }

        private void InventoryList_DragItemsStarting(object sender, DragItemsStartingEventArgs e)
        {
            if (InventoryList.SelectedItem != null)
            {
                var product = (Product)InventoryList.SelectedItem;
                e.Data.SetText(product.ProductID.ToString());
                e.Data.RequestedOperation = DataPackageOperation.Copy;
            }
        }
        #endregion

        private async void ViewOrders_Tapped(object sender, TappedRoutedEventArgs e)
        {
            // Create  Orders window on new thread
            var newView = CoreApplication.CreateNewView();
            int newViewId = 0;
            await newView.Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
            {
                Frame frame = new Frame();
                frame.Navigate(typeof(ViewOrders), null);
                Window.Current.Content = frame;
                Window.Current.Activate();

                // Save Id of Orders view
                newViewId = ApplicationView.GetForCurrentView().Id;
            }); 

            // Show the Orders window as standalone window
            // and switch control to the Orders window
            if (newViewId != 0)
            {
                bool isShown = await ApplicationViewSwitcher.TryShowAsStandaloneAsync(newViewId);
                if (isShown)
                {
                    await ApplicationViewSwitcher.SwitchAsync(newViewId);
                }
            }
        }
    }
}
