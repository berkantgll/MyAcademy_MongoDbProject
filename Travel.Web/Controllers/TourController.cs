using Microsoft.AspNetCore.Mvc;
using Travel.Web.Models;
using Travel.Web.Services.CategoryServices;
using Travel.Web.Services.CommentService;
using Travel.Web.Services.DestinationServices;
using Travel.Web.Services.QuestionService;
using Travel.Web.Services.ReservationService;
using Travel.Web.Services.TourService;

namespace Travel.Web.Controllers
{
    public class TourController : Controller
    {
        private readonly ITourService _tourService;
        private readonly ICategoryService _categoryService;
        private readonly IDestinationService _destinationService;
        private readonly IReservationService _reservationService;
        private readonly ICommentService _commentService;
        private readonly IQuestionService _questionService;


        public TourController(
            ITourService tourService,
            ICategoryService categoryService,
            IDestinationService destinationService,
            IReservationService reservationService,
            ICommentService commentService,
            IQuestionService questionService)
        {
            _tourService = tourService;
            _categoryService = categoryService;
            _destinationService = destinationService;
            _reservationService = reservationService;
            _commentService = commentService;
            _questionService = questionService;
        }


        public async Task<IActionResult> Index(
            string? destination,
            string? categoryId,
            DateTime? date,
            int? personCount,
            decimal? minPrice,
            decimal? maxPrice,
            string? sort)
        {
            var tours =
                await _tourService.GetAllAsync();

            var categories =
                await _categoryService.GetAllAsync();

            var destinations =
                await _destinationService.GetAllAsync();

            var reservations =
                await _reservationService.GetAllAsync();


            var activeTours = tours
                .Where(x => x.IsActive)
                .ToList();


            var model =
                new List<PublicTourListViewModel>();


            foreach (var tour in activeTours)
            {
                var category = categories
                    .FirstOrDefault(
                        x => x.Id == tour.CategoryId);


                var tourDestination = destinations
                    .FirstOrDefault(
                        x => x.Id == tour.DestinationId);


                var nextDate = tour.TourDates?
                    .Where(x =>
                        x.Date >= DateTime.Today &&
                        x.Capacity > 0)
                    .OrderBy(x => x.Date)
                    .FirstOrDefault();


                var reservationCount =
                    reservations.Count(x =>
                        x.TourId == tour.Id &&
                        x.Status != "İptal Edildi");


                model.Add(
                    new PublicTourListViewModel
                    {
                        Tour = tour,

                        CategoryName =
                            category?.CategoryName ?? "-",

                        DestinationName =
                            tourDestination?.DestinationName ?? "-",

                        NextDate =
                            nextDate?.Date,

                        RemainingCapacity =
                            nextDate?.Capacity ?? 0,

                        ReservationCount =
                            reservationCount
                    });
            }


            // DESTİNASYON

            if (!string.IsNullOrWhiteSpace(destination))
            {
                model = model
                    .Where(x =>
                        x.DestinationName.Contains(
                            destination,
                            StringComparison.OrdinalIgnoreCase)
                        ||
                        x.Tour.Country.Contains(
                            destination,
                            StringComparison.OrdinalIgnoreCase)
                        ||
                        x.Tour.City.Contains(
                            destination,
                            StringComparison.OrdinalIgnoreCase))
                    .ToList();
            }


            // KATEGORİ

            if (!string.IsNullOrWhiteSpace(categoryId))
            {
                model = model
                    .Where(x =>
                        x.Tour.CategoryId == categoryId)
                    .ToList();
            }


            // TARİH

            if (date.HasValue)
            {
                model = model
                    .Where(x =>
                        x.Tour.TourDates != null &&
                        x.Tour.TourDates.Any(d =>
                            d.Date.Date == date.Value.Date &&
                            d.Capacity >= (personCount ?? 1)))
                    .ToList();
            }


            // KİŞİ SAYISI

            if (personCount.HasValue &&
                !date.HasValue)
            {
                model = model
                    .Where(x =>
                        x.Tour.TourDates != null &&
                        x.Tour.TourDates.Any(d =>
                            d.Date.Date >= DateTime.Today &&
                            d.Capacity >= personCount.Value))
                    .ToList();
            }


            // MİNİMUM FİYAT

            if (minPrice.HasValue)
            {
                model = model
                    .Where(x =>
                        x.Tour.Price >= minPrice.Value)
                    .ToList();
            }


            // MAKSİMUM FİYAT

            if (maxPrice.HasValue)
            {
                model = model
                    .Where(x =>
                        x.Tour.Price <= maxPrice.Value)
                    .ToList();
            }


            // SIRALAMA

            model = sort switch
            {
                "priceAsc" =>
                    model
                        .OrderBy(x => x.Tour.Price)
                        .ToList(),

                "priceDesc" =>
                    model
                        .OrderByDescending(x => x.Tour.Price)
                        .ToList(),

                "popular" =>
                    model
                        .OrderByDescending(
                            x => x.ReservationCount)
                        .ToList(),

                "duration" =>
                    model
                        .OrderBy(x => x.Tour.Day)
                        .ToList(),

                _ =>
                    model
                        .OrderBy(x => x.Tour.TourName)
                        .ToList()
            };


            ViewBag.Categories =
                categories;

            ViewBag.Destination =
                destination;

            ViewBag.CategoryId =
                categoryId;

            ViewBag.Date =
                date?.ToString("yyyy-MM-dd");

            ViewBag.PersonCount =
                personCount;

            ViewBag.MinPrice =
                minPrice;

            ViewBag.MaxPrice =
                maxPrice;

            ViewBag.Sort =
                sort;


            return View(model);
        }


        public async Task<IActionResult> Details(
            string id)
        {
            var tour =
                await _tourService.GetByIdAsync(id);


            if (tour == null)
            {
                return NotFound();
            }


            var comments =
                await _commentService
                    .GetByTourIdAsync(id);


            var questions =
                await _questionService
                    .GetByTourIdAsync(id);


            ViewBag.Comments =
                comments;

            ViewBag.Questions =
                questions;


            return View(tour);
        }
    }
}