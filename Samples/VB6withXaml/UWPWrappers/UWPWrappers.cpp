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

// UWPWrappers.cpp : Defines the exported functions for the DLL application.
//

#include "stdafx.h"
using namespace std;
using namespace Platform;
using namespace Windows::Foundation;
using namespace Windows::Foundation::Collections;
using namespace Windows::Globalization;
using namespace Windows::System;
using namespace Windows::ApplicationModel;
using namespace Windows::System;
using namespace Windows::UI::Notifications;
using namespace Windows::Data::Xml::Dom;

ToastNotification^ toast = nullptr;
ToastNotifier^ notifier = nullptr;

DllExport bool __stdcall LaunchMap(double lat, double lon)
{
	try
	{
		//
		// format the URI to match the defined protocol:
		// desktopbridgemapsample://location?lat={latitude}&?lon={longitude}
		//
		String ^str = ref new String(L"desktopbridgemapsample://");
		Uri ^uri = ref new Uri(str + L"location?lat=" + lat.ToString() + L"&?lon=" + lon.ToString());
		Launcher::LaunchUriAsync(uri);
	}
	catch (Exception^ ex) { return false; }
	return true;
}

DllExport bool __stdcall CreateToast(int index)
{
	String ^file = ref new String();
	String ^text = ref new String();

	//
	// set text and image of the toast based on the landmark
	//
	switch (index)
	{
	case 0:
		file = L"TowerBridge.png";
		text = "Tower Bridge of London";
		break;
	case 1:
		file = L"SyndeyOpera.png";
		text = "Sydney Opera";
		break;
	case 2:
		file = L"StatueOfLiberty.png";
		text = "Statue of Liberty";
		break;
	case 3:
		file = L"Space Needle.png";
		text = "Seattle Space Needle";
		break;
	case 4:
		file = L"Tokyo.png";
		text = "Tokyo Tower";
		break;
	}

	String ^xml = "<toast><visual><binding template='ToastGeneric'><image src='Assets/" + file + "'/><text hint-maxLines='1'>New Landmark</text><text>" + text + "</text></binding></visual></toast>";
	try
	{
		if (notifier == nullptr)
		{
			notifier = ToastNotificationManager::CreateToastNotifier();
		}
		else
		{
			notifier->Hide(toast);
		}
		XmlDocument^ toastXml = ref new XmlDocument();
		toastXml->LoadXml(xml);
	
		toast = ref new ToastNotification(toastXml);
		notifier->Show(toast);
	}
	catch (Exception^ ex) { return false; }
	return true;
}