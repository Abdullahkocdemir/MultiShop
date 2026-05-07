using Microsoft.AspNetCore.Mvc;
using MultiShop.DTOLayer.CatalogDTOs.SpecialOfferDTO;
using MultiShop.WebUI.Services.CatalogService.SpecialOfferService;

namespace MultiShop.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SpecialOfferController : Controller
    {
        private readonly ISpecialOfferService _specialOfferService;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public SpecialOfferController(ISpecialOfferService specialOfferService, IWebHostEnvironment webHostEnvironment)
        {
            _specialOfferService = specialOfferService;
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task<IActionResult> Index()
        {
            var values = await _specialOfferService.GetAllSpecialOfferAsync();
            return View(values);
        }

        [HttpGet]
        public IActionResult CreateSpecialOffer() => View();

        [HttpPost]
        public async Task<IActionResult> CreateSpecialOffer(CreateSpecialOfferDTO createSpecialOfferDTO, IFormFile ImageFile)
        {
            if (ImageFile != null)
            {
                createSpecialOfferDTO.ImageUrl = await SaveImage(ImageFile);
            }

            await _specialOfferService.CreateSpecialOfferAsync(createSpecialOfferDTO);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> UpdateSpecialOffer(string id)
        {
            var values = await _specialOfferService.GetByIdSpecialOfferAsync(id);

            // Mapping: GetByIdSpecialOfferDTO -> UpdateSpecialOfferDTO
            var updateValue = new UpdateSpecialOfferDTO
            {
                SpecialOfferId = values.SpecialOfferId,
                Title = values.Title,
                SubTitle = values.SubTitle,
                ImageUrl = values.ImageUrl
            };

            return View(updateValue);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateSpecialOffer(UpdateSpecialOfferDTO updateSpecialOfferDTO, IFormFile? ImageFile)
        {
            if (ImageFile != null)
            {
                updateSpecialOfferDTO.ImageUrl = await SaveImage(ImageFile);
            }

            await _specialOfferService.UpdateSpecialOfferAsync(updateSpecialOfferDTO);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> DeleteSpecialOffer(string id)
        {
            await _specialOfferService.DeleteSpecialOfferAsync(id);
            TempData["DeleteSuccess"] = "Özel teklif başarıyla silindi.";
            return RedirectToAction("Index");
        }

        private async Task<string> SaveImage(IFormFile file)
        {
            string folder = Path.Combine(_webHostEnvironment.WebRootPath, "images", "SpecialOffer");
            if (!Directory.Exists(folder)) Directory.CreateDirectory(folder);

            string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
            string filePath = Path.Combine(folder, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }
            return "/images/SpecialOffer/" + fileName;
        }
    }
}