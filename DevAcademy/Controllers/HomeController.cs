using DevAcademy.Cryptography;
using DevAcademy.Data;
using DevAcademy.Extensions;
using DevAcademy.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace DevAcademy.Controllers
{
    public class HomeController : Controller
    {
        const string sessionId = "1";
        const string sessionFirstName = "firstName";
        const string sessionLastName = "lastName";
        const string sessionUsername = "username";
        const string sessionEmail = "email";
        const string sessionAccountType = "accountType";
        const string sessionPassword = "password";
        const string sessionDate = "date";

        private readonly ApplicationDataContext _dataBase;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, ApplicationDataContext dataBase)
        {
            _logger = logger;
            _dataBase = dataBase;
        }

        public IActionResult Index()
        {
            HttpContext.Session.Clear();
            return View();
        }

        public IActionResult About()
        {
            return View();
        }

        public IActionResult Contacts()
        {
            return View();
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(User user)
        {
            DateTime dateAndTime = DateTime.Now;
            user.CreatedDate = dateAndTime.ToString("dd/MM/yyyy");

            if (ModelState.IsValid)
            {
                var check = _dataBase.Users.FirstOrDefault(s => s.Email == user.Email);
                if (check == null)
                {
                    user.Password = Crypting.Crypt(user.Password);
                    user.ConfirmPassword = Crypting.Crypt(user.ConfirmPassword);
                    _dataBase.Users.Add(user);
                    _dataBase.SaveChanges();

                    if (user.AccountType == "Admin")
                    {
                        Admin admin = new Admin();
                        admin.FirstName = user.FirstName;
                        admin.LastName = user.LastName;
                        admin.Username = user.Username;
                        admin.Email = user.Email;
                        admin.Password = user.Password;
                        admin.ConfirmPassword = user.ConfirmPassword;
                        admin.AccountType = user.AccountType;
                        admin.CreatedDate = user.CreatedDate;
                        admin.Categories = new List<Category>();
                        admin.Courses = new List<Course>();
                        admin.Sections = new List<Section>(); 
                        _dataBase.Admins.Add(admin);
                        _dataBase.SaveChanges();

                        return RedirectToAction("Login");
                    }
                    else if (user.AccountType == "Teacher")
                    {
                        Teacher teacher = new Teacher();
                        teacher.FirstName = user.FirstName;
                        teacher.LastName = user.LastName;
                        teacher.Username = user.Username;
                        teacher.Email = user.Email;
                        teacher.Password = user.Password;
                        teacher.ConfirmPassword = user.ConfirmPassword;
                        teacher.AccountType = user.AccountType;
                        teacher.CreatedDate = user.CreatedDate;
                        teacher.Categories = new List<Category>();
                        teacher.Courses = new List<Course>();
                        teacher.Sections = new List<Section>();
                        _dataBase.Teachers.Add(teacher);
                        _dataBase.SaveChanges();

                        return RedirectToAction("Login");
                    }
                    else if (user.AccountType == "Student")
                    {
                        Student student = new Student();
                        student.FirstName = user.FirstName;
                        student.LastName = user.LastName;
                        student.Username = user.Username;
                        student.Email = user.Email;
                        student.Password = user.Password;
                        student.ConfirmPassword = user.ConfirmPassword;
                        student.AccountType = user.AccountType;
                        student.CreatedDate = user.CreatedDate;
                        student.Bio = null;
                        student.Points = 0;
                        student.Progress = 0;
                        student.TimeSpent = "00h:00m:01s";
                        student.Courses = null;
                        _dataBase.Students.Add(student);
                        _dataBase.SaveChanges();

                        return RedirectToAction("Login");
                    }
                    else
                    {
                        ViewBag.error = "Wrong account type.";
                        return View();
                    }
                }
                else
                {
                    ViewBag.error = "Email already exists.";
                    return View();
                }
            }
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(string email, string password)
        {
            if (ModelState.IsValid)
            {
                var cryptedPassword = Crypting.Crypt(password);
                var data = _dataBase.Users.Where(s => s.Email.Equals(email) && s.Password.Equals(cryptedPassword)).ToList();

                if (data.Count() > 0)
                {
                    HttpContext.Session.SetString(sessionId, data[0].Id.ToString());
                    HttpContext.Session.SetString(sessionFirstName, data[0].FirstName);
                    HttpContext.Session.SetString(sessionLastName, data[0].LastName);
                    HttpContext.Session.SetString(sessionUsername, data[0].Username);
                    HttpContext.Session.SetString(sessionEmail, data[0].Email);
                    HttpContext.Session.SetString(sessionAccountType, data[0].AccountType);
                    HttpContext.Session.SetString(sessionPassword, data[0].Password);
                    HttpContext.Session.SetString(sessionDate, data[0].CreatedDate);

                    if (data[0].AccountType == "Admin")
                    {
                        var admin = _dataBase.Admins.Where(s => s.Email.Equals(email) && s.Password.Equals(cryptedPassword)).ToList();

                        TempData.Put("userdata", admin[0]);

                        return RedirectToAction("Dashboard", "Admin");
                    }
                    else if (data[0].AccountType == "Teacher")
                    {
                        var teacher = _dataBase.Teachers.Where(s => s.Email.Equals(email) && s.Password.Equals(cryptedPassword)).ToList();

                        TempData.Put("userdata", teacher[0]);

                        return RedirectToAction("Dashboard", "Teacher");
                    }
                    else if (data[0].AccountType == "Student")
                    {
                        var student = _dataBase.Students.Where(s => s.Email.Equals(email) && s.Password.Equals(cryptedPassword)).ToList();

                        TempData.Put("userdata", student[0]);

                        return RedirectToAction("Dashboard", "Student");
                    }
                }
                else
                {
                    ViewBag.error = "Login Failed.";
                    return View();
                }
            }
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}