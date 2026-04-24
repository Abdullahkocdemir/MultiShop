using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MultiShop.DTOLayer.CatalogDTOs.ProductDTO;
using Newtonsoft.Json;

namespace MultiShop.WebUI.ViewComponents.ProductListComponentPartials
{
    public class _ProductListViewComponentPartial : ViewComponent
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public _ProductListViewComponentPartial(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        public async Task<IViewComponentResult> InvokeAsync(string id)
        {
            // ID boş gelirse API'ye istek atmadan boş bir view dönebiliriz 
            // veya API'ye gitmeden önce kontrol yapabiliriz.
            if (string.IsNullOrEmpty(id))
            {
                // Eğer tüm ürünleri getirmek istiyorsan burayı ona göre yönetmelisin
                // Şimdilik hata almamak için API'ye gitmesini engelleyelim:
                return View(new List<ResultProductWithCategoryDTO>());
            }

            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync($"https://localhost:7001/api/Products/GetListCategoryOrProduct/{id}");

            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<ResultProductWithCategoryDTO>>(jsonData);
                return View(values);
            }
            return View();
        }
    }
}
