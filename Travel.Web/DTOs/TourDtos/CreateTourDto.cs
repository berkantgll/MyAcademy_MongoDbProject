namespace Travel.Web.DTOs.TourDtos
{
    public class CreateTourDto
    {
        public string? CategoryId { get; set; }
        public string? DestinationId { get; set; }

        public string? TourName { get; set; }
        public string? Description { get; set; }

        public decimal? Price { get; set; }

        public string? Country { get; set; }
        public string? City { get; set; }

        public int? Day { get; set; }
        public int? Night { get; set; }

        public string? CoverImageUrl { get; set; }

        public List<string> GalleryImages { get; set; } = new();
        public List<string> Features { get; set; } = new();

        public bool? IsActive { get; set; }

        public List<CreateTourDateDto> TourDates { get; set; } = new();
        public List<CreateTourProgramDto> TourPrograms { get; set; } = new();
    }
}