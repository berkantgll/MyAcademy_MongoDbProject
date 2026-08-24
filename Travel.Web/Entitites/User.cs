using Travel.Web.Entitites.Common;

namespace Travel.Web.Entitites
{
    public class User : BaseEntity
    {
        public string FullName { get; set; }
        public string Mail { get; set; }
        public string PhoneNumber { get; set; }

    }
}
