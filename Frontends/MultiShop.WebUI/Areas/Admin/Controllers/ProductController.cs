using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MultiShop.DTOLayer.CatalogDTOs.ProductDTO;
using MultiShop.WebUI.Services.CatalogService.CategoryService;
using MultiShop.WebUI.Services.CatalogService.ProductService;
using X.PagedList.Extensions;

namespace MultiShop.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;

        public ProductController(IProductService productService, ICategoryService categoryService)
        {
            _productService = productService;
            _categoryService = categoryService;
        }

        private async Task GetCategoriesAsync()
        {
            var values = await _categoryService.GetAllCategoryAsync();
            List<SelectListItem> categoryValues = (from x in values
                                                   select new SelectListItem
                                                   {
                                                       Text = x.CategoryName,
                                                       Value = x.CategoryId
                                                   }).ToList();
            ViewBag.CategoryList = categoryValues;
        }

        public async Task<IActionResult> Index(int page = 1)
        {
            var values = await _productService.GetAllProductWithCategoryAsync();
            return View(values.ToPagedList(page, 5));
        }

        [HttpGet]
        public async Task<IActionResult> CreateProduct()
        {
            await GetCategoriesAsync();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct(CreateProductDTO createProductDTO, IFormFile file)
        {
            if (file != null && file.Length > 0)
            {
                var resource = Directory.GetCurrentDirectory();
                var extension = Path.GetExtension(file.FileName);
                var imagename = Guid.NewGuid() + extension;
                var saveLocation = Path.Combine(resource, "wwwroot/images", imagename);

                using var stream = new FileStream(saveLocation, FileMode.Create);
                await file.CopyToAsync(stream);
                createProductDTO.ProductImageUrl = "/images/" + imagename;
            }

            await _productService.CreateProductAsync(createProductDTO);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> UpdateProduct(string id)
        {
            await GetCategoriesAsync();
            var values = await _productService.GetByIdProductAsync(id);

            var updateProductDTO = new UpdateProductDTO
            {
                ProductId = values.ProductId,
                ProductName = values.ProductName,
                ProductPrice = values.ProductPrice,
                ProductDescription = values.ProductDescription,
                ProductImageUrl = values.ProductImageUrl,
                CategoryId = values.CategoryId
            };

            return View(updateProductDTO);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateProduct(UpdateProductDTO updateProductDTO, IFormFile file)
        {
            if (file != null && file.Length > 0)
            {
                var resource = Directory.GetCurrentDirectory();
                var extension = Path.GetExtension(file.FileName);
                var imagename = Guid.NewGuid() + extension;
                var saveLocation = Path.Combine(resource, "wwwroot/images", imagename);

                using var stream = new FileStream(saveLocation, FileMode.Create);
                await file.CopyToAsync(stream);
                updateProductDTO.ProductImageUrl = "/images/" + imagename;
            }

            await _productService.UpdateProductAsync(updateProductDTO);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> DeleteProduct(string id)
        {
            await _productService.DeleteProductAsync(id);
            return RedirectToAction("Index");
        }
    }
}