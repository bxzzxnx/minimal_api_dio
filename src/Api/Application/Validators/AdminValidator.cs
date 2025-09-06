using MinimalApi.Domain;
using MinimalApi.Domain.DTO;
using MinimalApi.Domain.Enums;

namespace MinimalApi.Application.Validators;

public static class AdminValidator
{
    public static List<string> Validate(AdminDto dto)
    {
        var errors = new ErrorMessages();

        if (string.IsNullOrWhiteSpace(dto.Email))
        {
            errors.Messages.Add("A propriedade Email não pode ser vazia");
        }

        if (string.IsNullOrWhiteSpace(dto.Password))
        {
            errors.Messages.Add("A propriedade Password não pode ser vazia");
        }
        
        if (dto.Profile is null || !Enum.IsDefined(typeof(Profile), dto.Profile))
        {
            errors.Messages.Add("A propriedade Profile está incorreta, não pode ser vazia e os valores são 0 (Admin) ou 1 (Editor)");
        }
        return errors.Messages;
    }
}