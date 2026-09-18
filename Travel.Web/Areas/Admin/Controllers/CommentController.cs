using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Travel.Web.Services.CommentService;

namespace Travel.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class CommentController : Controller
    {
        private readonly ICommentService _commentService;

        public CommentController(ICommentService commentService)
        {
            _commentService = commentService;
        }


        public async Task<IActionResult> Index()
        {
            var comments =
                await _commentService.GetAllAsync();

            return View(comments);
        }


        public async Task<IActionResult> Approve(string id)
        {
            await _commentService.ApproveAsync(id);

            return RedirectToAction(nameof(Index));
        }


        public async Task<IActionResult> Delete(string id)
        {
            await _commentService.DeleteAsync(id);

            return RedirectToAction(nameof(Index));
        }
    }
}