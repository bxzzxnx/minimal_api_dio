using System.ComponentModel.DataAnnotations;
using Minimal01.Domain.Enums;

namespace Minimal01.Domain.DTO;

public class AdminDto
{
    [EmailAddress]
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public Profile? Profile { get; set; }
}