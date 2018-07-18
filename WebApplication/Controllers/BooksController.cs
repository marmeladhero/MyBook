using ClassLibraryData.Abstract;
using ClassLibraryData.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication.Models;

namespace WebApplication.Controllers
{
    public class BooksController : Controller
    {
        private IViewModelRepository repository;
              
        public BooksController(IViewModelRepository rep)
        {
            repository = rep;
        }

        public ViewResult List(string genre = null)
        {
            List<ViewModelBookAuthor> LstModel = new List<ViewModelBookAuthor>();

            foreach (var item in this.repository.BookAuthors)
            {
                LstModel.Add(new ViewModelBookAuthor(item));
            }

            if (genre != null)
            {
                return View(LstModel.Where(x => x.bBook.Genre == genre));
            }

            return View(LstModel);
        }
    }
}