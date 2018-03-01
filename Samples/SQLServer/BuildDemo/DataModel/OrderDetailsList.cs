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

using System.Collections.ObjectModel;
using Windows.Data.Json;

namespace NorthwindDemo
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
