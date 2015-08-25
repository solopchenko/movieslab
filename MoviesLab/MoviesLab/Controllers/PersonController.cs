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
    public class PersonController : Controller
    {
        //Создаем контекст данных
        MoviesLabDbContext db = new MoviesLabDbContext();

        //Список персон
        [AllowAnonymous]
        public ActionResult List(int? page, StatusMessage? message)
        {
            ViewBag.StatusMessage =
            message == StatusMessage.PersonDeleteError ? "При удалении персоны произошла ошибка."
            : message == StatusMessage.PersonNotFound ? "Персона не найдена."
            : message == StatusMessage.PersonDeleted ? "Доступ к персоне ограничен."
            : "";

            //Получаем из базы данных все нескрытые объекты Person
            List<Person> people = db.People.Where(p => p.Delete == false).ToList();


            //Количесство объектов на странице
            int pageSize = 5;
            int pageNumber = (page ?? 1);

            //Возвращаем представление
            return View(people.ToPagedList(pageNumber, pageSize));
        }

        //Список удаленных персон
        [Authorize(Roles = "Администратор")]
        public ActionResult Deleted(int? page, StatusMessage? message)
        {
            ViewBag.StatusMessage =
            message == StatusMessage.PersonDeleteSuccess ? "Персона успешно удалена."
            : message == StatusMessage.PersonRestoreError ? "Не удалось восстановить информацию о персоне."
            : message == StatusMessage.PersonRestoreSuccess ? "Информация о персоне успешно восстановлена."
            : "";

            //Получаем из базы данных все объекты Movie
            List<Person> people = db.People.Where(p => p.Delete == true).ToList();

            //Количесство объектов на странице
            int pageSize = 5;
            int pageNumber = (page ?? 1);

            //Возвращаем представление
            return View("Deleted", (people.ToPagedList(pageNumber, pageSize)));
        }

        [AllowAnonymous]
        public ActionResult Index(int? PersonId, StatusMessage? message)
        {
            ViewBag.StatusMessage =
            message == StatusMessage.FavAddSuccess ? "Персона успешно добавлена в избранное."
            : message == StatusMessage.FavAddError ? "Ошибка добавления персоны в избранное."
            : message == StatusMessage.FavDeleteSuccess ? "Персона успешно удалёна из избранного."
            : message == StatusMessage.FavDeleteError ? "Ошибка удаления персоны из избранного."
            : "";

            Person person = db.People.Find(PersonId);

            if (person == null)
            {
                return RedirectToAction("List", new { message = StatusMessage.PersonNotFound });
            }

            if (person.Delete == true)
            {
                return RedirectToAction("List", new { message = StatusMessage.PersonDeleted });
            }

            ViewBag.PersonInFav = false;


            if (User.Identity.IsAuthenticated)
            {
                var currentUser = db.Users.Find(User.Identity.GetUserId());

                if (person.User.Contains(currentUser))
                {
                    ViewBag.PersonInFav = true;
                }
            }

            //Возвращаем представление
            return View(person);
        }

        [HttpGet]
        [Authorize(Roles = "Администратор")]
        public ActionResult Add()
        {
            ViewBag.Countries = new SelectList(db.Countries, "CountryId", "Name");

            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Администратор")]
        public ActionResult Add(Person person)
        {
            db.People.Add(person);
            db.SaveChanges();

            return Redirect("/Person?PersonId=" + person.PersonId);
        }


        [HttpGet]
        [Authorize(Roles = "Администратор")]
        public ActionResult Edit(int? PersonId)
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

            ViewBag.Countries = new SelectList(db.Countries, "CountryId", "Name");

            return View(person);
        }

        [HttpPost]
        [Authorize(Roles = "Администратор")]
        public ActionResult Edit(Person person)
        {
            db.Entry(person).State = EntityState.Modified;
            db.SaveChanges();

            return Redirect("/Person?PersonId=" + person.PersonId);
        }

        [Authorize(Roles = "Администратор")]
        public ActionResult Delete(int? PersonId)
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

            person.Delete = true;
            db.Entry(person).State = EntityState.Modified;
            db.SaveChanges();

            //Возвращаемся к списку фильмов
            return RedirectToAction("Deleted", new { message = StatusMessage.PersonDeleteSuccess });
        }

        [HttpGet]
        [Authorize(Roles = "Администратор")]
        public ActionResult AddRole(int? PersonId)
        {
            ViewBag.Movies = new SelectList(db.Movies, "MovieId", "TitleWithYear");

            Person person = db.People.Find(PersonId);
            if (person == null)
            {
                return RedirectToAction("List", new { message = StatusMessage.PersonNotFound });
            }

            if (person.Delete == true)
            {
                return RedirectToAction("List", new { message = StatusMessage.PersonDeleted });
            }

            ViewBag.Person = person;

            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Администратор")]
        public ActionResult AddRole(Actor actor)
        {

            db.Actors.Add(actor);
            db.SaveChanges();

            return Redirect("/Person?PersonId=" + actor.PersonId);
        }

        [HttpGet]
        [Authorize(Roles = "Администратор")]
        public ActionResult AddMovie(int? PersonId)
        {
            ViewBag.Movies = new SelectList(db.Movies, "MovieId", "TitleWithYear");
            ViewBag.CrewPositions = new SelectList(db.CrewPositions, "PositionId", "Name");

            Person person = db.People.Find(PersonId);

            if (person == null)
            {
                return RedirectToAction("List", new { message = StatusMessage.PersonNotFound });
            }

            if (person.Delete == true)
            {
                return RedirectToAction("List", new { message = StatusMessage.PersonDeleted });
            }

            ViewBag.Person = person;

            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Администратор")]
        public ActionResult AddPerson(FilmCrew filmCrew)
        {
            db.FilmCrew.Add(filmCrew);
            db.SaveChanges();

            return Redirect("/Person?PersonId=" + filmCrew.PersonId);
        }

        [HttpGet]
        [Authorize(Roles = "Пользователь")]
        public ActionResult AddFavorite(int personId)
        {

            Person person = db.People.Find(personId);
            if (person == null)
            {
                return RedirectToAction("List", new { message = StatusMessage.PersonNotFound });
            }

            if (person.Delete == true)
            {
                return RedirectToAction("List", new { message = StatusMessage.PersonDeleted });
            }

            var currentUser = db.Users.Find(User.Identity.GetUserId());

            if (person.User.Contains(currentUser))
            {
                return RedirectToAction("Index", new { PersonId = person.PersonId, message = StatusMessage.FavAddError });
            }
            else
            {
                person.User.Add(currentUser);
                db.Entry(person).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", new { PersonId = person.PersonId, message = StatusMessage.FavAddSuccess });
            }
        }

        [HttpGet]
        [Authorize(Roles = "Пользователь")]
        public ActionResult DeleteFavorite(int personId)
        {
            Person person = db.People.Find(personId);
            if (person == null)
            {
                return RedirectToAction("List", new { message = StatusMessage.PersonNotFound });
            }

            if (person.Delete == true)
            {
                return RedirectToAction("List", new { message = StatusMessage.PersonDeleted });
            }

            var currentUser = db.Users.Find(User.Identity.GetUserId());

            if (person.User.Contains(currentUser))
            {
                person.User.Remove(currentUser);
                db.Entry(person).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", new { PersonId = person.PersonId, message = StatusMessage.FavDeleteSuccess });
            }
            else
            {
                return RedirectToAction("Index", new { PersonId = person.PersonId, message = StatusMessage.FavDeleteError });
            }
        }

        [HttpGet]
        [Authorize(Roles = "Пользователь")]
        public ActionResult Favorites()
        {
            MoviesLabUser user = db.Users.Find(User.Identity.GetUserId());

            List<Person> favPerson = new List<Person>();
            if (user.FavMovie != null)
            {
                favPerson = user.FavPerson.Where(fp => fp.Delete = false).ToList();
            }

            return View(favPerson);
        }

        [AllowAnonymous]
        public PartialViewResult PeopleSearch(string search)
        {

            List<Person> people = db.People.Where((p => p.Fullname.Contains(search) && p.Delete == false || p.Biography.Contains(search) && p.Delete == false)).Distinct().ToList();

            return PartialView("_Search", people);
        }

        public enum StatusMessage
        {
            FavAddSuccess,
            FavAddError,
            FavDeleteSuccess,
            FavDeleteError,
            PersonDeleteSuccess,
            PersonDeleteError,
            PersonNotFound,
            PersonDeleted,
            PersonRestoreError,
            PersonRestoreSuccess,
            Error
        }

        public object[] PersonId { get; set; }
    }
}
