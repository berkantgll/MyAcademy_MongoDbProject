namespace Travel.Web.DTOs.ReservationDtos
{
    public class MonthlyReservationStatisticDto
    {
        public int Year { get; set; }

        public int Month { get; set; }

        public int ReservationCount { get; set; }
    }
}