//
// MainPage.xaml.cpp
// Implementation of the MainPage class.
//

#include "pch.h"
#include "MainPage.xaml.h"

using namespace Xaml_MapUI;

using namespace Platform;
using namespace Windows::Foundation;
using namespace Windows::Foundation::Collections;
using namespace Windows::UI::Xaml;
using namespace Windows::UI::Xaml::Controls;
using namespace Windows::UI::Xaml::Controls::Primitives;
using namespace Windows::UI::Xaml::Controls::Maps;
using namespace Windows::UI::Xaml::Data;
using namespace Windows::UI::Xaml::Input;
using namespace Windows::UI::Xaml::Media;
using namespace Windows::UI::Xaml::Navigation;
using namespace Windows::Devices::Geolocation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

MainPage::MainPage()
{
	InitializeComponent();
}

void MainPage::OnNavigatedTo(NavigationEventArgs^ e)
{	
	if (e->Parameter != nullptr)
	{
		// extracting latitude and longitude parameters
		double lat = 0, lon = 0;
		WwwFormUrlDecoder ^decoder = ref new WwwFormUrlDecoder(e->Parameter->ToString());
		lat = std::wcstod(decoder->GetAt(0)->Value->Data(), NULL);
		lon = std::wcstod(decoder->GetAt(1)->Value->Data(), NULL);
		
		// setting the Maps center to this location and zoom in
		BasicGeoposition pos;
		pos.Latitude = lat;
		pos.Longitude = lon;
		myMap->Center = ref new Geopoint(pos);
		myMap->LandmarksVisible = true;
	}
}


void Xaml_MapUI::MainPage::TrafficFlowVisible_Checked(Platform::Object^ sender, Windows::UI::Xaml::RoutedEventArgs^ e)
{
	myMap->TrafficFlowVisible = true;
}


void Xaml_MapUI::MainPage::trafficFlowVisibleCheckBox_Unchecked(Platform::Object^ sender, Windows::UI::Xaml::RoutedEventArgs^ e)
{
	myMap->TrafficFlowVisible = false;
}


void Xaml_MapUI::MainPage::styleCombobox_SelectionChanged(Platform::Object^ sender, Windows::UI::Xaml::Controls::SelectionChangedEventArgs^ e)
{
	switch (styleCombobox->SelectedIndex)
	{
	case 0:
		myMap->Style = MapStyle::None;
		break;
	case 1:
		myMap->Style = MapStyle::Road;
		break;
	case 2:
		myMap->Style = MapStyle::Aerial;
		break;
	case 3:
		myMap->Style = MapStyle::AerialWithRoads;
		break;
	case 4:
		myMap->Style = MapStyle::Terrain;
		break;
	case 5:
		myMap->Style = MapStyle::Aerial3D;
		break;
	case 6:
		myMap->Style = MapStyle::Aerial3DWithRoads;
		break;
	}
}

