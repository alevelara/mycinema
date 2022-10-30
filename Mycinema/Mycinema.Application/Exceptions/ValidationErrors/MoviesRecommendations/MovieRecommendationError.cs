using FluentValidation.Results;

namespace Mycinema.Application.Exceptions.ValidationErrors.MoviesRecommendations;

public static class MovieRecommendationError
{
    public static ValidationFailure INVALID_PARAMETER = new ValidationFailure("error.invalid.parameter", "Missing parameter");
}
