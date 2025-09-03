using Microsoft.AspNetCore.Mvc;
using Minimal01.Application.Validators;
using Minimal01.Domain.DTO;
using Minimal01.Domain.Interfaces;

namespace Minimal01.Extensions;

public static class Endpoints
{
    public static void AddEndpoints(this WebApplication app)
    {
        var api = app.MapGroup("/api");
        var vehicleRoutes = api.MapGroup("/vehicle").WithTags("Vehicles").RequireAuthorization();
        var adminRoutes = api.MapGroup("/admin").WithTags("Admin");
        
        api.MapGet("/", () => new { documentacao = "/swagger" }).WithTags("Home");
        
        
        adminRoutes.MapGet("/admins", async (IAdminService service) =>
        {
            var response = await service.ShowAll();
            return Results.Ok(response);
        }).RequireAuthorization();
        
        adminRoutes.MapGet("/{id:int}", async (int id, IAdminService service) =>
        {
            var response = await service.GetById(id);
            return response == null ? Results.NotFound() : Results.Ok(response);
        }).RequireAuthorization();
        
        adminRoutes.MapPost("/register", async ([FromBody] AdminDto request, IAdminService service) =>
        {
            var errors = AdminValidator.Validate(request);
            if (errors.Count > 0) return Results.BadRequest(errors);
            var response = await service.Register(request);
            return Results.Created(string.Empty, response);
        });
        
        adminRoutes.MapPost("/login", async ([FromBody] LoginDto request, IAdminService service) =>
        {
            var response = await service.Login(request);
            if (response != null) return Results.Ok(response);
            return Results.Unauthorized();
        });
        
        vehicleRoutes.MapGet("/", async (int? page, string? model, string? brand, IVehicleService service) =>
        {
            var response = await service.GetAll(page, model, brand);
            return Results.Ok(response);
        });

        vehicleRoutes.MapPost("/register", async ([FromBody] VehicleDto request, IVehicleService service) =>
        {
            var errors  = VehicleValidator.Validate(request);
            if (errors.Count > 0) return Results.BadRequest(errors);
            var vehicle = await service.Register(request);
            return Results.Created(string.Empty, vehicle);
        });

        vehicleRoutes.MapGet("/{id:int}", async (int id, IVehicleService service) =>
        {
            var response = await service.GetCarById(id);
            return response == null ? Results.NotFound() : Results.Ok(response);
        });


        vehicleRoutes.MapPut("/{id:int}", async (int id , VehicleDto request, IVehicleService service) =>
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
        
        vehicleRoutes.MapDelete("/{id:int}", async (int id, IVehicleService service) =>
        {
            var vehicle = await service.GetCarById(id);
            if (vehicle == null) return Results.NotFound();

            await service.Remove(vehicle);
            return Results.NoContent();
        });
    }
}