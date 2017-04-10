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

using namespace TestClient;

using namespace std;
using namespace concurrency;
using namespace Platform;
using namespace Windows::Foundation;
using namespace Windows::Foundation::Collections;
using namespace Windows::UI;
using namespace Windows::UI::Xaml;
using namespace Windows::UI::Xaml::Controls;
using namespace Windows::UI::Xaml::Controls::Primitives;
using namespace Windows::UI::Xaml::Data;
using namespace Windows::UI::Xaml::Input;
using namespace Windows::UI::Xaml::Media;
using namespace Windows::UI::Xaml::Navigation;
using namespace Windows::ApplicationModel::Background;
using namespace Windows::ApplicationModel::AppService;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

AppServiceConnection ^connection = nullptr;
MainPage::MainPage()
{
	InitializeComponent();
}


void TestClient::MainPage::connectBtn_Click(Platform::Object^ sender, Windows::UI::Xaml::RoutedEventArgs^ e)
{
	if (connection == nullptr)
	{
		this->connectBtn->IsEnabled = false;
		connection = ref new AppServiceConnection();
		connection->AppServiceName = "DataAppService";
		connection->PackageFamilyName = "WinformsAppService_bvbr2n7re90hg";
		create_task(connection->OpenAsync()).then([this](AppServiceConnectionStatus status)
		{
			this->connectBtn->IsEnabled = true;
			this->tbStatus->Text = "Status: " + status.ToString();
			if (status == AppServiceConnectionStatus::Success)
			{
				this->tbStatus->Foreground = ref new SolidColorBrush(Colors::Green);
				this->connectBtn->Content = "Disconnect";
				this->queryBtn->IsEnabled = true;
			}
		});
	}
	else
	{
		connection = nullptr;

		this->tbStatus->Text = "Status: Closed";
		this->tbStatus->Foreground = ref new SolidColorBrush(Colors::Red);
		this->connectBtn->Content = "Connect";
		this->queryBtn->IsEnabled = false;
		this->tbName->Text = "";
		this->tbPhone->Text = "";
		this->tbCity->Text = "";
		this->tbState->Text = "";
	}
}


void TestClient::MainPage::queryBtn_Click(Platform::Object^ sender, Windows::UI::Xaml::RoutedEventArgs^ e)
{
	ValueSet ^request = ref new ValueSet();
	request->Insert("ID", tbId->Text);
	create_task(connection->SendMessageAsync(request)).then([this](AppServiceResponse ^response)
	{
		this->tbName->Text = "Name: " + response->Message->Lookup("Name")->ToString();
		this->tbPhone->Text = "Phone: " + response->Message->Lookup("Phone")->ToString();
		this->tbCity->Text = "City: " + response->Message->Lookup("City")->ToString();
		this->tbState->Text = "State: " + response->Message->Lookup("State")->ToString();
	});
}
