using Microsoft.EntityFrameworkCore;
using MinimalApi.Domain.Entities;

namespace MinimalApi.Infra.Data;

public class ApiDbContext : DbContext
{

    public DbSet<Admin> Admins { get; set; }
    public DbSet<Vehicle> Cars { get; set; }

    public ApiDbContext(DbContextOptions options) : base(options)
    {

    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Admin>().HasData(
            new Admin
            {
                Id = 1, 
                Email = "admin@admin.com",
                Password = "$2a$11$LlNMMUOk9kQ7yrGlOLawherD/qmS.g/4hwAvUUT.7fYx7x979aqWi", // admin
                Profile = "Admin"
            }
        );
    }
    
    
}
