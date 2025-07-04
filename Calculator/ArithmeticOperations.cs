namespace Calculator
{
    static internal class ArithmeticOperations
    {
        static public double Addition(double firstValue, double secondValue)
        {
            return firstValue + secondValue;
        }

        static public double Subtraction(double firstValue, double secondValue)
        {
            return firstValue - secondValue;
        }

        static public double Multiplication(double firstValue, double secondValue)
        {
            return firstValue * secondValue;
        }

        static public double Division(double firstValue, double secondValue)
        {
            if (secondValue == 0)
                throw new DivideByZeroException("Делить на 0 нельзя");
            return firstValue / secondValue;
        }
    }
}
