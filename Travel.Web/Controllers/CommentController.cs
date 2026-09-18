using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Travel.Web.DTOs.CommentDtos;
using Travel.Web.Services.CommentService;
using Travel.Web.Services.TourService;

namespace Travel.Web.Controllers
{
    public class CommentController : Controller
    {
        private readonly ICommentService _commentService;
        private readonly ITourService _tourService;


        public CommentController(
            ICommentService commentService,
            ITourService tourService)
        {
            _commentService = commentService;
            _tourService = tourService;
        }


        // YORUMLAR SAYFASI
        public async Task<IActionResult> Index()
        {
            var comments =
                await _commentService.GetAllAsync();

            var tours =
                await _tourService.GetAllAsync();


            var approvedComments = comments
                .Where(x => x.IsApproved)
                .OrderByDescending(x => x.CreatedDate)
                .ToList();


            ViewBag.Tours = tours;


            return View(approvedComments);
        }


        // YORUM EKLE
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(
            CreateCommentDto createCommentDto)
        {
            var userId =
                User.FindFirstValue(
                    ClaimTypes.NameIdentifier);


            if (string.IsNullOrEmpty(userId))
            {
                return RedirectToAction(
                    "Login",
                    "Account");
            }


            createCommentDto.UserId = userId;


            if (string.IsNullOrWhiteSpace(
                createCommentDto.CommentText))
            {
                TempData["ErrorMessage"] =
                    "Yorum boş bırakılamaz.";

                return RedirectToAction(
                    "Details",
                    "Tour",
                    new { id = createCommentDto.TourId });
            }


            if (createCommentDto.Rating < 1 ||
                createCommentDto.Rating > 5)
            {
                TempData["ErrorMessage"] =
                    "Puan 1 ile 5 arasında olmalıdır.";

                return RedirectToAction(
                    "Details",
                    "Tour",
                    new { id = createCommentDto.TourId });
            }


            await _commentService
                .CreateAsync(createCommentDto);


            TempData["SuccessMessage"] =
                "Yorumunuz gönderildi. Onaylandıktan sonra yayınlanacaktır.";


            return RedirectToAction(
                "Details",
                "Tour",
                new { id = createCommentDto.TourId });
        }
    }
}