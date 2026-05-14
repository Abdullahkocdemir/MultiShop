using MultiShop.DTOLayer.BasketDTOs;

namespace MultiShop.WebUI.Services.BasketService
{
    public interface IBasketService
    {
        Task<BasketTotalDTO> GetBasket();
        Task SaveBasket(BasketTotalDTO basketTotalDTO);
        Task DeleteBasket(string userId);
        Task AddBasketItem(BasketItemDTO basketItemDTO);
        Task<bool> RemoveBasketItem(string productId);
    }
}
