using Microsoft.AspNetCore.Mvc;
using MultiShop.DTOLayer.CatalogDTOs.FeatureSliderDTO;
using MultiShop.WebUI.Services.CatalogService.FeatureSliderService;

namespace MultiShop.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class FeatureSliderController : Controller
    {
        private readonly IFeatureSliderService _featureSliderService;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public FeatureSliderController(IFeatureSliderService featureSliderService, IWebHostEnvironment webHostEnvironment)
        {
            _featureSliderService = featureSliderService;
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task<IActionResult> Index()
        {
            var values = await _featureSliderService.GetAllFeatureSliderAsync();
            return View(values);
        }

        [HttpGet]
        public IActionResult CreateFeature() => View();

        [HttpPost]
        public async Task<IActionResult> CreateFeature(CreateFeatureSliderDTO createFeatureDTO, IFormFile ImageFile)
        {
            if (ImageFile != null)
            {
                createFeatureDTO.ImageUrl = await SaveImage(ImageFile);
            }

            await _featureSliderService.CreateFeatureSliderAsync(createFeatureDTO);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> UpdateFeature(string id)
        {
            var values = await _featureSliderService.GetByIdFeatureSliderAsync(id);

            // Mapping: GetByIdFeatureSliderDTO -> UpdateFeatureSliderDTO
            var updateValue = new UpdateFeatureSliderDTO
            {
                FeatureSliderId = values.FeatureSliderId,
                Title = values.Title,
                Description = values.Description,
                ImageUrl = values.ImageUrl,
                Status = values.Status
            };

            return View(updateValue);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateFeature(UpdateFeatureSliderDTO updateFeatureDTO, IFormFile? ImageFile)
        {
            if (ImageFile != null)
            {
                updateFeatureDTO.ImageUrl = await SaveImage(ImageFile);
            }

            await _featureSliderService.UpdateFeatureSliderAsync(updateFeatureDTO);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> DeleteFeature(string id)
        {
            await _featureSliderService.DeleteFeatureSliderAsync(id);
            return RedirectToAction("Index");
        }

        #region Status Change Methods
        public async Task<IActionResult> FeatureSliderStatusToTrue(string id)
        {
            await _featureSliderService.FeatureSliderChageStatusToTrue(id);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> FeatureSliderStatusToFalse(string id)
        {
            await _featureSliderService.FeatureSliderChageStatusToFalse(id);
            return RedirectToAction("Index");
        }
        #endregion

        private async Task<string> SaveImage(IFormFile file)
        {
            string folder = Path.Combine(_webHostEnvironment.WebRootPath, "images", "FeatureSlider");
            if (!Directory.Exists(folder)) Directory.CreateDirectory(folder);

            string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
            string filePath = Path.Combine(folder, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }
            return "/images/FeatureSlider/" + fileName;
        }
    }
}