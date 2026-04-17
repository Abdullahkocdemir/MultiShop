using MultiShop.Basket.Dtos;
using MultiShop.Basket.Services;
using System.Text.Json;

public class BasketService : IBasketService
{
    private readonly RedisService _redisService;

    public BasketService(RedisService redisService)
    {
        _redisService = redisService;
    }

    public async Task<BasketTotalDTO> GetBasket(string userId)
    {
        var existBasket = await _redisService.GetDatabase().StringGetAsync(userId);

        if (string.IsNullOrEmpty(existBasket)) return null;

        return JsonSerializer.Deserialize<BasketTotalDTO>(existBasket);
    }

    public async Task SaveBasket(BasketTotalDTO basketTotalDTO)
    {
        // Burada return status demiyoruz, sadece işlemi bekliyoruz (await)
        await _redisService.GetDatabase().StringSetAsync(
            basketTotalDTO.UserId,
            JsonSerializer.Serialize(basketTotalDTO));
    }

    public async Task DeleteBasket(string userId)
    {
        // Burada da direkt await kullanarak bitiriyoruz
        await _redisService.GetDatabase().KeyDeleteAsync(userId);
    }
}