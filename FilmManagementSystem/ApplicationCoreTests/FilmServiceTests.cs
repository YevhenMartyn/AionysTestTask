using ApplicationCore.DTOs;
using ApplicationCore.Services;
using FluentAssertions;
using FluentValidation;
using Infrastructure.Entities;
using Infrastructure.Interfaces;
using Moq;
using Xunit;

namespace ApplicationCoreTests.FilmServiceTests
{
    public class FilmServiceTests
    {
        private readonly Mock<IRepository<Film>> _filmRepositoryMock;
        private readonly Mock<IValidator<RequestFilmDto>> _filmValidatorMock;
        private readonly FilmService _filmService;

        public FilmServiceTests()
        {
            _filmRepositoryMock = new Mock<IRepository<Film>>();
            _filmValidatorMock = new Mock<IValidator<RequestFilmDto>>();
            _filmService = new FilmService(_filmRepositoryMock.Object, _filmValidatorMock.Object);
        }

        [Fact]
        public async Task CreateFilmAsync_ShouldCreateFilm_WhenValidRequest()
        {
            var requestDto = new RequestFilmDto
            {
                Title = "1",
                Description = "1",
                Director = "1",
                Genre = "1",
                ReleaseYear = 2001,
                Rating = 1.0
            };

            var film = new Film
            {
                Id = Guid.NewGuid(),
                Title = requestDto.Title,
                Description = requestDto.Description,
                Director = requestDto.Director,
                Genre = requestDto.Genre,
                ReleaseYear = requestDto.ReleaseYear,
                Rating = requestDto.Rating
            };
            _filmValidatorMock.Setup(v => v.ValidateAsync(requestDto, default)).ReturnsAsync(new FluentValidation.Results.ValidationResult());
            _filmRepositoryMock.Setup(repo => repo.CreateAsync(It.IsAny<Film>())).ReturnsAsync(film);

            var result = await _filmService.CreateFilmAsync(requestDto);

            result.Title.Should().Be(requestDto.Title);
            _filmRepositoryMock.Verify(repo => repo.CreateAsync(It.IsAny<Film>()), Times.Once);
        }

        [Fact]
        public async Task GetFilmByIdAsync_ShouldReturnFilm_WhenFilmExists()
        {
            var filmId = Guid.NewGuid();
            var film = new Film
            {
                Id = filmId,
                Title = "1",
                Description = "1",
                Director = "1",
                Genre = "1",
                ReleaseYear = 2001,
                Rating = 1.0
            };
            _filmRepositoryMock.Setup(repo => repo.GetByIdAsync(filmId)).ReturnsAsync(film);

            var result = await _filmService.GetFilmByIdAsync(filmId);

            result.Id.Should().Be(filmId);
            result.Title.Should().Be("1");
        }

        [Fact]
        public async Task GetFilmByIdAsync_ShouldReturnNull_WhenFilmDoesNotExist()
        {
            _filmRepositoryMock.Setup(repo => repo.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync((Film?)null);

            var result = await _filmService.GetFilmByIdAsync(Guid.NewGuid());

            result.Should().BeNull();
        }


        [Fact]
        public async Task GetAllFilmsAsync_ShouldReturnListOfFilms()
        {
            var films = new List<Film>
            {
                new Film
                {
                    Id = Guid.NewGuid(),
                    Title = "1",
                    Description = "1",
                    Director = "1",
                    Genre = "1",
                    ReleaseYear = 2001,
                    Rating = 1.0
                },
                new Film
                {
                    Id = Guid.NewGuid(),
                    Title = "2",
                    Description = "2",
                    Director = "2",
                    Genre = "2",
                    ReleaseYear = 2002,
                    Rating = 2.0
                }
            };
            _filmRepositoryMock.Setup(repo => repo.GetAllAsync()).ReturnsAsync(films);

            var result = await _filmService.GetAllFilmsAsync();

            result.Should().HaveCount(2);
            result.Should().Contain(f => f.Title == "1");
        }

        [Fact]
        public async Task GetAllFilmsAsync_ShouldReturnEmptyList_WhenNoFilmsExist()
        {
            _filmRepositoryMock.Setup(repo => repo.GetAllAsync()).ReturnsAsync(new List<Film>());

            var result = await _filmService.GetAllFilmsAsync();

            result.Should().BeEmpty();
        }

        [Fact]
        public async Task DeleteFilmAsync_ShouldCallRepositoryDeleteMethod()
        {
            var filmId = Guid.NewGuid();

            await _filmService.DeleteFilmAsync(filmId);

            _filmRepositoryMock.Verify(repo => repo.DeleteAsync(filmId), Times.Once);
        }

        [Fact]
        public async Task UpdateFilmAsync_ShouldUpdateFilm_WhenValidRequest()
        {
            var filmId = Guid.NewGuid();
            var existingFilm = new Film
            {
                Id = filmId,
                Title = "Old Title",
                Description = "Old Description",
                Director = "Old Director",
                Genre = "Old Genre",
                ReleaseYear = 2000,
                Rating = 5.0
            };
            var requestDto = new RequestFilmDto
            {
                Title = "New Title",
                Description = "New Description",
                Director = "New Director",
                Genre = "New Genre",
                ReleaseYear = 2021,
                Rating = 9.0
            };

            _filmRepositoryMock.Setup(repo => repo.GetByIdAsync(filmId)).ReturnsAsync(existingFilm);
            _filmValidatorMock.Setup(v => v.ValidateAsync(requestDto, default)).ReturnsAsync(new FluentValidation.Results.ValidationResult());
            _filmRepositoryMock.Setup(repo => repo.UpdateAsync(existingFilm)).ReturnsAsync(existingFilm);

            var result = await _filmService.UpdateFilmAsync(filmId, requestDto);

            result.Title.Should().Be("New Title");
        }
    }
}