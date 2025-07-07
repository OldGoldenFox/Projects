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

            path = InputHelper.ReadString("Введите имя (к нему прикреплена история расходов): ") + ".json";

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

                chose = (OperationType)InputHelper.Choose("Выберите операцию: ", 5);

                Console.WriteLine();

                if (((int)chose >= 2 && (int)chose <= 4) && expenses.Count == 0)
                {
                    Console.WriteLine("Список расходов пуст");
                    continue;
                }

                switch (chose)
                {
                    case OperationType.AddExpense:
                        (category, description, amount, date) = InputHelper.ReadExpense();
                        expenses.AddExpense(category, description, amount, date);
                        break;

                    case OperationType.ShowExpenses:
                        expenses.ShowExpenses();
                        break;

                    case OperationType.FilterExpenses:
                        Console.WriteLine("1) Фильтровать по категории");
                        Console.WriteLine("2) Фильтровать по дате (диапозон)");
                        Console.WriteLine("3) Фильровать по месяцу");

                        FilterOperation filterChose = (FilterOperation)InputHelper.Choose("Выберите операцию: ", 3);

                        Console.WriteLine();

                        switch (filterChose)
                        {
                            case FilterOperation.FilterByCategory:
                                category = InputHelper.ChooseCategory("Выберите категорию: ");
                                Console.WriteLine();
                                expenses.ShowExpenses(category);
                                break;

                            case FilterOperation.FilterByRange:
                                DateTime startDate = InputHelper.ChooseYearAndMonthAndDay();
                                DateTime endDate;
                                do
                                {
                                    Console.WriteLine("Вторая дата должна быть позже 1й");
                                    endDate = InputHelper.ChooseYearAndMonthAndDay();
                                }
                                while (endDate <= startDate);

                                Console.WriteLine();
                                expenses.ShowExpenses(startDate, endDate);
                                break;

                            case FilterOperation.FilterByMonth:
                                DateTime yearAndMonth = InputHelper.ChooseYearAndMonth();
                                Console.WriteLine();
                                expenses.ShowExpenses(yearAndMonth);
                                break;
                        }
                        break;

                    case OperationType.CalculateTheAmountOfExpenses:

                        Console.WriteLine("1) Сумма всех расходов");
                        Console.WriteLine("2) Сумма по категории");
                        Console.WriteLine("3) Сумма по месяцу");

                        AmountOperation amountChose = (AmountOperation)InputHelper.Choose("Выберите операцию: ", 3);

                        Console.WriteLine();

                        switch (amountChose)
                        {
                            case AmountOperation.TotalExpenses:
                                expenses.CalculateTheAmountOfExpenses();
                                break;

                            case AmountOperation.TotalByCategory:
                                category = InputHelper.ChooseCategory("Выберите категорию: ");
                                Console.WriteLine();
                                expenses.CalculateTheAmountOfExpenses(category);
                                break;

                            case AmountOperation.TotalByYearAndMonth:                                
                                DateTime yearAndMonth = InputHelper.ChooseYearAndMonth();
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
    }
}