using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;
using Travel.Web.Areas.Admin.Models;
using Travel.Web.Services.QuestionService;
using Travel.Web.Services.ReservationService;
using Travel.Web.Services.TourService;
using Travel.Web.Services.UserServices;

namespace Travel.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class DashboardController : Controller
    {
        private readonly ITourService _tourService;
        private readonly IReservationService _reservationService;
        private readonly IUserService _userService;
        private readonly IQuestionService _questionService;


        public DashboardController(
            ITourService tourService,
            IReservationService reservationService,
            IUserService userService,
            IQuestionService questionService)
        {
            _tourService = tourService;
            _reservationService = reservationService;
            _userService = userService;
            _questionService = questionService;
        }


        public async Task<IActionResult> Index()
        {
            var tours =
                await _tourService.GetAllAsync();

            var reservations =
                await _reservationService.GetAllAsync();

            var users =
                await _userService.GetAllAsync();

            var questions =
                await _questionService.GetAllAsync();


            var monthlyStatistics =
                await _reservationService
                    .GetLast6MonthsReservationStatsAsync();


            var top5TourStatistics =
                await _reservationService
                    .GetTop5ToursByReservationAsync();


            var today =
                DateTime.Today;


            var model = new AdminDashboardViewModel
            {
                TotalTourCount =
                    tours.Count,

                ActiveTourCount =
                    tours.Count(x => x.IsActive),

                PassiveTourCount =
                    tours.Count(x => !x.IsActive),

                TotalReservationCount =
                    reservations.Count,

                PendingReservationCount =
                    reservations.Count(x =>
                        x.Status == "Bekliyor"),

                ThisMonthReservationCount =
                    reservations.Count(x =>
                        x.ReservationDate.Year == today.Year &&
                        x.ReservationDate.Month == today.Month),

                TotalUserCount =
                    users.Count,

                PendingQuestionCount =
                    questions.Count(x =>
                        !x.IsAnswered),

                TotalRevenue =
                    reservations
                        .Where(x =>
                            x.Status == "Onaylandı")
                        .Sum(x =>
                            x.TotalPrice),

                TotalCapacity =
                    tours.Sum(x =>
                        x.TourDates?
                            .Sum(d => d.Capacity)
                        ?? 0)
            };


            // SON 6 AYLIK GRAFİK

            var culture =
                CultureInfo.GetCultureInfo("tr-TR");


            for (int i = 5; i >= 0; i--)
            {
                var month =
                    today.AddMonths(-i);


                model.ChartLabels.Add(
                    month.ToString(
                        "MMM yyyy",
                        culture)
                );


                var monthStatistic =
                    monthlyStatistics
                        .FirstOrDefault(x =>
                            x.Year == month.Year &&
                            x.Month == month.Month);


                model.ChartReservationCounts.Add(
                    monthStatistic?
                        .ReservationCount
                    ?? 0
                );
            }


            // EN ÇOK REZERVASYON ALAN 5 TUR

            foreach (var statistic in top5TourStatistics)
            {
                var tour =
                    tours.FirstOrDefault(x =>
                        x.Id ==
                        statistic.TourId);


                if (tour == null)
                {
                    continue;
                }


                model.PopularTours.Add(
                    new AdminDashboardPopularTourViewModel
                    {
                        TourId =
                            tour.Id,

                        TourName =
                            tour.TourName,

                        CoverImageUrl =
                            tour.CoverImageUrl,

                        ReservationCount =
                            statistic.ReservationCount
                    }
                );
            }


            // SON 5 REZERVASYON

            var recentReservations =
                reservations
                    .OrderByDescending(x =>
                        x.ReservationDate)
                    .Take(5)
                    .ToList();


            foreach (var reservation in recentReservations)
            {
                var tour =
                    tours.FirstOrDefault(x =>
                        x.Id ==
                        reservation.TourId);


                var user =
                    users.FirstOrDefault(x =>
                        x.Id ==
                        reservation.UserId);


                model.RecentReservations.Add(
                    new AdminDashboardReservationViewModel
                    {
                        Id =
                            reservation.Id,

                        UserId =
                            reservation.UserId,

                        UserName =
                            user?.NameSurname
                            ?? "Kullanıcı Bulunamadı",

                        TourName =
                            tour?.TourName
                            ?? "Tur Bulunamadı",

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