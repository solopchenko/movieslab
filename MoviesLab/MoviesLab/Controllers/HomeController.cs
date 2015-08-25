using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MoviesLab.Models;

namespace MoviesLab.Controllers
{
    public class HomeController : Controller
    {
        //Создаем контекст данных
        MoviesLabDbContext db = new MoviesLabDbContext();

        public ActionResult Index()
        {
            List<Movie> movies = db.Movies.Where(m => m.Delete == false).OrderByDescending(m => m.MovieId).Take(3).ToList();

            ViewBag.Movies = movies;

            List<Person> people = db.People.Where(p => p.Delete == false).OrderByDescending(p => p.PersonId).Take(3).ToList();

            ViewBag.People = people;

            return View();
        }

        public ActionResult About()
        {
            return View();
        }
    }
}