using CORE.Entities;

namespace WebAPI.Services.Interfaces
{
    public interface ITokenService
    {
        string Generate(User user);
    }
}