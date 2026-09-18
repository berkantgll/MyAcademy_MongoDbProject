using Travel.Web.DTOs.TourDtos;

namespace Travel.Web.Models
{
    public class PublicTourListViewModel
    {
        public ResultTourDto Tour { get; set; }

        public string CategoryName { get; set; }

        public string DestinationName { get; set; }

        public DateTime? NextDate { get; set; }

        public int RemainingCapacity { get; set; }

        public int ReservationCount { get; set; }
    }
}