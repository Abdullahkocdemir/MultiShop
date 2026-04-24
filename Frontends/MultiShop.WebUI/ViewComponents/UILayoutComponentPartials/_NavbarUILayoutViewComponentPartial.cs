using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MultiShop.DTOLayer.CatalogDTOs.CategoryDTO;
using MultiShop.DTOLayer.CatalogDTOs.ProductDTO;
using Newtonsoft.Json;

namespace MultiShop.WebUI.ViewComponents.UILayoutComponentPartials
{
    public class _NavbarUILayoutViewComponentPartial : ViewComponent
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public _NavbarUILayoutViewComponentPartial(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        public async Task<IViewComponentResult> InvokeAsync ()
        {
            var client = _httpClientFactory.CreateClient();

            var responseMessage = await client.GetAsync("https://localhost:7001/api/Categories");
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<ResultCategoryDTO>>(jsonData);

                return View(values);
            }
            return View();
        }
    }
}
