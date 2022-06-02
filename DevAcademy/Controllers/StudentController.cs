using DevAcademy.Cryptography;
using DevAcademy.Data;
using DevAcademy.Extensions;
using DevAcademy.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace DevAcademy.Controllers
{
    public class StudentController : Controller
    {
        Student student = new Student();
        string sessionId = "1";
        string sessionUsername = "username";
        string sessionEmail = "email";
        string sessionTimeStart = "timeStart";

        private readonly ApplicationDataContext _dataBase;

        public StudentController(ApplicationDataContext dataBase)
        {
            _dataBase = dataBase;
        }

        public IActionResult Dashboard()
        {
            var categoriesList = _dataBase.Categories;
            var coursesList = _dataBase.Courses;
            var sectionsList = _dataBase.Sections;

            if (TempData["userdata"] != null)
            {
                var temp = TempData.Get<Student>("userdata");
                HttpContext.Session.SetString(sessionId, temp.Id.ToString());
                HttpContext.Session.SetString(sessionUsername, temp.Username);
                HttpContext.Session.SetString(sessionEmail, temp.Email);
                HttpContext.Session.SetString(sessionTimeStart, DateTime.Now.ToString());

                var findStudent = _dataBase.Students.Where(x => x.Id == temp.Id)
                                                .FirstOrDefault();
                ViewData["Student"] = findStudent;

                ViewData["Categories"] = categoriesList;
                ViewData["Courses"] = coursesList;
                ViewData["Sections"] = sectionsList;

                return View(findStudent);
            }
            else
            {
                student.Id = int.Parse(HttpContext.Session.GetString(sessionId));

                var findStudent = _dataBase.Students.Where(x => x.Id == student.Id)
                                                .FirstOrDefault();
                ViewData["Student"] = findStudent;

                ViewData["Categories"] = categoriesList;
                ViewData["Courses"] = coursesList;
                ViewData["Sections"] = sectionsList;

                DateTime startDate = Convert.ToDateTime(HttpContext.Session.GetString(sessionTimeStart));
                DateTime endDate = DateTime.Now;
                SetTimeSpent(startDate, endDate, student.Id);

                return View(findStudent);
            }
        }

        public IActionResult ControllerBetween(int id)
        {
            student.Id = int.Parse(HttpContext.Session.GetString(sessionId));

            var findStudent = _dataBase.Students.Where(x => x.Id == student.Id).FirstOrDefault();
            var findCourse = _dataBase.Courses.Where(x => x.Id == id).FirstOrDefault();

            DateTime date = DateTime.Now;
            string finishDate = date.ToString("dd/MM/yyyy");

            StringBuilder sb = new StringBuilder();
            var array = findStudent.CoursesIDs.Split(',', StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < array.Length; i++)
            {
                string courseItem = array[i];
                var courseArray = courseItem.Split('=', StringSplitOptions.RemoveEmptyEntries);
                int idCourseItem = int.Parse(courseArray[0].ToString());
                if (idCourseItem == findCourse.Id)
                {
                    sb.Append(idCourseItem + "=" + "1" + "=" + courseArray[2] + "=" + finishDate + ",");
                    findStudent.Points += findCourse.Points;
                }
                else 
                {
                    sb.Append(array[i] + ",");
                }
            }

            findStudent.CoursesIDs = sb.ToString();
            _dataBase.Students.Update(findStudent);
            _dataBase.SaveChanges();

            return RedirectToAction("Dashboard");
        }

        [HttpPost]
        public IActionResult AddBio(string bio)
        {
            student.Id = int.Parse(HttpContext.Session.GetString(sessionId));

            var findStudent = _dataBase.Students.Where(x => x.Id == student.Id)
                                                .FirstOrDefault();
            findStudent.Bio = bio;

            _dataBase.Students.Update(findStudent);
            _dataBase.SaveChanges();

            return RedirectToAction("Dashboard");
        }

        [HttpPost]
        public IActionResult UpdateProfile(Student student)
        {
            student.Id = int.Parse(HttpContext.Session.GetString(sessionId));

            var findStudent = _dataBase.Students.Where(x => x.Id == student.Id)
                                                .FirstOrDefault();

            findStudent.FirstName = student.FirstName;
            findStudent.LastName = student.LastName;
            findStudent.Username = student.Username;
            findStudent.Bio = student.Bio;

            _dataBase.Students.Update(findStudent);
            _dataBase.SaveChanges();

            return RedirectToAction("Dashboard");
        }

        public IActionResult About()
        {
            student.Id = int.Parse(HttpContext.Session.GetString(sessionId));

            var findStudent = _dataBase.Students.Where(x => x.Id == student.Id)
                                                .FirstOrDefault();
            
            DateTime startDate = Convert.ToDateTime(HttpContext.Session.GetString(sessionTimeStart));
            DateTime endDate = DateTime.Now;
            SetTimeSpent(startDate, endDate, student.Id);

            return View(findStudent);
        }

        public IActionResult Contacts()
        {
            student.Id = int.Parse(HttpContext.Session.GetString(sessionId));

            var findStudent = _dataBase.Students.Where(x => x.Id == student.Id)
                                                .FirstOrDefault();

            DateTime startDate = Convert.ToDateTime(HttpContext.Session.GetString(sessionTimeStart));
            DateTime endDate = DateTime.Now;
            SetTimeSpent(startDate, endDate, student.Id);
            return View(findStudent);
        }

        public IActionResult Categories()
        {
            student.Id = int.Parse(HttpContext.Session.GetString(sessionId));

            var findStudent = _dataBase.Students.Where(x => x.Id == student.Id)
                                                .FirstOrDefault();

            var categoriesList = _dataBase.Categories;
            ViewData["Categories"] = categoriesList;

            DateTime startDate = Convert.ToDateTime(HttpContext.Session.GetString(sessionTimeStart));
            DateTime endDate = DateTime.Now;
            SetTimeSpent(startDate, endDate, student.Id);
            return View(findStudent);
        }

        public IActionResult Category(int? id)
        {
            student.Id = int.Parse(HttpContext.Session.GetString(sessionId));

            var findStudent = _dataBase.Students.Where(x => x.Id == student.Id)
                                                .FirstOrDefault();

            var coursesList = _dataBase.Courses.Where(x => x.CategoryID == id);
            ViewData["Courses"] = coursesList;

            var category = _dataBase.Categories.Where(x => x.Id == id)
                                               .FirstOrDefault();
            ViewData["Category"] = category;

            DateTime startDate = Convert.ToDateTime(HttpContext.Session.GetString(sessionTimeStart));
            DateTime endDate = DateTime.Now;
            SetTimeSpent(startDate, endDate, student.Id);
            return View(findStudent);
        }

        public IActionResult Courses()
        {
            student.Id = int.Parse(HttpContext.Session.GetString(sessionId));

            var findStudent = _dataBase.Students.Where(x => x.Id == student.Id)
                                                .FirstOrDefault();

            var categoriesList = _dataBase.Categories;
            ViewData["Categories"] = categoriesList;

            var coursesList = _dataBase.Courses;
            ViewData["Courses"] = coursesList;

            DateTime startDate = Convert.ToDateTime(HttpContext.Session.GetString(sessionTimeStart));
            DateTime endDate = DateTime.Now;
            SetTimeSpent(startDate, endDate, student.Id);

            return View(findStudent);
        }

        public IActionResult Course(int? id)
        {
            student.Id = int.Parse(HttpContext.Session.GetString(sessionId));

            var findStudent = _dataBase.Students.Where(x => x.Id == student.Id)
                                                .FirstOrDefault();

            var sectionsList = _dataBase.Sections.Where(x => x.CourseID == id);
            ViewData["Sections"] = sectionsList;

            var course = _dataBase.Courses.Where(x => x.Id == id)
                                               .FirstOrDefault();
            ViewData["Course"] = course;

            var category = _dataBase.Categories.Where(x => x.Id == course.CategoryID)
                                               .FirstOrDefault();
            ViewData["Category"] = category;

            var adminsList = _dataBase.Admins;
            ViewData["Admins"] = adminsList;
            var teacherList = _dataBase.Teachers;
            ViewData["Teachers"] = teacherList;

            DateTime startDate = Convert.ToDateTime(HttpContext.Session.GetString(sessionTimeStart));
            DateTime endDate = DateTime.Now;
            SetTimeSpent(startDate, endDate, student.Id);
            return View(findStudent);
        }

        public IActionResult Sections()
        {
            student.Id = int.Parse(HttpContext.Session.GetString(sessionId));

            var findStudent = _dataBase.Students.Where(x => x.Id == student.Id)
                                                .FirstOrDefault();

            var categoriesList = _dataBase.Categories;
            ViewData["Categories"] = categoriesList;

            var coursesList = _dataBase.Courses;
            ViewData["Courses"] = coursesList;

            var sectionsList = _dataBase.Sections;
            ViewData["Sections"] = sectionsList;

            DateTime startDate = Convert.ToDateTime(HttpContext.Session.GetString(sessionTimeStart));
            DateTime endDate = DateTime.Now;
            SetTimeSpent(startDate, endDate, student.Id);
            return View(findStudent);
        }

        public IActionResult EnrollCourse(int? id)
        {
            student.Id = int.Parse(HttpContext.Session.GetString(sessionId));

            var findStudent = _dataBase.Students.Where(x => x.Id == student.Id).FirstOrDefault();
            var findCourse = _dataBase.Courses.Where(x => x.Id == id).FirstOrDefault();

            DateTime date = DateTime.Now;
            string startDate = date.ToString("dd/MM/yyyy");

            if (findStudent.CoursesIDs == null)
            {
                findStudent.CoursesIDs = findCourse.Id + "=" + "0" + "=" + startDate + ",";
                _dataBase.Students.Update(findStudent);
                _dataBase.SaveChanges();
            }
            else
            {
                bool isfind = false;
                var array = findStudent.CoursesIDs.Split(',', StringSplitOptions.RemoveEmptyEntries);
                for (int i = 0; i < array.Length; i++)
                {
                    string courseItem = array[i];
                    var courseArray = courseItem.Split('=', StringSplitOptions.RemoveEmptyEntries);
                    int idCourseItem = int.Parse(courseArray[0].ToString());
                    if (idCourseItem == findCourse.Id)
                    {
                        isfind = true;
                        break;
                    }
                }
                if (!isfind)
                {
                    findStudent.CoursesIDs += findCourse.Id + "=" + "0" + "=" + startDate + ",";
                    _dataBase.Students.Update(findStudent);
                    _dataBase.SaveChanges();
                }
            }

            if (findCourse.StudentsIDs == null)
            {
                findCourse.StudentsIDs = findStudent.Id + ",";
                _dataBase.Courses.Update(findCourse);
                _dataBase.SaveChanges();
            }
            else
            {
                bool isfind = false;
                var array = findCourse.StudentsIDs.Split(',', StringSplitOptions.RemoveEmptyEntries);
                for (int i = 0; i < array.Length; i++)
                {
                    int studentItemID = int.Parse(array[i]);
                    if (studentItemID == findStudent.Id)
                    {
                        isfind = true;
                        break;
                    }
                }
                if (!isfind)
                {
                    findCourse.StudentsIDs += findStudent.Id + ",";
                    _dataBase.Courses.Update(findCourse);
                    _dataBase.SaveChanges();
                }
            }

            var sectionsList = _dataBase.Sections.Where(x => x.CourseID == id);
            ViewData["Sections"] = sectionsList;

            var course = _dataBase.Courses.Where(x => x.Id == id)
                                               .FirstOrDefault();
            ViewData["Course"] = course;

            var category = _dataBase.Categories.Where(x => x.Id == course.CategoryID)
                                               .FirstOrDefault();
            ViewData["Category"] = category;

            var adminsList = _dataBase.Admins;
            ViewData["Admins"] = adminsList;
            var teacherList = _dataBase.Teachers;
            ViewData["Teachers"] = teacherList;

            DateTime startDatee = Convert.ToDateTime(HttpContext.Session.GetString(sessionTimeStart));
            DateTime endDate = DateTime.Now;
            SetTimeSpent(startDatee, endDate, student.Id);
            return View(findStudent);
        }

        public IActionResult Teacher(int? id)
        {
            student.Id = int.Parse(HttpContext.Session.GetString(sessionId));

            var generalStudent = _dataBase.Students.Where(x => x.Id == student.Id)
                                                .FirstOrDefault();

            var findTeacher = _dataBase.Teachers.Where(x => x.Id == id)
                                                .FirstOrDefault();
            ViewData["Teacher"] = findTeacher;

            var categoriesList = _dataBase.Categories;
            ViewData["Categories"] = categoriesList;
            var coursesList = _dataBase.Courses;
            ViewData["Courses"] = coursesList;
            var sectionsList = _dataBase.Sections;
            ViewData["Sections"] = sectionsList;
            var studentsList = _dataBase.Students;
            ViewData["Students"] = studentsList;

            DateTime startDate = Convert.ToDateTime(HttpContext.Session.GetString(sessionTimeStart));
            DateTime endDate = DateTime.Now;
            SetTimeSpent(startDate, endDate, student.Id);
            return View(generalStudent);
        }

        public IActionResult Student(int? id)
        {
            student.Id = int.Parse(HttpContext.Session.GetString(sessionId));

            var generalStudent = _dataBase.Students.Where(x => x.Id == student.Id)
                                                .FirstOrDefault();

            if (student.Id == id)
            {
                return RedirectToAction("Dashboard");
            }

            var findStudent = _dataBase.Students.Where(x => x.Id == id)
                                                .FirstOrDefault();
            ViewData["Student"] = findStudent;

            var categoriesList = _dataBase.Categories;
            ViewData["Categories"] = categoriesList;
            var coursesList = _dataBase.Courses;
            ViewData["Courses"] = coursesList;
            var sectionsList = _dataBase.Sections;
            ViewData["Sections"] = sectionsList;

            DateTime startDate = Convert.ToDateTime(HttpContext.Session.GetString(sessionTimeStart));
            DateTime endDate = DateTime.Now;
            SetTimeSpent(startDate, endDate, student.Id);
            return View(generalStudent);
        }

        [HttpGet]
        public IActionResult Settings()
        {
            student.Id = int.Parse(HttpContext.Session.GetString(sessionId));
            var generalStudent = _dataBase.Students.Where(x => x.Id == student.Id)
                                                .FirstOrDefault();

            ViewData["Student"] = generalStudent;

            var coursesList = _dataBase.Courses;
            ViewData["Courses"] = coursesList;

            if (generalStudent.Id == null || generalStudent.Id == 0)
            {
                return NotFound();
            }

            if (generalStudent == null)
            {
                return NotFound();
            }

            DateTime startDate = Convert.ToDateTime(HttpContext.Session.GetString(sessionTimeStart));
            DateTime endDate = DateTime.Now;
            SetTimeSpent(startDate, endDate, student.Id);
            return View(generalStudent);
        }

        [HttpPost]
        public IActionResult Settings(Student studentt)
        {
            student.Id = int.Parse(HttpContext.Session.GetString(sessionId));
            var generalStudent = _dataBase.Students.Where(x => x.Id == student.Id)
                                                .FirstOrDefault();

            ViewData["Student"] = generalStudent;

            if (ModelState.IsValid)
            {
                studentt.Password = Crypting.Crypt(studentt.Password);
                studentt.ConfirmPassword = Crypting.Crypt(studentt.ConfirmPassword);

                _dataBase.Entry(generalStudent).CurrentValues.SetValues(studentt);
                _dataBase.SaveChanges();

                var findUser = _dataBase.Users.Where(x => x.Email == generalStudent.Email)
                                                    .FirstOrDefault();
                findUser.FirstName = studentt.FirstName;
                findUser.LastName = studentt.LastName;
                findUser.Username = studentt.Username;
                findUser.Email = studentt.Email;
                findUser.Password = studentt.Password;
                findUser.ConfirmPassword = studentt.ConfirmPassword;
                _dataBase.Users.Update(findUser);
                _dataBase.SaveChanges();

                DateTime startDatee = Convert.ToDateTime(HttpContext.Session.GetString(sessionTimeStart));
                DateTime endDatee = DateTime.Now;
                SetTimeSpent(startDatee, endDatee, student.Id);
                return RedirectToAction("Dashboard");
            }
            DateTime startDate = Convert.ToDateTime(HttpContext.Session.GetString(sessionTimeStart));
            DateTime endDate = DateTime.Now;
            SetTimeSpent(startDate, endDate, student.Id);
            return View(studentt);
        }

        public IActionResult Logout(Student studentt)
        {
            DateTime startDate = Convert.ToDateTime(HttpContext.Session.GetString(sessionTimeStart));
            DateTime endDate = DateTime.Now;
            SetTimeSpent(startDate, endDate, student.Id);
            return RedirectToAction("Index", "Home");
        }

        public void SetTimeSpent(DateTime startDate, DateTime endDate, int id)
        {
            var result = endDate - startDate;
            var generalStudent = _dataBase.Students.Where(x => x.Id == id)
                                                .FirstOrDefault();

            string timeSpent = generalStudent.TimeSpent.ToString();
            string temp = timeSpent.Substring(0, 2);
            temp += timeSpent.Substring(3, 3);
            temp += timeSpent.Substring(7, 3);
            DateTime tempResult = Convert.ToDateTime(temp);

            var endResult = tempResult.TimeOfDay + result;

            string answer = string.Format("{0:D2}h:{1:D2}m:{2:D2}s",
                endResult.Hours,
                endResult.Minutes,
                endResult.Seconds);

            generalStudent.TimeSpent = answer;
            _dataBase.Students.Update(generalStudent);
            _dataBase.SaveChanges();
            HttpContext.Session.SetString(sessionTimeStart, DateTime.Now.ToString());
        }
    }
}
