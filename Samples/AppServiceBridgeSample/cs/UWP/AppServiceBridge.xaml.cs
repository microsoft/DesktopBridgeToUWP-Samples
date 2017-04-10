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

using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using System;
using Windows.UI.Popups;
using Windows.Foundation.Collections;
using Windows.ApplicationModel.AppService;

namespace SDKTemplate
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class AppServiceBridge : Page
    {
        private MainPage rootPage;

        public AppServiceBridge()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            rootPage = MainPage.Current;
        }

        /// <summary>
        /// Launches the Win32 background process via the new "fullTrustProcess" extenstion
        /// </summary>
        private async void LaunchBackgroundProcess_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Make sure the BackgroundProcess is in your AppX folder, if not rebuild the solution
                await Windows.ApplicationModel.FullTrustProcessLauncher.LaunchFullTrustProcessForCurrentAppAsync();
            }
            catch (Exception)
            {
                MessageDialog dialog = new MessageDialog("Rebuild the solution and make sure the BackgroundProcess is in your AppX folder");
                await dialog.ShowAsync();
            }
        }
        /// <summary>
        /// Sends message to the full trust process and receives a response back
        /// </summary>
        private async void SendRequest_Click(object sender, RoutedEventArgs e)
        {
            ValueSet valueSet = new ValueSet();
            valueSet.Add("request", MessageToSend.Text);

            if (App.Connection != null)
            {
                AppServiceResponse response = await App.Connection.SendMessageAsync(valueSet);
                MessageRecevied.Text = "Received response: " + response.Message["response"] as string;
            }
        }
    }
}
