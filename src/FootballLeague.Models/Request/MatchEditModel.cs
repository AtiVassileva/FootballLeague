using System.ComponentModel.DataAnnotations;

namespace FootballLeague.Models.Request
{
    public class MatchEditModel
    {
        public DateTime PlayedOn { get; set; }
        [Range(0, int.MaxValue)] public int HostGoals { get; set; }
        [Range(0, int.MaxValue)] public int GuestGoals { get; set; }
    }
}