namespace FootballLeague.Models.Response
{
    public class TeamResponseModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = default!;
        public string Country { get; set; } = default!;
        public int Points { get; set; }
    }
}