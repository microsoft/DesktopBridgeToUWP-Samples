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
//
//Building off the sample MSDN walkthru - https://msdn.microsoft.com/en-us/library/jj127117.aspx
//
#include "pch.h"
#include "Class1.h"

using namespace SimpleMathWinRT;
using namespace Platform;

SimpleMath::SimpleMath()
{
}
double SimpleMath::add(double firstNumber, double secondNumber)
{
	return firstNumber + secondNumber;
}

double SimpleMath::subtract(double firstNumber, double secondNumber)
{
	return firstNumber - secondNumber;
}
double SimpleMath::multiply(double firstNumber, double secondNumber)
{
	return firstNumber * secondNumber;
}

double SimpleMath::divide(double firstNumber, double secondNumber)
{
	if (0 == secondNumber)
		return -1;

	return firstNumber / secondNumber;
}

