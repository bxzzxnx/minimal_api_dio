using System.ComponentModel.DataAnnotations;
using MinimalApi.Domain.Enums;

namespace MinimalApi.Domain.DTO;

public class AdminDto
{
    [EmailAddress]
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public Profile? Profile { get; set; }
}