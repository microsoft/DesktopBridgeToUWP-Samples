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

// // Copyright (c) Microsoft. All rights reserved.
// // Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Linq;
using System.IO;
using System.Collections;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Reflection;
using System.Diagnostics;

namespace PhotoStoreDemo
{
    /// <summary>
    ///     Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly Stack _undoStack;
        private RubberbandAdorner _cropSelector;
        public PhotoList Photos;
        public PrintList ShoppingCart;

        public MainWindow()
        {
            _undoStack = new Stack();
            InitializeComponent();
            buttonAddPhoto.ToolTip = PhotosFolder.Current;
        }



        private void WindowLoaded(object sender, EventArgs e)
        {

            //Please refer to the following blogpost to learn how you can use the method "IsRunningAsUWP" to detect whether or not you are running as a packged app or not:
            //https://blogs.msdn.microsoft.com/appconsult/2016/11/03/desktop-bridge-identify-the-applications-context/
            if (ExecutionMode.IsRunningAsUwp())
            {
                this.Title = "Desktop Bridge App: PhotoStore --- (Windows App Package)";
                this.TitleSpan.Foreground = Brushes.Blue;

                //Detect if the previous version of the Desktop App is installed
                //The location of the uninstaller in the registry depends on the app's architecture and installer technology. This is different for every app.
                String uninstallString = (String)Microsoft.Win32.Registry.GetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\WOW6432Node\Microsoft\Windows\CurrentVersion\Uninstall\{7AD02FB8-B85E-44BC-8998-F4803BA5A0E3}\", "UninstallString", null);

                //Detect if there is previous user data
                //In this example, the previous user data lives under the "unpackaged" local app data location
                String sourceDir = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\PreviousPhotoStore";

                //Prepare the message for the dialog box to be shown to the user, depending on whether any of the following actions are requried: data migration, uninstallation, or both
                String userMessage = "";
                if (Directory.Exists(sourceDir) && uninstallString != null)
                {
                    userMessage = "We need to migrate your old data and uninstall the previous version of this app. Would you like to go ahead right now?";
                }
                else if (Directory.Exists(sourceDir) && uninstallString == null)
                {
                    userMessage = "We need to migrate your old data. Would you like to go ahead right now?";
                }
                else if (!Directory.Exists(sourceDir) && uninstallString != null)
                {
                    userMessage = "We need to uninstall the previous version of this app. Would you like to go ahead right now?";
                }

                if (Directory.Exists(sourceDir) || uninstallString != null)
                {
                    MessageBoxResult messageResult = MessageBox.Show(userMessage, "Migration in progress...", MessageBoxButton.YesNo);

                    if (messageResult.Equals(MessageBoxResult.Yes))
                    {
                        //Migrate user data (if required)
                        if (Directory.Exists(sourceDir))
                        {
                            //Obtain the path (String) to the "packaged" location, where the previous data will be migrated TO.
                            String destinationDir = Windows.Storage.ApplicationData.Current.LocalFolder.Path + "\\NewPhotoStore";

                            //If you are moving data from one of the redirected folders, you need to use robocopy.exe to bypass redirection. This is the only time you should bypass redirection
                            //Redirected folders: https://docs.microsoft.com/en-us/windows/uwp/porting/desktop-to-uwp-behind-the-scenes
                            //Robocopy.exe: https://technet.microsoft.com/en-us/library/cc733145(v=ws.11
                            //Otherwise, you should move files using System.IO
                            //Note: The "/move" flag is used to copy data over AND delete previous data in one operation
                            if (RunProcess("robocopy.exe", sourceDir + " " + destinationDir + " /move") > 1)
                            {
                                //Migration was unsuccessful -- App Developer can choose to block/retry/other action
                            }
                        }

                        //Uninstall previous version
                        if (uninstallString != null)
                        {
                            //In this case, the uninstaller takes additional arguments.
                            //Since the uninstallString is app-specific, make sure your call to the uninstaller is properly formatted before calling create process
                            string[] uninstallArgs = uninstallString.Split(' ');
                            if (RunProcess(uninstallArgs[0], uninstallArgs[1]) != 0)
                            {
                                //Uninstallation was unsuccessful - App Developer can choose to block the app here
                            }
                        }
                    }
                }
            }
            else
            {
                this.Title = "Desktop App";
                this.TitleSpan.Foreground = Brushes.Navy;
            }

            var layer = AdornerLayer.GetAdornerLayer(CurrentPhoto);
            _cropSelector = new RubberbandAdorner(CurrentPhoto) { Window = this };
            layer.Add(_cropSelector);
#if VISUALCHILD
            CropSelector.Rubberband.Visibility = Visibility.Hidden;
#endif
#if NoVISUALCHILD
            CropSelector.ShowRect = false;
#endif

            Photos = (PhotoList)(this.Resources["Photos"] as ObjectDataProvider)?.Data;
            Photos.Init(PhotosFolder.Current);
            ShoppingCart = (PrintList)(this.Resources["ShoppingCart"] as ObjectDataProvider)?.Data;

        }

        private void PhotoListSelection(object sender, RoutedEventArgs e)
        {
            var path = ((sender as ListBox)?.SelectedItem.ToString());
            BitmapSource img = BitmapFrame.Create(new Uri(path));
            CurrentPhoto.Source = img;
            ClearUndoStack();
            if (_cropSelector != null)
            {
#if VISUALCHILD
                if (Visibility.Visible == CropSelector.Rubberband.Visibility)
                    CropSelector.Rubberband.Visibility = Visibility.Hidden;
#endif
#if NoVISUALCHILD
                if (CropSelector.ShowRect)
                    CropSelector.ShowRect=false;
#endif
            }
            CropButton.IsEnabled = false;
        }

        private void AddToShoppingCart(object sender, RoutedEventArgs e)
        {
            if (PrintTypeComboBox.SelectedItem != null)
            {
                PrintBase item;
                switch (PrintTypeComboBox.SelectedIndex)
                {
                    case 0:
                        item = new Print(CurrentPhoto.Source as BitmapSource);
                        break;
                    case 1:
                        item = new GreetingCard(CurrentPhoto.Source as BitmapSource);
                        break;
                    case 2:
                        item = new Shirt(CurrentPhoto.Source as BitmapSource);
                        break;
                    default:
                        return;
                }
                ShoppingCart.Add(item);
                ShoppingCartListBox.ScrollIntoView(item);
                ShoppingCartListBox.SelectedItem = item;
                if (false == UploadButton.IsEnabled)
                    UploadButton.IsEnabled = true;
                if (false == RemoveButton.IsEnabled)
                    RemoveButton.IsEnabled = true;
            }
        }

        private void RemoveShoppingCartItem(object sender, RoutedEventArgs e)
        {
            if (null != ShoppingCartListBox.SelectedItem)
            {
                var item = ShoppingCartListBox.SelectedItem as PrintBase;
                ShoppingCart.Remove(item);
                ShoppingCartListBox.SelectedIndex = ShoppingCart.Count - 1;
            }
            if (0 == ShoppingCart.Count)
            {
                RemoveButton.IsEnabled = false;
                UploadButton.IsEnabled = false;
            }
        }

        private void Upload(object sender, RoutedEventArgs e)
        {
            if (ShoppingCart.Count > 0)
            {
                var scaleDuration = new TimeSpan(0, 0, 0, 0, ShoppingCart.Count * 200);
                var progressAnimation = new DoubleAnimation(0, 100, scaleDuration, FillBehavior.Stop);
                UploadProgressBar.BeginAnimation(RangeBase.ValueProperty, progressAnimation);
                ShoppingCart.Clear();
                UploadButton.IsEnabled = false;
                if (RemoveButton.IsEnabled)
                    RemoveButton.IsEnabled = false;
            }
        }

        private void Rotate(object sender, RoutedEventArgs e)
        {
            if (CurrentPhoto.Source != null)
            {
                var img = (BitmapSource)(CurrentPhoto.Source);
                _undoStack.Push(img);
                var cache = new CachedBitmap(img, BitmapCreateOptions.None, BitmapCacheOption.OnLoad);
                CurrentPhoto.Source = new TransformedBitmap(cache, new RotateTransform(90.0));
                if (false == UndoButton.IsEnabled)
                    UndoButton.IsEnabled = true;
                if (_cropSelector != null)
                {
#if VISUALCHILD
                    if (Visibility.Visible == CropSelector.Rubberband.Visibility)
                        CropSelector.Rubberband.Visibility = Visibility.Hidden;
#endif
#if NoVISUALCHILD
                if (CropSelector.ShowRect)
                    CropSelector.ShowRect=false;
#endif
                }
                CropButton.IsEnabled = false;
            }
        }

        private void BlackAndWhite(object sender, RoutedEventArgs e)
        {
            if (CurrentPhoto.Source != null)
            {
                var img = (BitmapSource)(CurrentPhoto.Source);
                _undoStack.Push(img);
                CurrentPhoto.Source = new FormatConvertedBitmap(img, PixelFormats.Gray8, BitmapPalettes.Gray256, 1.0);
                if (false == UndoButton.IsEnabled)
                    UndoButton.IsEnabled = true;
                if (_cropSelector != null)
                {
#if VISUALCHILD
                    if (Visibility.Visible == CropSelector.Rubberband.Visibility)
                        CropSelector.Rubberband.Visibility = Visibility.Hidden;
#endif
#if NoVISUALCHILD
                    if (CropSelector.ShowRect)
                        CropSelector.ShowRect = false;
#endif
                }
                CropButton.IsEnabled = false;
            }
        }

        private void OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            var anchor = e.GetPosition(CurrentPhoto);
            _cropSelector.CaptureMouse();
            _cropSelector.StartSelection(anchor);
            CropButton.IsEnabled = true;
        }

        private void Crop(object sender, RoutedEventArgs e)
        {
            if (CurrentPhoto.Source != null)
            {
                var img = (BitmapSource)(CurrentPhoto.Source);
                _undoStack.Push(img);
                var rect = new Int32Rect
                {
                    X = (int)(_cropSelector.SelectRect.X * img.PixelWidth / CurrentPhoto.ActualWidth),
                    Y = (int)(_cropSelector.SelectRect.Y * img.PixelHeight / CurrentPhoto.ActualHeight),
                    Width = (int)(_cropSelector.SelectRect.Width * img.PixelWidth / CurrentPhoto.ActualWidth),
                    Height = (int)(_cropSelector.SelectRect.Height * img.PixelHeight / CurrentPhoto.ActualHeight)
                };
                CurrentPhoto.Source = new CroppedBitmap(img, rect);
#if VISUALCHILD
                if (Visibility.Visible == CropSelector.Rubberband.Visibility)
                    CropSelector.Rubberband.Visibility = Visibility.Hidden;
#endif
#if NoVISUALCHILD
                if (CropSelector.ShowRect)
                    CropSelector.ShowRect = false;
#endif
                CropButton.IsEnabled = false;
                if (false == UndoButton.IsEnabled)
                    UndoButton.IsEnabled = true;
            }
        }

        private void Undo(object sender, RoutedEventArgs e)
        {
            if (_undoStack.Count > 0)
                CurrentPhoto.Source = (BitmapSource)_undoStack.Pop();
            if (0 == _undoStack.Count)
                UndoButton.IsEnabled = false;
#if VISUALCHILD
                if (Visibility.Visible == CropSelector.Rubberband.Visibility)
                    CropSelector.Rubberband.Visibility = Visibility.Hidden;
#endif
#if NoVISUALCHILD
            if (CropSelector.ShowRect)
                CropSelector.ShowRect = false;
#endif
        }

        private void ClearUndoStack()
        {
            _undoStack.Clear();
            UndoButton.IsEnabled = false;
        }

        private void AddPhoto(object sender, RoutedEventArgs e)
        {
            var ofd = new Microsoft.Win32.OpenFileDialog() { Filter = "JPG Files (*.jpg)|*.jpg|PNG Files (*.png)|*.png|GIF Files (*.gif)|*.gif" };
            var result = ofd.ShowDialog();
            if (result == false) return;
            ImageFile item = new ImageFile(ofd.FileName);
            item.AddToCache();
        }

        private int RunProcess(string appName, string arguments)
        {
            System.Diagnostics.Process process = new Process();
            process.StartInfo.FileName = appName;
            process.StartInfo.Arguments = arguments;
            process.StartInfo.CreateNoWindow = true;
            process.Start();
            process.WaitForExit();
            return process.ExitCode;
        }
    }
}