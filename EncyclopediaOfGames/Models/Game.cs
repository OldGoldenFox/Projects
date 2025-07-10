namespace EncyclopediaOfGames.Models
{
    internal class Game
    {
        public string Title { get; set; }
        public string Gengre { get; set; }
        //public float Rating { get; set; }
        //public string Platform { get; set; }
        //public DateOnly ReleaseDate { get; set; }

        public override string ToString()
        {
            return $"{Title} | {Gengre}";
        }
    }
}
