namespace FootballLeague.Models.Response
{
    public class MatchResponseModel
    {
        public Guid Id { get; set; }
        public DateTime PlayedOn { get; set; }
        public int HostGoals { get; set; }
        public int GuestGoals { get; set; }
        public string HostName { get; set; } = default!;
        public string GuestName { get; set; } = default!;
    }
}