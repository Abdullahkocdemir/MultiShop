using Microsoft.AspNetCore.Mvc;
using MultiShop.DTOLayer.CatalogDTOs.CategoryDTO;
using Newtonsoft.Json;
using System.Text;

namespace MultiShop.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public CategoryController(IHttpClientFactory httpClientFactory, IWebHostEnvironment webHostEnvironment)
        {
            _httpClientFactory = httpClientFactory;
            _webHostEnvironment = webHostEnvironment;
        }

        #region Yardımcı Metod (Dosya Yükleme)
        /// <summary>
        /// Resmi sunucuya kaydeder ve dosya yolunu döner.
        /// </summary>
        private async Task<string> UploadImageAsync(IFormFile imageFile)
        {
            if (imageFile == null || imageFile.Length == 0) return null;

            // 1. Klasör yolunu belirle ve yoksa oluştur
            var wwwrootPath = _webHostEnvironment.WebRootPath;
            var folderPath = Path.Combine(wwwrootPath, "Category");

            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }

            // 2. Benzersiz dosya ismi oluştur
            var extension = Path.GetExtension(imageFile.FileName);
            var imageName = Guid.NewGuid() + extension;
            var savePath = Path.Combine(folderPath, imageName);

            // 3. Dosyayı kaydet
            using (var stream = new FileStream(savePath, FileMode.Create))
            {
                await imageFile.CopyToAsync(stream);
            }

            // 4. Veritabanına kaydedilecek yolu dön
            return "/Category/" + imageName;
        }
        #endregion

        public async Task<IActionResult> Index()
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync("https://localhost:7001/api/Categories");
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<ResultCategoryDTO>>(jsonData);
                return View(values);
            }
            return View();
        }

        [HttpGet]
        public IActionResult CreateCategory() => View();

        [HttpPost]
        public async Task<IActionResult> CreateCategory(CreateCategoryDTO createCategoryDTO, IFormFile ImageFile)
        {
            // Yardımcı metodu kullanarak resmi yükle
            var uploadedPath = await UploadImageAsync(ImageFile);
            if (uploadedPath != null)
            {
                createCategoryDTO.ImageUrl = uploadedPath;
            }

            var client = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(createCategoryDTO);
            var stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var responseMessage = await client.PostAsync("https://localhost:7001/api/Categories", stringContent);

            if (responseMessage.IsSuccessStatusCode) return RedirectToAction("Index");
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> UpdateCategory(string id)
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync($"https://localhost:7001/api/Categories/{id}");
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<UpdateCategoryDTO>(jsonData);
                return View(values);
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> UpdateCategory(UpdateCategoryDTO updateCategoryDTO, IFormFile ImageFile)
        {
            // Yeni bir resim seçilmişse yükle, seçilmemişse mevcut resim kalır
            var uploadedPath = await UploadImageAsync(ImageFile);
            if (uploadedPath != null)
            {
                updateCategoryDTO.ImageUrl = uploadedPath;
            }

            var client = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(updateCategoryDTO);
            var stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var responseMessage = await client.PutAsync("https://localhost:7001/api/Categories", stringContent);

            if (responseMessage.IsSuccessStatusCode) return RedirectToAction("Index");
            return View();
        }

        public async Task<IActionResult> DeleteCategory(string id)
        {
            var client = _httpClientFactory.CreateClient();
            await client.DeleteAsync($"https://localhost:7001/api/Categories?id={id}");
            return RedirectToAction("Index");
        }
    }
}