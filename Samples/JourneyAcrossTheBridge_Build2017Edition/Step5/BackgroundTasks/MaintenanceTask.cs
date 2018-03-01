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

using System.Threading.Tasks;
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
            ShowToast("Perform maintenance in the background");
            DoLongRunningMaintenance();
        }

        private async void DoLongRunningMaintenance()
        {
            await Task.Delay(5000);
            deferral.Complete();
        }
        private void ShowToast(string message)
        {
            string xml = $@"<toast activationType='foreground'>
                                            <visual>
                                                <binding template='ToastGeneric'>
                                                    <image src='images/MyLogo.png'/>
                                                    <text>Fake Edge</text>
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
