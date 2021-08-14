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


// ClassicDLL.cpp : Defines the exported functions for the DLL application.
//

#include "stdafx.h"
#include <cstdlib>
#include <time.h>
#include <string>

using namespace std;

int Increment(int numberToIncrement)
{
    return ++numberToIncrement;
}

const wchar_t * EditText(const wchar_t * s)
{
    const wchar_t *text =
        L"Check out these great App Model sessions!"
        L"\n\nB8004: App Model Evolution"
        L"\n\nB8011: Bring your desktop apps to UWP and the Windows Store using the \n             Desktop Bridge"
        L"\n\nB8025: Cross-device and cross-platform experiences with Project Rome \n             and Microsoft Graph"
        L"\n\nB8108: App engagement in Windows and Cortana with User Activities and \n             Project Rome"
        L"\n\nB8012: Tip, tricks, and secrets: Building a great UWP app for PC"
        L"\n\nB8002: Introducing Adaptive Cards"
        L"\n\nB8093: Nextgen UWP app distribution: Building extensible, stream-able, \n             componentized apps";
    return text;
}
