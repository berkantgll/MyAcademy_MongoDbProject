namespace Travel.Web.DTOs.ReservationDtos
{
    public class CreateReservationDto
    {
        public string TourId { get; set; }
        public DateTime SelectedTourDate { get; set; }
        public int AdultCount { get; set; }
        public int ChildCount { get; set; }
    }
}
