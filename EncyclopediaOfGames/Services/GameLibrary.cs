using EncyclopediaOfGames.Interfaces;
using EncyclopediaOfGames.Models;

namespace EncyclopediaOfGames.Services
{
    internal class GameLibrary : IGameLibrary
    {
        private List<Game> games = new();
        public void AddGame(string title, string genre)
        {
            games.Add(new Game { Title = title, Gengre = genre });
        }

        public void RemoveGame(int gameIndex)
        {
            games.RemoveAt(gameIndex-1);
        }

        public void ShowAllGames()
        {
            for (int i = 0; i < games.Count; i++)
            {
                Console.WriteLine($"{i+1}. {games[i]}");
            }
        }
    }
}
