namespace Travel.Web.DTOs.UserDtos
{
    public class RegisterDto
    {
        public string NameSurname { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string Password { get; set; } = string.Empty;

        public string ConfirmPassword { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
    }
}