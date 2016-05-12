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
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.ApplicationModel.AppService;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace UWP
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        /// <summary>
        /// Loads XAML components 
        /// </summary>
        public MainPage()
        {
            this.InitializeComponent();
        }

        /// <summary>MessageToSend
        /// Launches the Win32 background process via the new "fullTrustProcess" extenstion
        /// </summary>
        private async void LaunchBackgroundProcess_Click(object sender, RoutedEventArgs e)
        {
            await Windows.ApplicationModel.FullTrustProcessLauncher.LaunchFullTrustProcessForCurrentAppAsync();
        }

        /// <summary>
        /// Sends message to the full trust process and receives a response back
        /// </summary>
        private async void SendRequest_Click(object sender, RoutedEventArgs e)
        {
            ValueSet valueSet = new ValueSet();
            valueSet.Add("request", MessageToSend.Text);

            AppServiceResponse response = await App.Connection.SendMessageAsync(valueSet);
            MessageRecevied.Text = "Received response: " + response.Message["response"] as string;
        }
    }
}
