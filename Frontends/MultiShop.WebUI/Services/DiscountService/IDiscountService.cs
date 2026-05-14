using MultiShop.DTOLayer.DiscountDTOs;

namespace MultiShop.WebUI.Services.DiscountService
{
    public interface IDiscountService
    {
        Task<GetByCodeDetailDiscountDTO> GetDiscountCode(string code);
    }
}
