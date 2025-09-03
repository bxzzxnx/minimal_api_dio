using Minimal01.Domain.DTO;
using Minimal01.Domain.Entities;

namespace Minimal01.Domain.Interfaces;   

public interface IAdminService
{
    Task<Admin?> Login(LoginDto request);
    Task<Admin> Register(AdminDto request);
    Task<List<AdminResponseDto>> ShowAll();
    Task<Admin?> GetById(int id);
}