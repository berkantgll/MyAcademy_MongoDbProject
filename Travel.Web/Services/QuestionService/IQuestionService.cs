using Travel.Web.DTOs.QuestionDtos;

namespace Travel.Web.Services.QuestionService
{
    public interface IQuestionService
    {
        Task<List<ResultQuestionDto>> GetAllAsync();
        Task<ResultQuestionDto> GetByIdAsync(string id);
        Task CreateAsync(CreateQuestionDto createQuestionDto);
        Task UpdateAsync(UpdateQuestionDto updateQuestionDto);
        Task DeleteAsync(string id);
    }
}
