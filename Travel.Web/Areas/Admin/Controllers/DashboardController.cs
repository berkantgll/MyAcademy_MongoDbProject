using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;
using Travel.Web.Areas.Admin.Models;
using Travel.Web.Services.ReservationService;
using Travel.Web.Services.TourService;

namespace Travel.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class DashboardController : Controller
    {
        private readonly ITourService _tourService;
        private readonly IReservationService _reservationService;

        public DashboardController(
            ITourService tourService,
            IReservationService reservationService)
        {
            _tourService = tourService;
            _reservationService = reservationService;
        }

        public async Task<IActionResult> Index()
        {
            var tours = await _tourService.GetAllAsync();
            var reservations = await _reservationService.GetAllAsync();

            var model = new AdminDashboardViewModel
            {
                TotalTourCount = tours.Count,

                ActiveTourCount = tours.Count(x => x.IsActive),

                TotalReservationCount = reservations.Count,

                PendingReservationCount = reservations.Count(x =>
                    x.Status == "Bekliyor"),

                TotalRevenue = reservations
                    .Where(x => x.Status == "Onaylandı")
                    .Sum(x => x.TotalPrice),

                TotalCapacity = tours.Sum(x =>
                    x.TourDates?.Sum(d => d.Capacity) ?? 0)
            };


            // -----------------------------
            // SON 6 AY REZERVASYON GRAFİĞİ
            // -----------------------------

            var today = DateTime.Today;

            var culture =
                CultureInfo.GetCultureInfo("tr-TR");

            for (int i = 5; i >= 0; i--)
            {
                var month = today.AddMonths(-i);

                model.ChartLabels.Add(
                    month.ToString("MMM yyyy", culture)
                );

                var reservationCount =
                    reservations.Count(x =>
                        x.ReservationDate.Year == month.Year &&
                        x.ReservationDate.Month == month.Month);

                model.ChartReservationCounts.Add(
                    reservationCount
                );
            }


            // -----------------------------
            // EN POPÜLER 5 TUR
            // -----------------------------

            var popularGroups = reservations
                .Where(x => x.Status != "İptal Edildi")
                .GroupBy(x => x.TourId)
                .Select(x => new
                {
                    TourId = x.Key,
                    Count = x.Count()
                })
                .OrderByDescending(x => x.Count)
                .Take(5)
                .ToList();


            foreach (var group in popularGroups)
            {
                var tour = tours
                    .FirstOrDefault(x =>
                        x.Id == group.TourId);

                if (tour == null)
                {
                    continue;
                }

                model.PopularTours.Add(
                    new AdminDashboardPopularTourViewModel
                    {
                        TourId = tour.Id,
                        TourName = tour.TourName,
                        CoverImageUrl = tour.CoverImageUrl,
                        ReservationCount = group.Count
                    }
                );
            }


            // -----------------------------
            // SON 5 REZERVASYON
            // -----------------------------

            var recentReservations = reservations
                .OrderByDescending(x => x.ReservationDate)
                .Take(5)
                .ToList();


            foreach (var reservation in recentReservations)
            {
                var tour = tours
                    .FirstOrDefault(x =>
                        x.Id == reservation.TourId);

                model.RecentReservations.Add(
                    new AdminDashboardReservationViewModel
                    {
                        Id = reservation.Id,

                        UserId = reservation.UserId,

                        TourName =
                            tour?.TourName ?? "Tur Bulunamadı",

                        SelectedTourDate =
                            reservation.SelectedTourDate,

                        ReservationDate =
                            reservation.ReservationDate,

                        AdultCount =
                            reservation.AdultCount,

                        ChildCount =
                            reservation.ChildCount,

                        TotalPrice =
                            reservation.TotalPrice,

                        Status =
                            reservation.Status
                    }
                );
            }


            return View(model);
        }
    }
}