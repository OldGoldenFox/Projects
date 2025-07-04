using PhoneBook.Models;
using PhoneBook.Services;
using PhoneBook.Interfaces;

namespace PhoneBook
{
    internal class Program
    {
        static void Main(string[] args)
        {
            IContactsService pBook = new Contacts();
            OperationType chose;
            string name;
            string path;

            Console.Write("Введите название телефонной книги: ");
            path = Console.ReadLine() + ".json";

            if (File.Exists(path))
            {
                pBook.LoadToJson(path);
            }

            while (true)
            {
                Console.WriteLine("\n1) Добавить контакт");
                Console.WriteLine("2) Найти контакт");
                Console.WriteLine("3) Удалить контакт");
                Console.WriteLine("4) Показать все контакты");
                Console.WriteLine("5) Выход");

                chose = (OperationType)ReadInt("Выберите операцию: ");

                if (((int) chose >= 2 && (int) chose <= 4) && pBook.Count == 0)
                {
                    Console.WriteLine("Список контактов пуст");
                    continue;
                }

                Console.WriteLine();

                switch (chose)
                {
                    case OperationType.AddContact:
                        name = ReadName("Введите имя (длина имении более 1 символа): ");
                        string number = ReadNumber("Введите номер (в формате 8**********): ");
                        pBook.AddContact(name, number);
                        Console.WriteLine("Контакт добавлен!");
                        break;

                    case OperationType.SearchByName:
                        name = ReadName("Введите имя (или часть имени): ");
                        pBook.SearchByName(name);
                        break;

                    case OperationType.DeleteContact:
                        pBook.ShowAllContacts();
                        int contactIndex = ChooseContact("Выберите контакт для удаления: ", pBook.Count);
                        pBook.DeleteContact(contactIndex);
                        Console.WriteLine("Контакт удален!");
                        break;

                    case OperationType.ShowAllContacts:
                        Console.WriteLine("Список контактов:");
                        pBook.ShowAllContacts();
                        break;

                    case OperationType.Exit:
                        pBook.SaveToJson(path);
                        return;

                    default:
                        Console.WriteLine("Выберите в диапазоне от 1 до 5");
                        break;
                }
            }
        }

        //Валидация
        static string ReadName(string message)
        {
            string result;
            Console.Write(message);
            while (string.IsNullOrWhiteSpace(result = Console.ReadLine().Trim()) || result.Length <= 1)
            {
                Console.WriteLine("Неверный ввод, попробуйте снова");
                Console.Write(message);
            }
            return result;
        }

        static string ReadNumber(string message)
        {
            string result;
            Console.Write(message);
            while (string.IsNullOrWhiteSpace(result = Console.ReadLine().Trim()) || result.Length != 11 || result[0] != '8')
            {
                Console.WriteLine("Неверный ввод, попробуйте снова");
                Console.Write(message);
            }
            return result;
        }

        //Вспомогательная логика
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

        public static int ChooseContact(string message, int count)
        {
            int number = ReadInt(message);
            while (!(number > 0 && number <= count))
            {
                Console.WriteLine("Выберите существующий контакт");
                number = ReadInt(message);
            }
            return number;
        }
    }

}
