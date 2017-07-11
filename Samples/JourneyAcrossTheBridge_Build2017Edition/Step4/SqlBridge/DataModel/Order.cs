using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Data.Json;

namespace DataAccess
{
    public class Order : INotifyPropertyChanged
    {
        public string CustomerID { get; set; }
        public int EmployeeID { get; set; }
        public DateTime OrderDate { get; set; }
        
        public OrderDetailsList OrderDetails { get; set; }

        public Order()
        {
            OrderDetails = new OrderDetailsList();

            // Set up default customer and employee IDs for demo
            CustomerID = "ALFKI";
            EmployeeID = 1;
            OrderDate = DateTime.Today;
        }

        public string ToJson()
        {
            JsonObject obj = new JsonObject();
            obj.SetNamedValue("CustomerID", JsonValue.CreateStringValue(CustomerID));
            obj.SetNamedValue("EmployeeID", JsonValue.CreateNumberValue(EmployeeID));
            obj.SetNamedValue("OrderDateString", JsonValue.CreateStringValue(OrderDate.ToString()));

            JsonArray array = OrderDetails.ToJsonArray();
            obj.Add("OrderDetails", array);

            var jsonString = obj.Stringify();
            return jsonString;
        }

        public void LoadFromJson(string orderJson)
        {
            JsonObject orderObj = JsonObject.Parse(orderJson);
            CustomerID = orderObj.GetNamedString("CustomerID");
            EmployeeID = (int)orderObj.GetNamedNumber("EmployeeID");
            OrderDate = DateTime.Parse(orderObj.GetNamedString("OrderDateString"));

            JsonArray detailArray = orderObj.GetNamedArray("OrderDetails");
            foreach(IJsonValue jsonValue in detailArray)
            {
                if (jsonValue.ValueType == JsonValueType.Object)
                {
                    JsonObject detailObj = jsonValue.GetObject();
                    var detail = new OrderDetail();
                    detail.ProductID = (int)detailObj.GetNamedNumber("ProductID");
                    detail.Quantity = (int)detailObj.GetNamedNumber("Quantity");
                    detail.UnitPrice = (decimal)detailObj.GetNamedNumber("UnitPrice");
                    detail.Discount = (decimal)detailObj.GetNamedNumber("Discount");
                    OrderDetails.Add(detail);
                }
            }
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
