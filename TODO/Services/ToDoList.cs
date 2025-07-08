using System.Text.Json;
using ToDo.Models;

namespace ToDo.Services
{
    internal class ToDoList
    {
        private List<TodoTask> tasks = new();
        public int Count => tasks.Count;

        public void AddTask(string description, bool status = false)
        {
            tasks.Add(new TodoTask { Status = status, Description = description});
        }

        public void EditTask(string newText, int taskIndex)
        {
            tasks[taskIndex - 1].Description = newText;
        }

        public void DeleteTask(int taskIndex)
        {
            tasks.RemoveAt(taskIndex - 1);
        }

        public void ToggleStatus(int taskIndex)
        {
            tasks[taskIndex - 1].Status = !tasks[taskIndex - 1].Status;
        }

        public void ShowTasks()
        {
            for (int i = 0; i < tasks.Count; i++)
            {
                string checkbox = tasks[i].Status ? "[*]" : "[ ]";
                Console.WriteLine($"{i + 1}) {checkbox} {tasks[i].Description}");
            }
        }

        //Сохранение в txt файл
        public void SaveToTextFile(string fileName)
        {
            using (StreamWriter writer = new StreamWriter(fileName))
            {
                for (int i = 0; i < tasks.Count; i++)
                {
                    string checkbox = tasks[i].Status ? "1" : "0";
                    writer.WriteLine($"{checkbox}|{tasks[i].Description}");
                }
            }
        }

        //Загрузка из txt файла
        public void ReadToTextFile(string fileName)
        {
            using (StreamReader reader = new StreamReader(fileName))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    var parts = line.Split("|");
                    bool status = parts[0] == "1";
                    string description = parts[1];
                    DateTime deadLine = DateTime.Parse(parts[2]);
                    AddTask(description, status);
                }
            }
        }

        //Сохранение в json файл
        public void SaveToJsonFile(string fileName)
        {
            //Сохранение без отступов (компактное)
            //string json = JsonSerializer.Serialize(tasks);

            //Сохранение с отступами (красивое)
            string json = JsonSerializer.Serialize(tasks, new JsonSerializerOptions { WriteIndented = true });

            File.WriteAllText(fileName, json);
        }

        //Загрузка из json файла
        public void ReadToJsonFile(string fileName)
        {
            string json = File.ReadAllText(fileName);
            tasks = JsonSerializer.Deserialize<List<TodoTask>>(json) ?? new(); //new на случай если результатом будет null
        }

    }
}
