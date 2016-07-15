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
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.ApplicationModel.AppService;

using NorthwindCent.DataModel;

namespace NorthwindCent.DesktopServer
{
    class Program
    {
        static AppServiceConnection connection = null;
        static AutoResetEvent appServiceExit;
        static string dbFileFolder;

		/// <summary>
        /// Copy database file from app package to App data folder, if it is not already there.
        /// So that the app can open the database for query and modification.
        /// </summary>
        static async void InitDatabaseFileAsync()
        {
            var appDataFolder = ApplicationData.Current.LocalFolder;
            var dbFile = await appDataFolder.TryGetItemAsync("Northwind.sdf");
            if (dbFile == null)
            {
                var dbURI = new Uri("ms-appx:///Northwind.sdf");
                var dbInPackage = await StorageFile.GetFileFromApplicationUriAsync(dbURI);

                await dbInPackage.CopyAsync(appDataFolder);
            }
        }

        static void Main(string[] args)
        {
            InitDatabaseFileAsync();

            dbFileFolder = ApplicationData.Current.LocalFolder.Path + "\\Northwind.sdf";

            appServiceExit = new AutoResetEvent(false);

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("***********************************");
            Console.WriteLine("**** Classic desktop server    ****");
            Console.WriteLine("**** Query SQL CE database     ****");
            Console.WriteLine("**** and return data to client ****");
            Console.WriteLine("***********************************");
            Console.WriteLine();

            InitializeAppServiceConnection();

			// Wait for the signal that App service is disconnected
            appServiceExit.WaitOne();
        }

		/// <summary>
        /// Initialize connection with UWA component via AppService.
        /// </summary>
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
        /// Called when desktop server received a request from UWA
		/// This method just distribute the request to methods that handle the respective request
        /// </summary>
        private static async void Connection_RequestReceived(AppServiceConnection sender, AppServiceRequestReceivedEventArgs args)
        {
            string key = args.Request.Message.First().Key;
            string request = args.Request.Message.First().Value.ToString();

            AppServiceResponseStatus status;
            switch(request)
            {
                case "GetCategories":
                    status = await SendCategoriesAsync(args.Request);
                    break;
                case "GetProducts":
                    int categoryId = Convert.ToInt32(args.Request.Message["CategoryId"]);
                    status = await SendProductsByCategoryAsync(args.Request, categoryId);
                    break;
                default:
                    status = await SendUnknownRequestErrorAsync(args.Request, request);
                    break;
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
		
		/// <summary>
        /// Handles "GetCategories" request and returns data of product categories to UWA
        /// </summary>
        private static IAsyncOperation<AppServiceResponseStatus> SendCategoriesAsync(AppServiceRequest request)
        {
            ConsoleColor oldColor = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Received GetCategories request.");

            ValueSet returnValues = new ValueSet();
            try
            {
                var db = new Database(dbFileFolder);

                // Serialize all DB query results into JSON
                // Because AppService only support passing data of primitive types
                int count = 0;
                JsonArray categories = new JsonArray();
                foreach (var cat in db.GetCategories())
                {
                    Console.WriteLine("Adding return value. {0} : {1}", count.ToString(), cat.Name);
                    JsonObject jsonCat = new JsonObject();
                    jsonCat.Add("ID", JsonValue.CreateNumberValue(cat.ID));
                    jsonCat.Add("Name", JsonValue.CreateStringValue(cat.Name));
                    jsonCat.Add("Description", JsonValue.CreateStringValue(cat.Description));
                    categories.Add(jsonCat);
                    count++;
                }

                returnValues.Add("Result", categories.Stringify());

                Console.WriteLine("Adding return value. {0} : {1}", "ResultCount", count);
                returnValues.Add("ResultCount", count);

                Console.WriteLine("Adding return value. {0} : {1}", "ErrorMessage", "Succeeded");
                returnValues.Add("ErrorMessage", "Succeeded");

                Console.WriteLine("Sending result with \"{0}\" categories", count);
                Console.WriteLine();
                Console.ForegroundColor = oldColor;
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
                returnValues.Add("ErrorMessage", e.Message);
            }

            return request.SendResponseAsync(returnValues);
        }

		/// <summary>
        /// Handles "GetProducts" request and returns data of products of give category to UWA
        /// </summary>
        private static IAsyncOperation<AppServiceResponseStatus> SendProductsByCategoryAsync(AppServiceRequest request, int categoryId)
        {
            ConsoleColor oldColor = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Received GetProducts request for category id: \"{0}\".", categoryId);

            ValueSet returnValues = new ValueSet();
            try
            {
                var db = new Database(dbFileFolder);

                // Serialize all DB query results into JSON
                // Because AppService only support passing data of primitive types
                int count = 0;
                JsonArray products = new JsonArray();
                foreach (var prod in db.GetProductsByCategory(categoryId))
                {
                    Console.WriteLine("Adding return value. {0} : {1}", count.ToString(), prod.Name);
                    JsonObject jsonProd = new JsonObject();
                    jsonProd.Add("ID", JsonValue.CreateNumberValue(prod.ID));
                    jsonProd.Add("Name", JsonValue.CreateStringValue(prod.Name));
                    jsonProd.Add("QuantityPerUnit", JsonValue.CreateStringValue(prod.QuantityPerUnit));
                    jsonProd.Add("UnitPrice", JsonValue.CreateNumberValue(prod.UnitPrice));
                    jsonProd.Add("UnitsInStock", JsonValue.CreateNumberValue(prod.UnitsInStock));
                    jsonProd.Add("UnitsOnOrder", JsonValue.CreateNumberValue(prod.UnitsOnOrder));
                    jsonProd.Add("ReorderLevel", JsonValue.CreateNumberValue(prod.ReorderLevel));
                    jsonProd.Add("Discontinued", JsonValue.CreateBooleanValue(prod.Discontinued));
                    jsonProd.Add("SupplierID", JsonValue.CreateNumberValue(prod.SupplierID));
                    jsonProd.Add("CategoryID", JsonValue.CreateNumberValue(prod.CategoryID));

                    products.Add(jsonProd);
                    count++;
                }

                returnValues.Add("Result", products.Stringify());
                Console.WriteLine("Adding return value. {0} : {1}", "ResultCount", count);
                returnValues.Add("ResultCount", count);

                Console.WriteLine("Adding return value. {0} : {1}", "ErrorMessage", "Succeeded");
                returnValues.Add("ErrorMessage", "Succeeded");

                Console.WriteLine("Sending result with \"{0}\" products", count);
                Console.WriteLine();
                Console.ForegroundColor = oldColor;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                returnValues.Add("ErrorMessage", e.Message);
            }

            return request.SendResponseAsync(returnValues);
        }
    }
}
