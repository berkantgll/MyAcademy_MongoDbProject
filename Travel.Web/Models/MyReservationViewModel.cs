using Travel.Web.DTOs.ReservationDtos;

namespace Travel.Web.Models
{
    public class MyReservationViewModel
    {
        public ResultReservationDto Reservation { get; set; } = null!;

        public string TourName { get; set; } = string.Empty;

        public string CoverImageUrl { get; set; } = string.Empty;
    }
}