using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Minimal01.Domain.Entities;

public class Vehicle
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    [Required, StringLength(150)]
    public string Model { get; set; } = string.Empty;
    [Required, StringLength(100)]
    public string Brand { get; set; } = string.Empty;
    [Required]
    public int Year { get; set; }
}