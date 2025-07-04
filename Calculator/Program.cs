namespace Calculator
{
    internal class Program
    {
        public static int ReadInt(string message)
        {
            int number;
            Console.Write(message);
            while (!int.TryParse(Console.ReadLine(), out number))
            {
                Console.WriteLine("Неверный ввод, попробуйте снова");
                Console.Write(message);
            }
            return number;
        }

        public static double ReadDouble(string message)
        {
            double number;
            Console.Write(message);
            while (!double.TryParse(Console.ReadLine(), out number))
            {
                Console.WriteLine("Неверный ввод, попробуйте снова");
                Console.Write(message);
            }
            return number;
        }

        static (double, double) ReadTwoNumbers()
        {
            double firstValue = ReadDouble("Введите первое число: ");
            double secondValue = ReadDouble("Введите второе число: ");
            return (firstValue, secondValue);
        }
        static void PrintResult(string operation, double firstValue, double secondValue, double result)
        {
            Console.WriteLine($"{firstValue} {operation} {secondValue} = {result}");
        }

        static void Main(string[] args)
        {
            Menu chose;
            double result;
            double firstValue;
            double secondValue;

            while (true) {
            Console.WriteLine("\n1. Сложение");
            Console.WriteLine("2. Вычитание");
            Console.WriteLine("3. Умножение");
            Console.WriteLine("4. Деление");
            Console.WriteLine("5. Выход");

            chose = (Menu) ReadInt("Выберите операцию: ");

                Console.WriteLine();

                switch (chose)
                {
                    case Menu.Addition:
                        (firstValue, secondValue) = ReadTwoNumbers();
                        result = ArithmeticOperations.Addition(firstValue, secondValue);
                        PrintResult("+",firstValue,secondValue, result);
                        break;

                    case Menu.Subtraction:
                        (firstValue, secondValue) = ReadTwoNumbers();
                        result = ArithmeticOperations.Subtraction(firstValue, secondValue);
                        PrintResult("-", firstValue, secondValue, result);

                        break;

                    case Menu.Multiplication:
                        (firstValue, secondValue) = ReadTwoNumbers();
                        result = ArithmeticOperations.Multiplication(firstValue, secondValue);
                        PrintResult("*", firstValue, secondValue, result);
                        break;

                    case Menu.Division:
                        try
                        {
                            (firstValue, secondValue) = ReadTwoNumbers();
                            result = ArithmeticOperations.Division(firstValue, secondValue);
                            PrintResult("/", firstValue, secondValue, result);
                        }
                        catch (DivideByZeroException ex)
                        {
                            Console.WriteLine($"Ошибка: {ex.Message}");
                        }
                        break;

                    case Menu.Exit:
                        Console.WriteLine("Завершение программы");
                        return;

                    default:
                        Console.WriteLine("Неверный выбор");
                        continue;
                }
            }
        }
    }
}