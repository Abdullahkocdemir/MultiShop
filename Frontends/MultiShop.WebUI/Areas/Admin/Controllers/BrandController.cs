using Microsoft.AspNetCore.Mvc;
using MultiShop.DTOLayer.CatalogDTOs.BrandDTO;
using Newtonsoft.Json;
using System.Text;

namespace MultiShop.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class BrandController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public BrandController(IHttpClientFactory httpClientFactory, IWebHostEnvironment webHostEnvironment)
        {
            _httpClientFactory = httpClientFactory;
            _webHostEnvironment = webHostEnvironment;
        }

        private async Task<string> UploadImageAsync(IFormFile imageFile)
        {
            if (imageFile == null || imageFile.Length == 0) return null;
            var wwwrootPath = _webHostEnvironment.WebRootPath;
            var folderPath = Path.Combine(wwwrootPath, "BrandImages");

            if (!Directory.Exists(folderPath)) Directory.CreateDirectory(folderPath);

            var extension = Path.GetExtension(imageFile.FileName);
            var imageName = Guid.NewGuid() + extension;
            var savePath = Path.Combine(folderPath, imageName);

            using (var stream = new FileStream(savePath, FileMode.Create))
            {
                await imageFile.CopyToAsync(stream);
            }
            return "/BrandImages/" + imageName;
        }

        public async Task<IActionResult> Index()
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync("https://localhost:7001/api/Brands");
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<ResultBrandDTO>>(jsonData);
                return View(values);
            }
            return View();
        }

        [HttpGet]
        public IActionResult CreateBrand() => View();

        [HttpPost]
        public async Task<IActionResult> CreateBrand(CreateBrandDTO createBrandDTO, IFormFile ImageFile)
        {
            var uploadedPath = await UploadImageAsync(ImageFile);
            createBrandDTO.ImageUrl = uploadedPath ?? "/BrandImages/no-brand.png";

            var client = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(createBrandDTO);
            var stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var responseMessage = await client.PostAsync("https://localhost:7001/api/Brands", stringContent);

            if (responseMessage.IsSuccessStatusCode) return RedirectToAction("Index");
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> UpdateBrand(string id)
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync($"https://localhost:7001/api/Brands/{id}");
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<UpdateBrandDTO>(jsonData);
                return View(values);
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> UpdateBrand(UpdateBrandDTO updateBrandDTO, IFormFile ImageFile)
        {
            // Yeni bir resim seçilmişse yükle
            var uploadedPath = await UploadImageAsync(ImageFile);
            if (uploadedPath != null)
            {
                updateBrandDTO.ImageUrl = uploadedPath;
            }

            var client = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(updateBrandDTO);
            var stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");

            // API'deki [HttpPut] metoduna istek atıyoruz
            var responseMessage = await client.PutAsync("https://localhost:7001/api/Brands", stringContent);

            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }

            return View(updateBrandDTO);
        }

        public async Task<IActionResult> DeleteBrand(string id)
        {
            var client = _httpClientFactory.CreateClient();
            await client.DeleteAsync($"https://localhost:7001/api/Brands?id={id}");
            return RedirectToAction("Index");
        }
    }
}