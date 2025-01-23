using ApplicationCore.DTOs;
using ApplicationCore.Interfaces;
using FluentValidation;
using Infrastructure.Entities;
using Infrastructure.Interfaces;
using Mapster;

namespace ApplicationCore.Services
{
    public class FilmService(
        IRepository<Film> repository,
        IValidator<RequestFilmDto> filmValidator)
        : IFilmService
    {
        public async Task<ResponseFilmDto> CreateFilmAsync(RequestFilmDto filmDto)
        {
            await filmValidator.ValidateAndThrowAsync(filmDto);
            var newFilm = filmDto.Adapt<Film>();

            var created = await repository.CreateAsync(newFilm);
            return created.Adapt<ResponseFilmDto>();
        }

        public async Task<ResponseFilmDto> UpdateFilmAsync(Guid filmId, RequestFilmDto filmDto)
        {
            await filmValidator.ValidateAndThrowAsync(filmDto);

            var existingFilm = await repository.GetByIdAsync(filmId);

            filmDto.Adapt(existingFilm);

            var updated = await repository.UpdateAsync(existingFilm);
            return updated.Adapt<ResponseFilmDto>();
        }

        public async Task<IEnumerable<ResponseFilmDto>> GetAllFilmsAsync()
        {
            var films = await repository.GetAllAsync();
            return films.Adapt<IEnumerable<ResponseFilmDto>>();
        }

        public async Task<ResponseFilmDto> GetFilmByIdAsync(Guid filmId)
        {
            var film = await repository.GetByIdAsync(filmId);
            return film.Adapt<ResponseFilmDto>();
        }

        public async Task DeleteFilmAsync(Guid filmId)
        {
            await repository.DeleteAsync(filmId);
        }
    }
}
