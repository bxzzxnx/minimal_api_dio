using System.Net.Http.Headers;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Mvc.Testing;
using MinimalApi.Domain.DTO;

namespace Test.Integration;

public abstract class IntegrationTestBase
{
    protected readonly HttpClient HttpClient;

    protected IntegrationTestBase()
    {
        var factory = new WebApplicationFactory<Program>();
        HttpClient = factory.CreateClient();
    }
    
    protected const string AdminEmail = "admin@admin.com";
    protected const string AdminPassword = "admin";
    protected const string EditorEmail = "ed@ed.com";
    protected const string EditorPassword = "editor";
    protected const int Id = 1;
    
    private async Task<string> GetAuthToken(string email, string password)
    {
        var loginDto = CreateLoginDto(email, password);
        var response = await HttpClient.PostAsJsonAsync("/api/admin/login", loginDto);
        var result = await response.Content.ReadFromJsonAsync<LoggedAdmDto>();
        return result!.Token;
    }
    protected static LoginDto CreateLoginDto(string email, string password) => new(email, password);
    protected async Task AuthenticateClient(string email, string password)
    {
        var token = await GetAuthToken(email, password);
        HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
    }
}