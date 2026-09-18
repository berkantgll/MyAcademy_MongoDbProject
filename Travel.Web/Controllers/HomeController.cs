using Microsoft.AspNetCore.Mvc;
using Travel.Web.Models;
using Travel.Web.Services.CategoryServices;
using Travel.Web.Services.CommentService;
using Travel.Web.Services.DestinationServices;
using Travel.Web.Services.ReservationService;
using Travel.Web.Services.TourService;
using Travel.Web.Services.UserServices;

namespace Travel.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ITourService _tourService;
        private readonly ICategoryService _categoryService;
        private readonly IDestinationService _destinationService;
        private readonly IReservationService _reservationService;
        private readonly ICommentService _commentService;
        private readonly IUserService _userService;


        public HomeController(
            ITourService tourService,
            ICategoryService categoryService,
            IDestinationService destinationService,
            IReservationService reservationService,
            ICommentService commentService,
            IUserService userService)
        {
            _tourService = tourService;
            _categoryService = categoryService;
            _destinationService = destinationService;
            _reservationService = reservationService;
            _commentService = commentService;
            _userService = userService;
        }


        public async Task<IActionResult> Index()
        {
            // VERİLER

            var tours =
                await _tourService.GetAllAsync();

            var categories =
                await _categoryService.GetAllAsync();

            var destinations =
                await _destinationService.GetAllAsync();

            var reservations =
                await _reservationService.GetAllAsync();

            var users =
                await _userService.GetAllAsync();


            // AKTİF TURLAR

            var activeTours = tours
                .Where(x => x.IsActive)
                .ToList();


            // ANA SAYFA İSTATİSTİKLERİ

            ViewBag.ActiveTourCount =
                activeTours.Count;


            ViewBag.DestinationCount =
                activeTours
                    .Select(x => x.DestinationId)
                    .Distinct()
                    .Count();


            ViewBag.UserCount =
                users.Count;



            // POPÜLER TURLAR

            var model =
                new List<PublicTourListViewModel>();


            foreach (var tour in activeTours)
            {
                var category =
                    categories.FirstOrDefault(
                        x => x.Id == tour.CategoryId);


                var destination =
                    destinations.FirstOrDefault(
                        x => x.Id == tour.DestinationId);


                // EN YAKIN GELECEK TARİH
                // KONTENJAN 0 OLAN TARİHLERİ ALMA

                var nextDate =
                    tour.TourDates?
                        .Where(x =>
                            x.Date.Date >= DateTime.Today &&
                            x.Capacity > 0)
                        .OrderBy(x => x.Date)
                        .FirstOrDefault();


                // İPTAL EDİLEN REZERVASYONLARI SAYMA

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
                            destination?.DestinationName ?? "-",

                        NextDate =
                            nextDate?.Date,

                        RemainingCapacity =
                            nextDate?.Capacity ?? 0,

                        ReservationCount =
                            reservationCount
                    });
            }


            // EN ÇOK REZERVASYON ALAN
            // İLK 4 TUR

            model = model
                .OrderByDescending(
                    x => x.ReservationCount)
                .Take(4)
                .ToList();



            // ÖNE ÇIKAN DESTİNASYONLAR

            var featuredDestinations =
                destinations
                    .Where(destination =>
                        activeTours.Any(tour =>
                            tour.DestinationId == destination.Id))
                    .Take(4)
                    .ToList();


            var destinationImages =
                new Dictionary<string, string>();


            foreach (var destination in featuredDestinations)
            {
                var relatedTour =
                    activeTours.FirstOrDefault(x =>
                        x.DestinationId == destination.Id &&
                        !string.IsNullOrWhiteSpace(
                            x.CoverImageUrl));


                destinationImages[destination.Id] =
                    relatedTour?.CoverImageUrl
                    ?? "/images/travelio-hero.png";
            }


            ViewBag.FeaturedDestinations =
                featuredDestinations;


            ViewBag.DestinationImages =
                destinationImages;



            // SAYFADA KULLANILACAK GÖRSELLER

            var imageTours =
                activeTours
                    .Where(x =>
                        !string.IsNullOrWhiteSpace(
                            x.CoverImageUrl))
                    .ToList();


            ViewBag.AllToursBannerImage =
                imageTours
                    .FirstOrDefault()?
                    .CoverImageUrl
                ?? "/images/travelio-hero.png";


            ViewBag.CtaImage =
                imageTours
                    .Skip(1)
                    .FirstOrDefault()?
                    .CoverImageUrl
                ?? "/images/travelio-hero.png";



            // ANA SAYFA YORUMLARI

            var comments =
                await _commentService.GetAllAsync();


            var approvedComments =
                comments
                    .Where(x => x.IsApproved)
                    .OrderByDescending(
                        x => x.CreatedDate)
                    .Take(3)
                    .ToList();


            ViewBag.HomeComments =
                approvedComments;


            return View(model);
        }
    }
}