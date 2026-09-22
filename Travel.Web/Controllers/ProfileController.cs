using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Travel.Web.Models;
using Travel.Web.Services.CommentService;
using Travel.Web.Services.FavoriteServices;
using Travel.Web.Services.QuestionService;
using Travel.Web.Services.ReservationService;
using Travel.Web.Services.TourService;
using Travel.Web.Services.UserServices;

namespace Travel.Web.Controllers
{
    [Authorize]
    public class ProfileController : Controller
    {
        private readonly IUserService _userService;
        private readonly ICommentService _commentService;
        private readonly IQuestionService _questionService;
        private readonly ITourService _tourService;
        private readonly IReservationService _reservationService;
        private readonly IFavoriteService _favoriteService;

        public ProfileController(
            IUserService userService,
            ICommentService commentService,
            IQuestionService questionService,
            ITourService tourService,
            IReservationService reservationService,
            IFavoriteService favoriteService)
        {
            _userService = userService;
            _commentService = commentService;
            _questionService = questionService;
            _tourService = tourService;
            _reservationService = reservationService;
            _favoriteService = favoriteService;
        }

        public async Task<IActionResult> Index()
        {
            var userId =
                User.FindFirstValue(
                    ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(userId))
            {
                return RedirectToAction(
                    "Login",
                    "Account");
            }

            var user =
                await _userService
                    .GetByIdAsync(userId);

            var comments =
                await _commentService
                    .GetAllAsync();

            var questions =
                await _questionService
                    .GetAllAsync();

            var tours =
                await _tourService
                    .GetAllAsync();

            var reservations =
                await _reservationService
                    .GetByUserIdAsync(userId);

            var favorites =
                await _favoriteService
                    .GetByUserIdAsync(userId);

            var myComments = comments
                .Where(x => x.UserId == userId)
                .OrderByDescending(x => x.CreatedDate)
                .ToList();

            var myQuestions = questions
                .Where(x => x.UserId == userId)
                .OrderByDescending(x => x.QuestionTime)
                .ToList();

            var activeReservations = reservations
                .Where(x => x.Status != "İptal Edildi")
                .ToList();

            var upcomingReservations = activeReservations
                .Where(x => x.SelectedTourDate.Date >= DateTime.Today)
                .OrderBy(x => x.SelectedTourDate)
                .ToList();

            var completedReservations = activeReservations
                .Where(x => x.SelectedTourDate.Date < DateTime.Today)
                .ToList();

            var upcomingReservation =
                upcomingReservations.FirstOrDefault();

            var upcomingTour =
                upcomingReservation == null
                    ? null
                    : tours.FirstOrDefault(
                        x => x.Id == upcomingReservation.TourId);

            var model = new ProfileViewModel
            {
                User = user,
                Reservations = reservations,
                Favorites = favorites,
                Comments = myComments,
                Questions = myQuestions,
                Tours = tours,

                UpcomingReservationCount =
                    upcomingReservations.Count,

                CompletedReservationCount =
                    completedReservations.Count,

                UpcomingReservation =
                    upcomingReservation,

                UpcomingTour =
                    upcomingTour
            };

            return View(model);
        }
    }
}