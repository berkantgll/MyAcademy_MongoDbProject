namespace Travel.Web.DTOs.TourDtos
{
    public class UpdateTourDto
    {
        public string Id { get; set; }
        public string? CategoryId { get; set; }
        public string? DestinationId { get; set; }

        public string? TourName { get; set; }
        public string? Description { get; set; }
        public string TourNameEn { get; set; } = string.Empty;
        public string DescriptionEn { get; set; } = string.Empty;

        public decimal? Price { get; set; }

        public string? Country { get; set; }
        public string? City { get; set; }

        public int? Day { get; set; }
        public int? Night { get; set; }

        public string? CoverImageUrl { get; set; }

        public List<string> GalleryImages { get; set; } = new();
        public List<string> Features { get; set; } = new();

        public bool? IsActive { get; set; }

        public List<UpdateTourDateDto> TourDates { get; set; } = new();
        public List<UpdateTourProgramDto> TourPrograms { get; set; } = new();
    }
}
