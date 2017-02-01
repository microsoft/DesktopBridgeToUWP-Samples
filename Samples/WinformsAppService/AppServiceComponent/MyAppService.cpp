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
#include "MyAppService.h"

using namespace Concurrency;
using namespace Platform;
using namespace Windows::ApplicationModel::Background;
using namespace Windows::ApplicationModel::AppService;
using namespace Windows::Data::Xml;
using namespace Windows::Data::Xml::Dom;
using namespace Windows::Foundation;
using namespace Windows::Foundation::Collections;
using namespace Windows::Storage;
using namespace Windows::Storage::Streams;
using namespace AppServiceComponent;


MyAppService::MyAppService()
{
}

void MyAppService::Run(IBackgroundTaskInstance^ taskInstance)
{
	_deferral = Platform::Agile<BackgroundTaskDeferral^>(taskInstance->GetDeferral());
	taskInstance->Canceled += ref new BackgroundTaskCanceledEventHandler(this, &AppServiceComponent::MyAppService::OnCanceled);
	AppServiceTriggerDetails ^details = safe_cast<AppServiceTriggerDetails^>(taskInstance->TriggerDetails);
	details->AppServiceConnection->RequestReceived +=
		ref new TypedEventHandler<AppServiceConnection ^, AppServiceRequestReceivedEventArgs ^>(this, &AppServiceComponent::MyAppService::OnRequestReceived);
}

/// <summary> 
/// Called when the app service received a request from a client
/// </summary> 
/// <param name="sender"></param>
/// <param name="args"></param>
void MyAppService::OnRequestReceived(AppServiceConnection^ /*sender*/, AppServiceRequestReceivedEventArgs^ args)
{
	_msgDeferral = Platform::Agile<AppServiceDeferral^>(args->GetDeferral());
	AppServiceRequest ^request = args->Request;
	ValueSet ^query = args->Request->Message;
	String ^Id = query->Lookup("ID")->ToString();

	// load data from LocalAppData
	String ^fileName = Windows::Storage::ApplicationData::Current->LocalCacheFolder->Path + "\\DesktopBridge.Samples\\datastore.xml";
	StorageFile^ file = create_task(StorageFile::GetFileFromPathAsync(fileName)).get();
	String ^xml = create_task(Windows::Storage::FileIO::ReadTextAsync(file)).get();	
	Windows::Data::Xml::Dom::XmlDocument ^doc = ref new Windows::Data::Xml::Dom::XmlDocument();
	doc->LoadXml(xml);

	// query the requested item and prepare the response
	auto node = doc->SelectSingleNode("descendant::contact[ID = '" + Id + "']");
	ValueSet ^response = ref new ValueSet();
	if (node != nullptr)
	{
		response->Insert("Name",
			node->SelectSingleNode("descendant::Name")->InnerText);
		response->Insert("Phone",
			node->SelectSingleNode("descendant::Phone")->InnerText);
		response->Insert("City",
			node->SelectSingleNode("descendant::City")->InnerText);
		response->Insert("State",
			node->SelectSingleNode("descendant::State")->InnerText);
	}
	else
	{
		response->Insert("Name", "RECORD NOT FOUND");
		response->Insert("Phone", "RECORD NOT FOUND");
		response->Insert("City", "RECORD NOT FOUND");
		response->Insert("State", "RECORD NOT FOUND");
	}

	AppServiceResponseStatus status = create_task(request->SendResponseAsync(response)).get();
	_msgDeferral->Complete();
}


/// <summary> 
/// Called when the background task is canceled by the app or by the system.
/// </summary> 
/// <param name="sender"></param>
/// <param name="reason"></param>
void MyAppService::OnCanceled(IBackgroundTaskInstance^ /*sender*/, BackgroundTaskCancellationReason reason)
{
	// Complete the background task (this raises the OnCompleted event on the corresponding
	// BackgroundTaskRegistration).
	_deferral->Complete();
}