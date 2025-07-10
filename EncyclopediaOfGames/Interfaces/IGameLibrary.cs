namespace EncyclopediaOfGames.Interfaces
{
    internal interface IGameLibrary
    {
        public void AddGame(string title, string genre);
        public void RemoveGame(int gameIndex);
        public void ShowAllGames();
    }
}
