using Minimal01.Domain.Entities;

namespace Minimal01.Domain.Interfaces;

public interface ITokenGenerator
{
    string Generate(Admin admin);
}