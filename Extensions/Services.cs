using Microsoft.EntityFrameworkCore;
using Minimal01.Application.Services;
using Minimal01.Domain.Interfaces;
using Minimal01.Infra.Data;

namespace Minimal01.Extensions;

public static class Services
{
    public static void AddServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddOpenApi();
        services.AddSwaggerGen();
        services.AddDbContext<ApiDbContext>(o =>
        {
            o.UseMySql(configuration.GetConnectionString("db"),
            ServerVersion.AutoDetect(configuration.GetConnectionString("db")));
        });

        services.AddScoped<IAdminService, AdminService>();
    }
}