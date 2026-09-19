using Travel.Web.Entitites.Common;

namespace Travel.Web.Entitites
{
    public class User : BaseEntity
    {
        public string NameSurname { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string PasswordHash { get; set; } = string.Empty;

        public string Role { get; set; } = "User";

        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public string Phone { get; set; } = string.Empty;
    }
}