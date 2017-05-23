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
using Windows.Data.Json;

namespace NorthwindDemo
{
    public class ProductList : ObservableCollection<Product>
    {
        public ProductList()
        {
        }

        public bool LoadFromJsonString(string json)
        {
            JsonArray result = JsonArray.Parse(json);
            ProductList products = new ProductList();
            foreach (var jsonProd in result)
            {
                JsonObject jsonProdObject = jsonProd.GetObject();
                products.Add(new Product
                {
                    ProductID = Convert.ToInt32(jsonProdObject.GetNamedNumber("ProductID")),
                    ProductName = jsonProdObject.GetNamedString("ProductName"),
                    UnitPrice = (decimal)jsonProdObject.GetNamedNumber("UnitPrice"),
                    QuantityPerUnit = jsonProdObject.GetNamedString("QuantityPerUnit"),
                    UnitsInStock = Convert.ToInt16(jsonProdObject.GetNamedNumber("UnitsInStock"))
                } );
            }
            foreach (Product p in products)
            {
                this.Items.Add(p);
            }
            return (Items.Count > 0);
        }

        public Product GetProductById(int id)
        {
            for(int i = 0; i < Items.Count; i++)
            {
                if (Items[i].ProductID == id)
                {
                    return Items[i];
                }
            }
            return null;
        }
    }
}
