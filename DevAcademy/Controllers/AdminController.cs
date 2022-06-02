using DevAcademy.Cryptography;
using DevAcademy.Data;
using DevAcademy.Extensions;
using DevAcademy.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace DevAcademy.Controllers
{
    public class AdminController : Controller
    {
        Admin admin = new Admin();
        string sessionId = "1";
        string sessionUsername = "username";
        string sessionEmail = "email";

        private readonly ApplicationDataContext _dataBase;

        public AdminController(ApplicationDataContext dataBase)
        {
            _dataBase = dataBase;
        }

        public IActionResult Dashboard()
        {
            var userslist = _dataBase.Users;
            var adminslist = _dataBase.Admins;
            var teacherslist = _dataBase.Teachers;
            var studentslist = _dataBase.Students;
            var categoriesList = _dataBase.Categories;
            var coursesList = _dataBase.Courses;
            var sectionsList = _dataBase.Sections;

            if (TempData["userdata"] != null)
            {
                var temp = TempData.Get<Admin>("userdata");
                HttpContext.Session.SetString(sessionId, temp.Id.ToString());
                HttpContext.Session.SetString(sessionUsername, temp.Username);
                HttpContext.Session.SetString(sessionEmail, temp.Email);

                var generalAdmin = _dataBase.Admins.Where(x => x.Id == temp.Id)
                                                .FirstOrDefault();

                ViewData["Admin"] = generalAdmin;
                ViewData["Users"] = userslist;
                ViewData["Admins"] = adminslist;
                ViewData["Teachers"] = teacherslist;
                ViewData["Students"] = studentslist;
                ViewData["Categories"] = categoriesList;
                ViewData["Courses"] = coursesList;
                ViewData["Sections"] = sectionsList;

                return View(generalAdmin);
            }
            else
            {
                admin.Id = int.Parse(HttpContext.Session.GetString(sessionId));
                admin.Username = HttpContext.Session.GetString(sessionUsername);
                admin.Email = HttpContext.Session.GetString(sessionEmail);

                var generalAdmin = _dataBase.Admins.Where(x => x.Id == admin.Id)
                                                .FirstOrDefault();

                ViewData["Admin"] = generalAdmin;
                ViewData["Users"] = userslist;
                ViewData["Admins"] = adminslist;
                ViewData["Teachers"] = teacherslist;
                ViewData["Students"] = studentslist;
                ViewData["Categories"] = categoriesList;
                ViewData["Courses"] = coursesList;
                ViewData["Sections"] = sectionsList;

                return View(generalAdmin);
            }
        }

        //User
        [HttpGet]
        public IActionResult AddUser()
        {
            admin.Id = int.Parse(HttpContext.Session.GetString(sessionId));
            var generalAdmin = _dataBase.Admins.Where(x => x.Id == admin.Id)
                                                .FirstOrDefault();

            ViewData["Admin"] = generalAdmin;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddUser(User user)
        {
            admin.Id = int.Parse(HttpContext.Session.GetString(sessionId));
            var generalAdmin = _dataBase.Admins.Where(x => x.Id == admin.Id)
                                                .FirstOrDefault();

            ViewData["Admin"] = generalAdmin;

            DateTime date = DateTime.Now;
            user.CreatedDate = date.ToString("dd/MM/yyyy");

            if (ModelState.IsValid)
            {
                var check = _dataBase.Users.FirstOrDefault(s => s.Email == user.Email);

                if (check == null)
                {
                    if (user.AccountType != "Admin" && user.AccountType != "Teacher" && user.AccountType != "Student")
                    {
                        ViewBag.error = "Invalid account type";
                        return View();
                    }

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

                        return RedirectToAction("Dashboard");
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
                    ViewBag.error = "Email already exists";
                    return View();
                }
            }
            return View(user);
        }

        [HttpGet]
        public IActionResult DeleteUser(int? id)
        {
            admin.Id = int.Parse(HttpContext.Session.GetString(sessionId));
            var generalAdmin = _dataBase.Admins.Where(x => x.Id == admin.Id)
                                                .FirstOrDefault();

            ViewData["Admin"] = generalAdmin;

            if (id == null || id == 0)
            {
                return NotFound();
            }

            var findUser = _dataBase.Users.Find(id);

            if (findUser == null)
            {
                return NotFound();
            }

            return View(findUser);
        }

        [HttpPost]
        public IActionResult DeleteUserPost(int? id)
        {
            admin.Id = int.Parse(HttpContext.Session.GetString(sessionId));
            var generalAdmin = _dataBase.Admins.Where(x => x.Id == admin.Id)
                                                .FirstOrDefault();

            ViewData["Admin"] = generalAdmin;

            var findUser = _dataBase.Users.Find(id);

            if (findUser == null)
            {
                return NotFound();
            }


            if (findUser.AccountType == "Admin")
            {
                var findAdmin = _dataBase.Admins.Where(x => x.Email == findUser.Email)
                                                .FirstOrDefault();

                var categories = _dataBase.Categories;
                foreach (var category in categories)
                {
                    if (category.AdminID == findAdmin.Id)
                    {
                        category.AdminID = null;
                        _dataBase.Categories.Update(category);
                    }
                }

                var courses = _dataBase.Courses;
                foreach (var course in courses)
                {
                    if (course.AdminID == findAdmin.Id)
                    {
                        course.AdminID = null;
                        _dataBase.Courses.Update(course);
                    }
                }

                var sections = _dataBase.Sections;
                foreach (var section in sections)
                {
                    if (section.AdminID == findAdmin.Id)
                    {
                        section.AdminID = null;
                        _dataBase.Sections.Update(section);
                    }
                }

                _dataBase.Admins.Remove(findAdmin);
                _dataBase.SaveChanges();
            }
            else if (findUser.AccountType == "Teacher")
            {
                var findTeacher = _dataBase.Teachers.Where(x => x.Email == findUser.Email)
                                                    .FirstOrDefault();

                var categories = _dataBase.Categories;
                foreach (var category in categories)
                {
                    if (category.TeacherID == findTeacher.Id)
                    {
                        category.TeacherID = null;
                        _dataBase.Categories.Update(category);
                    }
                }

                var courses = _dataBase.Courses;
                foreach (var course in courses)
                {
                    if (course.TeacherID == findTeacher.Id)
                    {
                        course.TeacherID = null;
                        _dataBase.Courses.Update(course);
                    }
                }

                var sections = _dataBase.Sections;
                foreach (var section in sections)
                {
                    if (section.TeacherID == findTeacher.Id)
                    {
                        section.TeacherID = null;
                        _dataBase.Sections.Update(section);
                    }
                }

                _dataBase.Teachers.Remove(findTeacher);
                _dataBase.SaveChanges();
            }
            else if (findUser.AccountType == "Student")
            {
                var findStudent = _dataBase.Students.Where(x => x.Email == findUser.Email)
                                                    .FirstOrDefault();

                string result = null;
                var courses = _dataBase.Courses;
                foreach (var course in courses)
                {
                    if (course.StudentsIDs != null)
                    {
                        var array = course.StudentsIDs.Split(',', StringSplitOptions.RemoveEmptyEntries);

                        for (int i = 0; i < array.Length; i++)
                        {
                            int studentId = int.Parse(array[i].ToString());
                            if (studentId != findStudent.Id)
                            {
                                result += studentId + ",";
                            }
                        }

                        course.StudentsIDs = result;
                    }
                    _dataBase.Courses.Update(course);
                    result = null;
                }

                _dataBase.Students.Remove(findStudent);
                _dataBase.SaveChanges();
            }

            _dataBase.Users.Remove(findUser);
            _dataBase.SaveChanges();
            return RedirectToAction("Dashboard");
        }

        [HttpGet]
        public IActionResult UpdateUser(int? id)
        {
            admin.Id = int.Parse(HttpContext.Session.GetString(sessionId));
            var generalAdmin = _dataBase.Admins.Where(x => x.Id == admin.Id)
                                                .FirstOrDefault();

            ViewData["Admin"] = generalAdmin;

            if (id == null || id == 0)
            {
                return NotFound();
            }
            var findUser = _dataBase.Users.Find(id);

            if (findUser == null)
            {
                return NotFound();
            }

            return View(findUser);
        }

        [HttpPost]
        public IActionResult UpdateUser(User user)
        {
            admin.Id = int.Parse(HttpContext.Session.GetString(sessionId));
            var generalAdmin = _dataBase.Admins.Where(x => x.Id == admin.Id)
                                                .FirstOrDefault();

            ViewData["Admin"] = generalAdmin;

            if (ModelState.IsValid)
            {

                if (user.AccountType != "Admin" && user.AccountType != "Teacher" && user.AccountType != "Student")
                {
                    ViewBag.error = "Invalid account type";
                    return View();
                }

                int id = user.Id;
                var findUser = _dataBase.Users.Where(x => x.Id == id)
                                              .FirstOrDefault();

                user.Password = Crypting.Crypt(user.Password);
                user.ConfirmPassword = Crypting.Crypt(user.ConfirmPassword);

                _dataBase.Entry(findUser).CurrentValues.SetValues(user);
                _dataBase.SaveChanges();


                if (user.AccountType == "Admin")
                {
                    var findAdmin = _dataBase.Admins.Where(x => x.Email == findUser.Email)
                                                    .FirstOrDefault();
                    findAdmin.FirstName = user.FirstName;
                    findAdmin.LastName = user.LastName;
                    findAdmin.Username = user.Username;
                    findAdmin.Email = user.Email;
                    findAdmin.Password = user.Password;
                    findAdmin.ConfirmPassword = user.ConfirmPassword;
                    _dataBase.Admins.Update(findAdmin);
                    _dataBase.SaveChanges();

                    return RedirectToAction("Dashboard");
                }
                else if (user.AccountType == "Teacher")
                {
                    var findTeacher = _dataBase.Teachers.Where(x => x.Email == findUser.Email)
                                                    .FirstOrDefault();
                    findTeacher.FirstName = user.FirstName;
                    findTeacher.LastName = user.LastName;
                    findTeacher.Username = user.Username;
                    findTeacher.Email = user.Email;
                    findTeacher.Password = user.Password;
                    findTeacher.ConfirmPassword = user.ConfirmPassword;
                    _dataBase.Teachers.Update(findTeacher);
                    _dataBase.SaveChanges();

                    return RedirectToAction("Dashboard");
                }
                else if (user.AccountType == "Student")
                {
                    var findStudent = _dataBase.Students.Where(x => x.Email == findUser.Email)
                                                        .FirstOrDefault();
                    findStudent.FirstName = user.FirstName;
                    findStudent.LastName = user.LastName;
                    findStudent.Username = user.Username;
                    findStudent.Email = user.Email;
                    findStudent.Password = user.Password;
                    findStudent.ConfirmPassword = user.ConfirmPassword;
                    _dataBase.Students.Update(findStudent);
                    _dataBase.SaveChanges();

                    return RedirectToAction("Dashboard");
                }

                _dataBase.Users.Update(user);
                _dataBase.SaveChanges();

            }
            return View(user);
        }

        //Admin
        [HttpGet]
        public IActionResult AddAdmin()
        {
            admin.Id = int.Parse(HttpContext.Session.GetString(sessionId));
            var generalAdmin = _dataBase.Admins.Where(x => x.Id == admin.Id)
                                                .FirstOrDefault();

            ViewData["Admin"] = generalAdmin;

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddAdmin(Admin adminn)
        {
            admin.Id = int.Parse(HttpContext.Session.GetString(sessionId));
            var generalAdmin = _dataBase.Admins.Where(x => x.Id == admin.Id)
                                                .FirstOrDefault();

            ViewData["Admin"] = generalAdmin;

            DateTime date = DateTime.Now;
            adminn.CreatedDate = date.ToString("dd/MM/yyyy");
            adminn.AccountType = "Admin";
            adminn.Categories = new List<Category>();
            adminn.Courses = new List<Course>();
            adminn.Sections = new List<Section>();

            if (ModelState.IsValid)
            {
                var check = _dataBase.Admins.FirstOrDefault(s => s.Email == adminn.Email);

                if (check == null)
                {
                    adminn.Password = Crypting.Crypt(adminn.Password);
                    adminn.ConfirmPassword = Crypting.Crypt(adminn.ConfirmPassword);
                    _dataBase.Admins.Add(adminn);
                    _dataBase.SaveChanges();

                    User user = new User();
                    user.FirstName = adminn.FirstName;
                    user.LastName = adminn.LastName;
                    user.Username = adminn.Username;
                    user.Email = adminn.Email;
                    user.AccountType = "Admin";
                    user.Password = adminn.Password;
                    user.ConfirmPassword = adminn.ConfirmPassword;
                    user.CreatedDate = adminn.CreatedDate;
                    _dataBase.Users.Add(user);
                    _dataBase.SaveChanges();

                    return RedirectToAction("Dashboard");
                }
                else
                {
                    ViewBag.error = "Email already exists";
                    return View();
                }
            }
            return View(adminn);
        }

        [HttpGet]
        public IActionResult DeleteAdmin(int? id)
        {
            admin.Id = int.Parse(HttpContext.Session.GetString(sessionId));
            var generalAdmin = _dataBase.Admins.Where(x => x.Id == admin.Id)
                                                .FirstOrDefault();

            ViewData["Admin"] = generalAdmin;

            var categoriesList = _dataBase.Categories;
            ViewData["Categories"] = categoriesList;
            var coursesList = _dataBase.Courses;
            ViewData["Courses"] = coursesList;
            var sectionsList = _dataBase.Sections;
            ViewData["Sections"] = sectionsList;

            if (id == null || id == 0)
            {
                return NotFound();
            }

            var findAdmin = _dataBase.Admins.Find(id);

            if (findAdmin == null)
            {
                return NotFound();
            }

            return View(findAdmin);
        }

        [HttpPost]
        public IActionResult DeleteAdminPost(int? id)
        {
            admin.Id = int.Parse(HttpContext.Session.GetString(sessionId));
            var generalAdmin = _dataBase.Admins.Where(x => x.Id == admin.Id)
                                                .FirstOrDefault();

            ViewData["Admin"] = generalAdmin;

            var findAdmin = _dataBase.Admins.Find(id);

            if (findAdmin == null)
            {
                return NotFound();
            }

            var findUser = _dataBase.Users.Where(x => x.Email == findAdmin.Email)
                                                .FirstOrDefault();
            _dataBase.Users.Remove(findUser);
            _dataBase.SaveChanges();

            var categories = _dataBase.Categories;
            foreach (var category in categories)
            {
                if (category.AdminID == findAdmin.Id)
                {
                    category.AdminID = null;
                    _dataBase.Categories.Update(category);
                }
            }

            var courses = _dataBase.Courses;
            foreach (var course in courses)
            {
                if (course.AdminID == findAdmin.Id)
                {
                    course.AdminID = null;
                    _dataBase.Courses.Update(course);
                }
            }

            var sections = _dataBase.Sections;
            foreach (var section in sections)
            {
                if (section.AdminID == findAdmin.Id)
                {
                    section.AdminID = null;
                    _dataBase.Sections.Update(section);
                }
            }

            _dataBase.Admins.Remove(findAdmin);
            _dataBase.SaveChanges();

            return RedirectToAction("Dashboard");
        }

        [HttpGet]
        public IActionResult UpdateAdmin(int? id)
        {
            admin.Id = int.Parse(HttpContext.Session.GetString(sessionId));
            var generalAdmin = _dataBase.Admins.Where(x => x.Id == admin.Id)
                                                .FirstOrDefault();

            ViewData["Admin"] = generalAdmin;

            var categoriesList = _dataBase.Categories;
            ViewData["Categories"] = categoriesList;
            var coursesList = _dataBase.Courses;
            ViewData["Courses"] = coursesList;
            var sectionsList = _dataBase.Sections;
            ViewData["Sections"] = sectionsList;

            if (id == null || id == 0)
            {
                return NotFound();
            }
            var findAdmin = _dataBase.Admins.Find(id);

            if (findAdmin == null)
            {
                return NotFound();
            }

            return View(findAdmin);
        }

        [HttpPost]
        public IActionResult UpdateAdmin(Admin adminn)
        {
            admin.Id = int.Parse(HttpContext.Session.GetString(sessionId));
            var generalAdmin = _dataBase.Admins.Where(x => x.Id == admin.Id)
                                                .FirstOrDefault();

            ViewData["Admin"] = generalAdmin;

            if (ModelState.IsValid)
            {
                int id = adminn.Id;
                var findAdmin = _dataBase.Admins.Where(x => x.Id == id)
                                              .FirstOrDefault();

                adminn.Password = Crypting.Crypt(adminn.Password);
                adminn.ConfirmPassword = Crypting.Crypt(adminn.ConfirmPassword);

                _dataBase.Entry(findAdmin).CurrentValues.SetValues(adminn);
                _dataBase.SaveChanges();

                var findUser = _dataBase.Users.Where(x => x.Email == findAdmin.Email)
                                                    .FirstOrDefault();
                findUser.FirstName = adminn.FirstName;
                findUser.LastName = adminn.LastName;
                findUser.Username = adminn.Username;
                findUser.Email = adminn.Email;
                findUser.Password = adminn.Password;
                findUser.ConfirmPassword = adminn.ConfirmPassword;
                _dataBase.Users.Update(findUser);
                _dataBase.SaveChanges();

                return RedirectToAction("Dashboard");
            }
            return View(adminn);
        }

        //Teacher
        [HttpGet]
        public IActionResult AddTeacher()
        {
            admin.Id = int.Parse(HttpContext.Session.GetString(sessionId));
            var generalAdmin = _dataBase.Admins.Where(x => x.Id == admin.Id)
                                                .FirstOrDefault();

            ViewData["Admin"] = generalAdmin;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddTeacher(Teacher teacher)
        {
            admin.Id = int.Parse(HttpContext.Session.GetString(sessionId));
            var generalAdmin = _dataBase.Admins.Where(x => x.Id == admin.Id)
                                                .FirstOrDefault();

            ViewData["Admin"] = generalAdmin;

            DateTime date = DateTime.Now;
            teacher.CreatedDate = date.ToString("dd/MM/yyyy");
            teacher.AccountType = "Teacher";
            teacher.Categories = new List<Category>();
            teacher.Courses = new List<Course>();
            teacher.Sections = new List<Section>();

            if (ModelState.IsValid)
            {

                var check = _dataBase.Teachers.FirstOrDefault(s => s.Email == teacher.Email);

                if (check == null)
                {
                    teacher.Password = Crypting.Crypt(teacher.Password);
                    teacher.ConfirmPassword = Crypting.Crypt(teacher.ConfirmPassword);
                    _dataBase.Teachers.Add(teacher);
                    _dataBase.SaveChanges();

                    User user = new User();
                    user.FirstName = teacher.FirstName;
                    user.LastName = teacher.LastName;
                    user.Username = teacher.Username;
                    user.Email = teacher.Email;
                    user.AccountType = "Teacher";
                    user.Password = teacher.Password;
                    user.ConfirmPassword = teacher.ConfirmPassword;
                    user.CreatedDate = teacher.CreatedDate;
                    _dataBase.Users.Add(user);
                    _dataBase.SaveChanges();

                    return RedirectToAction("Dashboard");
                }
                else
                {
                    ViewBag.error = "Email already exists";
                    return View();
                }
            }
            return View(teacher);
        }

        [HttpGet]
        public IActionResult DeleteTeacher(int? id)
        {
            admin.Id = int.Parse(HttpContext.Session.GetString(sessionId));
            var generalAdmin = _dataBase.Admins.Where(x => x.Id == admin.Id)
                                                .FirstOrDefault();

            ViewData["Admin"] = generalAdmin;

            var categoriesList = _dataBase.Categories;
            ViewData["Categories"] = categoriesList;
            var coursesList = _dataBase.Courses;
            ViewData["Courses"] = coursesList;
            var sectionsList = _dataBase.Sections;
            ViewData["Sections"] = sectionsList;

            if (id == null || id == 0)
            {
                return NotFound();
            }

            var findTeacher = _dataBase.Teachers.Find(id);

            if (findTeacher == null)
            {
                return NotFound();
            }

            return View(findTeacher);
        }

        [HttpPost]
        public IActionResult DeleteTeacherPost(int? id)
        {
            admin.Id = int.Parse(HttpContext.Session.GetString(sessionId));
            var generalAdmin = _dataBase.Admins.Where(x => x.Id == admin.Id)
                                                .FirstOrDefault();

            ViewData["Admin"] = generalAdmin;

            var findTeacher = _dataBase.Teachers.Find(id);

            if (findTeacher == null)
            {
                return NotFound();
            }

            var findUser = _dataBase.Users.Where(x => x.Email == findTeacher.Email)
                                                .FirstOrDefault();
            _dataBase.Users.Remove(findUser);
            _dataBase.SaveChanges();

            var categories = _dataBase.Categories;
            foreach (var category in categories)
            {
                if (category.TeacherID == findTeacher.Id)
                {
                    category.TeacherID = null;
                    _dataBase.Categories.Update(category);
                }
            }

            var courses = _dataBase.Courses;
            foreach (var course in courses)
            {
                if (course.TeacherID == findTeacher.Id)
                {
                    course.TeacherID = null;
                    _dataBase.Courses.Update(course);
                }
            }

            var sections = _dataBase.Sections;
            foreach (var section in sections)
            {
                if (section.TeacherID == findTeacher.Id)
                {
                    section.TeacherID = null;
                    _dataBase.Sections.Update(section);
                }
            }

            _dataBase.Teachers.Remove(findTeacher);
            _dataBase.SaveChanges();
            return RedirectToAction("Dashboard");
        }

        [HttpGet]
        public IActionResult UpdateTeacher(int? id)
        {
            admin.Id = int.Parse(HttpContext.Session.GetString(sessionId));
            var generalAdmin = _dataBase.Admins.Where(x => x.Id == admin.Id)
                                                .FirstOrDefault();

            ViewData["Admin"] = generalAdmin;

            var categoriesList = _dataBase.Categories;
            ViewData["Categories"] = categoriesList;
            var coursesList = _dataBase.Courses;
            ViewData["Courses"] = coursesList;
            var sectionsList = _dataBase.Sections;
            ViewData["Sections"] = sectionsList;

            if (id == null || id == 0)
            {
                return NotFound();
            }
            var findTeacher = _dataBase.Teachers.Find(id);

            if (findTeacher == null)
            {
                return NotFound();
            }

            return View(findTeacher);
        }

        [HttpPost]
        public IActionResult UpdateTeacher(Teacher teacher)
        {
            admin.Id = int.Parse(HttpContext.Session.GetString(sessionId));
            var generalAdmin = _dataBase.Admins.Where(x => x.Id == admin.Id)
                                                .FirstOrDefault();

            ViewData["Admin"] = generalAdmin;

            if (ModelState.IsValid)
            {
                int id = teacher.Id;
                var findTeacher = _dataBase.Teachers.Where(x => x.Id == id)
                                              .FirstOrDefault();

                teacher.Password = Crypting.Crypt(teacher.Password);
                teacher.ConfirmPassword = Crypting.Crypt(teacher.ConfirmPassword);

                _dataBase.Entry(findTeacher).CurrentValues.SetValues(teacher);
                _dataBase.SaveChanges();

                var findUser = _dataBase.Users.Where(x => x.Email == findTeacher.Email)
                                                    .FirstOrDefault();
                findUser.FirstName = teacher.FirstName;
                findUser.LastName = teacher.LastName;
                findUser.Username = teacher.Username;
                findUser.Email = teacher.Email;
                findUser.Password = teacher.Password;
                findUser.ConfirmPassword = teacher.ConfirmPassword;
                _dataBase.Users.Update(findUser);
                _dataBase.SaveChanges();

                return RedirectToAction("Dashboard");
            }
            return View(teacher);
        }

        //Student
        [HttpGet]
        public IActionResult AddStudent()
        {
            admin.Id = int.Parse(HttpContext.Session.GetString(sessionId));
            var generalAdmin = _dataBase.Admins.Where(x => x.Id == admin.Id)
                                                .FirstOrDefault();

            ViewData["Admin"] = generalAdmin;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddStudent(Student student)
        {
            admin.Id = int.Parse(HttpContext.Session.GetString(sessionId));
            var generalAdmin = _dataBase.Admins.Where(x => x.Id == admin.Id)
                                                .FirstOrDefault();

            ViewData["Admin"] = generalAdmin;

            DateTime date = DateTime.Now;
            student.CreatedDate = date.ToString("dd/MM/yyyy");
            student.AccountType = "Student";
            student.Points = 0;
            student.Progress = 0;
            student.TimeSpent = "00h:00m:01s";
            student.Courses = null;

            if (ModelState.IsValid)
            {
                var check = _dataBase.Students.FirstOrDefault(s => s.Email == student.Email);

                if (check == null)
                {
                    student.Password = Crypting.Crypt(student.Password);
                    student.ConfirmPassword = Crypting.Crypt(student.ConfirmPassword);
                    _dataBase.Students.Add(student);
                    _dataBase.SaveChanges();

                    User user = new User();
                    user.FirstName = student.FirstName;
                    user.LastName = student.LastName;
                    user.Username = student.Username;
                    user.Email = student.Email;
                    user.AccountType = "Student";
                    user.Password = student.Password;
                    user.ConfirmPassword = student.ConfirmPassword;
                    user.CreatedDate = student.CreatedDate;
                    _dataBase.Users.Add(user);
                    _dataBase.SaveChanges();

                    return RedirectToAction("Dashboard");
                }
                else
                {
                    ViewBag.error = "Email already exists";
                    return View();
                }
            }
            return View(student);
        }

        [HttpGet]
        public IActionResult DeleteStudent(int? id)
        {
            admin.Id = int.Parse(HttpContext.Session.GetString(sessionId));
            var generalAdmin = _dataBase.Admins.Where(x => x.Id == admin.Id)
                                                .FirstOrDefault();

            ViewData["Admin"] = generalAdmin;

            var coursesList = _dataBase.Courses;
            ViewData["Courses"] = coursesList;

            if (id == null || id == 0)
            {
                return NotFound();
            }

            var findStudent = _dataBase.Students.Find(id);

            if (findStudent == null)
            {
                return NotFound();
            }

            return View(findStudent);
        }

        [HttpPost]
        public IActionResult DeleteStudentPost(int? id)
        {
            admin.Id = int.Parse(HttpContext.Session.GetString(sessionId));
            var generalAdmin = _dataBase.Admins.Where(x => x.Id == admin.Id)
                                                .FirstOrDefault();

            ViewData["Admin"] = generalAdmin;

            var findStudent = _dataBase.Students.Find(id);

            if (findStudent == null)
            {
                return NotFound();
            }

            var findUser = _dataBase.Users.Where(x => x.Email == findStudent.Email)
                                                .FirstOrDefault();
            _dataBase.Users.Remove(findUser);
            _dataBase.SaveChanges();

            string result = null;
            var courses = _dataBase.Courses;
            foreach (var course in courses)
            {
                if (course.StudentsIDs != null)
                {
                    var array = course.StudentsIDs.Split(',', StringSplitOptions.RemoveEmptyEntries);

                    for (int i = 0; i < array.Length; i++)
                    {
                        int studentId = int.Parse(array[i].ToString());
                        if (studentId != findStudent.Id)
                        {
                            result += studentId + ",";
                        }
                    }

                    course.StudentsIDs = result;
                }
                _dataBase.Courses.Update(course);
                result = null;
            }

            _dataBase.Students.Remove(findStudent);
            _dataBase.SaveChanges();
            return RedirectToAction("Dashboard");
        }

        [HttpGet]
        public IActionResult UpdateStudent(int? id)
        {
            admin.Id = int.Parse(HttpContext.Session.GetString(sessionId));
            var generalAdmin = _dataBase.Admins.Where(x => x.Id == admin.Id)
                                                .FirstOrDefault();

            ViewData["Admin"] = generalAdmin;

            var coursesList = _dataBase.Courses;
            ViewData["Courses"] = coursesList;

            if (id == null || id == 0)
            {
                return NotFound();
            }

            var findStudent = _dataBase.Students.Find(id);

            if (findStudent == null)
            {
                return NotFound();
            }

            return View(findStudent);
        }

        [HttpPost]
        public IActionResult UpdateStudent(Student student)
        {
            admin.Id = int.Parse(HttpContext.Session.GetString(sessionId));
            var generalAdmin = _dataBase.Admins.Where(x => x.Id == admin.Id)
                                                .FirstOrDefault();

            ViewData["Admin"] = generalAdmin;

            if (ModelState.IsValid)
            {
                int id = student.Id;
                var findStudent = _dataBase.Students.Where(x => x.Id == id)
                                              .FirstOrDefault();

                student.Password = Crypting.Crypt(student.Password);
                student.ConfirmPassword = Crypting.Crypt(student.ConfirmPassword);

                _dataBase.Entry(findStudent).CurrentValues.SetValues(student);
                _dataBase.SaveChanges();

                var findUser = _dataBase.Users.Where(x => x.Email == findStudent.Email)
                                                    .FirstOrDefault();
                findUser.FirstName = student.FirstName;
                findUser.LastName = student.LastName;
                findUser.Username = student.Username;
                findUser.Email = student.Email;
                findUser.Password = student.Password;
                findUser.ConfirmPassword = student.ConfirmPassword;
                _dataBase.Users.Update(findUser);
                _dataBase.SaveChanges();

                return RedirectToAction("Dashboard");
            }
            return View(student);
        }

        //Category
        [HttpGet]
        public IActionResult CreateCategory()
        {
            admin.Id = int.Parse(HttpContext.Session.GetString(sessionId));
            var generalAdmin = _dataBase.Admins.Where(x => x.Id == admin.Id)
                                                .FirstOrDefault();

            ViewData["Admin"] = generalAdmin;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateCategory(Category category)
        {
            admin.Id = int.Parse(HttpContext.Session.GetString(sessionId));
            var generalAdmin = _dataBase.Admins.Where(x => x.Id == admin.Id)
                                                .FirstOrDefault();

            ViewData["Admin"] = generalAdmin;


            if (ModelState.IsValid)
            {

                category.AdminID = generalAdmin.Id;
                category.Courses = new List<Course>();

                _dataBase.Categories.Add(category);
                _dataBase.SaveChanges();

                return RedirectToAction("Dashboard");

            }
            return View(category);
        }

        [HttpGet]
        public IActionResult DeleteCategory(int? id)
        {
            admin.Id = int.Parse(HttpContext.Session.GetString(sessionId));
            var generalAdmin = _dataBase.Admins.Where(x => x.Id == admin.Id)
                                                .FirstOrDefault();

            ViewData["Admin"] = generalAdmin;

            var adminsList = _dataBase.Admins;
            ViewData["Admins"] = adminsList;
            var teachersList = _dataBase.Teachers;
            ViewData["Teachers"] = teachersList;
            var coursesList = _dataBase.Courses;
            ViewData["Courses"] = coursesList;

            if (id == null || id == 0)
            {
                return NotFound();
            }

            var findCategory = _dataBase.Categories.Find(id);

            if (findCategory == null)
            {
                return NotFound();
            }

            return View(findCategory);
        }

        [HttpPost]
        public IActionResult DeleteCategoryPost(int? id)
        {
            admin.Id = int.Parse(HttpContext.Session.GetString(sessionId));
            var generalAdmin = _dataBase.Admins.Where(x => x.Id == admin.Id)
                                                .FirstOrDefault();

            ViewData["Admin"] = generalAdmin;

            var findCategory = _dataBase.Categories.Find(id);

            if (findCategory == null)
            {
                return NotFound();
            }

            var courses = _dataBase.Courses;
            var sections = _dataBase.Sections;
            foreach (var course in courses)
            {
                if (course.CategoryID == findCategory.Id)
                {
                    foreach (var section in sections)
                    {
                        if (section.CourseID == course.Id)
                        {
                            _dataBase.Sections.Remove(section);
                        }
                    }
                    _dataBase.Courses.Remove(course);
                }
            }

            _dataBase.Categories.Remove(findCategory);
            _dataBase.SaveChanges();
            return RedirectToAction("Dashboard");
        }

        [HttpGet]
        public IActionResult UpdateCategory(int? id)
        {
            admin.Id = int.Parse(HttpContext.Session.GetString(sessionId));
            var generalAdmin = _dataBase.Admins.Where(x => x.Id == admin.Id)
                                                .FirstOrDefault();

            ViewData["Admin"] = generalAdmin;

            var adminsList = _dataBase.Admins;
            ViewData["Admins"] = adminsList;
            var teachersList = _dataBase.Teachers;
            ViewData["Teachers"] = teachersList;
            var coursesList = _dataBase.Courses;
            ViewData["Courses"] = coursesList;

            if (id == null || id == 0)
            {
                return NotFound();
            }
            var findCategory = _dataBase.Categories.Find(id);

            if (findCategory == null)
            {
                return NotFound();
            }

            return View(findCategory);
        }

        [HttpPost]
        public IActionResult UpdateCategory(Category category)
        {
            admin.Id = int.Parse(HttpContext.Session.GetString(sessionId));
            var generalAdmin = _dataBase.Admins.Where(x => x.Id == admin.Id)
                                                .FirstOrDefault();

            ViewData["Admin"] = generalAdmin;

            if (ModelState.IsValid)
            {
                _dataBase.Categories.Update(category);
                _dataBase.SaveChanges();

                return RedirectToAction("Dashboard");
            }
            return View(category);
        }

        //Course
        public IActionResult CreateCourse()
        {
            admin.Id = int.Parse(HttpContext.Session.GetString(sessionId));
            var generalAdmin = _dataBase.Admins.Where(x => x.Id == admin.Id)
                                                .FirstOrDefault();

            ViewData["Admin"] = generalAdmin;

            var categoriesList = _dataBase.Categories;
            ViewData["Categories"] = categoriesList;

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateCourse(Course course)
        {
            admin.Id = int.Parse(HttpContext.Session.GetString(sessionId));
            var generalAdmin = _dataBase.Admins.Where(x => x.Id == admin.Id)
                                                .FirstOrDefault();

            ViewData["Admin"] = generalAdmin;

            var categoriesList = _dataBase.Categories;
            ViewData["Categories"] = categoriesList;

            DateTime date = DateTime.Now;
            course.CreatedDate = date.ToString("dd/MM/yyyy");
            course.Sections = new List<Section>();
            course.Students = null;

            if (ModelState.IsValid)
            {
                course.AdminID = generalAdmin.Id;

                _dataBase.Courses.Add(course);
                _dataBase.SaveChanges();

                return RedirectToAction("Dashboard");

            }
            return View(course);
        }

        [HttpGet]
        public IActionResult DeleteCourse(int? id)
        {
            admin.Id = int.Parse(HttpContext.Session.GetString(sessionId));
            var generalAdmin = _dataBase.Admins.Where(x => x.Id == admin.Id)
                                                .FirstOrDefault();

            ViewData["Admin"] = generalAdmin;

            var adminsList = _dataBase.Admins;
            ViewData["Admins"] = adminsList;
            var teachersList = _dataBase.Teachers;
            ViewData["Teachers"] = teachersList;
            var studentsList = _dataBase.Students;
            ViewData["Students"] = studentsList;
            var categoriesList = _dataBase.Categories;
            ViewData["Categories"] = categoriesList;
            var sectionsList = _dataBase.Sections;
            ViewData["Sections"] = sectionsList;

            if (id == null || id == 0)
            {
                return NotFound();
            }

            var findCourse = _dataBase.Courses.Find(id);

            if (findCourse == null)
            {
                return NotFound();
            }

            return View(findCourse);
        }

        [HttpPost]
        public IActionResult DeleteCoursePost(int? id)
        {
            admin.Id = int.Parse(HttpContext.Session.GetString(sessionId));
            var generalAdmin = _dataBase.Admins.Where(x => x.Id == admin.Id)
                                                .FirstOrDefault();

            ViewData["Admin"] = generalAdmin;

            var findCourse = _dataBase.Courses.Find(id);

            if (findCourse == null)
            {
                return NotFound();
            }

            var sections = _dataBase.Sections;
            foreach (var section in sections)
            {
                if (section.CourseID == findCourse.Id)
                {
                    _dataBase.Sections.Remove(section);
                }
            }

            string result = null;
            var students = _dataBase.Students;
            foreach (var student in students)
            {
                if (student.CoursesIDs != null)
                {
                    var array = student.CoursesIDs.Split(',', StringSplitOptions.RemoveEmptyEntries);
                    for (int i = 0; i < array.Length; i++)
                    {
                        string courseInfo = array[i];
                        var courseArray = courseInfo.Split('=', StringSplitOptions.RemoveEmptyEntries);
                        int courseId = int.Parse(courseArray[0].ToString());
                        if (courseId != findCourse.Id)
                        {
                            result += courseInfo + ",";
                        }
                    }
                    student.CoursesIDs = result;
                }
                _dataBase.Students.Update(student);
                result = null;
            }

            _dataBase.Courses.Remove(findCourse);
            _dataBase.SaveChanges();

            return RedirectToAction("Dashboard");
        }

        [HttpGet]
        public IActionResult UpdateCourse(int? id)
        {
            admin.Id = int.Parse(HttpContext.Session.GetString(sessionId));
            var generalAdmin = _dataBase.Admins.Where(x => x.Id == admin.Id)
                                                .FirstOrDefault();

            ViewData["Admin"] = generalAdmin;

            var adminsList = _dataBase.Admins;
            ViewData["Admins"] = adminsList;
            var teachersList = _dataBase.Teachers;
            ViewData["Teachers"] = teachersList;
            var studentsList = _dataBase.Students;
            ViewData["Students"] = studentsList;
            var categoriesList = _dataBase.Categories;
            ViewData["Categories"] = categoriesList;
            var sectionsList = _dataBase.Sections;
            ViewData["Sections"] = sectionsList;

            if (id == null || id == 0)
            {
                return NotFound();
            }
            var findCourse = _dataBase.Courses.Find(id);

            if (findCourse == null)
            {
                return NotFound();
            }

            return View(findCourse);
        }

        [HttpPost]
        public IActionResult UpdateCourse(Course course)
        {
            admin.Id = int.Parse(HttpContext.Session.GetString(sessionId));
            var generalAdmin = _dataBase.Admins.Where(x => x.Id == admin.Id)
                                                .FirstOrDefault();

            ViewData["Admin"] = generalAdmin;

            if (ModelState.IsValid)
            {
                _dataBase.Courses.Update(course);
                _dataBase.SaveChanges();

                return RedirectToAction("Dashboard");
            }
            return View(course);
        }

        //Section
        public IActionResult CreateSection()
        {
            admin.Id = int.Parse(HttpContext.Session.GetString(sessionId));
            var generalAdmin = _dataBase.Admins.Where(x => x.Id == admin.Id)
                                                .FirstOrDefault();

            ViewData["Admin"] = generalAdmin;

            var coursesList = _dataBase.Courses;
            ViewData["Courses"] = coursesList;

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateSection(Section section)
        {
            admin.Id = int.Parse(HttpContext.Session.GetString(sessionId));
            var generalAdmin = _dataBase.Admins.Where(x => x.Id == admin.Id)
                                                .FirstOrDefault();

            ViewData["Admin"] = generalAdmin;

            var coursesList = _dataBase.Courses;
            ViewData["Courses"] = coursesList;
            

            if (ModelState.IsValid)
            {

                section.AdminID = generalAdmin.Id;

                _dataBase.Sections.Add(section);
                _dataBase.SaveChanges();

                return RedirectToAction("Dashboard");

            }
            return View(section);
        }

        [HttpGet]
        public IActionResult DeleteSection(int? id)
        {
            admin.Id = int.Parse(HttpContext.Session.GetString(sessionId));
            var generalAdmin = _dataBase.Admins.Where(x => x.Id == admin.Id)
                                                .FirstOrDefault();

            ViewData["Admin"] = generalAdmin;

            var adminsList = _dataBase.Admins;
            ViewData["Admins"] = adminsList;
            var teachersList = _dataBase.Teachers;
            ViewData["Teachers"] = teachersList;
            var coursesList = _dataBase.Courses;
            ViewData["Courses"] = coursesList;

            if (id == null || id == 0)
            {
                return NotFound();
            }

            var findSection = _dataBase.Sections.Find(id);

            if (findSection == null)
            {
                return NotFound();
            }

            return View(findSection);
        }

        [HttpPost]
        public IActionResult DeleteSectionPost(int? id)
        {
            admin.Id = int.Parse(HttpContext.Session.GetString(sessionId));
            var generalAdmin = _dataBase.Admins.Where(x => x.Id == admin.Id)
                                                .FirstOrDefault();

            ViewData["Admin"] = generalAdmin;

            var findSection = _dataBase.Sections.Find(id);

            if (findSection == null)
            {
                return NotFound();
            }

            _dataBase.Sections.Remove(findSection);
            _dataBase.SaveChanges();
            return RedirectToAction("Dashboard");
        }

        [HttpGet]
        public IActionResult UpdateSection(int? id)
        {
            admin.Id = int.Parse(HttpContext.Session.GetString(sessionId));
            var generalAdmin = _dataBase.Admins.Where(x => x.Id == admin.Id)
                                                .FirstOrDefault();

            ViewData["Admin"] = generalAdmin;

            var adminsList = _dataBase.Admins;
            ViewData["Admins"] = adminsList;
            var teachersList = _dataBase.Teachers;
            ViewData["Teachers"] = teachersList;
            var coursesList = _dataBase.Courses;
            ViewData["Courses"] = coursesList;

            if (id == null || id == 0)
            {
                return NotFound();
            }
            var findSection = _dataBase.Sections.Find(id);

            if (findSection == null)
            {
                return NotFound();
            }

            return View(findSection);
        }

        [HttpPost]
        public IActionResult UpdateSection(Section section)
        {
            admin.Id = int.Parse(HttpContext.Session.GetString(sessionId));
            var generalAdmin = _dataBase.Admins.Where(x => x.Id == admin.Id)
                                                .FirstOrDefault();

            ViewData["Admin"] = generalAdmin;

            if (ModelState.IsValid)
            {
                _dataBase.Sections.Update(section);
                _dataBase.SaveChanges();

                return RedirectToAction("Dashboard");
            }
            return View(section);
        }
    }
}
