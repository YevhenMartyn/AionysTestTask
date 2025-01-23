using ApplicationCore.DTOs;
using FluentValidation;

namespace ApplicationCore.Validators
{
    public class FilmValidator : AbstractValidator<RequestFilmDto>
    {
        public FilmValidator() {
            RuleFor(film => film.Title)
                .NotEmpty()
                .WithMessage("Title is required.")
                .MaximumLength(50)
                .WithMessage("Title must not exceed 100 characters.");

            RuleFor(film => film.Genre)
                .NotEmpty()
                .WithMessage("Genre is required.")
                .MaximumLength(50)
                .WithMessage("Genre must not exceed 50 characters.");

            RuleFor(film => film.Director)
                .NotEmpty()
                .WithMessage("Director is required.")
                .MaximumLength(50)
                .WithMessage("Director must not exceed 50 characters.");

            RuleFor(film => film.Description)
                .NotEmpty()
                .WithMessage("Description is required.")
                .MaximumLength(500)
                .WithMessage("Description must not exceed 500 characters.");

            RuleFor(film => film.ReleaseYear)
                .NotEmpty()
                .WithMessage("Release year is required.")
                .InclusiveBetween(1888, DateTime.Now.Year)
                .WithMessage("Release year must be between 1900 and 2100.");

            RuleFor(film => film.Rating)
                .NotEmpty()
                .WithMessage("Rating is required.")
                .InclusiveBetween(1, 10)
                .WithMessage("Rating must be between 1 and 10.");
        } 
    }
}
