using MultiShop.Basket.Dtos;       
using MultiShop.Basket.Settings;  
using System.Text.Json;            

namespace MultiShop.Basket.Services
{
    public class BasketService : IBasketService
    {
        private readonly RedisService _redisService; 

        public BasketService(RedisService redisService)
        {
            _redisService = redisService;
        }

        // Kullanıcının sepetini siler
        public async Task DeleteBasket(string userId)
        {
            // Redis'te userId anahtarını sil
            await _redisService.GetDb().KeyDeleteAsync(userId);
        }

        // Kullanıcının sepetini getirir
        public async Task<BasketTotalDto> GetBasket(string userId)
        {
            // Redis'ten userId anahtarına karşılık gelen JSON string veriyi çek
            var exitBasket = await _redisService.GetDb().StringGetAsync(userId);

            // JSON string veriyi BasketTotalDto nesnesine dönüştür
            return JsonSerializer.Deserialize<BasketTotalDto>(exitBasket!)!;
        }

        // Kullanıcının sepetini kaydeder
        public async Task SaveBasket(BasketTotalDto basketTotalDto)
        {
            // Sepet nesnesini JSON'a çevir ve Redis'e kaydet
            // TimeSpan.FromDays(30) → Verinin Redis'te 30 gün saklanmasını sağlar
            await _redisService.GetDb().StringSetAsync(
                basketTotalDto.UserId,                         // Anahtar: Kullanıcı ID'si
                JsonSerializer.Serialize(basketTotalDto),      // Değer: JSON string
                TimeSpan.FromDays(30)                          // Expire süresi: 30 gün
            );
        }
    }
}
