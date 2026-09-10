using Travel.Web.DTOs.TourDtos;

namespace Travel.Web.Areas.Admin.Models
{
    public class AdminTourListViewModel
    {
        public ResultTourDto Tour { get; set; } = null!;

        public int ReservationCount { get; set; }

        public string CategoryName { get; set; } = string.Empty;

        public string DestinationName { get; set; } = string.Empty;
    }
}