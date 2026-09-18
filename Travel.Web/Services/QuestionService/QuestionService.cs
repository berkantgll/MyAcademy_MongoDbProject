using AutoMapper;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using Travel.Web.DTOs.QuestionDtos;
using Travel.Web.Entitites;
using Travel.Web.Settings;

namespace Travel.Web.Services.QuestionService
{
    public class QuestionService : IQuestionService
    {
        private readonly IMongoCollection<Question> _questionCollection;
        private readonly IMapper _mapper;


        public QuestionService(
            IDatabaseSettings databaseSettings,
            IMapper mapper)
        {
            var client =
                new MongoClient(
                    databaseSettings.ConnectionString);

            var database =
                client.GetDatabase(
                    databaseSettings.DatabaseName);

            _questionCollection =
                database.GetCollection<Question>(
                    databaseSettings.QuestionCollectionName);

            _mapper = mapper;
        }


        public async Task CreateAsync(
            CreateQuestionDto createQuestionDto)
        {
            var question =
                _mapper.Map<Question>(
                    createQuestionDto);

            question.QuestionTime =
                DateTime.Now;

            question.IsAnswered =
                false;

            question.AnswerText =
                string.Empty;


            await _questionCollection
                .InsertOneAsync(question);
        }


        public async Task DeleteAsync(string id)
        {
            await _questionCollection
                .DeleteOneAsync(
                    x => x.Id == id);
        }


        public async Task<List<ResultQuestionDto>> GetAllAsync()
        {
            var questions =
                await _questionCollection
                    .AsQueryable()
                    .ToListAsync();


            return _mapper
                .Map<List<ResultQuestionDto>>(
                    questions);
        }


        public async Task<ResultQuestionDto> GetByIdAsync(
            string id)
        {
            var question =
                await _questionCollection
                    .Find(x => x.Id == id)
                    .FirstOrDefaultAsync();


            if (question == null)
            {
                throw new Exception(
                    "Soru bulunamadı!");
            }


            return _mapper
                .Map<ResultQuestionDto>(
                    question);
        }


        public async Task<List<ResultQuestionDto>> GetByTourIdAsync(
            string tourId)
        {
            var questions =
                await _questionCollection
                    .Find(x =>
                        x.TourId == tourId &&
                        x.IsAnswered == true)
                    .SortByDescending(
                        x => x.QuestionTime)
                    .ToListAsync();


            return _mapper
                .Map<List<ResultQuestionDto>>(
                    questions);
        }


        public async Task UpdateAsync(
            UpdateQuestionDto updateQuestionDto)
        {
            var question =
                await _questionCollection
                    .Find(x =>
                        x.Id == updateQuestionDto.Id)
                    .FirstOrDefaultAsync();


            if (question == null)
            {
                throw new Exception(
                    "Soru bulunamadı!");
            }


            question.AnswerText =
                updateQuestionDto.AnswerText;

            question.IsAnswered =
                true;


            await _questionCollection
                .FindOneAndReplaceAsync(
                    x => x.Id == question.Id,
                    question);
        }
    }
}