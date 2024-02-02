using FluentValidation;

namespace DISERTATIE.FLACARA.CAPTURII.VALIDATORS;

public static class Validate
{
    public static async Task FluentValidate<T>(IValidator<T> validator, T entity)
    {
        var result = await validator.ValidateAsync(entity);

        if (result.Errors.Count > 0)
        {
            throw new ValidationException(result.Errors);
        }
    }

}
