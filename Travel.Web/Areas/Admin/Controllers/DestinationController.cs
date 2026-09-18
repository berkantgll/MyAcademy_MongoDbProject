using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Travel.Web.DTOs.DestinationDtos;
using Travel.Web.Services.DestinationServices;

namespace Travel.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class DestinationController : Controller
    {
        private readonly IDestinationService _destinationService;

        public DestinationController(IDestinationService destinationService)
        {
            _destinationService = destinationService;
        }

        public async Task<IActionResult> Index()
        {
            var destinations = await _destinationService.GetAllAsync();
            return View(destinations);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateDestinationDto createDestinationDto)
        {
            if (!ModelState.IsValid)
            {
                return View(createDestinationDto);
            }

            await _destinationService.CreateAsync(createDestinationDto);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Update(string id)
        {
            var destination = await _destinationService.GetByIdAsync(id);

            var model = new UpdateDestinationDto
            {
                Id = destination.Id,
                DestinationName = destination.DestinationName,
                Country = destination.Country,
                City = destination.City
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Update(UpdateDestinationDto updateDestinationDto)
        {
            if (!ModelState.IsValid)
            {
                return View(updateDestinationDto);
            }

            await _destinationService.UpdateAsync(updateDestinationDto);

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(string id)
        {
            await _destinationService.DeleteAsync(id);

            return RedirectToAction(nameof(Index));
        }
    }
}