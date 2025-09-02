using System.ComponentModel.DataAnnotations;

namespace Minimal01.Domain.DTO;

public record VehicleDTO(string Model, string Brand, int Year);