using System.ComponentModel.DataAnnotations;

namespace FootballLeague.Models
{
    using static Common.DataConstants;

    public class Team
    {
        [Required] public Guid Id { get; set; }
        [Required]
        [MaxLength(DefaultMaxLength)]
        public string Name { get; set; } = default!;
        [Required]
        [MaxLength(DefaultMaxLength)]
        public string Country { get; set; } = default!;
        public int Points { get; set; }
        public HashSet<Match> PlayedMatches { get; set; } = new();
    }
}