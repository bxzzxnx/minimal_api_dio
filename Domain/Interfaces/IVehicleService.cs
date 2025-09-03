using Minimal01.Domain.DTO;
using Minimal01.Domain.Entities;

namespace Minimal01.Domain.Interfaces;

public interface IVehicleService
{
    Task<List<Vehicle>> GetAll(int? page, string? model = null, string? brand = null);
    Task<Vehicle?> GetCarById(int id);
    Task<Vehicle> Register(VehicleDto request);
    Task Remove(Vehicle vehicle);
    Task Update(Vehicle vehicle);
}