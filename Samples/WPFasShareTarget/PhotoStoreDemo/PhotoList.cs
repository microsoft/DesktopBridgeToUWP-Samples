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
using System.Collections.Specialized;
using System.IO;

namespace PhotoStoreDemo
{
    public class PhotoList : ObservableCollection<ImageFile>
    {
        private DirectoryInfo _directory;

        public void Init(string path)
        {
            _directory = new DirectoryInfo(path);
            Update();
        }
       
        private void Update()
        {
            foreach (var f in _directory.GetFiles("*"))
            {
                Add(new ImageFile(f.FullName));
            }
        }
    }
}