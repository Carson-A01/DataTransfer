namespace DataTransfer.Models
{
    public class CountryViewModel
    {
        public List<Country> Country { get; set; }
        public string activeGame { get; set; } = "all";
        public string activeSport { get; set; } = "all";
        private List<Game> games;
        public List<Game> Games { get => games; set { games = value; games.Insert(0, new Game { GameId = "all", Name = "All" }); } }
        private List<Sport> sports;
        public List<Sport> Sports { get => sports; set { sports = value; sports.Insert(0, new Sport { SportId = "all", Name = "All", Category = "All" }); } }

        public string CheckActiveGame(string g) => g.ToLower() == activeGame.ToLower() ? "active" : "";
        public string CheckActiveSport(string s) => s.ToLower() == activeSport.ToLower() ? "active" : "";
    }
}
