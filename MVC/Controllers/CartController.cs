using MVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC.Controllers
{
    public class CartController : ApplicationController
    {
        // GET: Cart
        public ActionResult Index()
        {
            ShoppingCart shoppingCart = HttpContext.Session["ShoppingCart"] as ShoppingCart;
            if(shoppingCart==null)
            {
                shoppingCart = new ShoppingCart() { Items = new List<CartItem>() };
            }
            return View(shoppingCart);
        }
        [HttpPost]
        public ActionResult Order(OrderInfomation model)
        {
            if(ModelState.IsValid)
            {
                if(OrderHelper.Ordered(HttpContext.User.Identity.Name,HttpContext.Session["ShoppingCart"] as ShoppingCart,model))
                {
                    HttpContext.Session["ShoppingCart"] = new ShoppingCart();
                    return Content("success");
                }
                ModelState.AddModelError("", "Có lỗi xảy ra, vui lòng thử lại sau");
            }
            return PartialView("FormInfo", model);
        }
        public ActionResult AddToCart(int id)
        {
            Product product = ProductHelper.GetProductByID(id);
            ShoppingCart shoppingCart = HttpContext.Session["ShoppingCart"] as ShoppingCart;
            if (shoppingCart == null)
            {
                shoppingCart = new ShoppingCart() { Items = new List<CartItem>() };
            }
            CartItem item = shoppingCart.Items.Where(x => x.Product.ID == id).SingleOrDefault();
            if (item == null)
                shoppingCart.Items.Add(new CartItem() { Product = product, Count = 1 });
            else
                item.Count++;
            HttpContext.Session["ShoppingCart"] = shoppingCart;
            return Content("success");
        }

        public ActionResult RemoveFromCart(int id)
        {
            ShoppingCart shoppingCart = HttpContext.Session["ShoppingCart"] as ShoppingCart;
            if (shoppingCart == null)
            {
                shoppingCart = new ShoppingCart() { Items = new List<CartItem>() };
            }
            if (shoppingCart.Items.Count>=id-1)
            {
                shoppingCart.Items.RemoveAt(id);
            }
            HttpContext.Session["ShoppingCart"] = shoppingCart;
            return RedirectToAction("Index", shoppingCart);
        }
        public ActionResult UpdateCartQuantity(ShoppingCart model)
        {
            ShoppingCart shoppingCart = HttpContext.Session["ShoppingCart"] as ShoppingCart;
            if (shoppingCart == null)
            {
                shoppingCart = new ShoppingCart() { Items = new List<CartItem>() };
            }
            for (int i = 0;i< shoppingCart.Items.Count;i++)
            {
                shoppingCart.Items[i].Count = model.Items[i].Count;
            }
            return RedirectToAction("Index", shoppingCart);
        }
    }
}