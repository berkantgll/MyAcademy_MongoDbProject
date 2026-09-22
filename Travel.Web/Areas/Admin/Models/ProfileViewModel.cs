using Travel.Web.DTOs.CommentDtos;
using Travel.Web.DTOs.QuestionDtos;
using Travel.Web.DTOs.ReservationDtos;
using Travel.Web.DTOs.TourDtos;
using Travel.Web.Entitites;

namespace Travel.Web.Models
{
    public class ProfileViewModel
    {
        public User User { get; set; } = new();

        public List<ResultReservationDto> Reservations { get; set; } = new();

        public List<Favorite> Favorites { get; set; } = new();

        public List<ResultCommentDto> Comments { get; set; } = new();

        public List<ResultQuestionDto> Questions { get; set; } = new();

        public List<ResultTourDto> Tours { get; set; } = new();

        public int UpcomingReservationCount { get; set; }

        public int CompletedReservationCount { get; set; }

        public ResultReservationDto? UpcomingReservation { get; set; }

        public ResultTourDto? UpcomingTour { get; set; }
    }
}
