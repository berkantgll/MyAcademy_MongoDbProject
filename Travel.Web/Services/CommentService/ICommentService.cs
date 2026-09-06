using Travel.Web.DTOs.CommentDtos;

namespace Travel.Web.Services.CommentService
{
    public interface ICommentService
    {
        Task<List<ResultCommentDto>> GetAllAsync();
        Task<ResultCommentDto> GetByIdAsync(string id);

        Task CreateAsync(CreateCommentDto createCommentDto);
        Task DeleteAsync(string id);
    }
}
