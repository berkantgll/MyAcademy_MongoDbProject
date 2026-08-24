namespace Travel.Web.DTOs.QuestionDtos
{
    public class UpdateQuestionDto
    {
        public string Id { get; set; }
        public string AnswerText { get; set; }
        public bool IsAnswered { get; set; }
    }
}
