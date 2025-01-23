using ApplicationCore.DTOs;
using ApplicationCore.Interfaces;
using Infrastructure.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    /// <summary>
    /// Controller for managing films.
    /// </summary>
    [Route("api/films")]
    [ApiController]
    public class FilmController(IFilmService filmService) : ControllerBase
    {
        /// <summary>
        /// Creates a new film.
        /// </summary>
        /// <param name="requestFilmDto">The film data to create.</param>
        /// <returns>The created film with its ID.</returns>
        /// <response code="201">Returns the newly created film.</response>
        /// <response code="400">If the request data is invalid.</response>
        [HttpPost]
        public async Task<IActionResult> CreateFilm([FromBody] RequestFilmDto requestFilmDto)
        {
            var created = await filmService.CreateFilmAsync(requestFilmDto);
            return CreatedAtRoute(
                "GetFilmById",
                routeValues: new { filmId = created.Id },
                value: created);
        }

        /// <summary>
        /// Updates an existing film.
        /// </summary>
        /// <param name="filmId">The ID of the film to update.</param>
        /// <param name="requestFilmDto">The updated film data.</param>
        /// <returns>The updated film.</returns>
        /// <response code="200">Returns the updated film.</response>
        /// <response code="404">If the film is not found.</response>
        [HttpPut("{filmId}")]
        public async Task<IActionResult> UpdateFilm(Guid filmId, [FromBody] RequestFilmDto requestFilmDto)
        {
            var updated = await filmService.UpdateFilmAsync(filmId, requestFilmDto);
            return Ok(updated);
        }

        /// <summary>
        /// Retrieves all films.
        /// </summary>
        /// <returns>A list of films.</returns>
        /// <response code="200">Returns a list of films.</response>
        [HttpGet]
        public async Task<IActionResult> GetAllFilms()
        {
            var films = await filmService.GetAllFilmsAsync();
            return Ok(films);
        }

        /// <summary>
        /// Retrieves a film by its ID.
        /// </summary>
        /// <param name="filmId">The ID of the film to retrieve.</param>
        /// <returns>The requested film.</returns>
        /// <response code="200">Returns the requested film.</response>
        /// <response code="404">If the film is not found.</response>
        [HttpGet("{filmId}", Name = "GetFilmById")]
        public async Task<IActionResult> GetFilmById(Guid filmId)
        {
            var film = await filmService.GetFilmByIdAsync(filmId);
            return Ok(film);
        }

        /// <summary>
        /// Deletes a film by its ID.
        /// </summary>
        /// <param name="filmId">The ID of the film to delete.</param>
        /// <returns>A response indicating the result.</returns>
        /// <response code="200">If the film was successfully deleted.</response>
        /// <response code="404">If the film is not found.</response>
        [HttpDelete("{filmId}")]
        public async Task<IActionResult> DeleteCar(Guid filmId)
        {
            await filmService.DeleteFilmAsync(filmId);
            return Ok();
        }
    }
}
