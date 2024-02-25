namespace DataTransfer.Models
{
    public class Country
    {
        public string CountryId { get; set; }
        public string Name { get; set; }
        public Game Game { get; set; }
        public Sport Sport { get; set; }
        public string CountryFlag { get; set; }
    }
}
