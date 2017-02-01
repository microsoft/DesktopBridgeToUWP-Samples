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

// // Copyright (c) Microsoft. All rights reserved.
// // Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Collections.ObjectModel;

namespace PhotoStoreDemo
{
    public class PrintTypeList : ObservableCollection<PrintType>
    {
        public PrintTypeList()
        {
            Add(new PrintType("4x6 Print", 0.15));
            Add(new PrintType("Greeting Card", 1.49));
            Add(new PrintType("T-Shirt", 14.99));
        }
    }
}