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
namespace PushNotificationBackgroundTask
{
	[Windows::Foundation::Metadata::WebHostHidden]
	public ref class MyTask sealed :
		public Windows::ApplicationModel::Background::IBackgroundTask
	{
	public:
		MyTask();

		virtual void Run(
			Windows::ApplicationModel::Background::IBackgroundTaskInstance^ taskInstance);		

	private:
		void CreateToast(Platform::String ^message);
		void OnCanceled(
			Windows::ApplicationModel::Background::IBackgroundTaskInstance^ sender,
			Windows::ApplicationModel::Background::BackgroundTaskCancellationReason reason);

		Platform::Agile<Windows::ApplicationModel::Background::BackgroundTaskDeferral^> _deferral;
	};
}

