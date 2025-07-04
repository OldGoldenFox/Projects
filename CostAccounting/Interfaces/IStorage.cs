using CostAccounting.Models;

namespace CostAccounting.Interfaces
{
    internal interface IStorage
    {
        void SaveToJson(string fileName);
        void LoadToJson(string fileName);
    }
}
