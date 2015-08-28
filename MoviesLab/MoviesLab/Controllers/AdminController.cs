using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using PagedList.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;
using MoviesLab.Models;
using System.Data.Entity;


namespace MoviesLab.Controllers
{
    public class AdminController : Controller
    {
        //Создаем контекст данных
        MoviesLabDbContext db = new MoviesLabDbContext();

        //
        // GET: /Admin/
        [Authorize(Roles = "Администратор")]
        public ActionResult Index()
        {
            return RedirectToAction("UserList");
        }

        [Authorize(Roles = "Администратор")]
        public ActionResult UserList(int? page, StatusMessage? message)
        {
            ViewBag.StatusMessage =
            message == StatusMessage.UserAlreadyInRole ? "Пользователь уже находится в данной роли."
            : message == StatusMessage.UserDeleteGrantSuccess ? "Роль успешно удалена."
            : message == StatusMessage.UserGrantSuccess ? "Роль успешно назначена."
            : message == StatusMessage.UserIsntInRole ? "Пользователь не находится в данной роли."
            : message == StatusMessage.UserNotFound ? "Пользователь не найден."
            : "";

            //Получаем из базы данных все объекты Movie
            List<MoviesLabUser> users = db.Users.ToList();

            //Количесство объектов на странице
            int pageSize = 10;
            int pageNumber = (page ?? 1);

            //Возвращаем представление
            return View(users.ToPagedList(pageNumber, pageSize));
        }

        [Authorize(Roles = "Администратор")]
        [HttpGet]
        public ActionResult GrantAdminPermission(string userId)
        {
            List<MoviesLabUser> user = db.Users.ToList();
            if (user == null)
            {
                return RedirectToAction("UserList", new { message = StatusMessage.UserNotFound });
            }

            var userManager = new UserManager<MoviesLabUser>(new UserStore<MoviesLabUser>(db));

            if (userManager.IsInRole(userId, "Администратор"))
            {
                return RedirectToAction("UserList", new { message = StatusMessage.UserAlreadyInRole });
            }
            else
            {
                userManager.AddToRole(userId, "Администратор");
                return RedirectToAction("UserList", new { message = StatusMessage.UserGrantSuccess });
            }
        }

        [Authorize(Roles = "Администратор")]
        [HttpGet]
        public ActionResult DeleteAdminPermission(string userId)
        {
            List<MoviesLabUser> user = db.Users.ToList();
            
            if (user == null)
            {
                return RedirectToAction("UserList", new { message = StatusMessage.UserNotFound });
            }

            var userManager = new UserManager<MoviesLabUser>(new UserStore<MoviesLabUser>(db));

            if (userManager.IsInRole(userId, "Администратор"))
            {
                userManager.RemoveFromRole(userId, "Администратор");
                return RedirectToAction("UserList", new { message = StatusMessage.UserDeleteGrantSuccess });
            }
            else
            {
                return RedirectToAction("UserList", new { message = StatusMessage.UserIsntInRole });
            }
        }

        public enum StatusMessage
        {
            UserNotFound,
            UserAlreadyInRole,
            UserGrantSuccess,
            UserIsntInRole,
            UserDeleteGrantSuccess,
        }
    }
}