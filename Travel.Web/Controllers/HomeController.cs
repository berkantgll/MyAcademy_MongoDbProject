using Microsoft.AspNetCore.Mvc;
using Travel.Web.Models;
using Travel.Web.Services.CategoryServices;
using Travel.Web.Services.DestinationServices;
using Travel.Web.Services.ReservationService;
using Travel.Web.Services.TourService;
using Travel.Web.Services.CommentService;


namespace Travel.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ITourService _tourService;
        private readonly ICategoryService _categoryService;
        private readonly IDestinationService _destinationService;
        private readonly IReservationService _reservationService;
        private readonly ICommentService _commentService;

        public HomeController(
            ITourService tourService,
            ICategoryService categoryService,
            IDestinationService destinationService,
            IReservationService reservationService,
            ICommentService commentService)
        {
            _tourService = tourService;
            _categoryService = categoryService;
            _destinationService = destinationService;
            _reservationService = reservationService;
            _commentService = commentService;
        }

        public async Task<IActionResult> Index()
        {
            var tours = await _tourService.GetAllAsync();
            var categories = await _categoryService.GetAllAsync();
            var destinations = await _destinationService.GetAllAsync();
            var reservations = await _reservationService.GetAllAsync();

            var activeTours = tours
                .Where(x => x.IsActive)
                .ToList();

            ViewBag.ActiveTourCount = activeTours.Count;

            ViewBag.DestinationCount = activeTours
                .Select(x => x.DestinationId)
                .Distinct()
                .Count();

            ViewBag.TotalCapacity = activeTours
                .Sum(x => x.TourDates?.Sum(d => d.Capacity) ?? 0);

            var model = new List<PublicTourListViewModel>();

            foreach (var tour in activeTours)
            {
                var category = categories
                    .FirstOrDefault(x => x.Id == tour.CategoryId);

                var destination = destinations
                    .FirstOrDefault(x => x.Id == tour.DestinationId);

                var nextDate = tour.TourDates?
                    .Where(x => x.Date >= DateTime.Today)
                    .OrderBy(x => x.Date)
                    .FirstOrDefault();

                var reservationCount = reservations.Count(x =>
                    x.TourId == tour.Id &&
                    x.Status != "İptal Edildi");

                model.Add(new PublicTourListViewModel
                {
                    Tour = tour,
                    CategoryName = category?.CategoryName ?? "-",
                    DestinationName = destination?.DestinationName ?? "-",
                    NextDate = nextDate?.Date,
                    RemainingCapacity = nextDate?.Capacity ?? 0,
                    ReservationCount = reservationCount
                });
            }

            model = model
                .OrderByDescending(x => x.ReservationCount)
                .Take(4)
                .ToList();

            var comments = await _commentService.GetAllAsync();

            var approvedComments = comments
                .Where(x => x.IsApproved)
                .OrderByDescending(x => x.CreatedDate)
                .Take(3)
                .ToList();

            ViewBag.HomeComments = approvedComments;

            return View(model);
        }
    }
}