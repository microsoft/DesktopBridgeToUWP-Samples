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
using System.Linq;
using System.Windows.Forms;
using System.Threading.Tasks;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Core;
using Windows.ApplicationModel.AppService;
using Windows.Foundation.Collections;

namespace SystrayComponent
{
    class SystrayApplicationContext : ApplicationContext
    {
        private AppServiceConnection connection = null;
        private NotifyIcon notifyIcon = null;
        private Form1 configWindow = new Form1();

        public SystrayApplicationContext()
        {
            MenuItem openMenuItem = new MenuItem("Open UWP", new EventHandler(OpenApp));
            MenuItem sendMenuItem = new MenuItem("Send message to UWP", new EventHandler(SendToUWP));
            MenuItem legacyMenuItem = new MenuItem("Open legacy companion", new EventHandler(OpenLegacy));
            MenuItem exitMenuItem = new MenuItem("Exit", new EventHandler(Exit));
            openMenuItem.DefaultItem = true;

            notifyIcon = new NotifyIcon();
            notifyIcon.DoubleClick += new EventHandler(OpenApp);
            notifyIcon.Icon = SystrayComponent.Properties.Resources.Icon1;
            notifyIcon.ContextMenu = new ContextMenu(new MenuItem[]{ openMenuItem, sendMenuItem, legacyMenuItem, exitMenuItem });
            notifyIcon.Visible = true;
        }

        private async void OpenApp(object sender, EventArgs e)
        {
            IEnumerable<AppListEntry> appListEntries = await Package.Current.GetAppListEntriesAsync();
            await appListEntries.First().LaunchAsync();
        }

        private async void SendToUWP(object sender, EventArgs e)
        {
            ValueSet message = new ValueSet();
            message.Add("content", "Message from Systray Extension");
            await SendToUWP(message);
        }

        private void OpenLegacy(object sender, EventArgs e)
        {
            Form1 form = new Form1();
            form.Show();
        }

        private async void Exit(object sender, EventArgs e)
        {
            ValueSet message = new ValueSet();
            message.Add("exit", "");
            await SendToUWP(message);
            Application.Exit();
        }

        private async Task SendToUWP(ValueSet message)
        { 
            if (connection == null)
            {
                connection = new AppServiceConnection();
                connection.PackageFamilyName = Package.Current.Id.FamilyName;
                connection.AppServiceName = "SystrayExtensionService";
                connection.ServiceClosed += Connection_ServiceClosed;
                AppServiceConnectionStatus connectionStatus = await connection.OpenAsync();
                if (connectionStatus != AppServiceConnectionStatus.Success)
                {
                    MessageBox.Show("Status: " + connectionStatus.ToString());
                    return;
                }
            }

            await connection.SendMessageAsync(message);
        }

        private void Connection_ServiceClosed(AppServiceConnection sender, AppServiceClosedEventArgs args)
        {
            connection.ServiceClosed -= Connection_ServiceClosed;
            connection = null;
        }
    }
}
