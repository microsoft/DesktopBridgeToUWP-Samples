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
using System.Linq;
using System.Threading;
using Windows.Data.Json;
using Windows.Foundation.Collections;
using Windows.ApplicationModel.AppService;
using NorthwindDemo;
using Windows.Foundation;

namespace SqlBridge
{
    class Program
    {
        static AppServiceConnection connection = null;
        static AutoResetEvent appServiceExit;
        static string SqlConnectionString = null;

        static void Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("***********************************");
            Console.WriteLine("**** Classic desktop server    ****");
            Console.WriteLine("**** Query SQL Server database ****");
            Console.WriteLine("**** and return data to client ****");
            Console.WriteLine("***********************************");

            appServiceExit = new AutoResetEvent(false);
            InitializeAppServiceConnection();

            // Wait for the signal that App service is disconnected
            appServiceExit.WaitOne();
        }

        static async void InitializeAppServiceConnection()
        {
            connection = new AppServiceConnection();
            connection.AppServiceName = "NorthwindDataService";
            connection.PackageFamilyName = Windows.ApplicationModel.Package.Current.Id.FamilyName;
            connection.RequestReceived += Connection_RequestReceived;
            connection.ServiceClosed += Connection_ServiceClosed;

            AppServiceConnectionStatus status = await connection.OpenAsync();
            switch (status)
            {
                case AppServiceConnectionStatus.Success:
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Connection established - waiting for requests");
                    Console.WriteLine();
                    break;
                case AppServiceConnectionStatus.AppNotInstalled:
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("The app AppServicesProvider is not installed.");
                    return;
                case AppServiceConnectionStatus.AppUnavailable:
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("The app AppServicesProvider is not available.");
                    return;
                case AppServiceConnectionStatus.AppServiceUnavailable:
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(string.Format("The app AppServicesProvider is installed but it does not provide the app service {0}.", connection.AppServiceName));
                    return;
                case AppServiceConnectionStatus.Unknown:
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(string.Format("An unkown error occurred while we were trying to open an AppServiceConnection."));
                    return;
            }
        }
        /// <summary>
        /// Event handler called when App Service is closed by the UWA
        /// </summary>
        private static void Connection_ServiceClosed(AppServiceConnection sender, AppServiceClosedEventArgs args)
        {
            // When App service is disconnected, send the signal for the desktop server to exit
            appServiceExit.Set();
        }

        /// <summary>
        /// Called when desktop server received a request from UWA NorthwindDemo app
        /// This method just distribute the request to methods that handle the respective request
        /// </summary>
        private static async void Connection_RequestReceived(AppServiceConnection sender, AppServiceRequestReceivedEventArgs args)
        {
            string sqlConnString = "";
            string key = args.Request.Message.First().Key;
            string value = args.Request.Message.First().Value.ToString();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine(string.Format("Received message '{0}' with value '{1}'", key, value));
            if (key == "request")
            {
                switch (value)
                {
                    case "GetProducts":
                        sqlConnString = args.Request.Message["ConnectionString"].ToString();
                        SendAllProducts(args.Request, sqlConnString);
                        break;
                    case "SaveOrder":
                        sqlConnString = args.Request.Message["ConnectionString"].ToString();
                        string json = (string)args.Request.Message["OrderJson"];
                        SaveOrder(args.Request, json, sqlConnString);
                        break;
                    case "GetOrders":
                        sqlConnString = args.Request.Message["ConnectionString"].ToString();
                        SendOrders(args.Request, sqlConnString);
                        break;
                    default:
                        await SendUnknownRequestErrorAsync(args.Request, value);
                        break;
                }
            }
        }
        /// <summary>
        /// Handles unknown request and return error message`
        /// </summary>
        private static IAsyncOperation<AppServiceResponseStatus> SendUnknownRequestErrorAsync(AppServiceRequest request, string requestString)
        {
            ConsoleColor oldColor = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Received unknown request: \"{0}\"", requestString);

            string message = "Unknown Request";
            ValueSet returnValues = new ValueSet();
            returnValues.Add("ErrorMessage", message);

            Console.WriteLine("Sending error message: \"{0}\"", message);
            Console.WriteLine();
            Console.ForegroundColor = oldColor;

            return request.SendResponseAsync(returnValues);
        }

        private static void SendAllProducts(AppServiceRequest request, string connectionString)
        {
            Console.WriteLine("Received GetProducts request. Working on it...");
            var json = GetProductsAsJson(connectionString);
            ValueSet returnValues = new ValueSet();
            Console.WriteLine("Adding return value. {0} : {1}", "response", json);
            returnValues.Add("response", json);
            Console.WriteLine("Returning response with products as json");
            request.SendResponseAsync(returnValues).Completed += delegate { };
        }

        private static void SendOrders(AppServiceRequest request, string connectionString)
        {
            Console.WriteLine("Received GetOrders request. Working on it...");
            var json = GetOrdersAsJson(connectionString);
            ValueSet returnValues = new ValueSet();
            Console.WriteLine("Adding return value. {0} : {1}", "response", json);
            returnValues.Add("response", json);
            Console.WriteLine("Returning response with orders as json");
            request.SendResponseAsync(returnValues).Completed += delegate { };
        }
        private static string GetProductsAsJson(string connectionString)
        {
            try
            {
                var products = DataHelper.GetProducts(connectionString);
                Console.WriteLine("Products count: {0}", products.Count);

                if (products.Count > 0)
                {
                    Console.WriteLine("Serializing products...");
                    JsonArray jsonArray = new JsonArray();

                    foreach (Product prod in products)
                    {
                        JsonObject jsonProd = new JsonObject();
                        jsonProd.Add("ProductID", JsonValue.CreateNumberValue(prod.ProductID));
                        jsonProd.Add("ProductName", JsonValue.CreateStringValue(prod.ProductName));
                        jsonProd.Add("QuantityPerUnit", JsonValue.CreateStringValue(prod.QuantityPerUnit));
                        jsonProd.Add("UnitPrice", JsonValue.CreateNumberValue((double)(prod.UnitPrice)));
                        jsonProd.Add("UnitsInStock", JsonValue.CreateNumberValue(prod.UnitsInStock));

                        jsonArray.Add(jsonProd);
                    }
                    var json = jsonArray.Stringify();
                    return json;
                }
            }
            catch (Exception e)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Exception " + e.Message);
            }
            return "";
        }
        private static void SaveOrder(AppServiceRequest request, string json, string connectionString)
        {
            Console.WriteLine("Received SaveOrder request. Working on it...");
            Order order = new Order();
            order.LoadFromJson(json);

            var orderID = SaveOrderAndDetails(order, connectionString);
            ValueSet returnValues = new ValueSet();
            returnValues.Add("response", string.IsNullOrEmpty(orderID) ? "ERROR" : orderID);
            request.SendResponseAsync(returnValues).Completed += delegate { };
        }
        private static string SaveOrderAndDetails(Order order, string connectionString)
        {
            try
            {
                return DataHelper.SaveOrderAndDetails(order, connectionString);
            }
            catch (Exception e)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Exception " + e.Message);
            }
            return "";
        }
        private static string GetOrdersAsJson(string connectionString)
        {
            try
            {
                Console.WriteLine("Calling Get100Orders");
                var orders = new OrderList();
                orders = DataHelper.Get100Orders(connectionString);
                Console.WriteLine("Orders count: {0}", orders.Count);

                if (orders.Count > 0)
                {
                    Console.WriteLine("Serializing orders...");
                    JsonArray jsonArray = new JsonArray();

                    foreach (OrderHeader o in orders)
                    {
                        JsonObject jsonOrder = new JsonObject();
                        jsonOrder.Add("OrderID", JsonValue.CreateNumberValue(o.OrderID));
                        jsonOrder.Add("CustomerID", JsonValue.CreateStringValue(o.CustomerID));
                        jsonOrder.Add("EmployeeID", JsonValue.CreateNumberValue(o.EmployeeID));
                        jsonOrder.Add("OrderTotal", JsonValue.CreateStringValue(o.OrderTotal));
                        jsonOrder.Add("OrderDateString", JsonValue.CreateStringValue(o.OrderDate));
                        jsonArray.Add(jsonOrder);
                    }
                    var json = jsonArray.Stringify();
                    return json;
                }
            }
            catch (Exception e)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Exception " + e.Message);
            }
            return "";
        }

    }

}



