using Microsoft.AspNetCore.Mvc;
using MultiShop.DTOLayer.CatalogDTOs.ProductDTO;
using MultiShop.WebUI.Services.CatalogService.ProductService;

namespace MultiShop.WebUI.ViewComponents.ProductListComponentPartials
{
    public class _ProductListViewComponentPartial : ViewComponent
    {
        private readonly IProductService _productService;

        public _ProductListViewComponentPartial(IProductService productService)
        {
            _productService = productService;
        }

        public async Task<IViewComponentResult> InvokeAsync(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                // Eğer kategori ID boşsa tüm ürünleri veya boş liste dönebilirsin.
                // Genelde kategori seçilmediyse boş liste veya öne çıkanlar döner.
                return View(new List<ResultProductWithCategoryDTO>());
            }

            // Kendi yazdığın servis metodunu çağırıyoruz
            var values = await _productService.GetListCategoryOrProduct(id);

            return View(values);
        }
    }
}