namespace Travel.Web.DTOs.ReservationDtos
{
    public class CreateReservationDto
    {
        public string UserId { get; set; } = string.Empty;

        public string TourId { get; set; } = string.Empty;

        public string TourDateId { get; set; } = string.Empty;

        public int AdultCount { get; set; }

        public int ChildCount { get; set; }
    }
}