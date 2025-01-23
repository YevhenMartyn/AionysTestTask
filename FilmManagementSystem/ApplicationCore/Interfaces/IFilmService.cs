
using ApplicationCore.DTOs;

namespace ApplicationCore.Interfaces
{
    public interface IFilmService
    {
        Task<ResponseFilmDto> CreateFilmAsync(RequestFilmDto filmDto);
        Task<ResponseFilmDto> UpdateFilmAsync(Guid filmId, RequestFilmDto filmDto);
        Task<IEnumerable<ResponseFilmDto>> GetAllFilmsAsync();
        Task<ResponseFilmDto> GetFilmByIdAsync(Guid filmId);
        Task DeleteFilmAsync(Guid filmId);
    }
}
