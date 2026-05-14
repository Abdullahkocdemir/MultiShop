using Microsoft.AspNetCore.Mvc;
using MultiShop.DTOLayer.CatalogDTOs.ContactDTO; // DTO namespace'ine dikkat
using MultiShop.WebUI.Services.CatalogService.ContactService;

namespace MultiShop.WebUI.Controllers
{
    public class ContactController : Controller
    {
        private readonly IContactService _contactService;

        public ContactController(IContactService contactService)
        {
            _contactService = contactService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(CreateContactDTO createContactDTO)
        {
            createContactDTO.SendDate = DateTime.Now;
            createContactDTO.IsRead = false;
            await _contactService.CreateContactAsync(createContactDTO);

            TempData["MessageSent"] = true;

            return RedirectToAction("Index");
        }
    }
}