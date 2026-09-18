namespace Travel.Web.DTOs.QuestionDtos
{
    public class CreateQuestionDto
    {
        public string UserId { get; set; } = string.Empty;
        public string TourId { get; set; } = string.Empty;
        public string QuestionText { get; set; } = string.Empty;
    }
}