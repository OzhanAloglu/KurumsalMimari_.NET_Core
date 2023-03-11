using Ab.Northwind.Business.Abstract;
using Abc.Northwind.Entities.Concrete;
using Abc.Northwind.MvcWebUI.Models;
using Microsoft.AspNetCore.Mvc;

namespace Abc.Northwind.MvcWebUI.Controllers
{
    public class AdminController : Controller
    {

        private IProductService _productService;
        private ICategoryService _categoryService;


        public AdminController(IProductService productService)
        {
            _productService = productService;
        }

        public IActionResult Index()
        {
            var productListViewModel = new ProductListViewModel
            {
                Products = _productService.GetAll()
            };

            return View(productListViewModel);
        }

        [HttpGet]
        public ActionResult Add()
        {
            var model = new ProductAddViewModel
            {
                Product =new Product (),
                Categories=_categoryService.GetAll()
            };
          return View();

        }

        [HttpPost]

        public ActionResult Add(Product product)
        {

            if (ModelState.IsValid)
            {
                _productService.Add(product);
                TempData.Add("message", "Product was successfully added");
            }

            return View();

        }
    }
}
