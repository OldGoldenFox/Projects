using CostAccounting.Models;
using CostAccounting.Interfaces;
using System.Text.Json;
using System.Xml.Linq;

namespace CostAccounting.Services
{
    internal class ExpenseManager : IExpenseManager
    {
        private List<Expense> expenses = new();
        public int Count => expenses.Count;
        public void AddExpense(ExpenseCategory category, string description, decimal amount, DateTime date)
        {
            description = description.Substring(0, 1).ToUpper() + description.Substring(1);
            expenses.Add(new Expense {Category = category, Description = description, Amount = amount, Date = date });
        }
        public void ShowExpenses()
        {
            Console.WriteLine("Все расходы:");
            foreach (var expense in expenses)
            {
                Console.WriteLine(expense.ToString());
            }
        }
        public void ShowExpenses(ExpenseCategory category)
        {
            var filteredExpenses = from expense in expenses
                                   where expense.Category == category
                                   select expense;
            Console.WriteLine($"Все расходы в категории {category.ToString().ToLower()}");
            foreach (var expense in filteredExpenses)
            {
                Console.WriteLine(expense.ToString());
            }
        }
        public void ShowExpenses(DateTime startDate, DateTime endDate)
        {
            var filteredExpenses = from expense in expenses
                                   where expense.Date >= startDate && expense.Date <= endDate.AddDays(1)
                                   select expense;
            Console.WriteLine($"Все расходы за период с {startDate:d} по {endDate:d}");
            foreach (var expense in filteredExpenses)
            {
                Console.WriteLine(expense.ToString());
            }
        }
        public void ShowExpenses(DateTime yearAndMonth)
        {
            var filteredExpenses = from expense in expenses
                                   where expense.Date.Month == yearAndMonth.Month
                                   select expense;
            Console.WriteLine($"Все расходы за {yearAndMonth:Y}");
            foreach (var expense in filteredExpenses)
            {
                Console.WriteLine(expense.ToString());
            }
        }
        public void CalculateTheAmountOfExpenses()
        {
            var sum = expenses.Sum(expense => expense.Amount);
            Console.WriteLine($"Общая сумма всех расходов: {sum}тг");
        }
        public void CalculateTheAmountOfExpenses(ExpenseCategory category)
        {
            var filteredExpenses = from expense in expenses
                                   where expense.Category == category
                                   select expense;

            var sum = filteredExpenses.Sum(expense => expense.Amount);
            Console.WriteLine($"Общая сумма расходов на {category.ToString().ToLower()}: {sum}тг");
        }
        public void CalculateTheAmountOfExpenses(DateTime date)
        {
            var filteredExpenses = from expense in expenses
                                   where expense.Date.Year == date.Year
                                   where expense.Date.Month == date.Month
                                   select expense;

            var sum = filteredExpenses.Sum(expense => expense.Amount);
            Console.WriteLine($"За {date:Y} общая сумма расходов: {sum}тг");
        }
        public void SaveToJson(string fileName)
        {
            //Для оформления с отступами и нормальными символами (русскими в нашем случае вместо юникода)
            var options = new JsonSerializerOptions
            {
                WriteIndented = true,
                Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping
            };

            string json = JsonSerializer.Serialize(expenses, options);
            //string json = JsonSerializer.Serialize(contacts); Самое обычное и практичное сохранение
            File.WriteAllText(fileName, json);
        }
        public void LoadToJson(string fileName)
        {
            string json = File.ReadAllText(fileName);
            expenses = JsonSerializer.Deserialize<List<Expense>>(json) ?? new(); //new на случай если результатом левой части будет null (поврежденный файл или т.п.)
        }
    }
}
