using AutoMapper;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Travel.Web.DTOs.UserDtos;
using Travel.Web.Entitites;
using Travel.Web.Settings;

namespace Travel.Web.Services.UserServices
{
    public class UserService : IUserService
    {
        private readonly IMongoCollection<User> _userCollection;
        private readonly IMapper _mapper;

        public UserService(
            IOptions<DatabaseSettings> databaseSettings,
            IMapper mapper)
        {
            var client = new MongoClient(
                databaseSettings.Value.ConnectionString
            );

            var database = client.GetDatabase(
                databaseSettings.Value.DatabaseName
            );

            _userCollection =
                database.GetCollection<User>("Users");

            _mapper = mapper;
        }


        public async Task<User?> GetByEmailAsync(string email)
        {
            return await _userCollection
                .Find(x => x.Email == email)
                .FirstOrDefaultAsync();
        }


        public async Task<User?> GetByIdAsync(string id)
        {
            return await _userCollection
                .Find(x => x.Id == id)
                .FirstOrDefaultAsync();
        }


        public async Task<List<ResultUserDto>> GetAllAsync()
        {
            var users = await _userCollection
                .Find(x => true)
                .ToListAsync();

            return _mapper.Map<List<ResultUserDto>>(users);
        }


        public async Task CreateAsync(User user)
        {
            await _userCollection.InsertOneAsync(user);
        }
    }
}