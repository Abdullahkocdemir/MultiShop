using MultiShop.DTOLayer.IdentityDTOs.LoginDTO;

namespace MultiShop.WebUI.Services.Interfaces
{
    public interface IIdentityService
    {
        Task<bool> SignInAsync(SignInDTO signUpDTO);
        Task<bool> GetRefreshTokenAsync();
    }
}
