using AutoMapper;
using MongoDB.Driver.Linq;
using MongoDB.Driver;
using Travel.Web.DTOs.UserDtos;
using Travel.Web.Entitites;
using Travel.Web.Settings;

namespace Travel.Web.Services.UserService
{
    public class UserService : IUserService
    {
        private readonly IMongoCollection<User> _userCollection;
        private readonly IMapper _mapper;

        public UserService(IDatabaseSettings databaseSettings,IMapper mapper)
        {
            var client = new MongoClient(databaseSettings.ConnectionString);
            var database = client.GetDatabase(databaseSettings.DatabaseName);
            _userCollection = database.GetCollection<User>(databaseSettings.UserCollectionName);
            _mapper = mapper;
        }
        public async Task CreateAsync(CreateUserDto createUserDto)
        {
            var user = _mapper.Map<User>(createUserDto);
            await _userCollection.InsertOneAsync(user);
        }

        public async Task DeleteAsync(string id)
        {
            await _userCollection.DeleteOneAsync(x=>x.Id==id);
        }

        public async Task<List<ResultUserDto>> GetAllAsync()
        {
            var user = await _userCollection.AsQueryable().ToListAsync();
            return _mapper.Map<List<ResultUserDto>>(user);
        }

        public async Task<ResultUserDto> GetByIdAsync(string id)
        {
            var user = await _userCollection.Find(x => x.Id == id).FirstOrDefaultAsync();
            return _mapper.Map<ResultUserDto>(user);
        }

        public async Task UpdateAsync(UpdateUserDto updateUserDto)
        {
            var user = _mapper.Map<User>(updateUserDto);
            await _userCollection.FindOneAndReplaceAsync(x => x.Id == user.Id, user);
        }
    }
}
