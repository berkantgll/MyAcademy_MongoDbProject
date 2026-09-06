using Travel.Web.DTOs.FavoriteDtos;

namespace Travel.Web.Services.FavoriteService
{
    public interface IFavoriteService
    {
        Task<List<ResultFavoriteDto>> GetAllAsync();
        Task<ResultFavoriteDto> GetByIdAsync(string id);
        Task CreateAsync(CreateFavoriteDto createFavoriteDto);
        Task DeleteAsync(string id);
    }
}
