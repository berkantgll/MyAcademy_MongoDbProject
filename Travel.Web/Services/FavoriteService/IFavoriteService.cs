using Travel.Web.Entitites;

namespace Travel.Web.Services.FavoriteServices
{
    public interface IFavoriteService
    {
        Task AddAsync(string userId, string tourId);

        Task RemoveAsync(string userId, string tourId);

        Task<bool> IsFavoriteAsync(string userId, string tourId);

        Task<List<Favorite>> GetByUserIdAsync(string userId);
    }
}