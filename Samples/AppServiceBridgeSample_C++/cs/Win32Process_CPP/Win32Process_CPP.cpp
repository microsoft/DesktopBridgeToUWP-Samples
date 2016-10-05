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

#include "stdafx.h"

using namespace std;
using namespace concurrency;
using namespace Platform;
using namespace Windows::ApplicationModel::AppService;
using namespace Windows::ApplicationModel::DataTransfer;
using namespace Windows::Foundation;
using namespace Windows::Foundation::Collections;
using namespace Windows::Storage;
using namespace Windows::UI::Notifications;
using namespace Windows::Data::Xml::Dom;


AppServiceConnection^ _connection = nullptr;
void RequestReceived(AppServiceConnection^ connection, AppServiceRequestReceivedEventArgs^ args);
void ServiceClosed(AppServiceConnection^ connection, AppServiceClosedEventArgs^ args);

/// <summary>
/// Gets the current package family name to intiate the connection
/// </summary>
int GetCurrentPackageFamilyName(PWSTR& familyName)
{
	UINT32 length = 0;
	LONG rc = GetCurrentPackageFamilyName(&length, NULL);
	if (rc != ERROR_INSUFFICIENT_BUFFER)
	{
		if (rc == APPMODEL_ERROR_NO_PACKAGE)
			wprintf(L"Process has no package identity\n");
		else
			wprintf(L"Error %d in GetCurrentPackageFamilyName\n", rc);
		return 1;
	}
	if (familyName == NULL)
	{
		wprintf(L"Error allocating memory\n");
		return 2;
	}

	rc = GetCurrentPackageFamilyName(&length, familyName);
	if (rc != ERROR_SUCCESS)
	{
		wprintf(L"Error %d retrieving PackageFamilyName\n", rc);
		return 3;
	}
	wprintf(L"For package family name - %s\n", familyName);

	return 0;
}

/// <summary>
/// Creates the app service connection
/// </summary>
IAsyncAction^ ConnectToAppServiceAsync()
{
	return create_async([]
	{
		// Get the package family name
		UINT32 length = 0;
		LONG rc = GetCurrentPackageFullName(&length, NULL);
		PWSTR familyName = (PWSTR)malloc(length * sizeof(*familyName));
		rc = GetCurrentPackageFamilyName(familyName);
		assert(rc == 0);

		// Create and set the connection
		auto connection = ref new AppServiceConnection();
		connection->PackageFamilyName = ref new String(familyName); ;
		connection->AppServiceName = "CommunicationService";
		SetConsoleTextAttribute(GetStdHandle(STD_OUTPUT_HANDLE), (FOREGROUND_GREEN));
		cout << "Opening connection..." << endl;
		free(familyName);

		// Open the connection
		create_task(connection->OpenAsync()).then([connection](AppServiceConnectionStatus status)
		{
			if (status == AppServiceConnectionStatus::Success)
			{
				_connection = connection;
				wcout << "Successfully opened connection." << endl;
				_connection->RequestReceived += ref new TypedEventHandler<AppServiceConnection^, AppServiceRequestReceivedEventArgs^>(RequestReceived);
				_connection->ServiceClosed += ref new TypedEventHandler<AppServiceConnection^, AppServiceClosedEventArgs^>(ServiceClosed);
			}
			else if (status == AppServiceConnectionStatus::AppUnavailable || status == AppServiceConnectionStatus::AppServiceUnavailable)
			{
				SetConsoleTextAttribute(GetStdHandle(STD_OUTPUT_HANDLE), (FOREGROUND_RED));
				cout << "AppService Unavailable" << endl;
			}
		});
	});
}

/// <summary>
/// Creates an app service thread
/// </summary>
int main(Platform::Array<Platform::String^>^ args)
{
	SetConsoleTextAttribute(GetStdHandle(STD_OUTPUT_HANDLE), (FOREGROUND_RED | FOREGROUND_GREEN));
	wcout << "*********************************" << endl;
	wcout << "**** Classic desktop C++ app ****" << endl;
	wcout << "*********************************" << endl << endl;
	wcout << L"Creating app service connection" << endl << endl;

	ConnectToAppServiceAsync();

	string result;
	cin >> result;
    return 0;
}

/// <summary>
/// Creates a toast with the recived message from the UWP 
/// </summary>
void SendNewToast(PCWSTR XMLString)
{
	// Use LoadXml to load the input xml string, which should be formatted as expected by the Toasts schema
	String^ toastXMLString = ref new String(XMLString);
	XmlDocument^ toastPayloadXML = ref new XmlDocument();
	toastPayloadXML->LoadXml(toastXMLString);
	ToastNotification^ toast = ref new ToastNotification(toastPayloadXML);
	ToastNotificationManager::CreateToastNotifier()->Show(toast);
}

/// <summary>
/// Creates aa toast with the recived message from the UWP 
/// </summary>
void ShowToast(PCWSTR data)
{
	wchar_t* buffer = new wchar_t[512];
	PCWSTR toast = LR"(<toast>
                        <visual>
                            <binding template="ToastGeneric">
                                <text>Request received:</text>
                                <text> %s</text>
                            </binding>
                        </visual>
						<audio src="ms-winsoundevent:Notification.IM"/>
                     </toast>)";

	wsprintf(buffer, toast, data);
	SendNewToast(buffer);
	delete []buffer;
}

/// <summary>
/// Receives message from UWP app and sends a response back
/// </summary>
void RequestReceived(AppServiceConnection^ connection, AppServiceRequestReceivedEventArgs^ args)
{
	auto deferral = args->GetDeferral();
	auto message = args->Request->Message;
	auto method = message->Lookup(L"request")->ToString();
	PCWSTR data = method->Data();

	SetConsoleTextAttribute(GetStdHandle(STD_OUTPUT_HANDLE), (FOREGROUND_RED | FOREGROUND_GREEN));
	wcout << data;
	wcout << L" - request received" << endl;
	SetConsoleTextAttribute(GetStdHandle(STD_OUTPUT_HANDLE), (FOREGROUND_GREEN));

	ShowToast(data);
	auto response = ref new ValueSet();
	response->Insert("response", "ack");
	create_task(args->Request->SendResponseAsync(response)).then([deferral](AppServiceResponseStatus status)
	{
		deferral->Complete();
	});
}

/// <summary>
/// Occurs when the other endpoint closes the connection to the app service
/// </summary>
void ServiceClosed(AppServiceConnection^ connection, AppServiceClosedEventArgs^ args)
{
}

