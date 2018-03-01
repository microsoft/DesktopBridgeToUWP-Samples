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
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel.AppService;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;


// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace SDKTemplate
{
    
    public sealed partial class Scenario4_scale : Page
    {
        private MainPage rootPage;

        public Scenario4_scale()
        {
            this.InitializeComponent();
            
        }

      

        private async void FindClaimEnable_Click(object sender, RoutedEventArgs e)
        {

            try
            {
                // Make sure the BackgroundProcess is in your AppX folder, if not rebuild the solution
                await Windows.ApplicationModel.FullTrustProcessLauncher.LaunchFullTrustProcessForCurrentAppAsync();
                rootPage.NotifyUser("Background Process running", NotifyType.StatusMessage);

                await System.Threading.Tasks.Task.Delay(2000);

                ValueSet valueSet = new ValueSet();
                valueSet.Add("request", "claimScale");

                if (App.Connection != null)
                {
                    //Send a msg asking to claim the scale (Open, Claim, Enable)
                    AppServiceResponse response = await App.Connection.SendMessageAsync(valueSet);
                    string aux = response.Message["response"] as string;
                    if (aux == "scaleReady")
                    {
                        rootPage.NotifyUser("Scale Ready", NotifyType.StatusMessage);
                    }
                    else
                    {
                        rootPage.NotifyUser("Scale could not be claimed or enabled", NotifyType.ErrorMessage);
                    }
                }

            }
            catch (Exception)
            {
                rootPage.NotifyUser("Rebuild the solution and make sure the BackgroundProcess is in your AppX folder", NotifyType.ErrorMessage);
            }

        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            try { 
            ValueSet valueSet = new ValueSet();
            valueSet.Add("request", "getWeight");

                if (App.Connection != null)
                {
                    //send a message asking for the wight in the scale
                    AppServiceResponse response = await App.Connection.SendMessageAsync(valueSet);
                    txtWeight.Text = response.Message["response"] as string;
                }
            }
            catch (Exception){ rootPage.NotifyUser("Error to communicate with scale", NotifyType.ErrorMessage); }
        }

        private async void EndScenario_Click(object sender, RoutedEventArgs e)
        {
            ValueSet valueSet = new ValueSet();
            valueSet.Add("request", "endProcess");

            try
            {

            
                if (App.Connection != null)
                {
                    AppServiceResponse response = await App.Connection.SendMessageAsync(valueSet);
                    string aux = response.Message["response"] as string;
                    if (aux == "processEnded")
                    {
                        rootPage.NotifyUser("Scenario Terminated", NotifyType.StatusMessage);
                    }
                    else
                    {
                        rootPage.NotifyUser("Communication error", NotifyType.ErrorMessage);
                    }
                }
                App.Connection.Dispose();
            }
            catch (Exception) { rootPage.NotifyUser("Communication error", NotifyType.ErrorMessage); }
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            rootPage = MainPage.Current;
        }
    }
}
