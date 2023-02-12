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
            CreateMap<Team, TeamRequestModel>()
            .ReverseMap();
            CreateMap<Team, TeamResponseModel>()
            .ReverseMap();
            CreateMap<Team, TeamPointsResponseModel>()
            .ReverseMap();
            CreateMap<Match, MatchRequestModel>()
            .ReverseMap();
            CreateMap<Match, MatchResponseModel>()
                .ForMember(x => x.HostName, cfg => cfg.MapFrom(x => x.Host.Name))
                .ForMember(x => x.GuestName, cfg => cfg.MapFrom(x => x.Guest.Name))
                .ReverseMap();
        }
    }
}