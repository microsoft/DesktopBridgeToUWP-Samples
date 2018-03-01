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

//
// MainPage.xaml.cpp
// Implementation of the MainPage class.
//

#include "pch.h"
#include "MainPage.xaml.h"

using namespace ShareUX;

using namespace Concurrency;
using namespace Platform;
using namespace Windows::Foundation;
using namespace Windows::Foundation::Collections;
using namespace Windows::UI::Core;
using namespace Windows::UI::Xaml;
using namespace Windows::UI::Xaml::Controls;
using namespace Windows::UI::Xaml::Controls::Primitives;
using namespace Windows::UI::Xaml::Data;
using namespace Windows::UI::Xaml::Input;
using namespace Windows::UI::Xaml::Media;
using namespace Windows::UI::Xaml::Media::Imaging;
using namespace Windows::UI::Xaml::Navigation;
using namespace Windows::ApplicationModel::DataTransfer;
using namespace Windows::ApplicationModel::DataTransfer::ShareTarget;
using namespace Windows::Storage;
using namespace Windows::Storage::Streams;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

MainPage::MainPage()
{
	InitializeComponent();
}

void MainPage::OnNavigatedTo(NavigationEventArgs^ e)
{
	if (e->Parameter != nullptr)
	{
		operation = safe_cast<ShareOperation^>(e->Parameter);
		if (operation->Data->Contains(StandardDataFormats::StorageItems))
		{
			task<IVectorView<IStorageItem^>^>(operation->Data->GetStorageItemsAsync()).then([this](IVectorView<IStorageItem^>^ storageItems)
			{
				file = safe_cast<StorageFile^>(storageItems->GetAt(0));
				task<IRandomAccessStreamWithContentType^>(file->OpenReadAsync()).then([this](IRandomAccessStreamWithContentType ^stream)
				{
					this->Dispatcher->RunAsync(CoreDispatcherPriority::Normal, ref new DispatchedHandler([this, stream]()
					{
						BitmapImage ^image = ref new BitmapImage();
						this->img->Source = image;
						image->SetSourceAsync(stream);						
					}));
				});
			});			
		}
	}
}


void ShareUX::MainPage::ShareBtn_Click(Platform::Object^ sender, Windows::UI::Xaml::RoutedEventArgs^ e)
{	

	file->CopyAsync(Windows::Storage::ApplicationData::Current->LocalFolder);
	operation->ReportCompleted();
}
