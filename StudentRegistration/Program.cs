using System.Xml.Linq;

namespace StudentRegistration
{
    internal class Program
    {
        static void Main(string[] args)
        {
            IStudentManager studentManager = new StudentManager();
            int choose;
            string name;


            while (true)
            {
                choose = InputHelper.ReadInt("Выберите действие: ");
                switch (choose)
                {
                    case 1:
                        string surname = InputHelper.ReadString("Фамилия: ");
                        name = InputHelper.ReadString("Имя: ");
                        string group = InputHelper.ReadString("Группа: ");
                        studentManager.AddStudent(name, surname, group);
                        break;

                    case 2:
                        name = InputHelper.ReadString("Имя: ");
                        studentManager.FindStudent(name);
                        break;
                    
                    case 3:
                        studentManager.ShowAllStudents();
                        break;

                        
                }
            }
        }
    }
}
