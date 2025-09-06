using MinimalApi.Domain.DTO;
using MinimalApi.Domain.Entities;

namespace MinimalApi.Domain.Interfaces;   

public interface IAdminService
{
    Task<LoggedAdmDto?> Login(LoginDto request);
    Task<Admin> Register(AdminDto request);
    Task<List<AdminResponseDto>> ShowAll();
    Task<Admin?> GetById(int id);
}