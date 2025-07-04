namespace PhoneBook.Interfaces
{
    internal interface IContactsService
    {
        void AddContact(string name, string number);
        void DeleteContact(int index);
        void SearchByName(string name);
        void ShowAllContacts();
        void SaveToJson(string path);
        void LoadToJson(string path);
        int Count { get; }
    }
}
