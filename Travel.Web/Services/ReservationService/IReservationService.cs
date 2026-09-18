using Travel.Web.DTOs.ReservationDtos;

namespace Travel.Web.Services.ReservationService
{
    public interface IReservationService
    {
        Task<List<ResultReservationDto>> GetAllAsync();
        Task<ResultReservationDto> GetByIdAsync(string id);
        Task CreateAsync(CreateReservationDto createReservationDto);
        Task UpdateStatusAsync(UpdateReservationDto updateReservationDto);
        Task DeleteAsync(string id);
        Task ApproveAsync(string id);
        Task CancelAsync(string id);

        Task<int> GetReservationCountByTourIdAsync(string tourId);
        Task<List<ResultReservationDto>> GetByUserIdAsync(string userId);
    }
}
