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
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

using Windows.ApplicationModel.AppService;
using Windows.Data.Json;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;


namespace NorthwindCent.UwaClient
{

    public sealed class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public BitmapImage Image { get; set; }
    }

    public sealed class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string QuantityPerUnit { get; set; }
        public float UnitPrice { get; set; }
        public short UnitsInStock { get; set; }
        public short UnitsOnOrder { get; set; }
        public short ReorderLevel { get; set; }
        public bool Discontinued { get; set; }
        public int SupplierID { get; set; }
        public int CategoryID { get; set; }

        public SolidColorBrush Color { get { return UnitsInStock > 0 ? new SolidColorBrush(Colors.Green) : new SolidColorBrush(Windows.UI.Color.FromArgb(0xff, 0xff, 0x8c, 0x00)); } }
        public bool Available { get { return UnitsInStock > 0; } }
    }

    public sealed class ViewModel : INotifyPropertyChanged
    {
        // Make ViewModel a singleton object
        // The ViewModel instance needs to be accessed via ViewModel.Instance
        private static ViewModel instance = null;
        public static ViewModel Instance
        {
            get
            {
                // Lazy create the singleton instance
                if(instance == null)
                {
                    // Launch desktop database server process
                    var r = Windows.ApplicationModel.FullTrustProcessLauncher.LaunchFullTrustProcessForCurrentAppAsync();
                    r.AsTask().Wait();
                    instance = new ViewModel();
                }
                return instance;
            }
        }

        // Data collection to bind to Categories page
        public readonly ObservableCollection<Category> categories = new ObservableCollection<Category>();
        public ObservableCollection<Category> Categories { get { return categories; } }

        // Data collection to bind to Products list in Products page
        private readonly ObservableCollection<Product> products = new ObservableCollection<Product>();        
        public ObservableCollection<Product> Products { get { return products; } }

        // Data collection to bing to Order list in Products page
        private readonly ObservableCollection<Product> order = new ObservableCollection<Product>();
        public ObservableCollection<Product> Order { get { return order; } }

        // Bind to Total amount Label in Products page
        private float _total;
        public float Total
        {
            get { return _total; }
            set
            {
                _total = value;
                NotifyPropertyChanged();
            }
        }

        // Make default creator private to make ViewModel class a singleton
        private ViewModel()
        {            
        }

        public async Task LoadCategoriesAsync()
        {
            Categories.Clear();

            // images are loaded from local assets
            var images = new[] { "beverages", "condiments", "confections", "dairy-products", "grains-cereals", "meat-poultry", "produce", "seafood" };

            if ((App.Current as App).DatabaseServiceConnection != null)
            {
                ValueSet valueSet = new ValueSet();
                valueSet.Add("Request", "GetCategories");

                AppServiceResponse response = await (App.Current as App).DatabaseServiceConnection.SendMessageAsync(valueSet);

                string errorMessage = response.Message["ErrorMessage"] as string;
                if (errorMessage.Equals("Succeeded"))
                {
                    int count = Convert.ToInt32(response.Message["ResultCount"]);
                    JsonArray result = JsonArray.Parse((string)(response.Message["Result"]));

                    int i = 0;
                    foreach (var jsonCat in result)
                    {
                        var uri = new Uri(string.Format("ms-appx:///Assets/{0}.png", images[i++]));

                        JsonObject jsonCatObject = jsonCat.GetObject();
                        Categories.Add(new Category {
                                                        Id = (int)jsonCatObject.GetNamedNumber("ID"),
                                                        Name = jsonCatObject.GetNamedString("Name"),
                                                        Description = jsonCatObject.GetNamedString("Description"),
                                                        Image = new BitmapImage(uri)
                                                    }
                                      );
                    }
                }
            }

            NotifyPropertyChanged();
        }

        public async Task LoadProductsForCategoryAsync(Category category)
        {
            Products.Clear();

            if ((App.Current as App).DatabaseServiceConnection != null)
            {
                ValueSet valueSet = new ValueSet();
                valueSet.Add("Request", "GetProducts");
                valueSet.Add("CategoryId", category.Id);

                AppServiceResponse response = await (App.Current as App).DatabaseServiceConnection.SendMessageAsync(valueSet);

                string errorMessage = response.Message["ErrorMessage"] as string;
                if (errorMessage.Equals("Succeeded"))
                {
                    int count = Convert.ToInt32(response.Message["ResultCount"]);
                    JsonArray result = JsonArray.Parse((string)(response.Message["Result"]));

                    foreach (var jsonProd in result)
                    {
                        JsonObject jsonProdObject = jsonProd.GetObject();
                        
                        Products.Add(new Product {
                                                    Id = Convert.ToInt32(jsonProdObject.GetNamedNumber("ID")),
                                                    Name = jsonProdObject.GetNamedString("Name"),
                                                    UnitPrice = (float)jsonProdObject.GetNamedNumber("UnitPrice"),
                                                    QuantityPerUnit = jsonProdObject.GetNamedString("QuantityPerUnit"),
                                                    UnitsInStock = Convert.ToInt16(jsonProdObject.GetNamedNumber("UnitsInStock"))
                                                  }
                                    );
                    }
                }
            }

            NotifyPropertyChanged();
        }

        public void UpdateTotal()
        {
            Total = Order.Sum(p => p.UnitPrice);
        }

        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        internal void CheckOut()
        {
            Order.Clear();
            UpdateTotal();
        }
    }
}
