using ApplicationCore.DTOs;
using ApplicationCore.Interfaces;
using Infrastructure.Entities;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    [Route("api/films")]
    [ApiController]
    public class FilmController(IFilmService filmService) : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> CreateFilm(RequestFilmDto requestFilmDto)
        {
            var created = await filmService.CreateFilmAsync(requestFilmDto);
            return CreatedAtRoute(
                "GetFilmById",
                routeValues: new {filmId = created.Id, },
                value: created);
        }

        [HttpPut("{filmId}")]
        public async Task<IActionResult> UpdateFilm(Guid filmId, RequestFilmDto requestFilmDto)
        {
            var updated = await filmService.UpdateFilmAsync(filmId, requestFilmDto);
            return Ok(updated);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllFilms()
        {
            var films = await filmService.GetAllFilmsAsync();
            return Ok(films);
        }

        [HttpGet("{filmId}", Name = "GetFilmById")]
        public async Task<IActionResult> GetFilmById(Guid filmId)
        {
            var film = await filmService.GetFilmByIdAsync(filmId);
            return Ok(film);
        }

        [HttpDelete("{filmId}")]
        public async Task<IActionResult> DeleteCar(Guid filmId)
        {
            await filmService.DeleteFilmAsync(filmId);
            return Ok();
        }
    }
}
