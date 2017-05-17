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
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.ApplicationModel.AppService;
using Windows.Data.Xml.Dom;
using Windows.UI.Notifications;
using Microsoft.PointOfService;
using System.Collections;

namespace BackgroundProcess
{
    class Program
    {
        static AppServiceConnection connection = null;

        static bool keepRuuning = true; //boolean to keep process alive if used as service without UI

        //OPOS variables
        static PosExplorer explorer;
        static DeviceInfo selectedDeviceInfo = null;
        static Scale claimedDevice = null;
        static string LastWeight = "";

        /// <summary>
        /// Creates an app service thread
        /// </summary>
        static void Main(string[] args)
        {
            Thread appServiceThread = new Thread(new ThreadStart(ThreadProc));
            appServiceThread.Start();
            
            //Use this line for Service APP Demo
            while (keepRuuning)Thread.Sleep(1000);
        }

        /// <summary>
        /// Creates the app service connection
        /// </summary>
        static async void ThreadProc()
        {
            connection = new AppServiceConnection();
            connection.AppServiceName = "CommunicationService";
            connection.PackageFamilyName = Windows.ApplicationModel.Package.Current.Id.FamilyName;
            connection.RequestReceived += Connection_RequestReceived;

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
        /// Receives message from UWP app and sends a response back
        /// </summary>
        private  static void Connection_RequestReceived(AppServiceConnection sender, AppServiceRequestReceivedEventArgs args)
        {
            string key = args.Request.Message.First().Key;
            string value = args.Request.Message.First().Value.ToString();
            ValueSet valueSet = new ValueSet();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine(string.Format("Received message '{0}' with value '{1}'", key, value));
            
            switch (value)
            {
                case "claimScale":
                    // send back: scaleReady
                    valueSet.Add("response", "scaleReady");
                    args.Request.SendResponseAsync(valueSet).Completed += delegate { };
                    Console.WriteLine(string.Format("Sending response: '{0}'", "scaleReady"));
                    Console.WriteLine();
                    valueSet.Clear();
                    
                    //Finds scales in the service enumeration and return the one we use for demo that has table in the name
                    //Call the classic OPOS code
                    // "table" parameter was specific to the device we used in video demo, please adapt to you target device
                    selectedDeviceInfo = Find("scale","table");
                    //Run Open, Claim, Enable device for usage
                    claimedDevice = Scale_ClaimOpenEnable(selectedDeviceInfo);
                    return;

                case "getWeight":
                    //send back: weight
                    String auxWeight = getWeight(); 
                    valueSet.Add("response", auxWeight);
                    args.Request.SendResponseAsync(valueSet).Completed += delegate { };
                    Console.WriteLine(string.Format("Sending response: '{0}'", auxWeight));
                    Console.WriteLine();
                    valueSet.Clear();
                    return;


                case "endProcess":
                    valueSet.Add("response", "processEnded");
                    args.Request.SendResponseAsync(valueSet).Completed += delegate { };
                    keepRuuning = false;
                    valueSet.Clear();
                    return;

            }
        }
        
        /*getWeight calls API to get the current weight in the scale*/
        private  static String getWeight()
        {
            Decimal weight = ReadWeight(); //Call method to read from Scale using .netOPOs code
            String textweight = weight.ToString(System.Globalization.CultureInfo.CurrentCulture);
            
            
            //Toast Notification Code
            String toastXML = "<toast><visual><binding template = 'ToastGeneric'><text>New Weight Read</text><text>" + textweight + "</text></binding></visual></toast>";
            XmlDocument xmldoc = new XmlDocument();
            xmldoc.LoadXml(toastXML);
            ToastNotification toast = new ToastNotification(xmldoc);
            ToastNotificationManager.CreateToastNotifier().Show(toast);

            return textweight;
        }

#region Classic_.netOPOS_Code

        static void InitPosCheck()
        {
            if (explorer == null)
            {
                explorer = new PosExplorer();
                explorer.DeviceAddedEvent += new DeviceChangedEventHandler(explorer_DeviceAddedEvent);
                explorer.DeviceRemovedEvent += new DeviceChangedEventHandler(explorer_DeviceRemovedEvent);
            }
        }

        static DeviceInfo Find(string DeviceClass, string SONameMatch)
        {
            InitPosCheck();

            ArrayList deviceDisplayList;
            DeviceInfo selectedDevice = null;

            WriteLog("Looking for POS class = {0}", DeviceClass);
            deviceDisplayList = new ArrayList(explorer.GetDevices(DeviceClass));
            foreach (DeviceInfo di in deviceDisplayList)
            {
                WriteLog("ServiceObjectNAme: {0}", di.ServiceObjectName);
                WriteLog("  Desc: {0}", di.Description);
                if (di.LogicalNames.Count() >= 1)
                {
                    int ix = 0;
                    foreach (string lname in di.LogicalNames)
                    {
                        WriteLog("  Logical[{0}]: {1}", ix++, lname);
                    }
                }

                if (!string.IsNullOrEmpty(SONameMatch) && di.ServiceObjectName.ToLower().Contains(SONameMatch.ToLower())) selectedDevice = di;
            }
            if (selectedDevice == null && deviceDisplayList.Count > 0) selectedDevice = (DeviceInfo)(deviceDisplayList[0]);
            return selectedDevice;
        }

        static Scale Scale_ClaimOpenEnable(DeviceInfo di)
        {
            InitPosCheck();
            if (di == null) return null;

            try
            {
                Scale scale = (Scale)explorer.CreateInstance(di);

                scale.Open();
                WriteLog("  Opened {0}.", di.ServiceObjectName);

                scale.Claim(5000);
                WriteLog("  Claimed {0}.", di.ServiceObjectName);

                scale.DeviceEnabled = true;
                WriteLog("  Enabled {0}.", di.ServiceObjectName);

                scale.DataEvent += Scale_DataEvent;
                return scale;
            }
            catch (PosControlException pe)
            {
                WriteLog("EXCEPTION: ClaimScale({0}) - {1} ", di.ServiceObjectName, pe.Message);
            }
            return null;
        }

        static Decimal ReadWeight()
        {
            Decimal w = 0;
            try
            {
                w = claimedDevice.ReadWeight(2000);
                WriteLog("Read Weight = {0}", w);
            }
            catch (Exception ex)
            {
                WriteLog("EXCEPTION: ReadWeight() - {0}", ex.Message);
            }
            return w;
        }

        
        private static void Scale_DataEvent(object sender, DataEventArgs e)
        {
            LastWeight = "Item Weight: " + e.Status.ToString(System.Globalization.CultureInfo.CurrentCulture);
            WriteLog(LastWeight);
        }


        static void WriteLog(string format, params object[] args)
        {
            string line = string.Format(format, args);
            Console.WriteLine(line);
        }


        static void explorer_DeviceRemovedEvent(object sender, DeviceChangedEventArgs e)
        {
            WriteLog(string.Format("Device Removed {0} : {1}",
                e.Device.Type.ToString(),
                e.Device.ServiceObjectName));
        }

        static void explorer_DeviceAddedEvent(object sender, DeviceChangedEventArgs e)
        {
            WriteLog(string.Format("Device Added {0} : {1}",
                e.Device.Type.ToString(), e.Device.ServiceObjectName));

        }
#endregion

    }
}
