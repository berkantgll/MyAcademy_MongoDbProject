using Microsoft.AspNetCore.Mvc;
using Travel.Web.Services.DestinationServices;
using Travel.Web.Services.TourService;

namespace Travel.Web.Controllers
{
    public class DestinationController : Controller
    {
        private readonly IDestinationService _destinationService;
        private readonly ITourService _tourService;


        public DestinationController(
            IDestinationService destinationService,
            ITourService tourService)
        {
            _destinationService = destinationService;
            _tourService = tourService;
        }


        public async Task<IActionResult> Index()
        {
            var destinations =
                await _destinationService.GetAllAsync();

            var tours =
                await _tourService.GetAllAsync();


            var destinationImages =
                new Dictionary<string, string>();


            foreach (var destination in destinations)
            {
                var relatedTour = tours
                    .FirstOrDefault(x =>
                        x.DestinationId == destination.Id &&
                        !string.IsNullOrWhiteSpace(
                            x.CoverImageUrl));


                destinationImages[destination.Id] =
                    relatedTour?.CoverImageUrl
                    ?? "/images/travelio-hero.png";
            }


            ViewBag.DestinationImages =
                destinationImages;


            return View(destinations);
        }
    }
}