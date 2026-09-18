namespace Travel.Web.DTOs.CommentDtos
{
    public class ResultCommentDto
    {
        public string Id { get; set; }
        public string UserId { get; set; }
        public string TourId { get; set; }
        public string CommentText { get; set; }
        public int Rating { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool IsApproved { get; set; }
    }
}
