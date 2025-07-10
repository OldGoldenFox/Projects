namespace StudentRegistration
{
    internal interface IStudentManager
    {
        void AddStudent(string name, string surname, string group);
        void FindStudent(string name);
        void ShowAllStudents();
    }
}
