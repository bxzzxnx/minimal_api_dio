using Microsoft.EntityFrameworkCore;
using Minimal01.Domain.DTO;
using Minimal01.Domain.Entities;
using Minimal01.Domain.Interfaces;
using Minimal01.Infra.Data;

namespace Minimal01.Application.Services;

public class VehicleService : IVehicleService
{
    private readonly ApiDbContext _context;

    public VehicleService(ApiDbContext context)
    {
        _context = context;
    }

    public async Task<Vehicle> Register(VehicleDTO request)
    {
        var vehicle = new Vehicle
        {
            Model = request.Model,
            Brand = request.Brand,
            Year = request.Year
        };

        await _context.Cars.AddAsync(vehicle);
        await _context.SaveChangesAsync();
        return vehicle;
    }


    public async Task<List<Vehicle>> GetAll(int? page, string? model = null, string? brand = null)
    {
        var query = _context.Cars.AsQueryable();
        int itemsPerPage = 10;
        if (!string.IsNullOrWhiteSpace(model))
            query = query.Where(v => EF.Functions.Like(v.Model, $"%{model}%"));
        
        if (!string.IsNullOrWhiteSpace(brand))
            query = query.Where(v => EF.Functions.Like(v.Brand, $"%{brand}%"));
    
        if (page != null)
            query = query.OrderBy(c => c.Id)
                .Skip(((int)page - 1) * itemsPerPage)
                .Take(itemsPerPage);
        
        return await query.ToListAsync();
    }

    public async Task<Vehicle?> GetCarById(int id) => await
        _context.Cars.Where(c => c.Id == id).FirstOrDefaultAsync();

    public async Task Remove(Vehicle vehicle)
    {
        _context.Cars.Remove(vehicle);
        await _context.SaveChangesAsync();
    }

    public async Task Update(Vehicle vehicle)
    {
        _context.Cars.Update(vehicle);
        await _context.SaveChangesAsync();  
    }
}