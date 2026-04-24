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
        public async Task<IActionResult> ProductImageDetail(string id)
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync($"https://localhost:7001/api/ProductImages/{id}");

            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<ResultProductImageDTO>(jsonData);
                if (values != null) return View(values);
            }

            // Hata almamak için: Görsel yoksa bile ProductId dolu boş model dönüyoruz
            return View(new ResultProductImageDTO { ProductId = id });
        }

        [HttpPost]
        public async Task<IActionResult> ProductImageDetail(ResultProductImageDTO model, IFormFile[] files)
        {
            var client = _httpClientFactory.CreateClient();
            // Mevcut görselleri koru, yeni yüklenenler varsa onların üzerine yazacağız
            string[] uploadedPaths = new string[4] { model.Image1 ?? "", model.Image2 ?? "", model.Image3 ?? "", model.Image4 ?? "" };

            if (files != null && files.Length > 0)
            {
                var uploadFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/ProductImages");
                if (!Directory.Exists(uploadFolder)) Directory.CreateDirectory(uploadFolder);

                for (int i = 0; i < files.Length; i++)
                {
                    if (i >= 4) break;

                    var extension = Path.GetExtension(files[i].FileName);
                    var imagename = Guid.NewGuid() + extension;
                    var saveLocation = Path.Combine(uploadFolder, imagename);

                    using var stream = new FileStream(saveLocation, FileMode.Create);
                    await files[i].CopyToAsync(stream);
                    uploadedPaths[i] = "/images/ProductImages/" + imagename;
                }
            }

            model.Image1 = uploadedPaths[0];
            model.Image2 = uploadedPaths[1];
            model.Image3 = uploadedPaths[2];
            model.Image4 = uploadedPaths[3];

            var jsonData = JsonConvert.SerializeObject(model);
            var content = new StringContent(jsonData, Encoding.UTF8, "application/json");

            if (string.IsNullOrEmpty(model.ProductImageId))
            {
                // Create
                var values = await client.PostAsync("https://localhost:7001/api/ProductImages", content);
            }
            else
            {
                // Update
                var values = await client.PutAsync("https://localhost:7001/api/ProductImages", content);
            }

            return RedirectToAction("Index", "Product", new { area = "Admin" });
        }

        public async Task<IActionResult> DeleteImages(string id, string productId)
        {
            var client = _httpClientFactory.CreateClient();
            await client.DeleteAsync($"https://localhost:7001/api/ProductImages?id={id}");
            return RedirectToAction("ProductImageDetail", new { id = productId });
        }
    }
}