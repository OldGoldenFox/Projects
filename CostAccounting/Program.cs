using CostAccounting.Helpers;
using CostAccounting.Interfaces;
using CostAccounting.Services;

namespace CostAccounting
{
    internal class Program
    {
        static void Main(string[] args)
        {
            IExpenseManager expenses = new ExpenseManager();

            string path = InputHelper.ReadString("Введите имя (к нему прикреплена история расходов): ") + ".json";

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
            ExpenseApp app = new ExpenseApp(expenses, path);
            app.Run();
        }
    }
}