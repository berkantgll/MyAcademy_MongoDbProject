using Travel.Web.Entitites.Common;

namespace Travel.Web.Entitites
{
    public class Comment : BaseEntity
    {
        public string UserId { get; set; }
        public string TourId { get; set; }
        public string CommentText { get; set; }
        public int Rating { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}