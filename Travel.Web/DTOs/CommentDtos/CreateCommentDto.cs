namespace Travel.Web.DTOs.CommentDtos
{
    public class CreateCommentDto
    {
        public string TourId { get; set; }
        public string CommentText { get; set; }
        public int Rating { get; set; }
    }
}
