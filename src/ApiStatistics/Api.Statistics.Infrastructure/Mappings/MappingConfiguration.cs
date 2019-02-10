using Api.Statistics.Domain.DTOs;
using Api.Statistics.Domain.Entity;
using AutoMapper;

namespace Api.Statistics.Infrastructure.Mappings
{
    public class MappingConfiguration : Profile
    {
        public MappingConfiguration()
        {
            CreateMap<UserGamesEntity, PlayerGamesDto>()
                .ForMember(d => d.GameId, opt => opt.MapFrom(s => s.GameId))
                .ForMember(d => d.StartTime, opt => opt.MapFrom(s => s.StartTime))
                .ForMember(d => d.EndTime, opt => opt.MapFrom(s => s.EndTime))
                .ForMember(d => d.MurdererNickname,
                    opt =>
                    {
                        opt.MapFrom(src => string.IsNullOrEmpty(src.KilledBy) ? "NPC" : src.KilledBy);
                    })
                .ForMember(d => d.GameDuration, opt => opt.MapFrom(s => s.EndTime - s.StartTime));
        }
    }
}
