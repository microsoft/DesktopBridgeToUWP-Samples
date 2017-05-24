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
    return L"Welcome to Build! Please give Andrew some applause! :)";
}
