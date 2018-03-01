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
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Windows.ApplicationModel.AppService;
using Windows.Foundation.Collections;
using Excel = Microsoft.Office.Interop.Excel;

namespace ExcelInterop
{
    class Program
    {
        static AppServiceConnection connection = null;
        static AutoResetEvent appServiceExit;

        static void Main(string[] args)
        {
            // connect to app service and wait until the connection gets closed
            appServiceExit = new AutoResetEvent(false);
            InitializeAppServiceConnection();
            appServiceExit.WaitOne();
        }

        static async void InitializeAppServiceConnection()
        {
            connection = new AppServiceConnection();
            connection.AppServiceName = "ExcelInteropService";
            connection.PackageFamilyName = Windows.ApplicationModel.Package.Current.Id.FamilyName;
            connection.RequestReceived += Connection_RequestReceived;
            connection.ServiceClosed += Connection_ServiceClosed;

            AppServiceConnectionStatus status = await connection.OpenAsync();
            if (status != AppServiceConnectionStatus.Success)
            {
                // TODO: error handling
            }
        }

        private static void Connection_ServiceClosed(AppServiceConnection sender, AppServiceClosedEventArgs args)
        {
            // signal the event so the process can shut down
            appServiceExit.Set();
        }

        private async static void Connection_RequestReceived(AppServiceConnection sender, AppServiceRequestReceivedEventArgs args)
        {
            string value = args.Request.Message["REQUEST"] as string;
            string result = "";
            switch (value)
            {
                case "CreateSpreadsheet":
                    try
                    {
                        // call Office Interop APIs to create the Excel spreadsheet
                        Excel.Application excel = new Excel.Application();
                        excel.Visible = true;
                        Excel.Workbook wb = excel.Workbooks.Add();
                        Excel.Worksheet sh = wb.Sheets.Add();
                        sh.Name = "DataGrid";
                        sh.Cells[1, "A"].Value2 = "Id";
                        sh.Cells[1, "B"].Value2 = "Description";
                        sh.Cells[1, "C"].Value2 = "Quantity";
                        sh.Cells[1, "D"].Value2 = "UnitPrice";

                        for (int i = 0; i < args.Request.Message.Values.Count / 4; i++)
                        {
                            sh.Cells[i + 2, "A"].Value2 = args.Request.Message["Id" + i.ToString()] as string;
                            sh.Cells[i + 2, "B"].Value2 = args.Request.Message["Description" + i.ToString()] as string;
                            sh.Cells[i + 2, "C"].Value2 = args.Request.Message["Quantity" + i.ToString()].ToString();
                            sh.Cells[i + 2, "D"].Value2 = args.Request.Message["UnitPrice" + i.ToString()].ToString();
                        }
                        result = "SUCCESS";
                    }
                    catch (Exception exc)
                    {
                        result = exc.Message;
                    }
                    break;
                default:
                    result = "unknown request";
                    break;
            }

            ValueSet response = new ValueSet();
            response.Add("RESPONSE", result);
            await args.Request.SendResponseAsync(response);
        }
    }
}
