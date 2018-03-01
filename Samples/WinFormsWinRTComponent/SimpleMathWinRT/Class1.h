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
#pragma once

namespace SimpleMathWinRT
{
    public ref class SimpleMath sealed
    {
    public:
        SimpleMath();
		double add(double firstNumber, double secondNumber);
		double subtract(double firstNumber, double secondNumber);
		double multiply(double firstNumber, double secondNumber);
		double divide(double firstNumber, double secondNumber);
    };
}

