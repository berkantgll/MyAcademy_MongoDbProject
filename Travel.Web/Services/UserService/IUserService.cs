using Travel.Web.DTOs.UserDtos;

namespace Travel.Web.Services.UserService
{
    public interface IUserService
    {
        Task<List<ResultUserDto>> GetAllAsync();
        Task<ResultUserDto> GetByIdAsync(string id);
        Task CreateAsync(CreateUserDto createUserDto);
        Task UpdateAsync(UpdateUserDto updateUserDto);
        Task DeleteAsync(string id);
    }
}
