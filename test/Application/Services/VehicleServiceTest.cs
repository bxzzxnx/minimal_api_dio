using Microsoft.EntityFrameworkCore;
using MinimalApi.Application.Services;
using MinimalApi.Domain.DTO;
using MinimalApi.Domain.Entities;
using MinimalApi.Infra.Data;


namespace Test.Application.Services;

[TestClass]
public class VehicleServiceTest
{
    private VehicleService _service = null!;
    private ApiDbContext _context = null!;


    [TestInitialize]
    public void Setup()
    {
        var options = new DbContextOptionsBuilder<ApiDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;
        _context = new ApiDbContext(options);
        _service = new VehicleService(_context);
    }
    
    private static VehicleDto GenerateDto() => new("Uno", "Fiat", 2003);

    private async Task GenerateFakeVehicle()
    {
        var dto = GenerateDto();
        var car = new Vehicle
        {
            Model = dto.Model,
            Brand = dto.Brand,
            Year = dto.Year,
        };
        
        await _context.Cars.AddAsync(car);
        await _context.SaveChangesAsync();
    }


    [TestMethod]
    public async Task ShouldReturnTheVehiclesFromList()
    {
        await GenerateFakeVehicle();
        var result = await _service.GetAll();
        Assert.AreEqual(1, result.Count);
        Assert.AreEqual(1, result[0].Id);
    }

    [TestMethod]
    public async Task ShouldReturnACreatedVehicle()
    {
        var dto = GenerateDto();
        var result = await _service.Register(dto);
        
        Assert.IsNotNull(result);
        Assert.AreEqual(dto.Brand, result.Brand);
        Assert.AreEqual(dto.Model, result.Model);
        Assert.IsInstanceOfType(result, typeof(Vehicle));
    }

    [TestMethod]
    public async Task ShouldReturnVehicleFromId()
    {
        await GenerateFakeVehicle();
        var result = await _service.GetCarById(1);
        Assert.IsNotNull(result);
        Assert.AreEqual(1, result.Id);
    }
    
    
    [TestMethod]
    public async Task ShouldReturnADeletedVehicle()
    {
        await GenerateFakeVehicle();
        var vehicle = await _service.GetCarById(1);
        Assert.IsNotNull(vehicle);
        
        await _service.Remove(vehicle);
        
        var deletedVehicle = await _service.GetCarById(1);
        Assert.IsNull(deletedVehicle);
    }
    
    
    
}