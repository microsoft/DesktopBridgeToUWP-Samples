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


using System;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.ExceptionServices;
using System.Windows;

namespace ClassicWin32Host
{
    public class Library
    {

        public class Functions
        {
            private bool _functionsLoaded = false;
            public bool FunctionsLoaded
            {
                get
                {
                    return _functionsLoaded;
                }
                set
                {
                    _functionsLoaded = value;
                }
            }

            
            public dEditText _EditText = null;
            [HandleProcessCorruptedStateExceptionsAttribute()]
            public string EditText(string s)
            {
                if (_EditText != null)
                {
                    try
                    {
                        IntPtr inPtr = Marshal.StringToHGlobalUni(s);
                        IntPtr outPtr = _EditText(inPtr);
                        if (outPtr == IntPtr.Zero)
                        {
                            return s;
                        }
                        return Marshal.PtrToStringUni(outPtr);
                    }
                    catch (Exception)
                    {
                        // Strange behavior appending, I suspect GC is wiping the data while it is being consumed.
                        //MessageBox.Show("Caught corruption exception!");
                        return s;
                    }
                }
                return s;
            }

            public dIncrement _Increment = null;
            public int Increment(int num)
            {
                if (_Increment != null)
                {
                    return _Increment(num);
                }
                return 0;
            }

            public void Unload()
            {
                _Increment = null;
                _EditText = null;
                _functionsLoaded = false;
            }

            public Functions()
            {

            }
        }

        private IntPtr _handle = IntPtr.Zero;
        private Functions _func = null;

        public Functions Func
        {
            get
            {
                return _func;
            }
        }

        public Library()
        {
            _func = new Functions();
            _handle = IntPtr.Zero;
        }

        #region Library Loading
        public void Load(string fullPathFilename)
        {
            // dll already loaded
            if (_handle != IntPtr.Zero)
            {
                // free it first
                FreeLibrary(_handle);
            }
            try
            {
                _handle = LoadLibrary(fullPathFilename);

                // display info for failure
                if (_handle == IntPtr.Zero)
                {
                    System.IO.FileInfo fi = new System.IO.FileInfo(fullPathFilename);
                    int err = Marshal.GetLastWin32Error();
                    string msg = "Cannot load DLL:                                         "
                        + "\n  Input: " + fullPathFilename
                        + "\n  ERROR: " + err.ToString()
                        + "\n  File Exists: " + fi.Exists.ToString()
                        ;
                    if (fi.Exists)
                    {
                        msg = msg
                        + "\n  Full Path: " + fi.FullName.ToString()
                        + "\n  Attributes: " + fi.Attributes.ToString()
                        ;
                    }
                    System.Windows.MessageBox.Show(msg);
                }

                // file loaded, load the functions
                GetFunctions();
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show("Exception thrown while loading code {" + ex.InnerException + "}");
            }
        }

        public void Free()
        {
            FreeLibrary(_handle);
            _handle = IntPtr.Zero;
            _func.Unload();
        }

        private void GetFunctions()
        {
            if (_handle == IntPtr.Zero || _func.FunctionsLoaded)
            {
                return;
            }

            IntPtr funcptr = IntPtr.Zero;

            // increment function
            funcptr = GetProcAddress(_handle, "Increment");
            if (funcptr != IntPtr.Zero)
            {
                _func._Increment  = (dIncrement)Marshal.GetDelegateForFunctionPointer(funcptr, typeof(dIncrement));
            }
            else
            {
                string msg = "Failed getting function ptr for Increment";
                System.Windows.MessageBox.Show(msg);
                return;
            }

            // chuck norris function
            funcptr = GetProcAddress(_handle, "EditText");
            if (funcptr != IntPtr.Zero)
            {
                _func._EditText = (dEditText)Marshal.GetDelegateForFunctionPointer(funcptr, typeof(dEditText));
            }
            else
            {
                string msg = "Failed getting function ptr for EditText";
                System.Windows.MessageBox.Show(msg);
                return;
            }

            _func.FunctionsLoaded = true;
        }
        #endregion

        #region Native Functions
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate int dIncrement(int numberToIncrement);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate IntPtr dEditText(IntPtr s);
        #endregion

        #region Interop

        [DllImport("kernel32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
        public static extern IntPtr LoadLibrary(string lpFileName);

        [DllImport("kernel32.dll", CharSet = CharSet.Ansi, ExactSpelling = true, SetLastError = true)]
        public static extern IntPtr GetProcAddress(IntPtr hModule, string procName);

        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern bool FreeLibrary(IntPtr hModule);

        #endregion

    }

}

