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
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel;
using Windows.ApplicationModel.AppService;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Foundation.Metadata;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

namespace DataGrid2Excel
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private ObservableCollection<Item> items = null;
        private ValueSet table = null;
        private string lorem = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum";

        public MainPage()
        {
            this.InitializeComponent();
            App.AppServiceConnected += MainPage_AppServiceConnected;

            // creating random content for the data grid
            Random rnd = new Random(Environment.TickCount);
            string[] words = lorem.Split(' ');
            items = new ObservableCollection<Item>();
            for (int i = 0; i < 10; i++)
            {
                Item item = new Item();
                item.Id = Guid.NewGuid().ToString().Substring(0, 8);
                item.Description = words[rnd.Next(words.Length)] + " " + words[rnd.Next(words.Length)];
                item.Quantity = rnd.Next(100);
                item.UnitPrice = rnd.Next(100) + Math.Round(rnd.NextDouble(), 2);
                items.Add(item);
            }
            this.DataContext = items;
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            // create a ValueSet from the datacontext
            table = new ValueSet();
            table.Add("REQUEST", "CreateSpreadsheet");
            for (int i=0; i<items.Count; i++)
            {
                table.Add("Description" + i.ToString(), items[i].Description);
                table.Add("Id" + i.ToString(), items[i].Id);
                table.Add("Quantity" + i.ToString(), items[i].Quantity);
                table.Add("UnitPrice" + i.ToString(), items[i].UnitPrice);
            }

            // launch the fulltrust process and for it to connect to the app service            
            if (ApiInformation.IsApiContractPresent("Windows.ApplicationModel.FullTrustAppContract", 1, 0))
            {
                await FullTrustProcessLauncher.LaunchFullTrustProcessForCurrentAppAsync();
            }
            else
            {
                MessageDialog dialog = new MessageDialog("This feature is only available on Windows 10 Desktop SKU");
                await dialog.ShowAsync();
            }
        }

        private async void MainPage_AppServiceConnected(object sender, EventArgs e)
        {
            // send the ValueSet to the fulltrust process
            AppServiceResponse response = await App.Connection.SendMessageAsync(table);

            // check the result
            object result;
            response.Message.TryGetValue("RESPONSE", out result);
            if (result.ToString() != "SUCCESS")
            {
                MessageDialog dialog = new MessageDialog(result.ToString());
                await dialog.ShowAsync();
            }
            // no longer need the AppService connection
            App.AppServiceDeferral.Complete();
        }
    }

    /// <summary>
    /// simple class to represent each line item in the data grid
    /// </summary>
    public class Item : INotifyPropertyChanged
    {
        private string id;
        private int quantity;
        private string description;
        private double unitPrice;

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

        public string Id
        {
            get { return id; }
            set
            {
                if (id != value)
                {
                    id = value;
                    OnPropertyChanged("Id");
                }
            }
        }

        public string Description
        {
            get { return description; }
            set
            {
                if (description != value)
                {
                    description = value;
                    OnPropertyChanged("Description");
                }
            }
        }

        public int Quantity
        {
            get { return quantity; }
            set
            {
                if (quantity != value)
                {
                    quantity = value;
                    OnPropertyChanged("Quantity");
                }
            }
        }

        public double UnitPrice
        {
            get { return unitPrice; }
            set
            {
                if (unitPrice != value)
                {
                    unitPrice = value;
                    OnPropertyChanged("UnitPrice");
                }
            }
        }
    }
}
