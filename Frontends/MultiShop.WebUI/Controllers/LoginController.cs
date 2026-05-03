using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using MultiShop.DTOLayer.IdentityDTOs.LoginDTO;
using MultiShop.WebUI.Models;
using MultiShop.WebUI.Services;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text.Json;

namespace MultiShop.WebUI.Controllers
{
    public class LoginController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILoginService _loginService;
        public LoginController(IHttpClientFactory httpClientFactory, ILoginService loginService)
        {
            _httpClientFactory = httpClientFactory;
            _loginService = loginService;
        }
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(CreateLoginDTO createLoginDTO)
        {
            var clinet = _httpClientFactory.CreateClient();
            var content = new StringContent(JsonSerializer.Serialize(createLoginDTO), System.Text.Encoding.UTF8, "application/json");
            var response = await clinet.PostAsync("https://localhost:5001/api/Logins", content);
            if (response.IsSuccessStatusCode)
            {
                var jsonData = await response.Content.ReadAsStringAsync();
                var tokenModel = JsonSerializer.Deserialize<JwtResponseModel>(jsonData,new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                });
                if (tokenModel != null)
                {
                    JwtSecurityTokenHandler handler= new JwtSecurityTokenHandler();
                    var jwtToken = handler.ReadJwtToken(tokenModel.Token);
                    var claims = jwtToken.Claims.ToList();
                    if(tokenModel!=null)
                    {
                        claims.Add(new Claim("multishoptoken", tokenModel.Token));
                        var claimsIdentity = new ClaimsIdentity(claims, JwtBearerDefaults.AuthenticationScheme);
                        var authProperties = new Microsoft.AspNetCore.Authentication.AuthenticationProperties
                        {
                            IsPersistent = true,
                            ExpiresUtc = tokenModel.ExpireDate,
                        };
                        await HttpContext.SignInAsync(JwtBearerDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProperties);
                        var id = _loginService.GetUserId;
                        return RedirectToAction("Index", "Default");
                    }
                }

            }
            if (string.IsNullOrEmpty(createLoginDTO.UserName) || string.IsNullOrEmpty(createLoginDTO.Password))
            {
                TempData["ErrorMessage"] = "Kullanıcı adı veya şifre hatalı!";
                return View();
            }
            return View();
        }
    }
}