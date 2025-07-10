using EncyclopediaOfGames.Helpers;
using EncyclopediaOfGames.Interfaces;
using EncyclopediaOfGames.Services;

namespace EncyclopediaOfGames
{
    internal class Program
    {
        static void Main(string[] args)
        {
            IGameLibrary library = new GameLibrary();
            int choose;

            while (true)
            {
                Console.WriteLine("\n1) Добавить");
                Console.WriteLine("2) Удалить");
                Console.WriteLine("3) Показать все");
                choose = InputHelper.ReadInt("Выберите действие: ");

                Console.WriteLine();

                switch (choose)
                {
                    case 1:
                        string title = InputHelper.ReadString("Введите название: ");
                        string genre = InputHelper.ReadString("Введите жанр: ");
                        library.AddGame(title, genre);
                        break;

                    case 2:
                        library.ShowAllGames();
                        int gameIndex = InputHelper.ReadInt("Выберите игру для удаления: ");
                        library.RemoveGame(gameIndex);
                        break;

                    case 3:
                        library.ShowAllGames();
                        break;
                }
            }
            
        }
    }
}
