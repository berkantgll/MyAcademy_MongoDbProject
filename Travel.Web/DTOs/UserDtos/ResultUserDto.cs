namespace Travel.Web.DTOs.UserDtos
{
    public class ResultUserDto
    {
        public string Id { get; set; } = string.Empty;

        public string NameSurname { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string Role { get; set; } = string.Empty;
        public DateTime CreatedDate { get; set; }
        public string Phone { get; set; } = string.Empty;
    }
}