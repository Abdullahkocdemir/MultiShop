using MultiShop.DTOLayer.DiscountDTOs;

namespace MultiShop.WebUI.Services.DiscountService
{
    public class DiscountService : IDiscountService
    {
        private readonly HttpClient _httpClient;
        public DiscountService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<GetByCodeDetailDiscountDTO> GetDiscountCode(string code)
        {
            code = "asd";
            var responseMessage = await _httpClient.GetAsync($"discounts/GetCodeByDetail?code={code}");
            var values = await responseMessage.Content.ReadFromJsonAsync<GetByCodeDetailDiscountDTO>();


            return values;
        }
    }
}
