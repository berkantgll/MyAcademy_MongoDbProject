using Travel.Web.Entitites.Common;

namespace Travel.Web.Entitites
{
    public class Favorite : BaseEntity
    {
        public string UserId { get; set; } = string.Empty;

        public string TourId { get; set; } = string.Empty;

        public DateTime CreatedDate { get; set; } = DateTime.Now;
    }
}