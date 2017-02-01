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

