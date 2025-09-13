using System.Net;
using System.Net.Http.Json;

namespace Test.Integration;

[TestClass]
public class AdminEndpoints : IntegrationTestBase
{
    private const string TestEmail = "teste@test.com";
    private const string TestPassword = "Test@123";
    
    [TestMethod]
    public async Task CreateAdmin_ShouldReturnUnauthorized()
    {
        var request = CreateLoginDto(TestEmail, TestPassword);
        var response = await HttpClient.PostAsJsonAsync("/api/admin/register", request);
        Assert.AreEqual(HttpStatusCode.Unauthorized, response.StatusCode);
    }

    [TestMethod]
    public async Task LoginAdmin_ShouldReturnOk()
    {
        var request = CreateLoginDto(AdminEmail, AdminPassword);
        var response = await HttpClient.PostAsJsonAsync("/api/admin/login", request);
        Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
    }

    [TestMethod]
    public async Task AuthorizedAdmin_ShouldReturnAllAdmins()
    {
        await AuthenticateClient(AdminEmail, AdminPassword);
        var response = await HttpClient.GetAsync("/api/admin/admins");
        Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
    }

    [TestMethod]
    public async Task AuthorizedAdmin_ShouldGetTheAdminFromId()
    {
        await AuthenticateClient(AdminEmail, AdminPassword);
        var response = await HttpClient.GetAsync($"/api/admin/{Id}");
        Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
    }

    [TestMethod]
    public async Task ShouldDenyAccessToTheEditor()
    {
        await AuthenticateClient(EditorEmail, EditorPassword);
        var response = await HttpClient.GetAsync("/api/admin/admins");
        Assert.AreEqual(HttpStatusCode.Forbidden, response.StatusCode);
    }
    
    [TestMethod]
    public async Task ShouldDenyAccessToCreateANewAdmin()
    {
        var request = CreateLoginDto(TestEmail, TestPassword);
        await AuthenticateClient(EditorEmail, EditorPassword);
        var response = await HttpClient.PostAsJsonAsync("/api/admin/register", request);
        Assert.AreEqual(HttpStatusCode.Forbidden, response.StatusCode);
    }
}