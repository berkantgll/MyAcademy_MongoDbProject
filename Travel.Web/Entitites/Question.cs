using Travel.Web.Entitites.Common;

namespace Travel.Web.Entitites
{
    public class Question : BaseEntity
    {
        public string UserId { get; set; }
        public string TourId { get; set; }

        public string QuestionText { get; set; }
        public string AnswerText { get; set; }

        public DateTime QuestionTime { get; set; }

        public bool IsAnswered { get; set; }
    }
}