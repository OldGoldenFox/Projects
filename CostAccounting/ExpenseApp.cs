using CostAccounting.Helpers;
using CostAccounting.Interfaces;
using CostAccounting.Models;

namespace CostAccounting
{
    internal class ExpenseApp
    {
        private readonly IExpenseManager expenses;
        private readonly string path;

        public ExpenseApp(IExpenseManager expenses, string path)
        {
            this.expenses = expenses;
            this.path = path;
        }

        public void Run()
        {
            while (true)
            {
                ShowMainMenu();
                OperationType choise = GetOperationChoice();

                if (NeedsDataButEmpty(choise))
                {
                    Console.WriteLine("Список расходов пуст");
                    continue;
                }

                Console.WriteLine();

                switch (choise)
                {
                    case OperationType.AddExpense:
                        AddExpense();
                        break;

                    case OperationType.ShowExpenses:
                        ShowAllExpenses();
                        break;

                    case OperationType.FilterExpenses:
                        FilterExpenses();
                        break;

                    case OperationType.CalculateTheAmountOfExpenses:
                        CalculateTotal();
                        break;

                    case OperationType.SaveAndExit:
                        SaveAndExit();
                        return;

                    default:
                        Console.WriteLine("Неизвестная ошибка");
                        break;
                }
            }
        }
        private OperationType GetOperationChoice()
        {
            return (OperationType)InputHelper.Choose("Выберите операцию: ", 5);
        }
        private bool NeedsDataButEmpty(OperationType op)
        {
            return ((int)op >= 2 && (int)op <= 4) && expenses.Count == 0;
        }
        private void ShowMainMenu()
        {
            Console.WriteLine("\n1) Добавить расход");
            Console.WriteLine("2) Посмотреть все расходы");
            Console.WriteLine("3) Фильтровать расходы");
            Console.WriteLine("4) Посчитать сумму расходов");
            Console.WriteLine("5) Сохранить и выйти");
        }
        private void AddExpense()
        {
            var (category, description, amount, date) = InputHelper.ReadExpense();
            expenses.AddExpense(category, description, amount, date);
        }
        private void ShowAllExpenses()
        {
            expenses.ShowExpenses();
        }
        private void FilterExpenses()
        {
            Console.WriteLine("1) Фильтровать по категории");
            Console.WriteLine("2) Фильтровать по дате (диапозон)");
            Console.WriteLine("3) Фильровать по месяцу");

            FilterOperation filterChose = (FilterOperation)InputHelper.Choose("Выберите операцию: ", 3);

            Console.WriteLine();

            switch (filterChose)
            {
                case FilterOperation.FilterByCategory:
                    var category = InputHelper.ChooseCategory("Выберите категорию: ");
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
        }
        private void CalculateTotal()
        {
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
                    var category = InputHelper.ChooseCategory("Выберите категорию: ");
                    Console.WriteLine();
                    expenses.CalculateTheAmountOfExpenses(category);
                    break;

                case AmountOperation.TotalByYearAndMonth:
                    DateTime yearAndMonth = InputHelper.ChooseYearAndMonth();
                    expenses.CalculateTheAmountOfExpenses(yearAndMonth);
                    break;
            }
        }
        private void SaveAndExit()
        {
            expenses.SaveToJson(path);
        }
    }
}
