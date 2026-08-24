namespace Travel.Web.DTOs.TourDtos
{
    public class UpdateTourProgramDto
    {
        public int? DayNumber { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? City { get; set; }
        public string? Transport { get; set; }
        public string? Meal { get; set; }
    }
}
