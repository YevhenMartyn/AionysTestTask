using ApplicationCore.DTOs;
using Infrastructure.Entities;
using Mapster;

namespace ApplicationCore.Mappers
{
    public class MapsterConfig
    {
        public static void FilmMappings() 
        {
            TypeAdapterConfig<RequestFilmDto, Film>
                .NewConfig();
            TypeAdapterConfig<Film, ResponseFilmDto>
                .NewConfig();
        }
    }
}
