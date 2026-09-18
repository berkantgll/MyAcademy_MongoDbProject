using Travel.Web.Entitites.Common;

namespace Travel.Web.Entitites
{
    public class Comment : BaseEntity
    {
        public string UserId { get; set; } = string.Empty;

        public string TourId { get; set; } = string.Empty;

        public string CommentText { get; set; } = string.Empty;

        public int Rating { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.Now;

        public bool IsApproved { get; set; } = false;
    }
}