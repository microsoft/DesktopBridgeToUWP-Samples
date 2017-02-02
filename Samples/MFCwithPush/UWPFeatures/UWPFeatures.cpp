// UWPFeatures.cpp : Defines the exported functions for the DLL application.
//

#include "stdafx.h"
using namespace std;

using namespace Platform;
using namespace concurrency;
using namespace Windows::Foundation;
using namespace Windows::Globalization;
using namespace Windows::Storage;
using namespace Windows::System;
using namespace Windows::ApplicationModel;
using namespace Windows::ApplicationModel::Background;
using namespace Windows::Networking::PushNotifications;

PUSHRECEIVED pushReceivedCallback;
void UnregisterBackgroundTasks(String ^name);
void OnPushNotificationReceived(PushNotificationChannel ^sender, PushNotificationReceivedEventArgs ^args);

DllExport void __cdecl SubscribePushNotificationEvent(void* pfnPushReceived)
{
	//
	// Hook up push notification event
	//
	pushReceivedCallback = (PUSHRECEIVED)pfnPushReceived;

	task<PushNotificationChannel^>(PushNotificationChannelManager::CreatePushNotificationChannelForApplicationAsync()).then([](PushNotificationChannel^ channel)
	{
		channel->PushNotificationReceived += ref new Windows::Foundation::TypedEventHandler<Windows::Networking::PushNotifications::PushNotificationChannel ^, Windows::Networking::PushNotifications::PushNotificationReceivedEventArgs ^>(&OnPushNotificationReceived);
		ApplicationData::Current->LocalSettings->Values->Insert("PushNotificationChannelUri", channel->Uri);
	});
}

void OnPushNotificationReceived(Windows::Networking::PushNotifications::PushNotificationChannel ^sender, Windows::Networking::PushNotifications::PushNotificationReceivedEventArgs ^args)
{
	if (pushReceivedCallback)
	{
		(pushReceivedCallback)(args->RawNotification->Content->Data());
	}
}

DllExport void __cdecl RegisterBackgroundTask(PCWSTR pszString)
{
	//
	// Register  notification background trigger
	//
	BackgroundTaskBuilder ^builder = ref new BackgroundTaskBuilder();
	builder->Name = ref new String(pszString);
	builder->TaskEntryPoint = "PushNotificationBackgroundTask.MyTask";
	builder->SetTrigger(ref new PushNotificationTrigger());
	UnregisterBackgroundTasks(builder->Name);
	builder->Register();
}

void UnregisterBackgroundTasks(String^ name)
{
	//
	// Loop through all background tasks and unregister any that have a name that matches
	// the name passed into this function.
	//
	auto iter = BackgroundTaskRegistration::AllTasks->First();
	auto hascur = iter->HasCurrent;
	while (hascur)
	{
		auto cur = iter->Current->Value;

		if (cur->Name == name)
		{
			cur->Unregister(true);
		}

		hascur = iter->MoveNext();
	}
}

