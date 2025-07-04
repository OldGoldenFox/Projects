namespace CostAccounting.Models
{
    internal class Expense
    {
        public ExpenseCategory Category {  get; set; }
        public string Description { get; set; }

        public decimal Amount { get; set; }

        public DateTime Date { get; set; }

        public override string ToString()
        {
            return $"{Date:g} | {Category} | {Description} | {Amount}тг";
        }
    }
}
