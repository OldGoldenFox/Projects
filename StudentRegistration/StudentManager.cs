namespace StudentRegistration
{
    internal class StudentManager: IStudentManager
    {
        List<Student> students = new();

        public void AddStudent(string name, string surname, string group)
        {
            students.Add(new Student(name, surname, group));
        }
        
        public void FindStudent(string name)
        {
            var foundStudents = students.Where(s => s.Name == name);
            foreach (var student in foundStudents)
            {
                Console.WriteLine(student);
            }
        }

        public void ShowAllStudents()
        {
            for (int i = 0; i < students.Count; i++)
            {
                Console.WriteLine(students[i]);
            }
        }
    }
}
