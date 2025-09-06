using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MinimalApi.Domain.Entities;

public class Admin
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; init; }
    [Required, StringLength(255)]
    public string Email { get; init; } = string.Empty;
    [Required, StringLength(100)]
    public string Password { get; init; } = string.Empty;
    [Required, StringLength(10)]
    public string Profile { get; init; } = string.Empty;       
}