namespace PhoneBook.Models
{
    internal class Contact
    {
        public string Name { get; set; }
        public string Number { get; set; }

        public override string ToString()
        {
            return $"{Number} | {Name}";
        }
    }
}
