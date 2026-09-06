using AutoMapper;
using MongoDB.Driver.Linq;
using MongoDB.Driver;
using Travel.Web.DTOs.FavoriteDtos;
using Travel.Web.Entitites;
using Travel.Web.Settings;

namespace Travel.Web.Services.FavoriteService
{
    public class FavoriteService : IFavoriteService
    {
        private readonly IMongoCollection<Favorite> _favoriteCollection;
        private readonly IMapper _mapper;

        public FavoriteService(IDatabaseSettings databaseSettings, IMapper mapper)
        {
            var client = new MongoClient(databaseSettings.ConnectionString);

            var database = client.GetDatabase(databaseSettings.DatabaseName);

            _favoriteCollection = database.GetCollection<Favorite>(databaseSettings.FavoriteCollectionName);

            _mapper = mapper;
        }
        public async Task CreateAsync(CreateFavoriteDto createFavoriteDto)
        {
            var favorite = _mapper.Map<Favorite>(createFavoriteDto);

            // favorite.UserId = Identity gelince

            /*  2.KEZ FAVORİLERE EKLENMESİN DİYE
              
             var existingFavorite = await _favoriteCollection.Find(x => x.UserId == userId && x.TourId == createFavoriteDto.TourId).FirstOrDefaultAsync();

              if (existingFavorite != null)
            {
            throw new Exception("Bu tur zaten favorilerinizde!");
            } 
            */

            await _favoriteCollection.InsertOneAsync(favorite);
        }

        public async Task DeleteAsync(string id)
        {
            await _favoriteCollection.DeleteOneAsync(x => x.Id == id);
        }

        public async Task<List<ResultFavoriteDto>> GetAllAsync()
        {
            var favorite = await _favoriteCollection.AsQueryable().ToListAsync();
            return _mapper.Map<List<ResultFavoriteDto>>(favorite);
        }

        public async Task<ResultFavoriteDto> GetByIdAsync(string id)
        {
            var favorite = await _favoriteCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

            if (favorite == null)
            {
                throw new Exception("Favori bulunamadı!");
            }

            return _mapper.Map<ResultFavoriteDto>(favorite);
        }
    }
}
