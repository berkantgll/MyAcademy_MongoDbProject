namespace Travel.Web.DTOs.CommentDtos
{
    public class CreateCommentDto
    {
        public string UserId { get; set; } = string.Empty;

        public string TourId { get; set; } = string.Empty;

        public string CommentText { get; set; } = string.Empty;

        public int Rating { get; set; }
    }
}