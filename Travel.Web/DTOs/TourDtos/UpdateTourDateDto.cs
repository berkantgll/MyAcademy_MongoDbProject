namespace Travel.Web.DTOs.TourDtos
{
    public class UpdateTourDateDto
    {
        public string Id { get; set; }
        public DateTime? Date { get; set; }
        public int? Capacity { get; set; }
    }
}
