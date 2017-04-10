// stdafx.h : include file for standard system include files,
// or project specific include files that are used frequently, but
// are changed infrequently
//

#pragma once

#include "targetver.h"

#define WIN32_LEAN_AND_MEAN             // Exclude rarely-used stuff from Windows headers
// Windows Header Files:
#include <windows.h>
#define DllExport   __declspec( dllexport )
EXTERN_C DllExport bool __stdcall LaunchMap(double lat, double lon);
EXTERN_C DllExport bool __stdcall CreateToast(int index);


// TODO: reference additional headers your program requires here
