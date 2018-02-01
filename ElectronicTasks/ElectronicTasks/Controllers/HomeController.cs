using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ElectronicTasks.Models;
using ElectronicTasks.App_Start;

namespace ElectronicTasks.Controllers
{
    public class HomeController : Controller
    {
        private ConnectionContext db = new ConnectionContext();
        public static User user = new User();  

        #region Стандартные методы
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        #endregion

        /*Страница авторизации*/
        public ActionResult Login()
        {
            AuthModel model = new AuthModel();
            return View(model);
        }

        /*Страница регистрации*/
        public ActionResult Register()
        {
            return View();
        }

        /*Главная страница*/
        public ActionResult MainPage(int id)
        {
            var userInDb = from u in db.Users where u.UserId == id select u;
            if (userInDb != null)
            {
                user = userInDb.First();
            }            
            ViewBag.User = user;
            return View();
        }

        [HttpPost]
        public ActionResult AddUser(string name,string secondName,string email, string password)
        {
            User user = new User();
            user.Name = name;
            user.SecondName = secondName;
            user.Email = email;
            user.Password = password;
            try
            {
                var currentUsers = db.Users;
                bool isAlreadyExists = false;
                foreach (User userFromBase in currentUsers)
                {
                    if (userFromBase.Email == user.Email)
                    {
                        isAlreadyExists = true;
                    }
                }
                if (!isAlreadyExists)
                {
                    db.Users.Add(user);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                db.SaveChanges();
            }
            return RedirectToAction("Login","Home");
        }

        [HttpPost]
        public JsonResult checkEmailToExist(string email)
        {
            string result = "NOT_FOUND";
            var currentUsers = db.Users;
            foreach (User userFromBase in currentUsers)
            {
                if (userFromBase.Email == email)
                {
                    result = "FOUND";
                }
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Login(string email,string password)
        {
            bool isUserExists=false;
            int userId = 0;
            foreach(User user in db.Users)
            {
                if (user.Email == email && user.Password == password)
                {
                    isUserExists = true;
                    userId = user.UserId;
                }
            }

            if (isUserExists)
            {
                return RedirectToAction("MainPage", "Home", new { id = userId });
            }
            else
            {
                AuthModel model = new AuthModel();
                model.email = email;
                model.password = password;
                model.errMsg = "Логин или пароль не верный!";
                return View(model);
            }
        }

        public ActionResult UserNotFound()
        {
            return View();
        }

        public ActionResult ShowTaskList()
        {
            var tasks = from t in db.Tasks where t.UserId == user.UserId select t;
            if (tasks.Any())
            {
                ViewBag.Tasks = tasks;
                ViewBag.User = user;
                return View();
            }
            else
            {
                return RedirectToAction("TasksNotFound","Home");
            }
        }

        public JsonResult TaskDone(int taskId)
        {
            string result="";
            var task = db.Tasks.Find(taskId);
            task.IsDone = true;
            db.SaveChanges();
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult TasksNotFound()
        {
            return View();
        }

        public ActionResult AddNewTask(string taskName, string taskText, DateTime taskDate)
        {
            Task newTask =new Task();
            newTask.TaskName = taskName;
            newTask.TaskText = taskText;
            newTask.TermDate = taskDate;
            newTask.UploadDate = DateTime.Now;
            newTask.IsDone = false;
            newTask.UserId = user.UserId;
            db.Tasks.Add(newTask);
            db.SaveChanges();
            return View();
        }
    }


}