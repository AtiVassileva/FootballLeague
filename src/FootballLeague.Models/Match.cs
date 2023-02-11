using System.ComponentModel.DataAnnotations;

namespace FootballLeague.Models
{
    public class Match
    {
        [Required] public Guid Id { get; set; }
        public DateTime PlayedOn { get; set; }
        public int HostGoals { get; set; }
        public int GuestGoals { get; set; }
        [Required] public Guid HostId { get; set; }
        public Team Host { get; set; } = default!;
        [Required] public Guid GuestId { get; set; }
        public Team Guest { get; set; } = default!;
    }
}