using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Travel.Web.DTOs.TourDtos;
using Travel.Web.Services.FavoriteServices;
using Travel.Web.Services.TourService;
using Travel.Web.Services.TourService;

namespace Travel.Web.Controllers
{
    [Authorize]
    public class FavoriteController : Controller
    {
        private readonly IFavoriteService _favoriteService;
        private readonly ITourService _tourService;

        public FavoriteController(
            IFavoriteService favoriteService,
            ITourService tourService)
        {
            _favoriteService = favoriteService;
            _tourService = tourService;
        }


        public async Task<IActionResult> Index()
        {
            var userId = User.FindFirstValue(
                ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(userId))
            {
                return RedirectToAction(
                    "Login",
                    "Account");
            }


            var favorites =
                await _favoriteService.GetByUserIdAsync(userId);

            var tours =
                await _tourService.GetAllAsync();


            var favoriteTours =
                new List<ResultTourDto>();


            foreach (var favorite in favorites)
            {
                var tour = tours.FirstOrDefault(
                    x => x.Id == favorite.TourId);

                if (tour != null)
                {
                    favoriteTours.Add(tour);
                }
            }


            return View(favoriteTours);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(string tourId)
        {
            var userId = User.FindFirstValue(
                ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(userId))
            {
                return RedirectToAction(
                    "Login",
                    "Account");
            }


            await _favoriteService.AddAsync(
                userId,
                tourId);


            return RedirectToAction(
                "Details",
                "Tour",
                new { id = tourId });
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Remove(string tourId)
        {
            var userId = User.FindFirstValue(
                ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(userId))
            {
                return RedirectToAction(
                    "Login",
                    "Account");
            }


            await _favoriteService.RemoveAsync(
                userId,
                tourId);


            return RedirectToAction(nameof(Index));
        }
    }
}