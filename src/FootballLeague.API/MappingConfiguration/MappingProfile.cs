using AutoMapper;
using FootballLeague.Models;
using FootballLeague.Models.Request;
using FootballLeague.Models.Response;

namespace FootballLeague.API.MappingConfiguration
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            this.CreateMap<Team, TeamRequestModel>()
                .ReverseMap();
            this.CreateMap<Team, TeamResponseModel>()
                .ReverseMap();
            this.CreateMap<Team, TeamPointsResponseModel>()
                .ReverseMap();
            this.CreateMap<Match, MatchRequestModel>()
                .ReverseMap();
            this.CreateMap<Match, MatchResponseModel>()
                .ReverseMap();
        }
    }
}
