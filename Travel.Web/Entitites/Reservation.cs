using Travel.Web.Entitites.Common;

namespace Travel.Web.Entitites
{
    public class Reservation : BaseEntity
    {
        public string UserId { get; set; }
        public string TourId { get; set; }

        public DateTime SelectedTourDate { get; set; }
        public DateTime ReservationDate { get; set; }

        public int AdultCount { get; set; }
        public int ChildCount { get; set; }

        public decimal TotalPrice { get; set; }

        public string Status { get; set; }
        public string TourDateId { get; set; } = string.Empty;

    }
}
