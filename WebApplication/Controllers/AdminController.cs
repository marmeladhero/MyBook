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

    [Authorize(Users="Admin")]
    public class AdminController : Controller
    {
        private IViewModelRepository repository;

        public AdminController(IViewModelRepository repo)
        {
            this.repository = repo;
        }

        public ViewResult Index()
        {
            return View(repository.BookAuthors);
        }

        public ViewResult Edit(int id)
        {
            ViewModelBookAuthor model = null;

            if (id == 0)
            {
                model = new ViewModelBookAuthor()
                {
                    bBook = new Book(),
                    LstAuthors = new List<Author>()
                };
            }
            else
            {
                model = new ViewModelBookAuthor(this.repository.BookAuthors.FirstOrDefault(x => x.Key.Id == id));
            }
            return View(model);
        }
   
        public ViewResult CreateNewAuthor(int id)
        {
            ViewModelBookAuthor model = null;

            if (id != 0)
            {
                model = new ViewModelBookAuthor(this.repository.BookAuthors.FirstOrDefault(x => x.Key.Id == id));
            }
            else
            {
                model = new ViewModelBookAuthor()
                {
                    bBook = new Book(),
                    LstAuthors = new List<Author>()
                };
            }

            model.LstAuthors.Add(new Author { Name = "", SurName = "", SecondName = "" });
           
            return View("Edit", model);
        }

        public ViewResult RemoveAuthor(int idBook, int idAuthor) 
        {
            ViewModelBookAuthor model = new ViewModelBookAuthor(this.repository.BookAuthors.FirstOrDefault(x => x.Key.Id == idBook));
            model.LstAuthors.Remove(model.LstAuthors.Find(x => x.Id == idAuthor));           
            return View("Edit", model);
        }

        public ActionResult Create()
        {
            return RedirectToAction("Edit", new { id=0 });            
        }

        public ActionResult Delete(int id)
        {
            ViewModelBookAuthor model = new ViewModelBookAuthor(this.repository.BookAuthors.FirstOrDefault(x => x.Key.Id == id));
            this.repository.RemoveAll(model.bBook, model.LstAuthors);
            TempData["message"] = $"Book {model.bBook.Name} deleted";
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Edit(ViewModelBookAuthor data)
        {
            if (ModelState.IsValid)
            {
                this.repository.SaveAll(data.bBook, data.LstAuthors);                
                TempData["message"] = $"Book {data.bBook.Name} changes saved";
                return RedirectToAction("Index");
            }
            else
            {
                return View(data);
            }
        }
    }
}