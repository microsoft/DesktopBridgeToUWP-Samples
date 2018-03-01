using System;
using System.Linq;
using System.Threading;
using System.IO;
using Windows.Data.Json;
using Windows.Foundation.Collections;
using Windows.ApplicationModel.AppService;
using System.Data;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace DataAccess
{
    class Program
    {
        static AppServiceConnection connection = null;
        static DataSet ds = null;
        static string filenName = @"datastore.xml";
        static string appDataFilePath = "";
        static AutoResetEvent connectionClosed = new AutoResetEvent(false);

        // Change this to the connection string for your SQL Server 
        const string ConnectionString = @"Data Source=sausing-desktop.redmond.corp.microsoft.com;Initial Catalog=Northwind;Integrated Security=True";
        static void Main(string[] args)
        {
            Thread appServiceThread = new Thread(new ThreadStart(ThreadProc));
            appServiceThread.Start();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("********************************");
            Console.WriteLine("**** Classic desktop server ****");
            Console.WriteLine("**** to query database and  ****");
            Console.WriteLine("**** return data to client  ****");
            Console.WriteLine("********************************");
            connectionClosed.WaitOne();
        }
        static async void ThreadProc()
        {
            connection = new AppServiceConnection();
            connection.AppServiceName = "DataService";
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

        private static void Connection_ServiceClosed(AppServiceConnection sender, AppServiceClosedEventArgs args)
        {
            connectionClosed.Set();
        }

        private static void Connection_RequestReceived(AppServiceConnection sender, AppServiceRequestReceivedEventArgs args)
        {
            string key = args.Request.Message.First().Key;
            string value = args.Request.Message.First().Value.ToString();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine(string.Format("Received message '{0}' with value '{1}'", key, value));
            if (key == "request")
            {
                switch (value)
                { 
                    case "GetProducts":
                       SendAllProductsAsync(args.Request); break;
                    case "SaveOrder":
                        string json = (string)args.Request.Message["OrderJson"];
                        SaveOrderAsync(args.Request, json); break;
                     
                }
            }
        }

        #region Don't look! Console hacks here!
        //[DllImport("user32.dll")]
        //static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);
        //private static void Minimize()
        //{
        //    var hwnd = Process.GetCurrentProcess().MainWindowHandle;
        //    ShowWindow(hwnd, 2);
        //}

        //[DllImport("user32.dll")]
        //static extern bool PostMessage(IntPtr hWnd, uint Msg, int wParam, int lParam);
        //private static void ExitConoleApp()
        //{
        //    Console.WriteLine("Exiting SQL Bridge");
        //    var hwnd = Process.GetCurrentProcess().MainWindowHandle;
        //    const uint WM_CHAR = 0x0102;
        //    PostMessage(hwnd, WM_CHAR, 13, 0);
        //}
        #endregion

        private static void SendAllProductsAsync(AppServiceRequest request)
        {
            Console.WriteLine("Received GetProducts request. Working on it...");
            var json = GetProductsAsJson();
            ValueSet returnValues = new ValueSet();
            //Console.WriteLine("Adding return value. {0} : {1}", "response", json);
            returnValues.Add("response", json);
            Console.WriteLine("Returning response with products as json");
            request.SendResponseAsync(returnValues).Completed += delegate { };
        }
        private static void SaveOrderAsync(AppServiceRequest request, string json)
        {
            Console.WriteLine("Received SaveOrder request. Working on it...");
            Order order = new Order();
            order.LoadFromJson(json);

            var orderID = SaveOrderAndDetails(order);
            ValueSet returnValues = new ValueSet();
            returnValues.Add("response", string.IsNullOrEmpty(orderID)? "ERROR": orderID);
            request.SendResponseAsync(returnValues).Completed += delegate { };
        }
        private static string GetProductsAsJson()
        {
            string json = "";
            var products = new ProductList();

            string appDataDirectory = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + @"\DesktopBridge.Samples\";
            string savedappDataDirectory = Windows.Storage.ApplicationData.Current.TemporaryFolder.Path;
            string savedappDataFilePath = savedappDataDirectory + @"\"+ filenName;
            appDataFilePath = appDataDirectory + filenName;

            //Load Product list from last save
            if (File.Exists(savedappDataFilePath))
            {
                File.Delete(appDataFilePath);
                File.Copy(savedappDataFilePath, appDataFilePath);
             }
            
            if (!File.Exists(appDataFilePath))
            {
                if (!Directory.Exists(appDataDirectory))
                {
                    Directory.CreateDirectory(appDataDirectory);
                }
                string installDirectory = System.Reflection.Assembly.GetExecutingAssembly().Location;
                DirectoryInfo info = Directory.GetParent(installDirectory);
                File.Copy(info.FullName + @"\" + filenName, appDataFilePath);
            }
            ds = new DataSet();
            ds.ReadXml(appDataFilePath);

            var reader = ds.CreateDataReader();
            while (reader.Read())
            {
                Product product = new Product();
                product.ProductID = int.Parse((string)reader.GetValue(0));
                product.ProductName = (string)reader.GetValue(1);
                product.QuantityPerUnit = (string)reader.GetValue(2);
                product.UnitPrice = decimal.Parse((string)reader.GetValue(3));
                product.UnitsInStock = int.Parse((string)reader.GetValue(4));
                products.Add(product);
                Console.WriteLine("Product: " + product.ProductName);
            }
            Console.WriteLine("Products count: {0}", products.Count);
            ds.Dispose();

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

                json = jsonArray.Stringify();
            }
    
            return json;
        }
        private static string SaveOrderAndDetails(Order order)
        {
            return "";
        }
    }

}



