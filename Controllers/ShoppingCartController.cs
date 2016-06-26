using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
 using SmartShop.Models;


namespace SmartShop.Controllers
{
    public class ShoppingCartController : Controller
    {


        SmartShopEntities storeDb = new SmartShopEntities();
        //
        // GET: /ShoppingCart/
        public ActionResult Index()
        {

              var cart = ShoppingCart.GetCart(this.HttpContext);
 
            // Set up our ViewModel
            var viewModel = new ShoppingCartViewModel
            {
                CartItems = cart.GetCartItems(),
                CartTotal = cart.GetTotal()
            };
            // Return the view

            return View();
        }

        //
        // GET: /Store/AddToCart/5
        public ActionResult AddToCart(int id)
        {
            // Retrieve the album from the database
            var addedSmartphone = storeDb.Smartphone
                .Single(smartphone => smartphone.SmartphoneId == id);

            // Add it to the shopping cart
            var cart = ShoppingCart.GetCart(this.HttpContext);

            cart.AddToCart(addedSmartphone);

            // Go back to the main store page for more shopping
            return RedirectToAction("Index");
        }

        //
        // AJAX: /ShoppingCart/RemoveFromCart/5
        [HttpPost]
        public ActionResult RemoveFromCart(int id)
        {
            // Remove the item from the cart
            var cart = ShoppingCart.GetCart(this.HttpContext);

            // Get the name of the album to display confirmation
            string smartphoneName = storeDb.Carts
                .Single(item => item.RecordId == id).Smartphone.Brand;
            string smartphoneModel = storeDb.Carts
             .Single(item => item.RecordId == id).Smartphone.ModelNr;
            // Remove from cart
            int itemCount = cart.RemoveFromCart(id);

            // Display the confirmation message
            var results = new ShoppingCartRemoveViewModel
            {
                Message = Server.HtmlEncode(smartphoneName) +" "+ Server.HtmlEncode(smartphoneModel) +
                    " has been removed from your shopping cart.",
                CartTotal = cart.GetTotal(),
                CartCount = cart.GetCount(),
                ItemCount = itemCount,
                DeleteId = id
            };
            return Json(results);
        }

        //
        // GET: /ShoppingCart/CartSummary
        [ChildActionOnly]
        public ActionResult CartSummary()
        {
            var cart = ShoppingCart.GetCart(this.HttpContext);

            ViewData["CartCount"] = cart.GetCount();
            return PartialView("CartSummary");
        }
	}
}