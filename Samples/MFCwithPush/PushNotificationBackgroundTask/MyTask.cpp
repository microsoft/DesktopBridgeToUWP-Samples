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

#include "pch.h"
#include "MyTask.h"

using namespace Concurrency;
using namespace Platform;
using namespace Windows::ApplicationModel::Background;
using namespace Windows::Data::Xml;
using namespace Windows::Data::Xml::Dom;
using namespace Windows::Foundation;
using namespace Windows::Foundation::Collections;
using namespace Windows::Networking::PushNotifications;
using namespace Windows::UI::Notifications;
using namespace Windows::Data::Xml::Dom;
using namespace PushNotificationBackgroundTask;

MyTask::MyTask()
{
}

void MyTask::Run(IBackgroundTaskInstance^ taskInstance)
{
	_deferral = Platform::Agile<BackgroundTaskDeferral^>(taskInstance->GetDeferral());
	taskInstance->Canceled += ref new BackgroundTaskCanceledEventHandler(this, &MyTask::MyTask::OnCanceled);
	RawNotification ^details = safe_cast<RawNotification^>(taskInstance->TriggerDetails);
	String ^rawContent = details->Content;

	//
	// process the raw message here ...
	// for the sample we just pop it up in a toast
	//
	CreateToast(rawContent);
	_deferral->Complete();
}

void MyTask::CreateToast(String ^message)
{
	String ^xml = "<toast><visual><binding template='ToastGeneric'><image src='Assets/mfc_LargeTile.png'/><text hint-maxLines='1'>New Code Review	</text><text>"
		+ message + "</text></binding></visual></toast>";
	ToastNotifier ^notifier = ToastNotificationManager::CreateToastNotifier();
	XmlDocument^ toastXml = ref new XmlDocument();
	toastXml->LoadXml(xml);
	ToastNotification ^toast = ref new ToastNotification(toastXml);
	notifier->Show(toast);
}

/// <summary> 
/// Called when the background task is canceled by the app or by the system.
/// </summary> 
/// <param name="sender"></param>
/// <param name="reason"></param>
void MyTask	::OnCanceled(IBackgroundTaskInstance^ /*sender*/, BackgroundTaskCancellationReason reason)
{
	// Complete the background task (this raises the OnCompleted event on the corresponding
	// BackgroundTaskRegistration).
	_deferral->Complete();
}