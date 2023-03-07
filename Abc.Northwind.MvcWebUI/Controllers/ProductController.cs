using Ab.Northwind.Business.Abstract;
using Abc.Northwind.MvcWebUI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Razor;

namespace Abc.Northwind.MvcWebUI.Controllers
{
    public class ProductController:Controller
    {
        private IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }


        public ActionResult Index()
        {
            var products = _productService.GetAll();

            ProductListViewModel model = new ProductListViewModel
            {
                Products = products
            };

            return View(model);
        }

    }
}
