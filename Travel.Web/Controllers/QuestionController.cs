using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Travel.Web.DTOs.QuestionDtos;
using Travel.Web.Services.QuestionService;

namespace Travel.Web.Controllers
{
    public class QuestionController : Controller
    {
        private readonly IQuestionService _questionService;


        public QuestionController(
            IQuestionService questionService)
        {
            _questionService =
                questionService;
        }


        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(
            CreateQuestionDto createQuestionDto)
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


            createQuestionDto.UserId =
                userId;


            if (string.IsNullOrWhiteSpace(
                createQuestionDto.QuestionText))
            {
                TempData["QuestionError"] =
                    "Soru boş bırakılamaz.";

                return RedirectToAction(
                    "Details",
                    "Tour",
                    new
                    {
                        id = createQuestionDto.TourId
                    });
            }


            await _questionService
                .CreateAsync(createQuestionDto);


            TempData["QuestionSuccess"] =
                "Sorunuz gönderildi. Cevaplandıktan sonra burada yayınlanacaktır.";


            return RedirectToAction(
                "Details",
                "Tour",
                new
                {
                    id = createQuestionDto.TourId
                });
        }
    }
}