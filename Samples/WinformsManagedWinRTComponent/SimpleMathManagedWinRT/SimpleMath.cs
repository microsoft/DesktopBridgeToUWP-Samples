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
