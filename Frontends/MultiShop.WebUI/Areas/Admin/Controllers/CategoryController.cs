using Microsoft.AspNetCore.Mvc;
using MultiShop.DTOLayer.CatalogDTOs.CategoryDTO;
using MultiShop.WebUI.Services.CatalogService.CategoryService;

namespace MultiShop.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public CategoryController(ICategoryService categoryService, IWebHostEnvironment webHostEnvironment)
        {
            _categoryService = categoryService;
            _webHostEnvironment = webHostEnvironment;
        }

        #region Yardımcı Metod (Dosya Yükleme)
        private async Task<string> UploadImageAsync(IFormFile imageFile)
        {
            if (imageFile == null || imageFile.Length == 0) return null;

            var wwwrootPath = _webHostEnvironment.WebRootPath;
            var folderPath = Path.Combine(wwwrootPath, "Category");

            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }

            var extension = Path.GetExtension(imageFile.FileName);
            var imageName = Guid.NewGuid() + extension;
            var savePath = Path.Combine(folderPath, imageName);

            using (var stream = new FileStream(savePath, FileMode.Create))
            {
                await imageFile.CopyToAsync(stream);
            }

            return "/Category/" + imageName;
        }
        #endregion

        public async Task<IActionResult> Index()
        {
            var values = await _categoryService.GetAllCategoryAsync();
            return View(values);
        }

        [HttpGet]
        public IActionResult CreateCategory() => View();

        [HttpPost]
        public async Task<IActionResult> CreateCategory(CreateCategoryDTO createCategoryDTO, IFormFile ImageFile)
        {
            var uploadedPath = await UploadImageAsync(ImageFile);
            if (uploadedPath != null)
            {
                createCategoryDTO.ImageUrl = uploadedPath;
            }

            await _categoryService.CreateCategoryAsync(createCategoryDTO);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> UpdateCategory(string id)
        {
            var values = await _categoryService.GetByIdCategoryAsync(id);
            var updateValue = new UpdateCategoryDTO
            {
                CategoryId = values.CategoryId,
                CategoryName = values.CategoryName,
                ImageUrl = values.ImageUrl
            };

            return View(updateValue);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateCategory(UpdateCategoryDTO updateCategoryDTO, IFormFile ImageFile)
        {
            var uploadedPath = await UploadImageAsync(ImageFile);
            if (uploadedPath != null)
            {
                updateCategoryDTO.ImageUrl = uploadedPath;
            }

            await _categoryService.UpdateCategoryAsync(updateCategoryDTO);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> DeleteCategory(string id)
        {
            await _categoryService.DeleteCategoryAsync(id);
            return RedirectToAction("Index");
        }
    }
}