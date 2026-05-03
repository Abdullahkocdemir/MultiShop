using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MultiShop.IdentityServer.Dtos;
using MultiShop.IdentityServer.Models;
using MultiShop.IdentityServer.Tools;
using System.Threading.Tasks;

namespace MultiShop.IdentityServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginsController : ControllerBase
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;

        public LoginsController(SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }

        [HttpPost]
        public async Task<IActionResult> UserLogin(UserLoginDTO userLoginDTO)
        {
            var result = await _signInManager.PasswordSignInAsync(userLoginDTO.UserName, userLoginDTO.Password, false, false);

            if (result.Succeeded)
            {
                var user = await _userManager.FindByNameAsync(userLoginDTO.UserName);

                // Kullanıcı bilgilerini token generator'a gönderiyoruz
                var model = new GetCheckAppUserViewModel
                {
                    Id = user.Id,
                    UserName = user.UserName,
                    Role = "Admin", // Not: Rol yönetimi eklendiğinde buradan dinamik çekilmeli
                    IsExist = true
                };

                var tokenResponse = JwtTokenGenerator.GenerateToken(model);

                return Ok(tokenResponse);
            }
            else
            {
                return BadRequest("Kullanıcı Adı veya Şifresi Yanlış.!!");
            }
        }
    }
}