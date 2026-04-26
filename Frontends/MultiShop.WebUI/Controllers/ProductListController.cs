using Microsoft.AspNetCore.Mvc;
using MultiShop.DTOLayer.CommentDTOs;
using Newtonsoft.Json;
using System.Text;

namespace MultiShop.WebUI.Controllers
{
    public class ProductListController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public ProductListController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public IActionResult Index(string id)
        {
            ViewBag.i = id;
            return View();
        }
        public IActionResult ProductDetail(string id)
        {
            ViewBag.i = id;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddComment(CreateCommentDTO createCommentDto)
        {
            createCommentDto.NameSurname = "Abdullah Koçdemir";
            createCommentDto.Email = "kcdmirapo96@gmail.com";
            createCommentDto.Status = true;
            createCommentDto.CreatedDate = DateTime.Now; // API tarafında da set edilebilir
            createCommentDto.ImageUrl = "string";
            createCommentDto.UserCommentId = "string";

            var client = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(createCommentDto);
            StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");

            var responseMessage = await client.PostAsync("https://localhost:7006/api/Comments", stringContent);

            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("ProductDetail", "ProductList", new { id = createCommentDto.ProductId });
            }

            // Hata durumunda boş View() dönmek yerine yine detay sayfasına yönlendirin.
            // İsterseniz buraya bir hata mesajı (TempData) ekleyebilirsiniz.
            return RedirectToAction("ProductDetail", "ProductList", new { id = createCommentDto.ProductId });
        }
    }
}
