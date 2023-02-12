using System.ComponentModel.DataAnnotations;

namespace FootballLeague.Models.Request
{
    using static Common.DataConstants;
    public class TeamRequestModel
    {
        [Required]
        [StringLength(DefaultMaxLength, MinimumLength = DefaultMinLength)]
        public string Name { get; set; } = default!;
        [Required]
        [StringLength(DefaultMaxLength, MinimumLength = DefaultMinLength)]
        public string Country { get; set; } = default!;
        [Range(0, int.MaxValue)] public int Points { get; set; }
    }
}