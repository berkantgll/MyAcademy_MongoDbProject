using Travel.Web.Entitites.Common;

namespace Travel.Web.Entitites
{
    public class Destination : BaseEntity
    {
        public string DestinationName { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
    }
}
