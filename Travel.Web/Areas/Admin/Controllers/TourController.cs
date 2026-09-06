using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Travel.Web.DTOs.TourDtos;
using Travel.Web.Services.TourService;

namespace Travel.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class TourController : Controller
    {
        private readonly ITourService _tourService;
        private readonly IMapper _mapper;

        public TourController(ITourService tourService, IMapper mapper)
        {
            _tourService = tourService;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            var tours = await _tourService.GetAllAsync();
            return View(tours);
        }

        [HttpGet]

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateTourDto createTourDto)
        {
            if (!ModelState.IsValid)
            {
                return View(createTourDto);
            }

            await _tourService.CreateAsync(createTourDto);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Update(string id)
        {
            var tour = await _tourService.GetByIdAsync(id);

            var updateTourDto = _mapper.Map<UpdateTourDto>(tour);

            return View(updateTourDto);
        }

        [HttpPost]
        public async Task<IActionResult> Update(UpdateTourDto updateTourDto)
        {
            if (!ModelState.IsValid)
            {
                return View(updateTourDto);
            }

            await _tourService.UpdateAsync(updateTourDto);

            return RedirectToAction(nameof(Index));

        }

        public async Task<IActionResult> Delete(string id)
        {
            await _tourService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> ChangeStatus(string id)
        {
            await _tourService.ChangeStatusAsync(id);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Details(string id)
        {
            var tour = await _tourService.GetByIdAsync(id);
            return View(tour);
        }

        [HttpGet]
        public IActionResult AddTourDate(string tourId)
        {
            ViewBag.TourId = tourId;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddTourDate(string tourId, CreateTourDateDto createTourDateDto)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.TourId = tourId;
                return View(createTourDateDto);
            }

            await _tourService.AddTourDateAsync(tourId, createTourDateDto);

            return RedirectToAction(nameof(Details), new { id = tourId });
        }

        [HttpGet]
        public async Task<IActionResult> UpdateTourDate(string tourId, string tourDateId)
        {
            var tourDate = await _tourService.GetTourDateByIdAsync(tourId, tourDateId);

            var updateTourDateDto = _mapper.Map<UpdateTourDateDto>(tourDate);

            ViewBag.TourId = tourId;

            return View(updateTourDateDto);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateTourDate(string tourId, UpdateTourDateDto updateTourDateDto)
        {

            if (!ModelState.IsValid)
            {
                ViewBag.TourId = tourId;
                return View(updateTourDateDto);
            }

            await _tourService.UpdateTourDateAsync(tourId, updateTourDateDto);

            return RedirectToAction(nameof(Details), new { id = tourId });
        }

        public async Task<IActionResult> DeleteTourDate(string tourId, string tourDateId)
        {
            await _tourService.DeleteTourDateAsync(tourId, tourDateId);

            return RedirectToAction(nameof(Details), new { id = tourId });
        }

        [HttpGet]
        public IActionResult AddTourProgram(string tourId)
        {
            ViewBag.TourId = tourId;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddTourProgram(string tourId, CreateTourProgramDto createTourProgramDto)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.TourId = tourId;
                return View(createTourProgramDto);
            }

            await _tourService.AddTourProgramAsync(tourId, createTourProgramDto);

            return RedirectToAction(nameof(Details), new { id = tourId });
        }

        [HttpGet]
        public async Task<IActionResult> UpdateTourProgram(string tourId, string tourProgramId)
        {
            var tourProgram = await _tourService
                .GetTourProgramByIdAsync(tourId, tourProgramId);

            var updateTourProgramDto =
                _mapper.Map<UpdateTourProgramDto>(tourProgram);

            ViewBag.TourId = tourId;

            return View(updateTourProgramDto);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateTourProgram(string tourId, UpdateTourProgramDto updateTourProgramDto)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.TourId = tourId;
                return View(updateTourProgramDto);
            }

            await _tourService.UpdateTourProgramAsync(tourId,updateTourProgramDto);

            return RedirectToAction(nameof(Details),new { id = tourId });
        }

        public async Task<IActionResult> DeleteTourProgram(string tourId, string tourProgramId)
        {
            await _tourService.DeleteTourProgramAsync(tourId,tourProgramId);

            return RedirectToAction(nameof(Details),new { id = tourId });
        }
    }
}