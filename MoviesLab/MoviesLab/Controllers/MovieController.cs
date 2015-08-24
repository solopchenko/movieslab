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
            List<Movie> movies = db.Movies.Where(m => m.Delete == false).ToList();

            //Количесство объектов на странице
            int pageSize = 5;
            int pageNumber = (page ?? 1);

            //Возвращаем представление
            return View(movies.ToPagedList(pageNumber, pageSize));
        }

        [AllowAnonymous]
        public ActionResult Index(int? MovieId, FavMessageId? message)
        {
            ViewBag.StatusMessage =
            message == FavMessageId.AddSuccess ? "Фильм добавлен в избранное."
            : message == FavMessageId.DeleteSuccess ? "Фильм удалён из избранного."
            : message == FavMessageId.Error ? "Произошла ошибка."
            : "";

            Movie movie = db.Movies.Find(MovieId);
            if ((movie == null) || (movie.Delete == true))
            {
                return RedirectToAction("List");
            }

            ViewBag.MovieInFav = false;
            
            if (User.Identity.IsAuthenticated)
            {
                var currentUser = db.Users.Find(User.Identity.GetUserId());

                if (movie.User.Contains(currentUser))
                {
                    ViewBag.MovieInFav = true;
                }
            }

            //Возвращаем представление
            return View(movie);
        }

        [HttpGet]
        [Authorize(Roles = "Администратор")]
        public ActionResult Add()
        {
            //Получаем все жанры
            ViewBag.Genres = db.Genres.ToList();
            //Получаем все страны
            ViewBag.Countries = db.Countries.ToList();

            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Администратор")]
        public ActionResult Add(Movie movie, int[] selectedGenres, string[] selectedCountries)
        {
            movie.Genre = new List<Genre>();

            if (selectedGenres != null)
            {
                //Получаем выбраные жанры
                foreach (var g in db.Genres.Where(gen => selectedGenres.Contains(gen.GenreId)))
                {
                    movie.Genre.Add(g);
                }
            }

            movie.Country = new List<Country>();

            if (selectedCountries != null)
            {
                //Получаем выбраные страны
                foreach (var c in db.Countries.Where(co => selectedCountries.Contains(co.CountryId)))
                {
                    movie.Country.Add(c);
                }
            }

            movie.Delete = false;

            if (movie.Duration <= 0)
            {
                ModelState.AddModelError("Duration", "Введите значение больше 0");
            }

            db.Entry(movie).State = EntityState.Added;
            db.SaveChanges();

            return Redirect("/Movie?MovieId=" + movie.MovieId);

//            if (ModelState.IsValid)
//            {
                
            //}
            //return View(movie);
        }

        [HttpGet]
        [Authorize(Roles = "Администратор")]
        public ActionResult Edit(int? MovieId)
        {
            Movie movie = db.Movies.Find(MovieId);
            if (movie == null)
            {
                return RedirectToAction("List");
            }

            ViewBag.Genres = db.Genres.ToList();
            ViewBag.Countries = db.Countries.ToList();

            return View(movie);
        }

        [HttpPost]
        [Authorize(Roles = "Администратор")]
        public ActionResult Edit(Movie movie, int[] selectedGenres, string[] selectedCountries)
        {
            Movie newMovie = db.Movies.Find(movie.MovieId);
            newMovie.Title = movie.Title;
            newMovie.Description = movie.Description;
            newMovie.Duration = movie.Duration;
            newMovie.Date = movie.Date;

            newMovie.Genre.Clear();

            if (selectedGenres != null)
            {
                //Получаем выбраные жанры
                foreach (var g in db.Genres.Where(gen => selectedGenres.Contains(gen.GenreId)))
                {
                    newMovie.Genre.Add(g);
                }
            }

            newMovie.Country.Clear();
            if (selectedCountries != null)
            {
                //Получаем выбраные страны
                foreach (var c in db.Countries.Where(co => selectedCountries.Contains(co.CountryId)))
                {
                    newMovie.Country.Add(c);
                }
            }

            db.Entry(newMovie).State = EntityState.Modified;
            db.SaveChanges();

            return Redirect("/Movie?MovieId=" + movie.MovieId);
        }

        [Authorize(Roles = "Администратор")]
        public ActionResult Delete(int? MovieId)
        {
            Movie movie = db.Movies.Find(MovieId);
            if ((movie != null) || (movie.Delete == false))
            {
                movie.Delete = true;
                db.Entry(movie).State = EntityState.Modified;
                db.SaveChanges();
            }

            //Возвращаемся к списку фильмов
            return RedirectToAction("List");
        }

               [HttpGet]
        [Authorize(Roles = "Администратор")]
        public ActionResult AddActor(int? MovieId)
        {
            ViewBag.People = new SelectList(db.People, "PersonId", "FullnameWithYears");

            Movie movie = db.Movies.Find(MovieId);
            if (movie == null)
            {
                return HttpNotFound();
            }
            else
            {
                ViewBag.Movie = movie;
            }

            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Администратор")]
        public ActionResult AddActor(Actor actor)
        {
            db.Actors.Add(actor);
            db.SaveChanges();

            return Redirect("/Movie?MovieId=" + actor.MovieId);
        }

        [HttpGet]
        [Authorize(Roles = "Администратор")]
        public ActionResult AddPerson(int? MovieId)
        {
            ViewBag.People = new SelectList(db.People, "PersonId", "FullnameWithYears");
            ViewBag.CrewPositions = new SelectList(db.CrewPositions, "PositionId", "Name");

            Movie movie = db.Movies.Find(MovieId);
            if (movie == null)
            {
                return HttpNotFound();
            }
            else
            {
                ViewBag.Movie = movie;
            }

            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Администратор")]
        public ActionResult AddPerson(FilmCrew filmCrew)
        {
            db.FilmCrew.Add(filmCrew);
            db.SaveChanges();

            return Redirect("/Movie?MovieId=" + filmCrew.MovieId);
        }

        [HttpGet]
        [Authorize(Roles = "Пользователь")]
        public ActionResult AddFavorite(int movieId)
        {
            Movie m = db.Movies.Find(movieId);
            if (m == null)
            {
                return RedirectToAction("Index", new { movieId = m.MovieId, message = FavMessageId.Error });
            }

            var currentUser = db.Users.Find(User.Identity.GetUserId());

            if(m.User.Contains(currentUser))
            {
                return RedirectToAction("Index", new { movieId = m.MovieId, message = FavMessageId.Error });
            }
            else
            {
                m.User.Add(currentUser);
                db.Entry(m).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", new { movieId = m.MovieId, message = FavMessageId.AddSuccess });
            }
        }

        [HttpGet]
        [Authorize(Roles = "Пользователь")]
        public ActionResult DeleteFavorite(int movieId)
        {
            Movie m = db.Movies.Find(movieId);
            if (m == null)
            {
                return RedirectToAction("Index", new { movieId = m.MovieId, message = FavMessageId.Error });
            }

            var currentUser = db.Users.Find(User.Identity.GetUserId());

            if (m.User.Contains(currentUser))
            {
                m.User.Remove(currentUser);
                db.Entry(m).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", new { movieId = m.MovieId, message = FavMessageId.DeleteSuccess });
            }
            else
            {
                return RedirectToAction("Index", new { movieId = m.MovieId, message = FavMessageId.Error });
            }
        }

        [HttpGet]
        [Authorize(Roles = "Пользователь")]
        public ActionResult Favorites()
        {
            MoviesLabUser user = db.Users.Find(User.Identity.GetUserId());

            List<Movie> fm = new List<Movie>();
            if (user.FavMovie != null)
            {
                fm = user.FavMovie.ToList();
            }

            return View(fm);
        }

        [AllowAnonymous]
        public PartialViewResult MovieSearch(string search)
        {

            List<Movie> movies = db.Movies.Where(b => b.Title.Contains(search) || b.Description.Contains(search)).Distinct().ToList();

            return PartialView("_Search", movies);
        }


        public enum FavMessageId
        {
            AddSuccess,
            DeleteSuccess,
            Error
        }
    }
}