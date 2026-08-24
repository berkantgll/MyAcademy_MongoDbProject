using Travel.Web.Entitites.Common;

namespace Travel.Web.Entitites
{
    public class Favorite : BaseEntity
    {
        public string TourId { get; set; }
        public string UserId { get; set; }
    }
}
