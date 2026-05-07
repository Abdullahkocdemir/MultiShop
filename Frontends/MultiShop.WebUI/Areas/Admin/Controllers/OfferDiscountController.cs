using Microsoft.AspNetCore.Mvc;
using MultiShop.DTOLayer.CatalogDTOs.OfferDiscountDTO;
using MultiShop.WebUI.Services.CatalogService.OfferService;

namespace MultiShop.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class OfferDiscountController : Controller
    {
        private readonly IOfferService _offerService;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public OfferDiscountController(IOfferService offerService, IWebHostEnvironment webHostEnvironment)
        {
            _offerService = offerService;
            _webHostEnvironment = webHostEnvironment;
        }

        #region Yardımcı Metod (Dosya Yükleme)
        private async Task<string> UploadImageAsync(IFormFile imageFile)
        {
            if (imageFile == null || imageFile.Length == 0) return null;
            var wwwrootPath = _webHostEnvironment.WebRootPath;
            var folderPath = Path.Combine(wwwrootPath, "OfferImages");

            if (!Directory.Exists(folderPath)) Directory.CreateDirectory(folderPath);

            var extension = Path.GetExtension(imageFile.FileName);
            var imageName = Guid.NewGuid() + extension;
            var savePath = Path.Combine(folderPath, imageName);

            using (var stream = new FileStream(savePath, FileMode.Create))
            {
                await imageFile.CopyToAsync(stream);
            }
            return "/OfferImages/" + imageName;
        }
        #endregion

        public async Task<IActionResult> Index()
        {
            var values = await _offerService.GetAllOfferDiscountAsync();
            return View(values);
        }

        [HttpGet]
        public IActionResult CreateOfferDiscount() => View();

        [HttpPost]
        public async Task<IActionResult> CreateOfferDiscount(CreateOfferDiscountDTO createOfferDiscountDTO, IFormFile ImageFile)
        {
            var uploadedPath = await UploadImageAsync(ImageFile);
            createOfferDiscountDTO.ImageUrl = uploadedPath ?? "/OfferImages/no-image.png";

            await _offerService.CreateOfferDiscountAsync(createOfferDiscountDTO);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> UpdateOfferDiscount(string id)
        {
            var values = await _offerService.GetByIdOfferDiscountAsync(id);

            // Mapping: GetByIdOfferDiscountDTO -> UpdateOfferDiscountDTO
            var updateValue = new UpdateOfferDiscountDTO
            {
                OfferDiscountId = values.OfferDiscountId,
                Title = values.Title,
                SubTitle = values.SubTitle,
                ImageUrl = values.ImageUrl
            };

            return View(updateValue);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateOfferDiscount(UpdateOfferDiscountDTO updateOfferDiscountDTO, IFormFile ImageFile)
        {
            var uploadedPath = await UploadImageAsync(ImageFile);
            if (uploadedPath != null) updateOfferDiscountDTO.ImageUrl = uploadedPath;

            await _offerService.UpdateOfferDiscountAsync(updateOfferDiscountDTO);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> DeleteOfferDiscount(string id)
        {
            await _offerService.DeleteOfferDiscountAsync(id);
            return RedirectToAction("Index");
        }
    }
}