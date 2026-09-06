using Travel.Web.DTOs.TourDtos;

namespace Travel.Web.Services.TourService
{
    public interface ITourService
    {
        //ANA TUR
        Task<List<ResultTourDto>> GetAllAsync();
        Task<ResultTourDto> GetByIdAsync(string id);
        Task CreateAsync(CreateTourDto createTourDto);
        Task UpdateAsync(UpdateTourDto updateTourDto);
        Task DeleteAsync(string id);

        //TOUR DATE

        Task AddTourDateAsync(string tourId ,CreateTourDateDto createTourDateDto);
        Task UpdateTourDateAsync(string tourId, UpdateTourDateDto updateTourDateDto);
        Task DeleteTourDateAsync(string tourId , string tourDateId);

        //TOUR PROGRAM 

        Task AddTourProgramAsync(string tourId, CreateTourProgramDto createTourProgramDto);
        Task UpdateTourProgramAsync(string tourId, UpdateTourProgramDto updateTourProgramDto);
        Task DeleteTourProgramAsync(string tourId, string tourProgramId);

        //ÖZEL METOTLAR 

        Task<List<ResultTourDto>> GetActiveToursAsync();
        Task ChangeStatusAsync(string id);

        // KAPASİTE AZALTMA
        Task DecreaseCapacityAsync
            (
            string tourId,
            string tourDateId,
            int personCount
            );

        // FİLTRELEME
        Task<List<ResultTourDto>> FilterAsync
            (
            string? destinationId,
            string? categoryId,
            decimal? minPrice,
            decimal? maxPrice,
            DateTime? dateTime
            );
    }
}
