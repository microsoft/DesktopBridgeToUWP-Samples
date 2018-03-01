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

#include "stdafx.h"

// trim whitespace in front and back of string
void trim(std::wstring& value)
{
    const wchar_t* whitespace = L"\n\r\t";
    size_t startpos = value.find_first_not_of(whitespace);
    if (std::wstring::npos != startpos)
    {
        value = value.substr(startpos);
    }

    size_t endpos = value.find_last_not_of(whitespace);
    if (std::wstring::npos != endpos)
    {
        value = value.substr(0, endpos + 1);
    }
}

void slash(std::wstring& value)
{
    if (value[0] != '\\' || value[0] != '/')
        value = L'\\' + value;
}

// Create a string with last error message
std::wstring message()
{
    DWORD error = GetLastError();
    if (error)
    {
        LPVOID buffer = nullptr;
        DWORD length = FormatMessageW(FORMAT_MESSAGE_ALLOCATE_BUFFER | FORMAT_MESSAGE_FROM_SYSTEM | FORMAT_MESSAGE_IGNORE_INSERTS,
            nullptr, error, MAKELANGID(LANG_NEUTRAL, SUBLANG_DEFAULT), (LPWSTR)&buffer, 0, nullptr);
        if (length)
        {
            LPCWSTR cstr = (LPCWSTR)buffer;
            std::wstring result(cstr, cstr + length);
            LocalFree(buffer);
            trim(result);
            return result;
        }
    }
    return std::wstring();
}

int wmain()
{
    wchar_t* exePath = 0;
    if (_get_wpgmptr(&exePath) != 0)
        return 1;

    std::wstring launcherDirectory = exePath;
    if (launcherDirectory.empty())
    {
        std::wcout << L"error -- failed to get exe path: " << message() << L" (" << GetLastError() << L")" << std::endl;
        return 1;
    }

    auto pos = launcherDirectory.find_last_of('\\');
    if (pos == std::wstring::npos)
    {
        std::wcout << L"error -- failed to get exe path: " << message() << L" (" << GetLastError() << L")" << std::endl;
        return 1;
    }

    launcherDirectory = launcherDirectory.substr(0, pos);

    std::wifstream infile(L"launcher.cfg");
    if (!infile)
    {
        std::wcout << L"error -- unable to find 'launcher.cfg'!" << std::endl;
        return 1;
    }

    const size_t length = 256;
    wchar_t buffer[length];
    if (!infile.getline(buffer, length))
    {
        std::wcout << L"error -- unable to read executable name from line 1 of 'launcher.cfg'!" << std::endl;
        std::wcout << L"(example exe: 'age2.exe')" << std::endl;
        return 1;
    }

    std::wstring executable = buffer;

    if (!infile.getline(buffer, length))
    {
        std::wcout << L"error -- unable to read current directory from line 2 of 'launcher.cfg'!" << std::endl;
        std::wcout << L"(example package-relative current directory: 'bin\\x64')" << std::endl;
        return 1;
    }

    std::wstring currentDirectory = buffer;

    trim(executable);
    trim(currentDirectory);

    if (executable.empty())
    {
        std::wcout << L"error -- invalid executable specified on line 1 of 'launcher.cfg'!" << std::endl;
        return 1;
    }

    if (currentDirectory.empty())
    {
        std::wcout << L"error -- invalid current directory specified on line 2 of 'launcher.cfg'!" << std::endl;
        return 1;
    }

    slash(executable);
    slash(currentDirectory);

    std::wstring currentDirectoryPath = launcherDirectory + currentDirectory;
    std::wstring executablePath = currentDirectoryPath + executable;

    STARTUPINFO si;
    PROCESS_INFORMATION pi;

    ZeroMemory(&si, sizeof(si));
    si.cb = sizeof(si);
    ZeroMemory(&pi, sizeof(pi));

    BOOL created = CreateProcessW(executablePath.c_str(), 0, 0, 0, FALSE, 0, 0, currentDirectoryPath.c_str(), &si, &pi);
    if (FALSE == created)
    {
        std::wcout << L"error -- failed to create process: " << message() << L" (" << GetLastError() << L")" << std::endl;
        std::wcout << L"path: " << executablePath << std::endl;
        return 1;
    }

    CloseHandle(pi.hProcess);
    CloseHandle(pi.hThread);

    return 0;
}