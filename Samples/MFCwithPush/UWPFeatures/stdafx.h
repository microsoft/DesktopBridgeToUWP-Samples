// stdafx.h : include file for standard system include files,
// or project specific include files that are used frequently, but
// are changed infrequently
//

#pragma once

#include "targetver.h"
#include <collection.h>
#include <ppltasks.h>

#define WIN32_LEAN_AND_MEAN             // Exclude rarely-used stuff from Windows headers
// Windows Header Files:
#include <windows.h>
typedef int(__cdecl *PUSHRECEIVED)(PCWSTR content);
#define DllExport   __declspec( dllexport )
EXTERN_C DllExport void __cdecl SubscribePushNotificationEvent(void* callback);
EXTERN_C DllExport void __cdecl RegisterBackgroundTask(PCWSTR pszString);

// TODO: reference additional headers your program requires here
