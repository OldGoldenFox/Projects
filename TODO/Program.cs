namespace TODO
{
    internal class Program
    {
        static int ChooseTask(string message, int count)
        {
            int number = ReadInt(message);
            while (!(number > 0 && number <= count))
            {
                Console.WriteLine("Выберите существующую задачу");
                number = ReadInt(message);
            }
            return number;
        }

        static int ReadInt (string message)
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

        static string ReadString(string message)
        {
            string result;
            Console.Write(message);
            while (String.IsNullOrWhiteSpace(result = Console.ReadLine().Trim()))
            {
                Console.WriteLine("Неверный ввод, попробуйте снова");
                Console.Write(message);
            }
            return result;
        }

        static void Main(string[] args)
        {
            ToDoList tList = new();
            string taskDescription;
            int chose;
            int taskIndex;

            if (File.Exists("tasks.json"))
            {
                tList.ReadToJsonFile("tasks.json");
            }

            while (true)
            {

                Console.WriteLine("\n1) Добавить задачу");
                Console.WriteLine("2) Редактировать задачу");
                Console.WriteLine("3) Удалить задачу");
                Console.WriteLine("4) Сменить статус задачи");
                Console.WriteLine("5) Просмотреть все задачи");
                Console.WriteLine("6) Выход");

                chose = ReadInt("Выберите действие: ");

                Console.WriteLine();

                if ((chose >= 2 && chose <= 5) && tList.Count == 0)
                {
                    Console.WriteLine("Список задач пуст");
                    continue;
                }

                switch (chose)
                {
                    case 1:
                        //Добавление задачи
                        taskDescription = ReadString("Введите описание задачи: ");
                        tList.AddTask(taskDescription);
                        Console.WriteLine("Задача добавлена!");
                        break;

                    case 2:
                        //Редактирование задачи
                        tList.ShowTasks();
                        taskIndex = ChooseTask("Выберите номер задачи для редактирования: ", tList.Count);
                        taskDescription = ReadString("Новое описание задачи: ");
                        tList.EditTask(taskDescription, taskIndex);
                        Console.WriteLine("Задача обновлена!");
                        break;

                    case 3:
                        //Удаление задачи
                        tList.ShowTasks();
                        taskIndex = ChooseTask("Выберите номер задачи для удаления: ", tList.Count);
                        tList.DeleteTask(taskIndex);
                        Console.WriteLine("Задача удалена!");
                        break;
                        
                    case 4:
                        //Обновление статуса задачи
                        tList.ShowTasks();
                        taskIndex = ChooseTask("Выберите номер задачи для изменения статуса: ", tList.Count);
                        tList.ToggleStatus(taskIndex);
                        Console.WriteLine("Статус задачи обновлен!");
                        break;

                    case 5:
                        tList.ShowTasks();
                        break;

                    case 6:
                        tList.SaveToJsonFile("tasks.json");
                        //tList.SaveToTextFile("tasks.txt");
                        return;

                    default:
                        Console.WriteLine("Неверный выбор");
                        continue;
                }
            }
        }
    }
}
