using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Data.Json;

namespace DataAccess
{
    public class OrderDetailsList : ObservableCollection<OrderDetail>
    {
        public decimal GetNetTotal()
        {
            decimal total = 0;
            foreach(OrderDetail detail in Items)
            {
                total += detail.NetAmount;
            }
            return total;
        }
        public JsonArray ToJsonArray()
        {
            JsonArray array = new JsonArray();
            foreach (OrderDetail detail in Items)
            {
                var obj = detail.ToJsonObject();
                array.Add(obj);
            }
            return array;
        }
        public string ToJson()
        {
            return ToJsonArray().Stringify();
        }

    }
}
