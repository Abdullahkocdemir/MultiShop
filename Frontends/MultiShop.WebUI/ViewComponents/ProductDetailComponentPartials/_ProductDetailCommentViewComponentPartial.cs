using Microsoft.AspNetCore.Mvc;
using MultiShop.WebUI.Services.CommentService;

namespace MultiShop.WebUI.ViewComponents.ProductDetailComponentPartials
{
    public class _ProductDetailCommentViewComponentPartial : ViewComponent
    {
        private readonly ICommentService _commentService;
        public _ProductDetailCommentViewComponentPartial(ICommentService commentService)
        {
            _commentService = commentService;
        }
        public async Task<IViewComponentResult> InvokeAsync(string id)
        {
            ViewBag.productId = id;
            var comments = await _commentService.GetCommentsByProductIdAsync(id);
            return View(comments);

        }
    }
}