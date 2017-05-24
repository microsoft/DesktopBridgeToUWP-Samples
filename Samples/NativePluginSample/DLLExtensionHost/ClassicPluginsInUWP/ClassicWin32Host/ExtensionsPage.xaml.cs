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
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ClassicWin32Host
{
    /// <summary>
    /// Interaction logic for ExtensionsPage.xaml
    /// </summary>
    public partial class ExtensionsPage : Page
    {

        public ObservableCollection<Extension> Items = null;

        public ExtensionsPage()
        {
            this.InitializeComponent();

            Items = AppData.ExtensionManager.Extensions;
            //this.DataContext = Items;
            lvExtensions.ItemsSource = Items;

        }

        private async void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            CheckBox cb = sender as CheckBox;
            Extension ext = cb.DataContext as Extension;
            if (!ext.Enabled)
            {
                await ext.Enable();
            }
        }

        private void CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            CheckBox cb = sender as CheckBox;
            Extension ext = cb.DataContext as Extension;
            if (ext.Enabled)
            {
                ext.Disable();
            }
        }

        private void RemoveButton_Click(object sender, RoutedEventArgs e)
        {
            // remove the package
            Button btn = sender as Button;
            Extension ext = btn.DataContext as Extension;
            AppData.ExtensionManager.RemoveExtension(ext);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            // get our extensions
            AppData.ExtensionManager.FindAllExtensions();
        }

        private void lvExtensions_MouseDown(object sender, MouseButtonEventArgs e)
        {
            ListViewItem item = sender as ListViewItem;
            Extension ext = item.DataContext as Extension;
            ext.TestExtension();
        }
    }
}
