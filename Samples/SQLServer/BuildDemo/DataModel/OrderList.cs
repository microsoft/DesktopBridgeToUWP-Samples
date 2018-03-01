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
    public class OrderList : ObservableCollection<OrderHeader>
    {
        public OrderList()
        {
        }

        public bool LoadFromJsonString(string json)
        {
            JsonArray result = JsonArray.Parse(json);
            OrderList orders = new OrderList();
            foreach (var jsonOrder in result)
            {
                JsonObject jsonOrderObject = jsonOrder.GetObject();
                var order = new OrderHeader();
                order.OrderID = Convert.ToInt32(jsonOrderObject.GetNamedNumber("OrderID"));
                order.EmployeeID = Convert.ToInt32(jsonOrderObject.GetNamedNumber("EmployeeID"));
                order.CustomerID = jsonOrderObject.GetNamedString("CustomerID");
                order.OrderTotal = jsonOrderObject.GetNamedString("OrderTotal");
                order.OrderDate = jsonOrderObject.GetNamedString("OrderDateString");
                orders.Add(order);
            }
            foreach (OrderHeader o in orders)
            {
                this.Items.Add(o);
            }
            return (Items.Count > 0);
        }
    }
}
