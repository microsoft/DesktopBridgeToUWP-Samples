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
#include <iostream> 
#include "MainPage.xaml.h"

using namespace Xaml_MapUI;

using namespace std;
using namespace Platform;
using namespace Windows::ApplicationModel;
using namespace Windows::ApplicationModel::Activation;
using namespace Windows::Foundation;
using namespace Windows::Foundation::Collections;
using namespace Windows::UI::Xaml;
using namespace Windows::UI::Xaml::Controls;
using namespace Windows::UI::Xaml::Controls::Primitives;
using namespace Windows::UI::Xaml::Data;
using namespace Windows::UI::Xaml::Input;
using namespace Windows::UI::Xaml::Interop;
using namespace Windows::UI::Xaml::Media;
using namespace Windows::UI::Xaml::Navigation;
using namespace Windows::UI::ViewManagement;
using namespace Windows::UI;

/// <summary>
/// Initializes the singleton application object.  This is the first line of authored code
/// executed, and as such is the logical equivalent of main() or WinMain().
/// </summary>
App::App()
{
    InitializeComponent();
    Suspending += ref new SuspendingEventHandler(this, &App::OnSuspending);

	// specify an preferred size for the application view
	Size preferredSize;
	preferredSize.Width = 840;
	preferredSize.Height = 480;
	ApplicationView::PreferredLaunchViewSize = preferredSize;	
	ApplicationView::PreferredLaunchWindowingMode = ApplicationViewWindowingMode::PreferredLaunchViewSize;
}

/// <summary>
/// Invoked when the application is activated via protocol, file asociation, etc.
/// </summary>
/// <param name="e">Details about the activation request.</param>
void App::OnActivated(Windows::ApplicationModel::Activation::IActivatedEventArgs^ e)
{
	if (e->Kind == ActivationKind::Protocol)
	{
		ProtocolActivatedEventArgs^ protocolArgs = (ProtocolActivatedEventArgs^)e;
		Uri ^uri = protocolArgs->Uri;
		if (uri->SchemeName == "desktopbridgemapsample")
		{
			Frame ^rootFrame = ref new Frame();
			Window::Current->Content = rootFrame;
			rootFrame->Navigate(TypeName(MainPage::typeid), uri->Query);
			Window::Current->Activate();

			ApplicationView ^view = ApplicationView::GetForCurrentView();
			view->TitleBar->BackgroundColor = Colors::Black;
			view->TitleBar->InactiveBackgroundColor = Colors::Black;
			view->TitleBar->InactiveForegroundColor = Colors::White;
			view->TitleBar->ButtonBackgroundColor = Colors::Black;
			view->TitleBar->ButtonInactiveBackgroundColor = Colors::Black;
			view->TitleBar->ButtonInactiveForegroundColor = Colors::White;
		}
	}
}

/// <summary>
/// Invoked when application execution is being suspended.  Application state is saved
/// without knowing whether the application will be terminated or resumed with the contents
/// of memory still intact.
/// </summary>
/// <param name="sender">The source of the suspend request.</param>
/// <param name="e">Details about the suspend request.</param>
void App::OnSuspending(Object^ sender, SuspendingEventArgs^ e)
{
    (void) sender;  // Unused parameter
    (void) e;   // Unused parameter

    //TODO: Save application state and stop any background activity
}

/// <summary>
/// Invoked when Navigation to a certain page fails
/// </summary>
/// <param name="sender">The Frame which failed navigation</param>
/// <param name="e">Details about the navigation failure</param>
void App::OnNavigationFailed(Platform::Object ^sender, Windows::UI::Xaml::Navigation::NavigationFailedEventArgs ^e)
{
    throw ref new FailureException("Failed to load Page " + e->SourcePageType.Name);
}