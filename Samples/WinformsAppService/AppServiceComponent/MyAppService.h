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

#pragma once
namespace AppServiceComponent
{
	[Windows::Foundation::Metadata::WebHostHidden]
	public ref class MyAppService sealed :
		public Windows::ApplicationModel::Background::IBackgroundTask
	{
	public:
		MyAppService();

		virtual void Run(
			Windows::ApplicationModel::Background::IBackgroundTaskInstance^ taskInstance);

	private:
		void OnCanceled(
			Windows::ApplicationModel::Background::IBackgroundTaskInstance^ sender,
			Windows::ApplicationModel::Background::BackgroundTaskCancellationReason reason);
		void OnRequestReceived(
			Windows::ApplicationModel::AppService::AppServiceConnection^ sender,
			Windows::ApplicationModel::AppService::AppServiceRequestReceivedEventArgs^ args);
		void OnRequestCompleted(
			Windows::Foundation::IAsyncOperation<Windows::ApplicationModel::AppService::AppServiceResponseStatus>^ status,
			Windows::Foundation::AsyncStatus unused);
		void AppendFile(Platform::String^ filename, Platform::String^ data);

	private:
		// Used to defer the task from completion until it is canceled.
		Platform::Agile<Windows::ApplicationModel::Background::BackgroundTaskDeferral^> _deferral;
		Platform::Agile<Windows::ApplicationModel::AppService::AppServiceDeferral^> _msgDeferral;
	};
}


