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
using Windows.UI.Xaml.Controls;

namespace SystrayExtension
{
    public enum CloseAction
    {
        Terminate,
        Systray,
        Consolidate
    }
    public sealed partial class ConfirmCloseDialog : ContentDialog
    {
        public CloseAction Result { get; private set; }
        public ConfirmCloseDialog()
        {
            this.InitializeComponent();
        }

        private void ContentDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            if (rbTerminate.IsChecked == true)
            {
                Result = CloseAction.Terminate;
            }
            else if (rbSystray.IsChecked == true)
            {
                Result = CloseAction.Systray;
            }
            else
            {
                Result = CloseAction.Consolidate;
            }
        }

        private void ContentDialog_SecondaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            // cancel the close request
        }
    }
}
