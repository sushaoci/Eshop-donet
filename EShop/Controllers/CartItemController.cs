﻿using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using EShop.Models;
using Microsoft.AspNet.Identity;

namespace EShop.Controllers
{
    public class CartItemController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: CartItem
        public ActionResult Index()
        {
            var cartItems = new List<CartItem>();
            if (User.Identity.IsAuthenticated)
            {
                var userId = User.Identity.GetUserId();
                var cart = db.Carts.FirstOrDefault(x => x.UserId == userId);
                if (cart != null)
                {
                    var cartItems0 = db.CartItems.Include(c => c.Cart).Include(c => c.Product).ToList();

                    cartItems = cartItems0.Where(x => x.CartId == cart.Id).ToList();
                }
            }
            return View(cartItems.ToList());
        }

        // GET: CartItem/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var cartItems = db.CartItems.Include(c => c.Cart).Include(c => c.Product).ToList();
            var cartItem = cartItems.FirstOrDefault(x => x.Id ==id);
            if (cartItem == null)
            {
                return HttpNotFound();
            }
            return View(cartItem);
        }

        public ActionResult AjaxClearCart()
        {

            if (!User.Identity.IsAuthenticated)
            {
                return JavaScript("window.location = '/Account/Login'");

            }
            var userId = User.Identity.GetUserId();
            var cart = db.Carts.FirstOrDefault(x => x.UserId == userId);

            var cartItems = new List<CartItem>();
            if (cart != null)
            {
                var cartItems0 = db.CartItems.Include(c => c.Cart).Include(c => c.Product).ToList();
                cartItems = cartItems0.Where(x => x.CartId == cart.Id).ToList();
            }
            decimal money = 0;
            foreach(CartItem c in cartItems)
            {
                money += c.ProductCount * c.Product.Price;
            }
            ViewData["money"] = money;

            if (cart != null)//有返回值
            {
                try
                {
                    db.Carts.Remove(cart);
                    db.SaveChanges();
                }
                catch { }
            }
            
            return View(ProductCount());
        }

        public ActionResult AjaxDeleteFromCart(int id)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return JavaScript("window.location = '/Account/Login'");
                
            }
            var userId = User.Identity.GetUserId();
            var cartitem0 = db.CartItems.Single(s => s.Id == id);
            if(id == null)
            {
                return JavaScript("window.location = '/Account/Login'");
            }

            if (cartitem0 != null)//有返回值
            {
                try
                {
                    db.CartItems.Remove(cartitem0);
                    db.SaveChanges();
                }
                catch { }
            }

            return View(ProductCount());
        }

        public int ProductCount()   //购物车中的产品数量 
        {
            if (!User.Identity.IsAuthenticated)
            {
                return 0;
            }

            var userId = User.Identity.GetUserId();
            var cart = db.Carts.FirstOrDefault(x => x.UserId == userId);
            if (cart == null)
            {
                return 0;
            }
            var cartItems = db.CartItems.ToList();
            var userCartItems = cartItems.Where(x => x.CartId == cart.Id).ToList();
            return userCartItems.Sum(x => x.ProductCount);


        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
