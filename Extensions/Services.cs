using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Minimal01.Application.Services;
using Minimal01.Domain.Interfaces;
using Minimal01.Infra.Data;
using Minimal01.Infra.Security;

namespace Minimal01.Extensions;

public static class Services
{
    public static void AddServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddOpenApi();
        services.AddDbContext<ApiDbContext>(o =>
        {
            o.UseMySql(configuration.GetConnectionString("db"),
            ServerVersion.AutoDetect(configuration.GetConnectionString("db")));
        });

        services.AddScoped<IAdminService, AdminService>();
        services.AddScoped<IVehicleService, VehicleService>();
        services.AddScoped<ITokenGenerator, TokenGenerator>();
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options => 
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateAudience = false,
                    ValidateIssuer = false,
                    ValidateLifetime = true,
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(configuration.GetSection("Jwt")["Key"]!))
                };
        });
        services.AddSwaggerGen(options =>
        {
            options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Name = "Authorization",
                Type = SecuritySchemeType.Http,
                Scheme = "Bearer",
                In = ParameterLocation.Header,
            });
            
            options.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        },
                        Scheme = "oauth2",
                        Name = "Bearer",
                        In = ParameterLocation.Header
                    }, []
                }
            });
        });
        services.AddAuthorization();
    }
}