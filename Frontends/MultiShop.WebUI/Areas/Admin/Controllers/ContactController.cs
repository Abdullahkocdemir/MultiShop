using Microsoft.AspNetCore.Mvc;
using MultiShop.WebUI.Services.CatalogService.ContactService;

namespace MultiShop.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ContactController : Controller
    {
        private readonly IContactService _contactService;

        public ContactController(IContactService contactService)
        {
            _contactService = contactService;
        }

        public async Task<IActionResult> Index()
        {
            var values = await _contactService.GetAllContactAsync();
            return View(values);
        }

        public async Task<IActionResult> DeleteContact(string id)
        {
            await _contactService.DeleteContactAsync(id);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> ContactDetail(string id)
        {
            var values = await _contactService.GetByIdContactAsync(id);
            return View(values);
        }
    }
}