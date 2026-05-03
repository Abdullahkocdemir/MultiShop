using Microsoft.AspNetCore.Mvc;
using MultiShop.DTOLayer.IdentityDTOs.RegisterDTO;
using System.Net.Http.Json;

namespace MultiShop.WebUI.Controllers
{
    public class RegisterController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public RegisterController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(CreateRegisterDTO createRegisterDTO)
        {
            // Şifre kontrolü
            if (createRegisterDTO.Password != createRegisterDTO.ConfirmPassword)
            {
                TempData["ErrorMessage"] = "Şifreler uyuşmuyor!";
                return View(createRegisterDTO);
            }

            var client = _httpClientFactory.CreateClient();
            var response = await client.PostAsJsonAsync("http://localhost:5001/api/Registers", createRegisterDTO);

            if (response.IsSuccessStatusCode)
            {
                TempData["SuccessMessage"] = "Kayıt Başarılı";
                return View();
            }

            TempData["ErrorMessage"] = "Kayıt işlemi başarısız oldu.";
            return View(createRegisterDTO);
        }
    }
}