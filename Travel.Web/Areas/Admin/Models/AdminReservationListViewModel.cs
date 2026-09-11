using Travel.Web.DTOs.ReservationDtos;

namespace Travel.Web.Areas.Admin.Models
{
    public class AdminReservationListViewModel
    {
        public ResultReservationDto Reservation { get; set; } = null!;

        public string TourName { get; set; } = string.Empty;
    }
}