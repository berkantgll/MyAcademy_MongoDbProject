namespace Travel.Web.DTOs.ReservationDtos
{
    public class TourReservationStatisticDto
    {
        public string TourId { get; set; } = string.Empty;

        public int ReservationCount { get; set; }
    }
}