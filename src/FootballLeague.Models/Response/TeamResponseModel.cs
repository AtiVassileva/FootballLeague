namespace FootballLeague.Models.Response
{
    public class TeamResponseModel : TeamPointsResponseModel
    {
        public string Country { get; set; } = default!;
    }
}