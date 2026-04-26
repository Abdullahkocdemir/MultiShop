using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MultiShop.Comment.Context;
using MultiShop.Comment.Entities;

namespace MultiShop.Comment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentsController : ControllerBase
    {
        private readonly CommentContext _context;

        public CommentsController(CommentContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> CommentList()
        {
            var values = await _context.UserCommnets.AsNoTracking().ToListAsync();
            return Ok(values);
        }

        [HttpPost]
        public async Task<IActionResult> CreateComment(UserComment comment)
        {
            // Eğer ID string ise ve boş geliyorsa yeni bir GUID atayalım
            if (string.IsNullOrEmpty(comment.UserCommentId) || comment.UserCommentId == "string")
            {
                comment.UserCommentId = Guid.NewGuid().ToString();
            }

            comment.CreatedDate = DateTime.Now;
            _context.UserCommnets.Add(comment);
            await _context.SaveChangesAsync();
            return Ok("Yorum başarıyla eklendi.");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteComment(string id)
        {
            var value = await _context.UserCommnets.FindAsync(id);
            if (value == null)
            {
                return NotFound("Silinecek yorum bulunamadı.");
            }

            _context.UserCommnets.Remove(value);
            await _context.SaveChangesAsync();
            return Ok("Yorum başarıyla silindi.");
        }

        [HttpPut]
        public async Task<IActionResult> UpdateComment(UserComment comment)
        {
            // Veritabanında bu yorumun var olduğundan emin olalım
            var existComment = await _context.UserCommnets.AsNoTracking().FirstOrDefaultAsync(x => x.UserCommentId == comment.UserCommentId);

            if (existComment == null)
            {
                return NotFound("Güncellenecek yorum bulunamadı.");
            }

            _context.UserCommnets.Update(comment);
            await _context.SaveChangesAsync();
            return Ok("Yorum başarıyla güncellendi.");
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCommentById(string id)
        {
            var value = await _context.UserCommnets.FindAsync(id);
            if (value == null)
            {
                return NotFound("Yorum bulunamadı.");
            }
            return Ok(value);
        }

        [HttpGet("GetCommentByProductId/{productId}")]
        public async Task<IActionResult> GetCommentByProductId(string productId)
        {
            var values = await _context.UserCommnets
                .Where(c => c.ProductId == productId)
                .AsNoTracking()
                .ToListAsync();
            return Ok(values);
        }
        [HttpGet("GetCommentCount")]
        public IActionResult GetCommentCount(string productId)
        {
            var values = _context.UserCommnets.Where(w => w.ProductId == productId).Count();
            return Ok(values);
        }
    }
}