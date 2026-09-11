using Microsoft.AspNetCore.Mvc;
using Travel.Web.Areas.Admin.Models;
using Travel.Web.Services.ReservationService;
using Travel.Web.Services.TourService;


namespace Travel.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
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

        public async Task<IActionResult> Index()
        {
            var reservations =
                await _reservationService.GetAllAsync();

            var tours =
                await _tourService.GetAllAsync();

            var model =
                new List<AdminReservationListViewModel>();

            foreach (var reservation in reservations)
            {
                var tour = tours
                    .FirstOrDefault(x =>
                        x.Id == reservation.TourId);

                model.Add(
                    new AdminReservationListViewModel
                    {
                        Reservation = reservation,

                        TourName =
                            tour?.TourName ?? "Tur Bulunamadı"
                    }
                );
            }

            return View(model);
        }

        public async Task<IActionResult> Approve(string id)
        {
            await _reservationService.ApproveAsync(id);

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Cancel(string id)
        {
            await _reservationService.CancelAsync(id);

            return RedirectToAction(nameof(Index));
        }
    }
}