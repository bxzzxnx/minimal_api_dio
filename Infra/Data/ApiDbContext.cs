using Microsoft.EntityFrameworkCore;
using Minimal01.Domain.Entities;

namespace Minimal01.Infra.Data;

public class ApiDbContext : DbContext
{

    public DbSet<Admin> Admins { get; set; }
    public DbSet<Vehicle> Cars { get; set; }

    public ApiDbContext(DbContextOptions options) : base(options)
    {

    }
}