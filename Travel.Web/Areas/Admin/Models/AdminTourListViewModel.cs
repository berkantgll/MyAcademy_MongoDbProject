using Travel.Web.DTOs.TourDtos;

namespace Travel.Web.Areas.Admin.Models
{
    public class AdminTourListViewModel
    {
        public ResultTourDto Tour { get; set; }
        public int ReservationCount { get; set;}
    }
}
