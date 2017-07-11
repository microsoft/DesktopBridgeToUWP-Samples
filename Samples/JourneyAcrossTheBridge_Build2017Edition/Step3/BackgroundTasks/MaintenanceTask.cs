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
using Windows.Data.Xml.Dom;
using Windows.UI.Notifications;
using Windows.ApplicationModel.Background;

namespace BackgroundTasks
{
    sealed public class MaintenanceTask : IBackgroundTask
    {
        BackgroundTaskDeferral deferral = null;
        public void Run(IBackgroundTaskInstance taskInstance)
        {
            deferral = taskInstance.GetDeferral();
            ShowToast("Running background maintenance");
            DoMaintenance();
        }

        private void DoMaintenance()
        {
            DateTime start = DateTime.Now;
            while (DateTime.Now.Subtract(start).TotalSeconds < 45)
            {
                var d = Math.Pow(Math.Sqrt(Math.PI), Math.PI);
            }
            deferral.Complete();
        }
        private void ShowToast(string message)
        {
            string xml = $@"<toast activationType='foreground'>
                                            <visual>
                                                <binding template='ToastGeneric'>
                                                    <image src='images/MyLogo.png'/>
                                                    <text>Demo Step3</text>
                                                </binding>
                                            </visual>
                                        </toast>";

            XmlDocument doc = new XmlDocument();
            doc.LoadXml(xml);
            var binding = doc.SelectSingleNode("//binding");
            var el = doc.CreateElement("text");
            el.InnerText = message;
            binding.AppendChild(el); //Add content to notification

            var toast = new ToastNotification(doc);
            ToastNotificationManager.CreateToastNotifier().Show(toast); //Show the toast
        }
    }
}
