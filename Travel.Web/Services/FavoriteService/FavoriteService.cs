using MongoDB.Driver;
using Travel.Web.Entitites;
using Travel.Web.Settings;

namespace Travel.Web.Services.FavoriteServices
{
    public class FavoriteService : IFavoriteService
    {
        private readonly IMongoCollection<Favorite> _favoriteCollection;


        public FavoriteService(
            IDatabaseSettings databaseSettings)
        {
            var client =
                new MongoClient(
                    databaseSettings.ConnectionString);

            var database =
                client.GetDatabase(
                    databaseSettings.DatabaseName);

            _favoriteCollection =
                database.GetCollection<Favorite>(
                    databaseSettings.FavoriteCollectionName);
        }


        public async Task AddAsync(
            string userId,
            string tourId)
        {
            var favorite = await _favoriteCollection
                .Find(x =>
                    x.UserId == userId &&
                    x.TourId == tourId)
                .FirstOrDefaultAsync();


            if (favorite != null)
            {
                return;
            }


            var newFavorite = new Favorite
            {
                UserId = userId,
                TourId = tourId,
                CreatedDate = DateTime.Now
            };


            await _favoriteCollection
                .InsertOneAsync(newFavorite);
        }


        public async Task RemoveAsync(
            string userId,
            string tourId)
        {
            await _favoriteCollection
                .DeleteOneAsync(x =>
                    x.UserId == userId &&
                    x.TourId == tourId);
        }


        public async Task<bool> IsFavoriteAsync(
            string userId,
            string tourId)
        {
            var favorite = await _favoriteCollection
                .Find(x =>
                    x.UserId == userId &&
                    x.TourId == tourId)
                .FirstOrDefaultAsync();


            return favorite != null;
        }


        public async Task<List<Favorite>>
            GetByUserIdAsync(string userId)
        {
            var favorites = await _favoriteCollection
                .Find(x => x.UserId == userId)
                .SortByDescending(x => x.CreatedDate)
                .ToListAsync();


            return favorites;
        }
    }
}