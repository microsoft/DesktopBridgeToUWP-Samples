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
using Windows.UI.Notifications;
using Windows.Data.Xml.Dom;
using Windows.ApplicationModel.Background;
using Windows.Storage;

namespace BackgroundTasks
{
    public sealed class MyBackgroundTask : IBackgroundTask
    {
        /// <summary>
        /// The Run method is the entry point of a background task.
        /// </summary>
        public void Run(IBackgroundTaskInstance taskInstance)
        {
            string currentStatus = ApplicationData.Current.LocalSettings.Values["Status"] as string;
            if (currentStatus == null)
            {
                currentStatus = "Error - couldn't retrieve current status";
            }
            ShowToast("Status: " + currentStatus);
        }

        /// <summary>
        /// Show toast notification
        /// </summary>
        private void ShowToast(string msg)
        {
            ToastTemplateType toastTemplate = ToastTemplateType.ToastText02;
            XmlDocument toastXml = ToastNotificationManager.GetTemplateContent(toastTemplate);

            XmlNodeList toastTextElements = toastXml.GetElementsByTagName("text");
            toastTextElements[0].AppendChild(toastXml.CreateTextNode(msg));
            toastTextElements[1].AppendChild(toastXml.CreateTextNode(DateTime.Now.ToString()));

            ToastNotification toast = new ToastNotification(toastXml);
            ToastNotificationManager.CreateToastNotifier().Show(toast);
        }
    }
}
