using CostAccounting.Models;

namespace CostAccounting.Interfaces
{
    internal interface IExpenseManager: IStorage
    {
        int Count { get; }
        void AddExpense(ExpenseCategory category, string title, decimal amount, DateTime time);
        void ShowExpenses();
        void ShowExpenses(ExpenseCategory category);
        void ShowExpenses(DateTime startDate, DateTime endDate);
        void ShowExpenses(DateTime yearAndMonth);
        void CalculateTheAmountOfExpenses();
        void CalculateTheAmountOfExpenses(ExpenseCategory category);
        void CalculateTheAmountOfExpenses(DateTime date);
        void SaveToJson(string fileName);
        void LoadToJson(string fileName);
    }
}
