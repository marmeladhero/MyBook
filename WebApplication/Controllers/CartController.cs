using ClassLibraryData.Abstract;
using ClassLibraryData.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication.Filters;
using WebApplication.Models;

namespace WebApplication.Controllers
{
    public class CartController : Controller
    {

        private IViewModelRepository repository;
        private IOrderProcessor order;
        public CartController(IViewModelRepository repo, IOrderProcessor order)
        {
            this.repository = repo;
            this.order = order;
        }

        public ViewResult Index(Cart cart, string returnUrl)
        {
            return View(new CartViewModel
            {
                cart = cart,
                ReturnUrl = returnUrl
            });
        }

       
        public RedirectToRouteResult AddToCart(Cart cart, int bookId, string returnUrl)
        {
            Book book = repository.BookAuthors.FirstOrDefault(x => x.Key.Id == bookId).Key;
            if (book != null)
            {
                cart.AddItem(book, 1);
            }

            return RedirectToAction("Index", new { returnUrl });
        }     

        public RedirectToRouteResult RemoveFromCart(Cart cart, int bookId, string returnUrl)
        {
            Book book = repository.BookAuthors.Where(x => x.Key.Id == bookId).FirstOrDefault().Key;
            if (book != null)
            {
                cart.RemoveItem(book);
            }

            return RedirectToAction("Index", new { returnUrl });
        }

        public PartialViewResult Summary(Cart cart)
        {
            return PartialView(cart);
        }

        public ViewResult Checkout()
        {
            return View(new User());
        }

        [HttpPost]
        public ViewResult Checkout(Cart cart, User user)
        {
            if(cart.Lines.Count() == 0)
            {
                ModelState.AddModelError("", "Sorry cart is empty");
            }

            if (ModelState.IsValid)
            {
                foreach(var i in cart.Lines)
                {
                    i.book.Quantity--;
                    this.repository.SaveBook(i.book);
                }

                this.order.ProcessorOrder(cart, user);
                cart.Clear();
                
                return View("Complete");
            }
            else
            {
                return View(new User());
            }
            
        }
    }
}