using MultiShop.Basket.Dtos;

namespace MultiShop.Basket.Services
{
    public interface IBasketService
    {
        // Burada Redis'ten sepeti alacak kodu yazılacak
        // Örnek olarak, sepeti Redis'ten alıp deserialize edebilirsiniz.
        Task<BasketTotalDto> GetBasket(string userId);
        Task SaveBasket(BasketTotalDto basketTotalDto);
        Task DeleteBasket(string userId);
    }
}
