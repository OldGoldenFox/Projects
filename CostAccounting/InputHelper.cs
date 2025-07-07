using CostAccounting.Models;

namespace CostAccounting
{
    public static class InputHelper
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


        public static string ReadString(string message)
        {
            string result;
            Console.Write(message);
            while (string.IsNullOrWhiteSpace(result = Console.ReadLine().Trim()))
            {
                Console.WriteLine("Неверный ввод, попробуйте снова");
                Console.Write(message);
            }
            return result;
        }

        public static int Choose(string message, int countEnd, int countStart = 0)
        {
            int number;
            do
            {
                number = ReadInt(message);
            }
            while (!(number > countStart && number <= countEnd));
            return number;
        }
        public static ExpenseCategory ChooseCategory(string message)
        {
            ExpenseCategory category;
            ShowAllCategories();
            category = (ExpenseCategory)Choose(message, Enum.GetValues(typeof(ExpenseCategory)).Length);
            return category;
        }
        
        public static DateTime ChooseYearAndMonth()
        {

            var year = Choose($"Введите год (2001-{DateTime.Now.Year}): ", countEnd: DateTime.Now.Year, countStart: 2000);
            var month = Choose("Выберите месяц (1-12): ", 12);
            return new DateTime(year, month, 1);
        }
        public static DateTime ChooseYearAndMonthAndDay()
        {
            var date = ChooseYearAndMonth();
            var day = Choose($"Выберите день в диапазоне 1-{DateTime.DaysInMonth(date.Year, date.Month)}: ", DateTime.DaysInMonth(date.Year, date.Month));
            return new DateTime(date.Year, date.Month, day);
        }

        public static void ShowAllCategories()
        {
            int i = 1;
            foreach (var category in (ExpenseCategory[])Enum.GetValues(typeof(ExpenseCategory)))
            {
                Console.WriteLine($"{i}) {category}");
                i++;
            }
        }

        public static (ExpenseCategory, string, decimal, DateTime) ReadExpense()
        {
            ExpenseCategory category = ChooseCategory("Выберите категорию: ");
            string description = ReadString($"Описание расхода ({category.ToString().ToLower()}): ");
            decimal amount = ReadInt("Введите цену: ");
            DateTime date = DateTime.Now;
            return (category, description, amount, date);
        }
    }
}
