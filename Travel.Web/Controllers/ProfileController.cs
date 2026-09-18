using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Travel.Web.Services.CommentService;
using Travel.Web.Services.QuestionService;
using Travel.Web.Services.TourService;
using Travel.Web.Services.UserServices;

namespace Travel.Web.Controllers
{
    [Authorize]
    public class ProfileController : Controller
    {
        private readonly IUserService _userService;
        private readonly ICommentService _commentService;
        private readonly IQuestionService _questionService;
        private readonly ITourService _tourService;


        public ProfileController(
            IUserService userService,
            ICommentService commentService,
            IQuestionService questionService,
            ITourService tourService)
        {
            _userService = userService;
            _commentService = commentService;
            _questionService = questionService;
            _tourService = tourService;
        }


        public async Task<IActionResult> Index()
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


            var user =
                await _userService
                    .GetByIdAsync(userId);


            var comments =
                await _commentService
                    .GetAllAsync();


            var questions =
                await _questionService
                    .GetAllAsync();


            var tours =
                await _tourService
                    .GetAllAsync();


            var myComments = comments
                .Where(x => x.UserId == userId)
                .OrderByDescending(x => x.CreatedDate)
                .ToList();


            var myQuestions = questions
                .Where(x => x.UserId == userId)
                .OrderByDescending(x => x.QuestionTime)
                .ToList();


            ViewBag.MyComments =
                myComments;


            ViewBag.MyQuestions =
                myQuestions;


            ViewBag.Tours =
                tours;


            return View(user);
        }
    }
}