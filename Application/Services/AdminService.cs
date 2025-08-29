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

    public async Task<Admin?> Login(LoginDTO request)
    {
        var queue = await _context.Admins
            .Where(a => a.Email == request.Email && a.Password == request.Password)
            .FirstOrDefaultAsync();
        return queue;
    }
}