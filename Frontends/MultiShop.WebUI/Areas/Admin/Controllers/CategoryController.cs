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
        public CategoryController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> Index()
        {
            ViewBag.v1 = "Ana SAyfa";
            ViewBag.v2 = "Kategoriler";
            ViewBag.v3 = "Kategori Listesi";
            ViewBag.Title = "Kategori İşlemleri";

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
        public IActionResult CreateCategory()
        {
            ViewBag.v1 = "Ana SAyfa";
            ViewBag.v2 = "Kategoriler";
            ViewBag.v3 = "Kategori Ekleme";
            ViewBag.Title = "Kategori İşlemleri";
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateCategory([FromForm] CreateCategoryDTO createCategoryDTO)
        {

            if (ModelState.IsValid)
            {
                var client = _httpClientFactory.CreateClient();
                var contentCategoryCreate = JsonConvert.SerializeObject(createCategoryDTO);
                StringContent stringContent = new StringContent(contentCategoryCreate, Encoding.UTF8, "application/json");

                var responseMessage = await client.PostAsync("https://localhost:7001/api/Categories", stringContent);

                if (responseMessage.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(createCategoryDTO);
        }


        [HttpGet]
        public async Task<IActionResult> UpdateCategory(string id)
        {
            ViewBag.v1 = "Ana SAyfa";
            ViewBag.v2 = "Kategoriler";
            ViewBag.v3 = "Kategori Güncellme";
            ViewBag.Title = "Kategori İşlemleri";
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
        public async Task<IActionResult> UpdateCategory(UpdateCategoryDTO updateCategoryDTO)
        {
            if (ModelState.IsValid)
            {
                var client = _httpClientFactory.CreateClient();
                var contentCategoryCreate = JsonConvert.SerializeObject(updateCategoryDTO);
                StringContent stringContent = new StringContent(contentCategoryCreate, Encoding.UTF8, "application/json");

                var responseMessage = await client.PutAsync("https://localhost:7001/api/Categories", stringContent);

                if (responseMessage.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            return View();
        }
        public async Task<IActionResult> DeleteCategory(string id)
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.DeleteAsync($"https://localhost:7001/api/Categories?id={id}");

            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }

            return RedirectToAction("Index");
        }

    }
}
