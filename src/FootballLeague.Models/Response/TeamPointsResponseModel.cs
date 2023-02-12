namespace FootballLeague.Models.Response
{
    public class TeamPointsResponseModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = default!;
        public int Points { get; set; }
    }
}