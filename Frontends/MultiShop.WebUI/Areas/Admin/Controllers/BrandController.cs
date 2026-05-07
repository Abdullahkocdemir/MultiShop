using Microsoft.AspNetCore.Mvc;
using MultiShop.DTOLayer.CatalogDTOs.BrandDTO;
using MultiShop.WebUI.Services.CatalogService.BrandService;

namespace MultiShop.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class BrandController : Controller
    {
        private readonly IBrandService _brandService;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public BrandController(IBrandService brandService, IWebHostEnvironment webHostEnvironment)
        {
            _brandService = brandService;
            _webHostEnvironment = webHostEnvironment;
        }

        #region Yardımcı Metod (Dosya Yükleme)
        private async Task<string> UploadImageAsync(IFormFile imageFile)
        {
            if (imageFile == null || imageFile.Length == 0) return null;
            var wwwrootPath = _webHostEnvironment.WebRootPath;
            var folderPath = Path.Combine(wwwrootPath, "BrandImages");

            if (!Directory.Exists(folderPath)) Directory.CreateDirectory(folderPath);

            var extension = Path.GetExtension(imageFile.FileName);
            var imageName = Guid.NewGuid() + extension;
            var savePath = Path.Combine(folderPath, imageName);

            using (var stream = new FileStream(savePath, FileMode.Create))
            {
                await imageFile.CopyToAsync(stream);
            }
            return "/BrandImages/" + imageName;
        }
        #endregion

        public async Task<IActionResult> Index()
        {
            var values = await _brandService.GetAllBrandAsync();
            return View(values);
        }

        [HttpGet]
        public IActionResult CreateBrand() => View();

        [HttpPost]
        public async Task<IActionResult> CreateBrand(CreateBrandDTO createBrandDTO, IFormFile ImageFile)
        {
            var uploadedPath = await UploadImageAsync(ImageFile);
            createBrandDTO.ImageUrl = uploadedPath ?? "/BrandImages/no-brand.png";

            await _brandService.CreateBrandAsync(createBrandDTO);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> UpdateBrand(string id)
        {
            var values = await _brandService.GetByIdBrandAsync(id);

            // Mapping: GetByIdBrandDTO -> UpdateBrandDTO
            var updateValue = new UpdateBrandDTO
            {
                BrandId = values.BrandId,
                BrandName = values.BrandName,
                ImageUrl = values.ImageUrl
            };

            return View(updateValue);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateBrand(UpdateBrandDTO updateBrandDTO, IFormFile ImageFile)
        {
            var uploadedPath = await UploadImageAsync(ImageFile);
            if (uploadedPath != null)
            {
                updateBrandDTO.ImageUrl = uploadedPath;
            }

            await _brandService.UpdateBrandAsync(updateBrandDTO);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> DeleteBrand(string id)
        {
            await _brandService.DeleteBrandAsync(id);
            return RedirectToAction("Index");
        }
    }
}