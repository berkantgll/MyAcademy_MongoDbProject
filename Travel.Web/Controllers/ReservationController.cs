using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Travel.Web.DTOs.ReservationDtos;
using Travel.Web.Models;
using Travel.Web.Services.ReservationService;
using Travel.Web.Services.TourService;

namespace Travel.Web.Controllers
{
    [Authorize]
    public class ReservationController : Controller
    {
        private readonly IReservationService _reservationService;
        private readonly ITourService _tourService;

        public ReservationController(
            IReservationService reservationService,
            ITourService tourService)
        {
            _reservationService = reservationService;
            _tourService = tourService;
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
            CreateReservationDto createReservationDto)
        {
            var userId = User.FindFirstValue(
                ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(userId))
            {
                return RedirectToAction(
                    "Login",
                    "Account");
            }

            createReservationDto.UserId = userId;


            if (!ModelState.IsValid)
            {
                TempData["ErrorMessage"] =
                    "Rezervasyon bilgileri geçerli değil.";

                return RedirectToAction(
                    "Details",
                    "Tour",
                    new
                    {
                        id = createReservationDto.TourId
                    });
            }


            try
            {
                await _reservationService
                    .CreateAsync(createReservationDto);

                TempData["SuccessMessage"] =
                    "Rezervasyonunuz başarıyla oluşturuldu.";

                return RedirectToAction(
                    "Details",
                    "Tour",
                    new
                    {
                        id = createReservationDto.TourId
                    });
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] =
                    ex.Message;

                return RedirectToAction(
                    "Details",
                    "Tour",
                    new
                    {
                        id = createReservationDto.TourId
                    });
            }
        }



        [HttpGet]
        public async Task<IActionResult> MyReservations()
        {
            var userId = User.FindFirstValue(
                ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(userId))
            {
                return RedirectToAction(
                    "Login",
                    "Account");
            }


            var reservations = await _reservationService
                .GetByUserIdAsync(userId);


            var tours = await _tourService
                .GetAllAsync();


            var model = new List<MyReservationViewModel>();


            foreach (var reservation in reservations)
            {
                var tour = tours.FirstOrDefault(
                    x => x.Id == reservation.TourId);


                model.Add(new MyReservationViewModel
                {
                    Reservation = reservation,

                    TourName =
                        tour?.TourName ?? "Tur",

                    CoverImageUrl =
                        tour?.CoverImageUrl ?? ""
                });
            }


            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Cancel(string id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(userId))
            {
                return RedirectToAction("Login", "Account");
            }

            var reservation = await _reservationService.GetByIdAsync(id);

            // Başkasının rezervasyonunu iptal edemesin
            if (reservation.UserId != userId)
            {
                return Forbid();
            }

            try
            {
                await _reservationService.CancelAsync(id);

                TempData["SuccessMessage"] =
                    "Rezervasyonunuz iptal edildi.";
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
            }

            return RedirectToAction(nameof(MyReservations));
        }
    }
}