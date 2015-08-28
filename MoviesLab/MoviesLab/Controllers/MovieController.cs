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
        public ActionResult List(int? page, StatusMessage? message)
        {
            ViewBag.StatusMessage =
            message == StatusMessage.MovieDeleteError ? "При удалении фильма произошла ошибка."
            : message == StatusMessage.MovieNotFound ? "Фильм не найден."
            : message == StatusMessage.MovieDeleted ? "Доступ к фильму ограничен."
            : "";

            //Получаем из базы данных все объекты Movie
            List<Movie> movies = db.Movies.Where(m => m.Delete == false).ToList();

            //Количесство объектов на странице
            int pageSize = 5;
            int pageNumber = (page ?? 1);

            //Возвращаем представление
            return View(movies.ToPagedList(pageNumber, pageSize));
        }

        //Список удаленных фильмов
        [Authorize(Roles = "Администратор")]
        public ActionResult Deleted(int? page, StatusMessage? message)
        {
            ViewBag.StatusMessage =
            message == StatusMessage.MovieDeleteSuccess ? "Фильм успешно удалён."
            : message == StatusMessage.MovieRestoreError ? "Не удалось восстановить фильм."
            : message == StatusMessage.MovieRestoreSuccess ? "Фильм успешно восстановлен."
            : "";

            //Получаем из базы данных все объекты Movie
            List<Movie> movies = db.Movies.Where(m => m.Delete == true).ToList();

            //Количесство объектов на странице
            int pageSize = 5;
            int pageNumber = (page ?? 1);

            //Возвращаем представление
            return View("Deleted", (movies.ToPagedList(pageNumber, pageSize)));
        }

        [AllowAnonymous]
        public ActionResult Index(int? MovieId, StatusMessage? message)
        {
            ViewBag.StatusMessage =
            message == StatusMessage.FavAddSuccess ? "Фильм успешно добавлен в избранное."
            : message == StatusMessage.FavAddError ? "Ошибка добавления фильма в избранное."
            : message == StatusMessage.FavDeleteSuccess ? "Фильм успешно удалён из избранного."
            : message == StatusMessage.FavDeleteError ? "Ошибка удаления фильма из избранного."
            : "";

            Movie movie = db.Movies.Find(MovieId);
            if (movie == null)
            {
                return RedirectToAction("List", new { message = StatusMessage.MovieNotFound });
            }

            if (movie.Delete == true)
            {
                return RedirectToAction("List", new { message = StatusMessage.MovieDeleted });
            }

            ViewBag.MovieInFav = false;
            ViewBag.MovieDeleted = movie.Delete;
            
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
                foreach (var genre in db.Genres.Where(g => selectedGenres.Contains(g.GenreId)))
                {
                    movie.Genre.Add(genre);
                }
            }

            movie.Country = new List<Country>();

            if (selectedCountries != null)
            {
                //Получаем выбраные страны
                foreach (var country in db.Countries.Where(c => selectedCountries.Contains(c.CountryId)))
                {
                    movie.Country.Add(country);
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
        }

        [HttpGet]
        [Authorize(Roles = "Администратор")]
        public ActionResult Edit(int? MovieId, StatusMessage? message)
        {
            ViewBag.StatusMessage =
            message == StatusMessage.ActorNotFound ? "Актер не найден."
            : message == StatusMessage.PersonDeleted ? "Доступ к персоне ограничен."
            : message == StatusMessage.PersonNotFound ? "Персона не найдена."
            : message == StatusMessage.PositionNotFound ? "Участник съёмочной группы не найден."
            : message == StatusMessage.ActorDeleteSuccess ? "Роль успешно удалена."
            : message == StatusMessage.PersonDeleteSuccess ? "Персона успешно удалена из участников съёмочной группы."
            : "";

            Movie movie = db.Movies.Find(MovieId);
            if (movie == null)
            {
                return RedirectToAction("List", new { message = StatusMessage.MovieNotFound });
            }

            if (movie.Delete == true)
            {
                return RedirectToAction("List", new { message = StatusMessage.MovieDeleted });
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
                foreach (var genre in db.Genres.Where(g => selectedGenres.Contains(g.GenreId)))
                {
                    newMovie.Genre.Add(genre);
                }
            }

            newMovie.Country.Clear();
            if (selectedCountries != null)
            {
                //Получаем выбраные страны
                foreach (var country in db.Countries.Where(c => selectedCountries.Contains(c.CountryId)))
                {
                    newMovie.Country.Add(country);
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

            if (movie == null)
            {
                return RedirectToAction("List", new { message = StatusMessage.MovieNotFound });
            }

            if (movie.Delete == true)
            {
                return RedirectToAction("List", new { message = StatusMessage.MovieDeleted });
            }

            movie.Delete = true;
            db.Entry(movie).State = EntityState.Modified;
            db.SaveChanges();

            //Возвращаемся к списку фильмов
            return RedirectToAction("Deleted", new { message = StatusMessage.MovieDeleteSuccess });
        }


        [Authorize(Roles = "Администратор")]
        public ActionResult Restore(int? MovieId)
        {
            Movie movie = db.Movies.Find(MovieId);

            if (movie == null)
            {
                return RedirectToAction("List", new { message = StatusMessage.MovieNotFound });
            }

            if (movie.Delete == false)
            {
                return RedirectToAction("Deleted", new { message = StatusMessage.MovieRestoreError });
            }

            movie.Delete = false;
            db.Entry(movie).State = EntityState.Modified;
            db.SaveChanges();

            //Возвращаемся к списку фильмов
            return RedirectToAction("Deleted", new { message = StatusMessage.MovieRestoreSuccess });
        }


        [HttpGet]
        [Authorize(Roles = "Администратор")]
        public ActionResult AddActor(int? MovieId)
        {
            ViewBag.People = new SelectList(db.People.Where(p => p.Delete == false), "PersonId", "FullnameWithYears");

            Movie movie = db.Movies.Find(MovieId);

            if (movie == null)
            {
                return RedirectToAction("List", new { message = StatusMessage.MovieNotFound });
            }

            if (movie.Delete == true)
            {
                return RedirectToAction("List", new { message = StatusMessage.MovieDeleted });
            }

            ViewBag.Movie = movie;

            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Администратор")]
        public ActionResult AddActor(Actor actor)
        {
            db.Actors.Add(actor);
            db.SaveChanges();

            return Redirect("/Movie/Edit?MovieId=" + actor.MovieId);
        }

        [HttpGet]
        [Authorize(Roles = "Администратор")]
        public ActionResult DeleteActor(int PersonId, int MovieId, string Character)
        {
            Person person = db.People.Find(PersonId);
            if (person == null)
            {
                return RedirectToAction("List", new { message = StatusMessage.PersonNotFound });
            }

            if (person.Delete == true)
            {
                return RedirectToAction("List", new { message = StatusMessage.PersonDeleted });
            }

            Movie movie = db.Movies.Find(MovieId);
            if (movie == null)
            {
                return RedirectToAction("Edit", new { message = StatusMessage.MovieNotFound });
            }

            if (movie.Delete == true)
            {
                return RedirectToAction("Edit", new { message = StatusMessage.MovieDeleted });
            }

            Actor actor = db.Actors.Find(PersonId, MovieId, Character);
            if (actor == null)
            {
                return RedirectToAction("Edit", new { message = StatusMessage.ActorNotFound });
            }

            db.Actors.Remove(actor);
            db.SaveChanges();

            return RedirectToAction("Edit", new { MovieId = MovieId, message = StatusMessage.ActorDeleteSuccess });
        }

        //// Добавление актера к фильму через модальное окно
        //[HttpGet]
        //public ActionResult AddActor1(int? MovieId)
        //{
        //    ViewBag.People = new SelectList(db.People, "PersonId", "FullnameWithYears");

        //    Movie movie = db.Movies.Find(MovieId);

        //    if (movie == null)
        //    {
        //        return RedirectToAction("List", new { message = StatusMessage.MovieNotFound });
        //    }

        //    if (movie.Delete == true)
        //    {
        //        return RedirectToAction("List", new { message = StatusMessage.MovieDeleted });
        //    }

        //    ViewBag.Movie = movie;

        //    //return View();
        //    return PartialView("_AddActor");
        //}

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult AddActor1(Actor actor)
        //{
        //    db.Actors.Add(actor);
        //    db.SaveChanges();

        //    return Redirect("/Movie/Edit?MovieId=" + actor.MovieId);
        //}

        [HttpGet]
        [Authorize(Roles = "Администратор")]
        public ActionResult AddPerson(int? MovieId)
        {
            ViewBag.People = new SelectList(db.People.Where(p => p.Delete == false), "PersonId", "FullnameWithYears");
            ViewBag.CrewPositions = new SelectList(db.CrewPositions, "PositionId", "Name");

            Movie movie = db.Movies.Find(MovieId);

            if (movie == null)
            {
                return RedirectToAction("List", new { message = StatusMessage.MovieNotFound });
            }

            if (movie.Delete == true)
            {
                return RedirectToAction("List", new { message = StatusMessage.MovieDeleted });
            }

            ViewBag.Movie = movie;

            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Администратор")]
        public ActionResult AddPerson(FilmCrew filmCrew)
        {
            db.FilmCrew.Add(filmCrew);
            db.SaveChanges();

            return Redirect("/Movie/Edit?MovieId=" + filmCrew.MovieId);
        }

        [HttpGet]
        [Authorize(Roles = "Администратор")]
        public ActionResult DeletePerson(int PersonId, int MovieId, int PositionId)
        {
            Person person = db.People.Find(PersonId);
            if (person == null)
            {
                return RedirectToAction("Edit", new { message = StatusMessage.PersonNotFound });
            }

            if (person.Delete == true)
            {
                return RedirectToAction("Edit", new { message = StatusMessage.PersonDeleted });
            }

            Movie movie = db.Movies.Find(MovieId);
            if (movie == null)
            {
                return RedirectToAction("List", new { message = StatusMessage.MovieNotFound });
            }

            if (movie.Delete == true)
            {
                return RedirectToAction("List", new { message = StatusMessage.MovieDeleted });
            }


            FilmCrew filmCrew = db.FilmCrew.Find(PersonId, MovieId, PositionId);
            if (filmCrew == null)
            {
                return RedirectToAction("Edit", new { message = StatusMessage.PositionNotFound });
            }

            db.FilmCrew.Remove(filmCrew);
            db.SaveChanges();

            return RedirectToAction("Edit", new { MovieId = MovieId, message = StatusMessage.PersonDeleteSuccess });
        }

        [HttpGet]
        [Authorize(Roles = "Пользователь")]
        public ActionResult AddFavorite(int movieId)
        {
            Movie movie = db.Movies.Find(movieId);

            if (movie == null)
            {
                return RedirectToAction("List", new { message = StatusMessage.MovieNotFound });
            }

            if (movie.Delete == true)
            {
                return RedirectToAction("List", new { message = StatusMessage.MovieDeleted });
            }

            var currentUser = db.Users.Find(User.Identity.GetUserId());

            if(movie.User.Contains(currentUser))
            {
                return RedirectToAction("Index", new { movieId = movie.MovieId, message = StatusMessage.FavAddError });
            }
            else
            {
                movie.User.Add(currentUser);
                db.Entry(movie).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", new { movieId = movie.MovieId, message = StatusMessage.FavAddSuccess });
            }
        }

        [HttpGet]
        [Authorize(Roles = "Пользователь")]
        public ActionResult DeleteFavorite(int movieId)
        {
            Movie movie = db.Movies.Find(movieId);

            if (movie == null)
            {
                return RedirectToAction("List", new { message = StatusMessage.MovieNotFound });
            }

            if (movie.Delete == true)
            {
                return RedirectToAction("List", new { message = StatusMessage.MovieDeleted });
            }

            var currentUser = db.Users.Find(User.Identity.GetUserId());

            if (movie.User.Contains(currentUser))
            {
                movie.User.Remove(currentUser);
                db.Entry(movie).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", new { movieId = movie.MovieId, message = StatusMessage.FavDeleteSuccess });
            }
            else
            {
                return RedirectToAction("Index", new { movieId = movie.MovieId, message = StatusMessage.FavDeleteError });
            }
        }

        [HttpGet]
        [Authorize(Roles = "Пользователь")]
        public ActionResult Favorites()
        {
            MoviesLabUser user = db.Users.Find(User.Identity.GetUserId());

            List<Movie> favMovie = new List<Movie>();
            if (user.FavMovie != null)
            {
                favMovie = user.FavMovie.Where(fm => fm.Delete = false).ToList();
            }

            return View(favMovie);
        }

        [AllowAnonymous]
        public PartialViewResult MoviesSearch(string search)
        {

            List<Movie> movies = db.Movies.Where((m => m.Title.Contains(search) && m.Delete == false || m.Description.Contains(search) && m.Delete == false)).Distinct().ToList();

            return PartialView("_Search", movies);
        }

        public enum StatusMessage
        {
            FavAddSuccess,
            FavAddError,
            FavDeleteSuccess,
            FavDeleteError,
            MovieDeleteSuccess,
            MovieDeleteError,
            MovieNotFound,
            MovieDeleted,
            MovieRestoreSuccess,
            MovieRestoreError,
            ActorNotFound,
            PositionNotFound,
            PersonNotFound,
            PersonDeleted,
            PersonDeleteSuccess,
            ActorDeleteSuccess,
            Error
        }
    }
}