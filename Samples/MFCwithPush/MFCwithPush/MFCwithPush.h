
// MFCwithPush.h : main header file for the MFCwithPush application
//
#pragma once

#ifndef __AFXWIN_H__
	#error "include 'stdafx.h' before including this file for PCH"
#endif

#include "resource.h"       // main symbols


// CMFCwithPushApp:
// See MFCwithPush.cpp for the implementation of this class
//

class CMFCwithPushApp : public CWinAppEx
{
public:
	CMFCwithPushApp();


// Overrides
public:
	virtual BOOL InitInstance();
	virtual int ExitInstance();

// Implementation
	UINT  m_nAppLook;
	BOOL  m_bHiColorIcons;
	Platform::String ^pushNotificationChannelUri;

	virtual void PreLoadState();
	virtual void LoadCustomState();
	virtual void SaveCustomState();

	afx_msg void OnAppAbout();
	DECLARE_MESSAGE_MAP()
	static void OnPushNotificationReceived(Windows::Networking::PushNotifications::PushNotificationChannel ^sender, Windows::Networking::PushNotifications::PushNotificationReceivedEventArgs ^args);
	void CMFCwithPushApp::UnregisterBackgroundTasks(Platform::String^ name);
};

extern CMFCwithPushApp theApp;
