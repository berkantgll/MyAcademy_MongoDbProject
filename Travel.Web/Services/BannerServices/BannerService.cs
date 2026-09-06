using AutoMapper;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using Travel.Web.DTOs.BannerDtos;
using Travel.Web.Entitites;
using Travel.Web.Settings;

namespace Travel.Web.Services.BannerServices
{
    public class BannerService : IBannerService
    {
        private readonly IMongoCollection<Banner> _bannerCollection;
        private readonly IMapper _mapper;

        public BannerService(IDatabaseSettings databaseSettings, IMapper mapper)
        {
            var client = new MongoClient(databaseSettings.ConnectionString);
            var database = client.GetDatabase(databaseSettings.DatabaseName);
            _bannerCollection = database.GetCollection<Banner>(databaseSettings.BannerCollectionName);
            _mapper = mapper;
        }

        public async Task CreateAsync(CreateBannerDto createBannerDto)
        {
            var banner = _mapper.Map<Banner>(createBannerDto);
            await _bannerCollection.InsertOneAsync(banner);
        }

        public async Task DeleteAsync(string id)
        {
            await _bannerCollection.DeleteOneAsync(x => x.Id == id);
        }

        public async Task<List<ResultBannerDto>> GetAllAsync()
        {
            var banners = await _bannerCollection.AsQueryable().ToListAsync();
            return _mapper.Map<List<ResultBannerDto>>(banners);
        }

        public async Task<ResultBannerDto> GetByIdAsync(string id)
        {
            var banner = await _bannerCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

            if (banner == null)
            {
                throw new Exception("Banner bulunamadı!");
            }

            return _mapper.Map<ResultBannerDto>(banner);
        }

        public async Task UpdateAsync(UpdateBannerDto updateBannerDto)
        {
            var existingBanner = await _bannerCollection.Find(x => x.Id == updateBannerDto.Id).FirstOrDefaultAsync();

            if (existingBanner == null)
            {
                throw new Exception("Banner bulunamadı!");
            }

            var banner = _mapper.Map<Banner>(updateBannerDto);

            await _bannerCollection.FindOneAndReplaceAsync(x => x.Id == banner.Id, banner);
        }
    }
}
