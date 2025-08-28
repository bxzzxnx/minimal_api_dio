using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Minimal01.Domain.Entities;

public class Admin
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    [Required, StringLength(255)]
    public string Email { get; set; } = string.Empty;
    [Required, StringLength(50)]
    public string Password { get; set; } = string.Empty;
    [Required, StringLength(10)]
    public string Profile { get; set; } = string.Empty;       
}