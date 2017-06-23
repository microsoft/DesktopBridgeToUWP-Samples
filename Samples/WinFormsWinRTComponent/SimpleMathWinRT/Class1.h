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
