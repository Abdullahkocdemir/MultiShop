using Microsoft.AspNetCore.Mvc;
using MultiShop.DTOLayer.CatalogDTOs.ProductImageDTO;
using MultiShop.WebUI.Services.CatalogService.ProductImageService;

namespace MultiShop.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductImageController : Controller
    {
        private readonly IProductImageService _productImageService;

        public ProductImageController(IProductImageService productImageService)
        {
            _productImageService = productImageService;
        }

        [HttpGet]
        public async Task<IActionResult> Index(string id)
        {
            try
            {
                // API'den ürün ID'sine göre görselleri çekiyoruz
                var values = await _productImageService.GetByProductIdProductImageAsync(id);

                // ResultDTO'ya eşleyerek View'a gönderiyoruz
                var result = new ResultProductImageDTO
                {
                    ProductImageId = values.ProductImageId,
                    Image1 = values.Image1,
                    Image2 = values.Image2,
                    Image3 = values.Image3,
                    Image4 = values.Image4, // Modelinde Image4 varsa ekle
                    ProductId = values.ProductId
                };
                return View(result);
            }
            catch
            {
                // Eğer ürünün henüz görseli yoksa boş form açıyoruz
                return View(new ResultProductImageDTO { ProductId = id });
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateProductDetail(ResultProductImageDTO model)
        {
            if (!string.IsNullOrEmpty(model.ProductImageId))
            {
                // Güncelleme Senaryosu
                var updateDto = new UpdateProductImageDTO
                {
                    ProductImageId = model.ProductImageId,
                    Image1 = model.Image1,
                    Image2 = model.Image2,
                    Image3 = model.Image3,
                    Image4 = model.Image4,
                    ProductId = model.ProductId
                };
                await _productImageService.UpdateProductImageAsync(updateDto);
            }
            else
            {
                // Yeni Kayıt Senaryosu
                var createDto = new CreateProductImageDTO
                {
                    Image1 = model.Image1,
                    Image2 = model.Image2,
                    Image3 = model.Image3,
                    Image4 = model.Image4,
                    ProductId = model.ProductId
                };
                await _productImageService.CreateProductImageAsync(createDto);
            }

            return RedirectToAction("Index", "Product", new { area = "Admin" });
        }

        [HttpPost]
        public async Task<IActionResult> UploadImage(IFormFile file)
        {
            if (file == null || file.Length == 0) return BadRequest("Dosya seçilmedi.");

            var uploadFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/ProductImages");
            if (!Directory.Exists(uploadFolder)) Directory.CreateDirectory(uploadFolder);

            var imagename = Guid.NewGuid() + Path.GetExtension(file.FileName);
            var saveLocation = Path.Combine(uploadFolder, imagename);

            using var stream = new FileStream(saveLocation, FileMode.Create);
            await file.CopyToAsync(stream);

            // View tarafındaki AJAX'ın beklediği path formatı
            return Ok(new { path = "/images/ProductImages/" + imagename });
        }
    }
}