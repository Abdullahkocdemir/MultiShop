using Microsoft.AspNetCore.Mvc;
using MultiShop.WebUI.Services.CatalogService.BrandService;
using System.Threading.Tasks;

namespace MultiShop.WebUI.ViewComponents.DefaultComponentPartials
{
    public class _VendorDefaultViewComponentPartial : ViewComponent
    {
        private readonly IBrandService _brandService;

        public _VendorDefaultViewComponentPartial(IBrandService brandService)
        {
            _brandService = brandService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var rvalues=await _brandService.GetAllBrandAsync();
            return View(rvalues);
        }
    }
}
