using ClassLibraryData.Abstract;
using System.Collections;
using System.Collections.Generic;
using System.Web.Mvc;

namespace WebApplication.Controllers
{
    public class NavController : Controller
    {

        private IViewModelRepository repository;
        
        public NavController(IViewModelRepository rep)
        {
            repository = rep;
        }

        public PartialViewResult Menu(string genre = null)
        {
            ViewBag.SelectedGenre = genre;

            List<string> genres = new List<string>();
            foreach (var q in this.repository.BookAuthors.Keys)
            {
                if (!genres.Contains(q.Genre))
                    genres.Add(q.Genre);
            }

            return PartialView(genres);
        }
    }
}