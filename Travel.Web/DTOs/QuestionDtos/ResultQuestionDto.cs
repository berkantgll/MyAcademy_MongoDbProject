namespace Travel.Web.DTOs.QuestionDtos
{
    public class ResultQuestionDto
    {
        public string Id { get; set; }
        public string UserId { get; set; }
        public string TourId { get; set; }
        public string QuestionText { get; set; }
        public string AnswerText { get; set; }
        public DateTime QuestionTime { get; set; }
        public bool IsAnswered { get; set; }
    }
}
