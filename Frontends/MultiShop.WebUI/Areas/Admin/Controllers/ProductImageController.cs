using Microsoft.AspNetCore.Mvc;
using MultiShop.DTOLayer.CatalogDTOs.ProductImageDTO;
using Newtonsoft.Json;
using System.Text;

namespace MultiShop.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductImageController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public ProductImageController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [HttpGet]
        public async Task<IActionResult> Index(string id)
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync($"https://localhost:7001/api/ProductImages/GetProductImagesByProductId?id={id}");
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<ResultProductImageDTO>(jsonData);
                return View(values);
            }

            return View(new ResultProductImageDTO { ProductId = id });
        }

        [HttpPost]
        public async Task<IActionResult> UploadImage(IFormFile file)
        {
            if (file == null || file.Length == 0) return BadRequest("Dosya seçilmedi.");

            var uploadFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/ProductImages");
            if (!Directory.Exists(uploadFolder)) Directory.CreateDirectory(uploadFolder);

            var imagename = Guid.NewGuid() + Path.GetExtension(file.FileName);
            var saveLocation = Path.Combine(uploadFolder, imagename);

            using var stream = new FileStream(saveLocation, FileMode.Create);
            await file.CopyToAsync(stream);

            return Ok(new { path = "/images/ProductImages/" + imagename });
        }

        [HttpPost]
        public async Task<IActionResult> CreateProductDetail(ResultProductImageDTO model)
        {
            var client = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(model);
            var content = new StringContent(jsonData, Encoding.UTF8, "application/json");

            HttpResponseMessage response;

            if (!string.IsNullOrEmpty(model.ProductImageId))
            {
                response = await client.PutAsync("https://localhost:7001/api/ProductImages", content);
            }
            else
            {
                response = await client.PostAsync("https://localhost:7001/api/ProductImages", content);
            }

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index", "Product", new { area = "Admin" });
            }
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> DeleteProductImage(string id)
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.DeleteAsync($"https://localhost:7001/api/ProductImages/{id}");
            if (responseMessage.IsSuccessStatusCode) return Ok();
            return BadRequest();
        }
    }
}