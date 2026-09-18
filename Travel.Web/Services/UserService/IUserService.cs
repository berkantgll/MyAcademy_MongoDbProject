using Travel.Web.DTOs.UserDtos;
using Travel.Web.Entitites;

namespace Travel.Web.Services.UserServices
{
    public interface IUserService
    {
        Task<User?> GetByEmailAsync(string email);

        Task<User?> GetByIdAsync(string id);

        Task<List<ResultUserDto>> GetAllAsync();

        Task CreateAsync(User user);
    }
}