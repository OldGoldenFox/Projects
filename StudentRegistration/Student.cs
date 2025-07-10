namespace StudentRegistration
{
    internal class Student
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Group { get; set; }
        public string DateOfBirth { get; set; }

        public Student(string name, string surname, string group)
        {
            Name = name;
            Surname = surname;
            Group = group;
        }

        public override string ToString()
        {
            return $"{Group} | {Surname} {Name}";
        }
    }
}
