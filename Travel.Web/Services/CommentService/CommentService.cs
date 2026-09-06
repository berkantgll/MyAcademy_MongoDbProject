using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;
using Travel.Web.DTOs.CommentDtos;
using Travel.Web.Entitites;
using Travel.Web.Settings;

namespace Travel.Web.Services.CommentService
{
    public class CommentService : ICommentService
    {
        private readonly IMongoCollection<Comment> _commentCollection;
        private readonly IMapper _mapper;

        public CommentService(IDatabaseSettings databaseSettings , IMapper mapper)
        {
            var client = new MongoClient(databaseSettings.ConnectionString);
            var database = client.GetDatabase(databaseSettings.DatabaseName);
            _commentCollection = database.GetCollection<Comment>(databaseSettings.CommentCollectionName);
            _mapper = mapper;
        }
        public async Task CreateAsync(CreateCommentDto createCommentDto)
        {
            var comment = _mapper.Map<Comment>(createCommentDto);
            comment.CreatedDate = DateTime.Now;

            //UserId Identity gelince kurulucak.

            await _commentCollection.InsertOneAsync(comment);
        }

        public async Task DeleteAsync(string id)
        {
            await _commentCollection.DeleteOneAsync(x => x.Id == id);
        }

        public async Task<List<ResultCommentDto>> GetAllAsync()
        {
            var comment = await _commentCollection.AsQueryable().ToListAsync();
            return _mapper.Map<List<ResultCommentDto>>(comment);
        }

        public async Task<ResultCommentDto> GetByIdAsync(string id)
        {
            var comment = await _commentCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

            if(comment == null)
            {
                throw new Exception("Yorum bulunamadı!");
            }

            return _mapper.Map<ResultCommentDto>(comment);
        }
    }
}
