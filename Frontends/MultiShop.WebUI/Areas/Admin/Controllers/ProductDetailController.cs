using Microsoft.AspNetCore.Mvc;
using MultiShop.DTOLayer.CatalogDTOs.ProductDetailDTO;
using Newtonsoft.Json;
using System.Text;

namespace MultiShop.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductDetailController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public ProductDetailController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [HttpGet]
        public async Task<IActionResult> Index(string id)
        {
            ViewBag.v1 = id; // Ürün ID'sini saklayalım
            var client = _httpClientFactory.CreateClient();
            // API'den ürün ID'sine göre detayı getiren endpoint'i çağırıyoruz
            var responseMessage = await client.GetAsync($"https://localhost:7001/api/ProductDetails/GetProductDetailByProductId?id={id}");

            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<ResultProductDetailDTO>(jsonData);
                return View(values);
            }

            return View(new ResultProductDetailDTO { ProductId = id });
        }

        [HttpPost]
        public async Task<IActionResult> Index(ResultProductDetailDTO model)
        {
            var client = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(model);
            var content = new StringContent(jsonData, Encoding.UTF8, "application/json");

            HttpResponseMessage response;

            if (!string.IsNullOrEmpty(model.ProductDetailId))
            {
                response = await client.PutAsync("https://localhost:7001/api/ProductDetails", content);
            }
            else
            {
                response = await client.PostAsync("https://localhost:7001/api/ProductDetails", content);
            }

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index", "Product", new { area = "Admin" });
            }
            return View(model);
        }
    }
}