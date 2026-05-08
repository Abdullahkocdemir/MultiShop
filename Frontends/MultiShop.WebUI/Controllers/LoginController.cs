using Microsoft.AspNetCore.Mvc;
using MultiShop.DTOLayer.IdentityDTOs.LoginDTO;
using MultiShop.WebUI.Services.Interfaces;

namespace MultiShop.WebUI.Controllers
{
    public class LoginController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILoginService _loginService;
        private readonly IIdentityService _identityService;
        public LoginController(IHttpClientFactory httpClientFactory, ILoginService loginService, IIdentityService identityService)
        {
            _httpClientFactory = httpClientFactory;
            _loginService = loginService;
            _identityService = identityService;
        }
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(SignInDTO signUpDTO)
        {
            var token = await _identityService.SignInAsync(signUpDTO);
            return RedirectToAction("Deneme2", "Test");
            //return View();
        }


        //[HttpGet]
        //public async Task<IActionResult> SignUp()
        //{
        //    return RedirectToAction("Index", "Login");
        //}
        //[HttpPost]
        //public async Task<IActionResult> SignUp(SignInDTO signUpDTO)
        //{

        //    //signUpDTO.UserName = "samet01";
        //    //signUpDTO.Password = "123456aA*";

        //    signUpDTO.UserName = "Apo01";
        //    signUpDTO.Password = "123456aA*";

        //    var token = await _identityService.SignInAsync(signUpDTO);
        //    return RedirectToAction("Index", "User");
        //}

    }
}