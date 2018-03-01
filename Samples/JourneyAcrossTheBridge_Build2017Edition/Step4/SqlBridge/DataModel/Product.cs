using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class Product : INotifyPropertyChanged
    {
        public int ProductID { get; set; }
        public string ProductCode {  get { return ProductID.ToString(); } }
        public string ProductName { get; set; }
        public string QuantityPerUnit { get; set; }
        public decimal UnitPrice { get; set; }
        public string UnitPriceString { get { return UnitPrice.ToString("######.00"); } }
        public int UnitsInStock { get; set; }
        public string UnitsInStockString {  get { return UnitsInStock.ToString("#####0"); } }

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

    }
}
