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

using System;
using System.IO;
using System.Windows.Media.Imaging;

namespace PhotoStoreDemo
{
    public class ImageFile
    {
        public ImageFile(string path)
        {
            Path = path;
            Uri = new Uri(Path);
            Image = BitmapFrame.Create(Uri);
        }

        public string Path { get; }
        public Uri Uri { get; }
        public BitmapFrame Image { get; }

        public override string ToString() => Path;

        public void AddToCache()
        { 
            var fi = new FileInfo(Path);
            var cachedFile = new FileInfo(System.IO.Path.Combine(PhotosFolder.Current, fi.Name));
            if (!cachedFile.Exists)
            {
                File.Copy(fi.FullName, cachedFile.FullName);
            }
            
            

        }
    }
}