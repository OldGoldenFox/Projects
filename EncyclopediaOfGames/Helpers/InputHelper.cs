namespace EncyclopediaOfGames.Helpers
{
    internal class InputHelper
    {
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

        public static string ReadString(string message)
        {
            string result;
            Console.Write(message);
            while (string.IsNullOrWhiteSpace(result = Console.ReadLine().Trim()))
            {
                Console.WriteLine("Неверный ввод, попробуйте снова");
                Console.Write(message);
            }
            return result;
        }
    }
}
