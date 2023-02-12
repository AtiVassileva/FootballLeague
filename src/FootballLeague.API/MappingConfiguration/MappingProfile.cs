using AutoMapper;
using FootballLeague.Models;
using FootballLeague.Models.Request;

namespace FootballLeague.API.MappingConfiguration
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            this.CreateMap<Team, TeamRequestModel>()
                .ReverseMap();
        }
    }
}
