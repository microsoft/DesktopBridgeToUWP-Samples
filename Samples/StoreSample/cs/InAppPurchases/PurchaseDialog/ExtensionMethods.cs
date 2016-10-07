using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Services.Store;

namespace InAppPurchases
{
    public static class ExtensionMethods
    {
        public static double AsDouble(this String price)
        {
            // Remove the currency character and parse as a double
            price = price.Substring(1);
            return Double.Parse(price);
        }
    }
}
