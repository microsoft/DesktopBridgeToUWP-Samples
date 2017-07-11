using System;
using System.Collections.ObjectModel;
using Windows.Data.Json;

namespace DataAccess
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
                    //CategoryName = jsonProdObject.GetNamedString("CategoryName"),
                    UnitPrice = (decimal)jsonProdObject.GetNamedNumber("UnitPrice"),
                    QuantityPerUnit = jsonProdObject.GetNamedString("QuantityPerUnit"),
                    UnitsInStock = Convert.ToInt16(jsonProdObject.GetNamedNumber("UnitsInStock"))
                }
                            );
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
