using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using MoviesLab.Models;
using PagedList;
using PagedList.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;

namespace MoviesLab.Controllers
{
    public class MovieController : Controller
    {
        //Создаем контекст данных
        MoviesLabDbContext db = new MoviesLabDbContext();

        //Список фильмов
        [AllowAnonymous]
        public ActionResult List(int? page)
        {
            //Получаем из базы данных все объекты Movie
            List<Movie> movies = db.Movies.ToList();

            //Количесство объектов на странице
            int pageSize = 5;
            int pageNumber = (page ?? 1);

            //Возвращаем представление
            return View(movies.ToPagedList(pageNumber, pageSize));
        }

        //
        // GET: /Movie/
        public ActionResult Index()
        {
            return View();
        }
	}
}