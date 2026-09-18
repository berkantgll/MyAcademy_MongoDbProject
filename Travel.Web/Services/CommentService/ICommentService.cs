using Travel.Web.DTOs.CommentDtos;

namespace Travel.Web.Services.CommentService
{
    public interface ICommentService
    {
        Task CreateAsync(CreateCommentDto createCommentDto);

        Task DeleteAsync(string id);

        Task<List<ResultCommentDto>> GetAllAsync();

        Task<ResultCommentDto> GetByIdAsync(string id);

        Task<List<ResultCommentDto>> GetByTourIdAsync(string tourId);

        Task ApproveAsync(string id);
    }
}