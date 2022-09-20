using AutoMapper;
using Mycinema.Application.Models.DTOs.Entities;
using Mycinema.Application.Models.DTOs.Entities.TmdbAPI;
using Mycinema.Domain.Entities;

namespace Mycinema.Application.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMapFromEntitiesToDto();
        CreateMapFromQueriesToEntities();
        CreateMapFromExernalApiToDto();
    }

    private void CreateMapFromEntitiesToDto() {
        CreateMap<Movie, MovieDto>();
        CreateMap<Genre, GenreDto>();
        CreateMap<Session, SessionDto>();
        CreateMap<City, CityDto>();
        CreateMap<Room, RoomDto>();
    }

    private void CreateMapFromQueriesToEntities()
    {

    }

    private void CreateMapFromExernalApiToDto()
    {
        CreateMap<TmdbMovieDto, MovieRecommendation>()
            .ForMember(dest => dest.Tittle, opt => opt.MapFrom(src => src.title))
            .ForMember(dest => dest.Overview, opt => opt.MapFrom(src => src.overview))
            .ForMember(dest => dest.Genres, opt => opt.MapFrom(src => src.genre_ids.ToList()))
            .ForMember(dest => dest.Language, opt => opt.MapFrom(src => src.original_language))
            .ForMember(dest => dest.ReleaseDate, opt => opt.MapFrom(src => src.release_date));

        CreateMap<TmdbTvShowDto, TvShowRecommendation>()
           .ForMember(dest => dest.Tittle, opt => opt.MapFrom(src => src.name))
           .ForMember(dest => dest.Overview, opt => opt.MapFrom(src => src.overview))
           .ForMember(dest => dest.Genres, opt => opt.MapFrom(src => src.genre_ids.ToList()))
           .ForMember(dest => dest.Language, opt => opt.MapFrom(src => src.original_language))
           .ForMember(dest => dest.ReleaseDate, opt => opt.MapFrom(src => src.first_air_date));
    }
}
