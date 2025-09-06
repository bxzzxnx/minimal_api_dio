using MinimalApi.Domain.Entities;

namespace MinimalApi.Domain.Interfaces;

public interface ITokenGenerator
{
    string Generate(Admin admin);
}