namespace FootballLeague.API.Common
{
    public class ExceptionMessages
    {
        public const string NoTeamsAvailableExceptionMessage = "No teams available!";
        public const string NonExistingTeamExceptionMessage = "Team does not exist!";

        public const string NoMatchesAvailableExceptionMessage = "No football games available!";
        public const string NonExistingMatchExceptionMessage = "Match does not exist!";

        public const string ServerErrorExceptionMessage = "A server error occured while processing your request: {0}";

        public const string NegativePointsExceptionMessage = "Points cannot be a negative number!";
    }
}