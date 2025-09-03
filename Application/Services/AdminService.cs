using Microsoft.EntityFrameworkCore;
using Minimal01.Domain.DTO;
using Minimal01.Domain.Entities;
using Minimal01.Domain.Interfaces;
using Minimal01.Infra.Data;

namespace Minimal01.Application.Services;

public class AdminService : IAdminService
{
    private readonly ApiDbContext _context;

    public AdminService(ApiDbContext context)
    {
        _context = context;
    }

    public async Task<Admin?> Login(LoginDto request)
    {
        var admin = await _context.Admins
            .Where(a => a.Email == request.Email)
            .FirstOrDefaultAsync();
        if (admin is not null && BCrypt.Net.BCrypt.Verify(request.Password, admin.Password))
        {
            return admin;
        }
        return null;
    }

    public async Task<Admin> Register(AdminDto request)
    {
        
        var admin = new Admin
        {
            Email = request.Email,
            Password = BCrypt.Net.BCrypt.HashPassword(request.Password),
            Profile = request.Profile.ToString()!
        };
        await _context.Admins.AddAsync(admin);
        await _context.SaveChangesAsync();
        return admin;
    }

    public async Task<List<AdminResponseDto>> ShowAll()
    {
        var queue = await _context.Admins.Select(a => new AdminResponseDto
        {
            Id = a.Id,
            Email = a.Email,
            Profile = a.Profile,
        }).ToListAsync();
        return queue;
    }
    public async Task<Admin?> GetById(int id) => 
        await _context.Admins.Where(a => a.Id == id).FirstOrDefaultAsync();

}