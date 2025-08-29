using Microsoft.AspNetCore.Mvc;
using Minimal01.Domain.DTO;
using Minimal01.Domain.Interfaces;

namespace Minimal01.Extensions;

public static class Endpoints
{
    public static void AddEndpoints(this WebApplication app)
    {
        var api = app.MapGroup("/api/cars");

        api.MapPost("/login", async ([FromBody] LoginDTO request, IAdminService service) =>
        {
            var response = await service.Login(request);
            if (response != null)
            {
                return Results.Ok(response);
            }
            return Results.Unauthorized();
        });
    }
}