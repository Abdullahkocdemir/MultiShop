using MultiShop.DTOLayer.BasketDTOs;
using System.Net.Http.Json;

namespace MultiShop.WebUI.Services.BasketService
{
    public class BasketService : IBasketService
    {
        private readonly HttpClient _httpClient;

        public BasketService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<BasketTotalDTO> GetBasket()
        {
            var response = await _httpClient.GetAsync("baskets");
            return await response.Content.ReadFromJsonAsync<BasketTotalDTO>();
        }

        public async Task SaveBasket(BasketTotalDTO basketTotalDTO)
        {
            await _httpClient.PostAsJsonAsync("baskets", basketTotalDTO);
        }

        public async Task DeleteBasket(string userId)
        {
            await _httpClient.DeleteAsync("baskets");
        }

        public async Task AddBasketItem(BasketItemDTO basketItemDTO)
        {
            // HATA DÜZELTME: await eklendi
            var values = await GetBasket();

            if (values == null)
            {
                // Sepet yoksa yeni oluştur
                values = new BasketTotalDTO { BasketItems = new List<BasketItemDTO>() };
            }

            if (values.BasketItems == null)
            {
                values.BasketItems = new List<BasketItemDTO>();
            }

            // Ürün sepette yoksa ekle, varsa miktarı artırabilirsin (tercihe bağlı)
            if (!values.BasketItems.Any(x => x.ProductId == basketItemDTO.ProductId))
            {
                values.BasketItems.Add(basketItemDTO);
            }
            else
            {
                // Opsiyonel: Ürün varsa miktarını artır
                var existingItem = values.BasketItems.FirstOrDefault(x => x.ProductId == basketItemDTO.ProductId);
                existingItem.Quantity += basketItemDTO.Quantity;
            }

            await SaveBasket(values);
        }

        public async Task<bool> RemoveBasketItem(string productId)
        {
            // HATA DÜZELTME: await eklendi
            var values = await GetBasket();

            if (values == null || values.BasketItems == null) return false;

            var deletedItem = values.BasketItems.FirstOrDefault(x => x.ProductId == productId);
            if (deletedItem == null) return false;

            var result = values.BasketItems.Remove(deletedItem);

            // HATA DÜZELTME: await eklendi
            await SaveBasket(values);
            return result;
        }
    }
}