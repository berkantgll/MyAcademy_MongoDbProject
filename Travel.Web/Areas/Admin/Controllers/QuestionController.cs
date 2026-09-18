using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Travel.Web.DTOs.QuestionDtos;
using Travel.Web.Services.QuestionService;
using Travel.Web.Services.TourService;

namespace Travel.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class QuestionController : Controller
    {
        private readonly IQuestionService _questionService;
        private readonly ITourService _tourService;

        public QuestionController(
            IQuestionService questionService,
            ITourService tourService)
        {
            _questionService = questionService;
            _tourService = tourService;
        }

        public async Task<IActionResult> Index()
        {
            var questions =
                await _questionService.GetAllAsync();

            var tours =
                await _tourService.GetAllAsync();

            ViewBag.Tours = tours;

            var orderedQuestions = questions
                .OrderBy(x => x.IsAnswered)
                .ThenByDescending(x => x.QuestionTime)
                .ToList();

            return View(orderedQuestions);
        }

        [HttpGet]
        public async Task<IActionResult> Answer(string id)
        {
            var question =
                await _questionService.GetByIdAsync(id);

            var model =
                new UpdateQuestionDto
                {
                    Id = question.Id,
                    AnswerText = question.AnswerText
                };

            ViewBag.QuestionText =
                question.QuestionText;

            var tour =
                await _tourService.GetByIdAsync(
                    question.TourId);

            ViewBag.TourName =
                tour?.TourName ?? "-";

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Answer(
            UpdateQuestionDto updateQuestionDto)
        {
            if (string.IsNullOrWhiteSpace(
                updateQuestionDto.AnswerText))
            {
                ModelState.AddModelError(
                    "AnswerText",
                    "Cevap boş bırakılamaz.");
            }

            if (!ModelState.IsValid)
            {
                var question =
                    await _questionService
                        .GetByIdAsync(updateQuestionDto.Id);

                ViewBag.QuestionText =
                    question.QuestionText;

                var tour =
                    await _tourService.GetByIdAsync(
                        question.TourId);

                ViewBag.TourName =
                    tour?.TourName ?? "-";

                return View(updateQuestionDto);
            }

            await _questionService
                .UpdateAsync(updateQuestionDto);

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(string id)
        {
            await _questionService.DeleteAsync(id);

            return RedirectToAction(nameof(Index));
        }
    }
}