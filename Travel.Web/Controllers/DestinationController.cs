using Microsoft.AspNetCore.Mvc;
using Travel.Web.Services.DestinationServices;

namespace Travel.Web.Controllers
{
    public class DestinationController : Controller
    {
        private readonly IDestinationService _destinationService;

        public DestinationController(
            IDestinationService destinationService)
        {
            _destinationService = destinationService;
        }


        public async Task<IActionResult> Index()
        {
            var destinations =
                await _destinationService.GetAllAsync();

            return View(destinations);
        }
    }
}