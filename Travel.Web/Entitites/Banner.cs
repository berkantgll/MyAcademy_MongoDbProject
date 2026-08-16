using Travel.Web.Entitites.Common;

namespace Travel.Web.Entitites
{
    public class Banner : BaseEntity
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
    }
}
