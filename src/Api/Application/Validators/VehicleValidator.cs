using MinimalApi.Domain;
using MinimalApi.Domain.DTO;

namespace MinimalApi.Application.Validators;

public  static class VehicleValidator
{
    public static List<string> Validate(VehicleDto dto)
    {
        var errors = new ErrorMessages();

        if (dto.Year < 1950)
        {
            errors.Messages.Add($"Erro na propriedade Year, Valor não pode ser nulo e , " +
                                $"O ano deve ser maior ou igual a 1950");
        }
        if (string.IsNullOrEmpty(dto.Brand))
        {
            errors.Messages.Add($"Erro na propriedade Brand, A marca não pode ser vazia");
        }

        if (string.IsNullOrEmpty(dto.Model))
        {
            errors.Messages.Add($"Erro na propriedade Model, O modelo não pode ser vazio");
        }

        return errors.Messages;
    }
}