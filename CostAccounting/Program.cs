using CostAccounting.Interfaces;
using CostAccounting.Models;
using CostAccounting.Services;

namespace CostAccounting
{
    internal class Program
    {
        static void Main(string[] args)
        {
            IExpenseManager expenses = new ExpenseManager();

            OperationType chose;
            ExpenseCategory category;
            string description;
            decimal amount;
            DateTime date;
            string path;

            path = ReadString("Введите имя (к нему прикреплена история расходов): ") + ".json";

            if (File.Exists(path))
            {
                try
                {
                    expenses.LoadToJson(path);
                }
                catch 
                {
                    Console.WriteLine("Файл поврежден");
                }
            }

            while (true)
            {
                Console.WriteLine("\n1) Добавить расход");
                Console.WriteLine("2) Посмотреть все расходы");
                Console.WriteLine("3) Фильтровать расходы");
                Console.WriteLine("4) Посчитать сумму расходов");
                Console.WriteLine("5) Сохранить и выйти");

                chose = (OperationType) Choose("Выберите операцию: ", 5);

                Console.WriteLine();

                if (((int)chose >= 2 && (int)chose <= 4) && expenses.Count == 0)
                {
                    Console.WriteLine("Список расходов пуст");
                    continue;
                }

                switch (chose)
                {
                    case OperationType.AddExpense:
                        (category, description, amount, date) = ReadExpense();
                        expenses.AddExpense(category, description, amount, date);
                        break;

                    case OperationType.ShowExpenses:
                        expenses.ShowExpenses();
                        break;

                    case OperationType.FilterExpenses:
                        Console.WriteLine("1) Фильтровать по категории");
                        Console.WriteLine("2) Фильтровать по дате (диапозон)");
                        Console.WriteLine("3) Фильровать по месяцу");

                        FilterOperation filterChose = (FilterOperation)Choose("Выберите операцию: ", 3);

                        Console.WriteLine();

                        switch (filterChose)
                        {
                            case FilterOperation.FilterByCategory:
                                category = ChooseCategory("Выберите категорию: ");
                                Console.WriteLine();
                                expenses.ShowExpenses(category);
                                break;

                            case FilterOperation.FilterByRange:
                                DateTime startDate = ChooseYearAndMonthAndDay();
                                DateTime endDate;
                                do
                                {
                                    Console.WriteLine("Вторая дата должна быть позже 1й");
                                    endDate = ChooseYearAndMonthAndDay();
                                }
                                while (endDate <= startDate);

                                Console.WriteLine();
                                expenses.ShowExpenses(startDate, endDate);
                                break;

                            case FilterOperation.FilterByMonth:
                                DateTime yearAndMonth = ChooseYearAndMonth();
                                Console.WriteLine();
                                expenses.ShowExpenses(yearAndMonth);
                                break;
                        }
                        break;

                    case OperationType.CalculateTheAmountOfExpenses:

                        Console.WriteLine("1) Сумма всех расходов");
                        Console.WriteLine("2) Сумма по категории");
                        Console.WriteLine("3) Сумма по месяцу");

                        AmountOperation amountChose = (AmountOperation) Choose("Выберите операцию: ", 3);

                        Console.WriteLine();

                        switch (amountChose)
                        {
                            case AmountOperation.TotalExpenses:
                                expenses.CalculateTheAmountOfExpenses();
                                break;

                            case AmountOperation.TotalByCategory:
                                category = ChooseCategory("Выберите категорию: ");
                                Console.WriteLine();
                                expenses.CalculateTheAmountOfExpenses(category);
                                break;

                            case AmountOperation.TotalByYearAndMonth:                                
                                DateTime yearAndMonth = ChooseYearAndMonth();
                                expenses.CalculateTheAmountOfExpenses(yearAndMonth);
                                break;
                        }
                        break;
                    
                    case OperationType.SaveAndExit:
                        expenses.SaveToJson(path);
                        return;

                    default:
                        Console.WriteLine("Неизвестная ошибка");
                        break;
                }
            }
        }

        //Вспомогательные методы
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


        static string ReadString(string message)
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

       static int Choose(string message, int countEnd, int countStart = 0)
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
            var month = Choose("Выберите месяц (1-12): ",12);
            DateTime date = new DateTime(year, month, 1);
            return date;
        }
        public static DateTime ChooseYearAndMonthAndDay()
        {
            var year = Choose($"Введите год (2001-{DateTime.Now.Year}): ", countEnd: DateTime.Now.Year, countStart: 2000);
            var month = Choose("Выберите месяц (1-12): ", 12);
            var day = Choose($"Выберите день в диапазоне 1-{DateTime.DaysInMonth(year, month)}: ", DateTime.DaysInMonth(year, month));
            DateTime date = new DateTime(year, month, day);
            return date;
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