using Travel.Web.Entitites.Common;

namespace Travel.Web.Entitites
{
    public class Route : BaseEntity
    {
        public string City { get; set; }
        public string Country { get; set; }
        public string Duration { get; set; }
        public Decimal Price { get; set; }
        public string ImageUrl { get; set; }
    }
}
