using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MultiShop.IdentityServern.Dtos;
using MultiShop.IdentityServern.Models;
using System.Threading.Tasks;

namespace MultiShop.IdentityServern.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegistersController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public RegistersController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }
        [HttpPost]
        public async Task<IActionResult> RegisterUser(UserRegisterDtos userRegisterDtos)
        {
            var value = new ApplicationUser()
            {
                UserName = userRegisterDtos.Username,
                Name = userRegisterDtos.Name,
                SurName = userRegisterDtos.Surname,
                Email = userRegisterDtos.Email
            };
            var result = await _userManager.CreateAsync(value, userRegisterDtos.Password);
            if (result.Succeeded)
            {
                return Ok("Kullanıcı Başarıyla Eklendi");
            }
            else
            {
                return Ok("Bir Hata Oluştu Tekrar Deneyiniz");
            }
        }
    }
}
