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
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleMathManagedWinRT
{
    public sealed class SimpleMath
    {

        public double add(double firstNumber, double secondNumber)
        {
            return firstNumber + secondNumber;
        }

        public double subtract(double firstNumber, double secondNumber)
        {
            return firstNumber - secondNumber;
        }
        public double multiply(double firstNumber, double secondNumber)
        {
            return firstNumber * secondNumber;
        }

        public double divide(double firstNumber, double secondNumber)
        {
            if (0 == secondNumber)
                return -1;

            return firstNumber / secondNumber;
        }

    }
}

