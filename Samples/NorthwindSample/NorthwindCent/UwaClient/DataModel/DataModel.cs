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

namespace NorthwindCent.DataModel
{
    public struct Category
    {
        public int ID;
        public string Name;
        public string Description;
    }

    public struct Product
    {
        public int ID;
        public string Name;
        public string QuantityPerUnit;
        public float UnitPrice;
        public short UnitsInStock;
        public short UnitsOnOrder;
        public short ReorderLevel;
        public bool Discontinued;
        public int SupplierID;
        public int CategoryID;
    }
}
