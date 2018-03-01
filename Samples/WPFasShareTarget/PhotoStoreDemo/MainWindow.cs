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

            if (ExecutionMode.IsRunningAsUwp())
            {
                this.Title = "Desktop Bridge App: PhotoStore --- (Windows App Package)";
                this.TitleSpan.Foreground = Brushes.Blue;
            }
            else
            {
                this.Title = "Desktop App";
                this.TitleSpan.Foreground = Brushes.Navy;
            }
            
            var layer = AdornerLayer.GetAdornerLayer(CurrentPhoto);
            _cropSelector = new RubberbandAdorner(CurrentPhoto) {Window = this};
            layer.Add(_cropSelector);
#if VISUALCHILD
            CropSelector.Rubberband.Visibility = Visibility.Hidden;
#endif
#if NoVISUALCHILD
            CropSelector.ShowRect = false;
#endif

            Photos = (PhotoList) (this.Resources["Photos"] as ObjectDataProvider)?.Data;
            Photos.Init(PhotosFolder.Current); 
            ShoppingCart = (PrintList) (this.Resources["ShoppingCart"] as ObjectDataProvider)?.Data;

            // listen for files being created via Share UX
            FileSystemWatcher watcher = new FileSystemWatcher(PhotosFolder.Current);
            watcher.EnableRaisingEvents = true;
            watcher.Created += Watcher_Created;
        }

        private void Watcher_Created(object sender, FileSystemEventArgs e)
        {
            // new file got created, adding it to the list
            Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Normal, new Action(() =>
            {
                if (File.Exists(e.FullPath))
                {
                    ImageFile item = new ImageFile(e.FullPath);
                    Photos.Insert(0, item);
                    PhotoListBox.SelectedIndex = 0;
                    CurrentPhoto.Source = (BitmapSource)item.Image;
                }
            }));
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
                var scaleDuration = new TimeSpan(0, 0, 0, 0, ShoppingCart.Count*200);
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
                var img = (BitmapSource) (CurrentPhoto.Source);
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
                var img = (BitmapSource) (CurrentPhoto.Source);
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
                var img = (BitmapSource) (CurrentPhoto.Source);
                _undoStack.Push(img);
                var rect = new Int32Rect
                {
                    X = (int) (_cropSelector.SelectRect.X*img.PixelWidth/CurrentPhoto.ActualWidth),
                    Y = (int) (_cropSelector.SelectRect.Y*img.PixelHeight/CurrentPhoto.ActualHeight),
                    Width = (int) (_cropSelector.SelectRect.Width*img.PixelWidth/CurrentPhoto.ActualWidth),
                    Height = (int) (_cropSelector.SelectRect.Height*img.PixelHeight/CurrentPhoto.ActualHeight)
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
                CurrentPhoto.Source = (BitmapSource) _undoStack.Pop();
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
    }
}