using Microsoft.EntityFrameworkCore;
using MinimalApi.Application.Services;
using MinimalApi.Domain.DTO;
using MinimalApi.Domain.Entities;
using MinimalApi.Domain.Enums;
using MinimalApi.Domain.Interfaces;
using MinimalApi.Infra.Data;
using Moq;

namespace Test.Application.Services;

[TestClass]
public class AdminServiceTest
{
    private ApiDbContext _context = null!;
    private Mock<ITokenGenerator> _mockTokenGenerator = null!;
    private AdminService _service = null!;
    
    
    [TestInitialize]
    public void Setup()
    {
        var options = new DbContextOptionsBuilder<ApiDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;
        _context = new ApiDbContext(options);
        _mockTokenGenerator = new Mock<ITokenGenerator>();
        _service = new AdminService(_context, _mockTokenGenerator.Object);
    }
    

    private static AdminDto GenerateDto()
    {
        return new AdminDto
        {
            Email = "admin@teste.com",
            Password = "123456",
            Profile = Profile.Admin
        };
    }
    
    private async Task GenerateAdmin()
    {
        var admin = new Admin
        {
            Email = "admin@admin.com",
            Password = BCrypt.Net.BCrypt.HashPassword("admin"),
            Profile = nameof(Profile.Admin)
        };
        await _context.Admins.AddAsync(admin);
        await _context.SaveChangesAsync();
    }
    
    [TestMethod]
    public async Task Register_ShouldAddAdmin()
    {
        var dto = GenerateDto();
        var result = await _service.Register(dto);

        Assert.IsNotNull(result);
        Assert.AreEqual(dto.Email, result.Email);
        Assert.AreEqual(dto.Profile.ToString(), result.Profile);
        Assert.IsInstanceOfType(result, typeof(Admin));
    }

    [TestMethod]
    public async Task ShowAdmins_ShouldReturnsAEmptyList()
    {
        var result = await _service.ShowAll();
        Assert.AreEqual(0, result.Count);
    }

    [TestMethod]
    public async Task ShouldReturnAListOfAdmins()
    {
        await GenerateAdmin();
        var result = await _service.ShowAll();
        Assert.IsNotNull(result);
    }
    
    [TestMethod]
    public async Task Login_ShouldReturnToken()
    {
        await GenerateAdmin();
        _mockTokenGenerator.Setup(t => t.Generate(It.IsAny<Admin>())).Returns("qualquerToken");
        
        var dto = new LoginDto("admin@admin.com", "admin");
        var result = await _service.Login(dto);
        
        Assert.IsNotNull(result);
        Assert.AreEqual("qualquerToken", result.Token);
    }
    
    [TestMethod]
    public async Task ShowAdmins_ShouldReturnListOfAdmins()
    {
        await GenerateAdmin();

        var result = await _service.ShowAll();

        Assert.AreEqual(1, result.Count);
        Assert.AreEqual("admin@admin.com", result[0].Email);
    }

    [TestMethod]
    public async Task ShouldReturnAIdFromAdmin()
    {
        await GenerateAdmin();
        var result = await _service.GetById(1);
        Assert.IsNotNull(result);
    }
}