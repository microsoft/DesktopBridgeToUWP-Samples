using System;
using System.Collections.Generic;
using System.Globalization;
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
            decimal priceAsDecimal;

            // Remove the currency character
            price = price.Substring(1);

            if (Decimal.TryParse(price, NumberStyles.Currency, CultureInfo.InvariantCulture, out priceAsDecimal))
            {
                return (double)priceAsDecimal;
            }
            else
            {
                throw new FormatException($"Unable to parse value as currency: {price}");
            }
        }
    }
}
