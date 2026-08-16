namespace Travel.Web.DTOs.RouteDtos
{
    public class UpdateRouteDto
    {
        public string Id { get; set; }
        public string? City { get; set; }
        public string? Country { get; set; }
        public string? Duration { get; set; }
        public Decimal Price { get; set; }
        public string? ImageUrl { get; set; }
    }
}
