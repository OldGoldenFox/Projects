using PhoneBook.Models;
using PhoneBook.Interfaces;
using System.Text.Json;

namespace PhoneBook.Services
{
    internal class Contacts : IContactsService
    {
        private List<Contact> contacts = new();
        public int Count => contacts.Count;

        //методы
        public void AddContact(string name, string number)
        {
            name = name.Substring(0, 1).ToUpper() + name.Substring(1);
            name = name.Replace(" ", "-");
            contacts.Add(new Contact { Name = name, Number = number});
        }

        public void SearchByName(string name)
        {
            bool found = false;
            for (int i = 0; i < Count; i++)
            {
                if (contacts[i].Name.ToLower().Contains(name.Trim().ToLower()))
                {
                    found = true;
                    Console.WriteLine(contacts[i].ToString());
                }
            }
            if (!found)
            {
                Console.WriteLine("Контакты не найдены");
            }
        }

        public void DeleteContact(int contactIndex)
        {
            contacts.RemoveAt(contactIndex-1);
        }

        public void ShowAllContacts()
        {
            for (int i = 0; i < Count; i++)
            {
                Console.WriteLine($"{i+1}) {contacts[i].ToString()}");
            }
        }

        public void SaveToJson(string fileName)
        {
            //Для оформления с отступами и нормальными символами (русскими в нашем случае вместо юникода)
            var options = new JsonSerializerOptions
            {
                WriteIndented = true,
                Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping
            };

            string json = JsonSerializer.Serialize(contacts, options);
            //string json = JsonSerializer.Serialize(contacts); Самое обычное и практичное сохранение
            File.WriteAllText(fileName, json);
        }

        public void LoadToJson(string fileName)
        {
            string json = File.ReadAllText(fileName);
            contacts = JsonSerializer.Deserialize<List<Contact>>(json) ?? new(); //new на случай если результатом левой части будет null (поврежденный файл или т.п.)
        }

    }
}
