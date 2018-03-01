using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Data.Json;

namespace DataAccess
{
    public class OrderDetail : INotifyPropertyChanged
    {
        public int OrderID { get; set; }
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        private int quantity;
        public int Quantity
        {
            get { return quantity; }
            set
            {
                if (quantity == value) return;
                quantity = value;
                NotifyPropertyChanged("Quantity");
            }
        }
        public decimal UnitPrice { get; set; }
        public string UnitPriceString { get { return UnitPrice.ToString("######.00"); } }
        public decimal TaxAmount { get; set; }
        public decimal NetAmount { get { return netAmount; } }
        private decimal netAmount;
        public string NetAmountString {  get { return NetAmount.ToString("######.00"); } }
        public decimal Discount { get; set; }

        public OrderDetail()
        {
            Quantity = 1;
            TaxAmount = 0;
            Discount = 0;
        }
        public void UpdateTotal()
        {
            var preTax = Math.Round(Quantity * UnitPrice, 2);
            netAmount = preTax + TaxAmount;
        }

        public JsonObject ToJsonObject()
        {
            JsonObject obj = new JsonObject();
            obj.SetNamedValue("ProductID", JsonValue.CreateNumberValue(ProductID));
            obj.SetNamedValue("Quantity", JsonValue.CreateNumberValue(Quantity));
            obj.SetNamedValue("UnitPrice", JsonValue.CreateNumberValue((double)UnitPrice));
            obj.SetNamedValue("Discount", JsonValue.CreateNumberValue((double)Discount));
            return obj;
        }
        public string ToJsonString()
        {
            return this.ToJsonObject().Stringify();
        }

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
