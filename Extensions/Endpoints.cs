using Microsoft.AspNetCore.Mvc;
using Minimal01.Application;
using Minimal01.Domain.DTO;
using Minimal01.Domain.Interfaces;

namespace Minimal01.Extensions;

public static class Endpoints
{
    public static void AddEndpoints(this WebApplication app)
    {
        var api = app.MapGroup("/api");
        var vehicleRoutes = api.MapGroup("/vehicle").WithTags("Vehicles");

        api.MapGet("/home", () => new { documentacao = "/swagger" }).WithTags("Home");

        api.MapPost("/login", async ([FromBody] LoginDTO request, IAdminService service) =>
        {
            var response = await service.Login(request);
            if (response != null)
            {
                return Results.Ok(response);
            }
            return Results.Unauthorized();
        }).WithTags("Admin");

        vehicleRoutes.MapGet("/", async (int? page, string? model, string? brand, IVehicleService service) =>
        {
            var response = await service.GetAll(page, model, brand);
            return Results.Ok(response);
        });

        vehicleRoutes.MapPost("/register", async ([FromBody] VehicleDTO request, IVehicleService service) =>
        {
            var errors  = VehicleValidator.Validate(request);
            if (errors.Count > 0) return Results.BadRequest(errors);
            var vehicle = await service.Register(request);
            return Results.Created(string.Empty, vehicle);
        });

        vehicleRoutes.MapGet("/{id}", async (int id, IVehicleService service) =>
        {
            var response = await service.GetCarById(id);
            if (response == null)
            {
                return Results.NotFound();
            }
            return Results.Ok(response);
        });


        vehicleRoutes.MapPut("/{id}", async (int id , VehicleDTO request, IVehicleService service) =>
        {
            var vehicle = await service.GetCarById(id);
            if (vehicle == null) return Results.NotFound();

            var errors  = VehicleValidator.Validate(request);
            if (errors.Count > 0) return Results.BadRequest(errors);

            vehicle.Model = request.Model;
            vehicle.Brand = request.Brand;
            vehicle.Year = request.Year;

            await service.Update(vehicle);

            return Results.Ok(vehicle);
        });
        

        vehicleRoutes.MapDelete("/{id}", async (int id, IVehicleService service) =>
        {
            var vehicle = await service.GetCarById(id);
            if (vehicle == null) return Results.NotFound();

            await service.Remove(vehicle);
            return Results.NoContent();
        });
    }
}