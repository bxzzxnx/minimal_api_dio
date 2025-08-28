using Microsoft.EntityFrameworkCore;
using Minimal01.Domain.DTO;
using Minimal01.Domain.Entities;
using Minimal01.Infra.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ApiDbContext>(o =>
{
    o.UseMySql(builder.Configuration.GetConnectionString("db"),
    ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("db")));
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();



app.MapGet("/admins", (ApiDbContext db) =>
{
    var admins = db.Admins.ToList();
    return Results.Ok(admins);
});


app.Run();

