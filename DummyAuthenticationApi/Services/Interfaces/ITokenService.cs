using DummyAuthenticationApi.Models;

namespace DummyAuthenticationApi.Services.Interfaces;

public interface ITokenService
{
    public string GenerateTokem(User user);
}