using Minimal01.Domain.DTO;
using Minimal01.Domain.Entities;

namespace Minimal01.Domain.Interfaces;   

public interface IAdminService
{
    Task<Admin?> Login(LoginDTO request);
}