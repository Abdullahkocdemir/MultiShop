using Microsoft.AspNetCore.Mvc;
using MultiShop.DTOLayer.CommentDTOs;
using Newtonsoft.Json;

namespace MultiShop.WebUI.ViewComponents.ProductDetailComponentPartials
{
    public class _ProductDetailCommentViewComponentPartial : ViewComponent
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public _ProductDetailCommentViewComponentPartial(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IViewComponentResult> InvokeAsync(string id)
        {
            // ViewBag.i veya id'yi buraya taşıyalım ki View'daki form da kullanabilsin
            ViewBag.productId = id;

            var client = _httpClientFactory.CreateClient();
            // Parametre isminin productId=id olduğundan emin ol
            var responseMessage = await client.GetAsync($"https://localhost:7006/api/Comments/GetCommentByProductId/{id}");

            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<ResultCommentDTO>>(jsonData);
                TempData["CommentCount"] = values.Count;
                return View(values);

            }
            TempData["CommentCount"] = 0;
            return View(new List<ResultCommentDTO>());
        }
    }
}