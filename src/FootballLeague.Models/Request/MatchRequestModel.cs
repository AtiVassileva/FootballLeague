using System.ComponentModel.DataAnnotations;

namespace FootballLeague.Models.Request
{
    public class MatchRequestModel
    {
        public DateTime PlayedOn { get; set; }
        [Range(0, int.MaxValue)] public int HostGoals { get; set; }
        [Range(0, int.MaxValue)] public int GuestGoals { get; set; }
        [Required] public Guid HostId { get; set; }
        [Required] public Guid GuestId { get; set; }
    }
}