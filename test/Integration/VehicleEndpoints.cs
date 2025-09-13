using System.Net;
using System.Net.Http.Json;
using MinimalApi.Domain.DTO;

namespace Test.Integration;

[TestClass]
public class VehicleEndpoints : IntegrationTestBase
{
    private static VehicleDto CreateVehicleDto() => new("Uno", "Fiat", 2003);
    
    [TestMethod]
    public async Task GetAllVehicles_WithAdminAuth_ShouldReturnOk()
    {
        await AuthenticateClient(AdminEmail, AdminPassword);
        var response = await HttpClient.GetAsync("/api/vehicle");
        Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
    }

    [TestMethod]
    public async Task GetAllVehicles_WithEditorAuth_ShouldReturnOk()
    {
        await AuthenticateClient(EditorEmail, EditorPassword);
        var response = await HttpClient.GetAsync("/api/vehicle");
        Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
    }

    [TestMethod]
    public async Task GetVehicle_WithEditorAuth_ShouldReturnOk()
    {
        await AuthenticateClient(EditorEmail, EditorPassword);
        var response = await HttpClient.GetAsync($"/api/vehicle/{Id}");
        Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
    }
    
    [TestMethod]
    public async Task GetVehicle_WithAdminAuth_ShouldReturnOk()
    {
        await AuthenticateClient(AdminEmail, AdminPassword);
        var response = await HttpClient.GetAsync($"/api/vehicle/{Id}");
        Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
    }
    
    [TestMethod]
    public async Task CreateVehicle_WithEditorAuth_ShouldReturnOk()
    {
        var request = CreateVehicleDto();
        await AuthenticateClient(EditorEmail, EditorPassword);
        var response = await HttpClient.PostAsJsonAsync("/api/vehicle/register", request);
        Assert.AreEqual(HttpStatusCode.Created, response.StatusCode);
    }
    
    [TestMethod]
    public async Task CreateVehicle_WithAdminAuth_ShouldReturnOk()
    {
        var request = CreateVehicleDto();
        await AuthenticateClient(AdminEmail, AdminPassword);
        var response = await HttpClient.PostAsJsonAsync("/api/vehicle/register", request);
        Assert.AreEqual(HttpStatusCode.Created, response.StatusCode);
    }

    [TestMethod]
    public async Task UpdateVehicle_WithEditorAuth_ShouldReturnForbidden()
    {
        var request = CreateVehicleDto();
        await AuthenticateClient(EditorEmail, EditorPassword);
        var response = await HttpClient.PutAsJsonAsync($"/api/vehicle/{Id}", request);
        Assert.AreEqual(HttpStatusCode.Forbidden, response.StatusCode);
    }
    
    
    [TestMethod]
    public async Task UpdateVehicle_WithAdminAuth_ShouldReturnOk()
    {
        var request = CreateVehicleDto();
        await AuthenticateClient(AdminEmail, AdminPassword);
        var response = await HttpClient.PutAsJsonAsync($"/api/vehicle/{Id}", request);
        Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
    }

    [TestMethod]
    public async Task DeleteVehicle_WithEditorAuth_ShouldReturnForbidden()
    {
        await AuthenticateClient(EditorEmail, EditorPassword);
        var response = await HttpClient.DeleteAsync($"/api/vehicle/{Id}");
        Assert.AreEqual(HttpStatusCode.Forbidden, response.StatusCode);
    }
}