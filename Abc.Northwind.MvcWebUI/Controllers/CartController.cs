using Ab.Northwind.Business.Abstract;
using Abc.Northwind.Entities.Concrete;
using Abc.Northwind.MvcWebUI.Services;
using Microsoft.AspNetCore.Mvc;
using System.Drawing;

namespace Abc.Northwind.MvcWebUI.Controllers
{
    public class CartController : Controller
    {

        private ICartSessionService _cartSessionService;

        private ICartService _cartService;

        private IProductService _productService;

        public CartController(ICartSessionService cartSessionService, ICartService cartService, IProductService productService)
        {
            _cartSessionService = cartSessionService;
            _cartService = cartService;
            _productService = productService;
        }

        public IActionResult AddToCart(int productId)
        {
            var productToBeAdded = _productService.GetById(productId);

            var cart = _cartSessionService.GetCart();

            _cartService.AddToCart(cart, productToBeAdded);
            _cartSessionService.SetCart(cart);

            TempData.Add("message", string.Format("Your product, {0}, was successfully added to the cart!", productToBeAdded.ProductName));

            return RedirectToAction("Index", "Product");
        }

        public ActionResult List()
        {
            var cart=_cartSessionService.GetCart();
            CartListViewModel cartListViewModel = new CartListViewModel
            {
                Cart = cart,
            };

            return View(cartListViewModel);
        }

        public ActionResult Remove(int productId)
        {
            var cart=_cartSessionService.GetCart();
            _cartService.RemoveFromCart(cart, productId);
            _cartSessionService.SetCart(cart);

            TempData.Add("message", string.Format("Your product was successfully removed to the cart!"));
            return RedirectToAction("List");



        }


        public ActionResult Complete()
        {

            var shippigDetailsViewModel = new ShippingDetailsViewModel
            {
                ShippingDetails=new ShippingDetails()
            };

            return View(shippigDetailsViewModel);
        }


        [HttpPost]
        public ActionResult Complete(ShippingDetails shippingDetails)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            TempData.Add("message",String.Format( "Thank you {0}you order is in process  ", shippingDetails.FirstName));

            return View();

        }
    }
}
