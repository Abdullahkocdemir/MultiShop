using Microsoft.AspNetCore.Mvc;
using MultiShop.DTOLayer.CatalogDTOs.ProductDetailDTO;
using MultiShop.WebUI.Services.CatalogService.IProductDetailService;

namespace MultiShop.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductDetailController : Controller
    {
        private readonly IProductDetailService _productDetailService;

        public ProductDetailController(IProductDetailService productDetailService)
        {
            _productDetailService = productDetailService;
        }

        [HttpGet]
        public async Task<IActionResult> Index(string id)
        {
            ViewBag.v1 = id;
            try
            {
                var values = await _productDetailService.GetByProductIdProductDetailAsync(id);
                // Servisten dönen GetByIdProductDetailDTO'yu View'ın beklediği Result modeline çevirelim
                var result = new ResultProductDetailDTO
                {
                    ProductDetailId = values.ProductDetailId,
                    ProductDescription = values.ProductDescription,
                    ProductInfo = values.ProductInfo,
                    ProductId = values.ProductId
                };
                return View(result);
            }
            catch
            {
                // Eğer detay henüz yoksa boş bir model döner
                return View(new ResultProductDetailDTO { ProductId = id });
            }
        }

        [HttpPost]
        public async Task<IActionResult> Index(ResultProductDetailDTO model)
        {
            if (!string.IsNullOrEmpty(model.ProductDetailId))
            {
                // Güncelleme işlemi için UpdateDTO eşlemesi
                var updateDto = new UpdateProductDetailDTO
                {
                    ProductDetailId = model.ProductDetailId,
                    ProductDescription = model.ProductDescription,
                    ProductInfo = model.ProductInfo,
                    ProductId = model.ProductId
                };
                await _productDetailService.UpdateProductDetailAsync(updateDto);
            }
            else
            {
                // Ekleme işlemi için CreateDTO eşlemesi
                var createDto = new CreateProductDetailDTO
                {
                    ProductDescription = model.ProductDescription,
                    ProductInfo = model.ProductInfo,
                    ProductId = model.ProductId
                };
                await _productDetailService.CreateProductDetailAsync(createDto);
            }

            return RedirectToAction("Index", "Product", new { area = "Admin" });
        }
    }
}