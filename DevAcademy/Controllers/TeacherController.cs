using DevAcademy.Cryptography;
using DevAcademy.Data;
using DevAcademy.Extensions;
using DevAcademy.Models;
using Microsoft.AspNetCore.Mvc;

namespace DevAcademy.Controllers
{
    public class TeacherController : Controller
    {
        Teacher teacher = new Teacher();
        string sessionId = "1";
        string sessionUsername = "username";
        string sessionEmail = "email";

        private readonly ApplicationDataContext _dataBase;

        public TeacherController(ApplicationDataContext dataBase)
        {
            _dataBase = dataBase;
        }

        public IActionResult Dashboard()
        {
            var categoriesList = _dataBase.Categories;
            var coursesList = _dataBase.Courses;
            var sectionsList = _dataBase.Sections;
            var studentsList = _dataBase.Students;

            if (TempData["userdata"] != null)
            {
                var temp = TempData.Get<Student>("userdata");
                HttpContext.Session.SetString(sessionId, temp.Id.ToString());
                HttpContext.Session.SetString(sessionUsername, temp.Username);
                HttpContext.Session.SetString(sessionEmail, temp.Email);

                var generalTeacher = _dataBase.Teachers.Where(x => x.Id == temp.Id)
                                                .FirstOrDefault();
                ViewData["Teacher"] = generalTeacher;

                ViewData["Categories"] = categoriesList;
                ViewData["Courses"] = coursesList;
                ViewData["Sections"] = sectionsList;
                ViewData["Students"] = studentsList;

                return View(generalTeacher);
            }
            else
            {
                teacher.Id = int.Parse(HttpContext.Session.GetString(sessionId));

                var generalTeacher = _dataBase.Teachers.Where(x => x.Id == teacher.Id)
                                                .FirstOrDefault();
                ViewData["Teacher"] = generalTeacher;

                ViewData["Categories"] = categoriesList;
                ViewData["Courses"] = coursesList;
                ViewData["Sections"] = sectionsList;
                ViewData["Students"] = studentsList;

                return View(generalTeacher);
            }
        }

        [HttpPost]
        public IActionResult UpdateProfile(Teacher teacherr)
        {
            teacher.Id = int.Parse(HttpContext.Session.GetString(sessionId));

            var generalTeacher = _dataBase.Teachers.Where(x => x.Id == teacher.Id)
                                                .FirstOrDefault();

            generalTeacher.FirstName = teacherr.FirstName;
            generalTeacher.LastName = teacherr.LastName;
            generalTeacher.Username = teacherr.Username;

            _dataBase.Teachers.Update(generalTeacher);
            _dataBase.SaveChanges();

            return RedirectToAction("Dashboard");
        }

        public IActionResult About()
        {
            int id = int.Parse(HttpContext.Session.GetString(sessionId));

            var generalTeacher = _dataBase.Teachers.Where(x => x.Id == id)
                                                .FirstOrDefault();
            return View(generalTeacher);
        }

        public IActionResult Contacts()
        {
            int id = int.Parse(HttpContext.Session.GetString(sessionId));

            var generalTeacher = _dataBase.Teachers.Where(x => x.Id == id)
                                                .FirstOrDefault();
            return View(generalTeacher);
        }

        public IActionResult Categories()
        {
            teacher.Id = int.Parse(HttpContext.Session.GetString(sessionId));

            var generalTeacher = _dataBase.Teachers.Where(x => x.Id == teacher.Id)
                                                .FirstOrDefault();

            var categoriesList = _dataBase.Categories;
            ViewData["Categories"] = categoriesList;

            return View(generalTeacher);
        }

        public IActionResult Category(int? id)
        {
            teacher.Id = int.Parse(HttpContext.Session.GetString(sessionId));

            var generalTeacher = _dataBase.Teachers.Where(x => x.Id == teacher.Id)
                                                .FirstOrDefault();

            var coursesList = _dataBase.Courses.Where(x => x.CategoryID == id);
            ViewData["Courses"] = coursesList;

            var category = _dataBase.Categories.Where(x => x.Id == id)
                                               .FirstOrDefault();
            ViewData["Category"] = category;

            return View(generalTeacher);
        }

        public IActionResult Courses()
        {
            teacher.Id = int.Parse(HttpContext.Session.GetString(sessionId));

            var generalTeacher = _dataBase.Teachers.Where(x => x.Id == teacher.Id)
                                                .FirstOrDefault();

            var categoriesList = _dataBase.Categories;
            ViewData["Categories"] = categoriesList;

            var coursesList = _dataBase.Courses;
            ViewData["Courses"] = coursesList;

            return View(generalTeacher);
        }

        public IActionResult Course(int? id)
        {
            teacher.Id = int.Parse(HttpContext.Session.GetString(sessionId));

            var generalTeacher = _dataBase.Teachers.Where(x => x.Id == teacher.Id)
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

            return View(generalTeacher);
        }

        public IActionResult Sections()
        {
            teacher.Id = int.Parse(HttpContext.Session.GetString(sessionId));

            var generalTeacher = _dataBase.Teachers.Where(x => x.Id == teacher.Id)
                                                .FirstOrDefault();

            var categoriesList = _dataBase.Categories;
            ViewData["Categories"] = categoriesList;

            var coursesList = _dataBase.Courses;
            ViewData["Courses"] = coursesList;

            var sectionsList = _dataBase.Sections;
            ViewData["Sections"] = sectionsList;

            return View(generalTeacher);
        }

        public IActionResult PreviewCourse(int? id)
        {
            teacher.Id = int.Parse(HttpContext.Session.GetString(sessionId));

            var generalTeacher = _dataBase.Teachers.Where(x => x.Id == teacher.Id)
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

            return View(generalTeacher);
        }

        public IActionResult Student(int? id)
        {
            teacher.Id = int.Parse(HttpContext.Session.GetString(sessionId));

            var generalTeacher = _dataBase.Teachers.Where(x => x.Id == teacher.Id)
                                                   .FirstOrDefault();

            var findStudent = _dataBase.Students.Where(x => x.Id == id)
                                                .FirstOrDefault();
            ViewData["Student"] = findStudent;

            var categoriesList = _dataBase.Categories;
            ViewData["Categories"] = categoriesList;
            var coursesList = _dataBase.Courses;
            ViewData["Courses"] = coursesList;
            var sectionsList = _dataBase.Sections;
            ViewData["Sections"] = sectionsList;

            return View(generalTeacher);
        }

        public IActionResult Teacher(int? id)
        {
            teacher.Id = int.Parse(HttpContext.Session.GetString(sessionId));

            var generalTeacher = _dataBase.Students.Where(x => x.Id == teacher.Id)
                                                .FirstOrDefault();

            if (teacher.Id == id)
            {
                return RedirectToAction("Dashboard");
            }

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

            return View(generalTeacher);
        }

        [HttpGet]
        public IActionResult Settings()
        {
            teacher.Id = int.Parse(HttpContext.Session.GetString(sessionId));
            var generalTeacher = _dataBase.Teachers.Where(x => x.Id == teacher.Id)
                                                .FirstOrDefault();

            ViewData["Teacher"] = generalTeacher;

            var categoriesList = _dataBase.Categories;
            ViewData["Categories"] = categoriesList;
            var coursesList = _dataBase.Courses;
            ViewData["Courses"] = coursesList;
            var sectionsList = _dataBase.Sections;
            ViewData["Sections"] = sectionsList;

            if (generalTeacher.Id == null || generalTeacher.Id == 0)
            {
                return NotFound();
            }

            if (generalTeacher == null)
            {
                return NotFound();
            }

            return View(generalTeacher);
        }

        [HttpPost]
        public IActionResult Settings(Teacher teacherr)
        {
            teacher.Id = int.Parse(HttpContext.Session.GetString(sessionId));
            var generalStudent = _dataBase.Teachers.Where(x => x.Id == teacher.Id)
                                                .FirstOrDefault();

            ViewData["Student"] = generalStudent;

            if (ModelState.IsValid)
            {
                teacherr.Password = Crypting.Crypt(teacherr.Password);
                teacherr.ConfirmPassword = Crypting.Crypt(teacherr.ConfirmPassword);

                _dataBase.Entry(generalStudent).CurrentValues.SetValues(teacherr);
                _dataBase.SaveChanges();

                var findUser = _dataBase.Users.Where(x => x.Email == generalStudent.Email)
                                                    .FirstOrDefault();
                findUser.FirstName = teacherr.FirstName;
                findUser.LastName = teacherr.LastName;
                findUser.Username = teacherr.Username;
                findUser.Email = teacherr.Email;
                findUser.Password = teacherr.Password;
                findUser.ConfirmPassword = teacherr.ConfirmPassword;
                _dataBase.Users.Update(findUser);
                _dataBase.SaveChanges();

                return RedirectToAction("Dashboard");
            }
            return View(teacherr);
        }

        //Category
        [HttpGet]
        public IActionResult CreateCategory()
        {
            teacher.Id = int.Parse(HttpContext.Session.GetString(sessionId));
            var generalTeacher = _dataBase.Teachers.Where(x => x.Id == teacher.Id)
                                                .FirstOrDefault();

            ViewData["Teacher"] = generalTeacher;
            var categoriesList = _dataBase.Categories;
            ViewData["Categories"] = categoriesList;
            var coursesList = _dataBase.Courses;
            ViewData["Courses"] = coursesList;
            var sectionsList = _dataBase.Sections;
            ViewData["Sections"] = sectionsList;

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateCategory(Category category)
        {
            teacher.Id = int.Parse(HttpContext.Session.GetString(sessionId));
            var generalTeacher = _dataBase.Teachers.Where(x => x.Id == teacher.Id)
                                                .FirstOrDefault();

            ViewData["Teacher"] = generalTeacher;
            var categoriesList = _dataBase.Categories;
            ViewData["Categories"] = categoriesList;
            var coursesList = _dataBase.Courses;
            ViewData["Courses"] = coursesList;
            var sectionsList = _dataBase.Sections;
            ViewData["Sections"] = sectionsList;


            if (ModelState.IsValid)
            {
                category.TeacherID = generalTeacher.Id;
                category.Courses = null;

                _dataBase.Categories.Add(category);
                _dataBase.SaveChanges();

                return RedirectToAction("Dashboard");

            }
            return View(category);
        }

        [HttpGet]
        public IActionResult DeleteCategory(int? id)
        {
            teacher.Id = int.Parse(HttpContext.Session.GetString(sessionId));
            var generalTeacher = _dataBase.Teachers.Where(x => x.Id == teacher.Id)
                                                .FirstOrDefault();

            ViewData["Teacher"] = generalTeacher;

            var adminsList = _dataBase.Admins;
            ViewData["Admins"] = adminsList;
            var teachersList = _dataBase.Teachers;
            ViewData["Teachers"] = teachersList;
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
            teacher.Id = int.Parse(HttpContext.Session.GetString(sessionId));
            var generalTeacher = _dataBase.Teachers.Where(x => x.Id == teacher.Id)
                                                .FirstOrDefault();

            ViewData["Teacher"] = generalTeacher;

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
            teacher.Id = int.Parse(HttpContext.Session.GetString(sessionId));
            var generalTeacher = _dataBase.Teachers.Where(x => x.Id == teacher.Id)
                                                .FirstOrDefault();

            ViewData["Teacher"] = generalTeacher;

            var adminsList = _dataBase.Admins;
            ViewData["Admins"] = adminsList;
            var teachersList = _dataBase.Teachers;
            ViewData["Teachers"] = teachersList;
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
            teacher.Id = int.Parse(HttpContext.Session.GetString(sessionId));
            var generalTeacher = _dataBase.Teachers.Where(x => x.Id == teacher.Id)
                                                .FirstOrDefault();

            ViewData["Teacher"] = generalTeacher;

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
            teacher.Id = int.Parse(HttpContext.Session.GetString(sessionId));
            var generalTeacher = _dataBase.Teachers.Where(x => x.Id == teacher.Id)
                                                .FirstOrDefault();

            ViewData["Teacher"] = generalTeacher;

            var categoriesList = _dataBase.Categories;
            ViewData["Categories"] = categoriesList;
            var coursesList = _dataBase.Courses;
            ViewData["Courses"] = coursesList;
            var sectionsList = _dataBase.Sections;
            ViewData["Sections"] = sectionsList;

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateCourse(Course course)
        {
            teacher.Id = int.Parse(HttpContext.Session.GetString(sessionId));
            var generalTeacher = _dataBase.Teachers.Where(x => x.Id == teacher.Id)
                                                .FirstOrDefault();

            ViewData["Teacher"] = generalTeacher;

            var categoriesList = _dataBase.Categories;
            ViewData["Categories"] = categoriesList;
            var coursesList = _dataBase.Courses;
            ViewData["Courses"] = coursesList;
            var sectionsList = _dataBase.Sections;
            ViewData["Sections"] = sectionsList;

            DateTime date = DateTime.Now;
            course.CreatedDate = date.ToString("dd/MM/yyyy");
            course.Sections = null;
            course.Students = null;

            if (ModelState.IsValid)
            {
                course.TeacherID = generalTeacher.Id;

                _dataBase.Courses.Add(course);
                _dataBase.SaveChanges();

                return RedirectToAction("Dashboard");

            }
            return View(course);
        }

        [HttpGet]
        public IActionResult DeleteCourse(int? id)
        {
            teacher.Id = int.Parse(HttpContext.Session.GetString(sessionId));
            var generalTeacher = _dataBase.Teachers.Where(x => x.Id == teacher.Id)
                                                .FirstOrDefault();

            ViewData["Teacher"] = generalTeacher;

            var adminsList = _dataBase.Admins;
            ViewData["Admins"] = adminsList;
            var teachersList = _dataBase.Teachers;
            ViewData["Teachers"] = teachersList;
            var studentsList = _dataBase.Students;
            ViewData["Students"] = studentsList;
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
            teacher.Id = int.Parse(HttpContext.Session.GetString(sessionId));
            var generalTeacher = _dataBase.Teachers.Where(x => x.Id == teacher.Id)
                                                .FirstOrDefault();

            ViewData["Teacher"] = generalTeacher;

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
            teacher.Id = int.Parse(HttpContext.Session.GetString(sessionId));
            var generalTeacher = _dataBase.Teachers.Where(x => x.Id == teacher.Id)
                                                .FirstOrDefault();

            ViewData["Teacher"] = generalTeacher;

            var adminsList = _dataBase.Admins;
            ViewData["Admins"] = adminsList;
            var teachersList = _dataBase.Teachers;
            ViewData["Teachers"] = teachersList;
            var studentsList = _dataBase.Students;
            ViewData["Students"] = studentsList;
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
            teacher.Id = int.Parse(HttpContext.Session.GetString(sessionId));
            var generalTeacher = _dataBase.Teachers.Where(x => x.Id == teacher.Id)
                                                .FirstOrDefault();

            ViewData["Teacher"] = generalTeacher;

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
            teacher.Id = int.Parse(HttpContext.Session.GetString(sessionId));
            var generalTeacher = _dataBase.Teachers.Where(x => x.Id == teacher.Id)
                                                .FirstOrDefault();

            ViewData["Teacher"] = generalTeacher;

            var categoriesList = _dataBase.Categories;
            ViewData["Categories"] = categoriesList;
            var coursesList = _dataBase.Courses;
            ViewData["Courses"] = coursesList;
            var sectionsList = _dataBase.Sections;
            ViewData["Sections"] = sectionsList;

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateSection(Section section)
        {
            teacher.Id = int.Parse(HttpContext.Session.GetString(sessionId));
            var generalTeacher = _dataBase.Teachers.Where(x => x.Id == teacher.Id)
                                                .FirstOrDefault();

            ViewData["Teacher"] = generalTeacher;

            var categoriesList = _dataBase.Categories;
            ViewData["Categories"] = categoriesList;
            var coursesList = _dataBase.Courses;
            ViewData["Courses"] = coursesList;
            var sectionsList = _dataBase.Sections;
            ViewData["Sections"] = sectionsList;


            if (ModelState.IsValid)
            {

                section.TeacherID = generalTeacher.Id;

                _dataBase.Sections.Add(section);
                _dataBase.SaveChanges();

                return RedirectToAction("Dashboard");

            }
            return View(section);
        }

        [HttpGet]
        public IActionResult DeleteSection(int? id)
        {
            teacher.Id = int.Parse(HttpContext.Session.GetString(sessionId));
            var generalTeacher = _dataBase.Teachers.Where(x => x.Id == teacher.Id)
                                                .FirstOrDefault();

            ViewData["Teacher"] = generalTeacher;

            var adminsList = _dataBase.Admins;
            ViewData["Admins"] = adminsList;
            var teachersList = _dataBase.Teachers;
            ViewData["Teachers"] = teachersList;
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
            teacher.Id = int.Parse(HttpContext.Session.GetString(sessionId));
            var generalTeacher = _dataBase.Teachers.Where(x => x.Id == teacher.Id)
                                                .FirstOrDefault();

            ViewData["Teacher"] = generalTeacher;

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
            teacher.Id = int.Parse(HttpContext.Session.GetString(sessionId));
            var generalTeacher = _dataBase.Teachers.Where(x => x.Id == teacher.Id)
                                                .FirstOrDefault();

            ViewData["Teacher"] = generalTeacher;

            var adminsList = _dataBase.Admins;
            ViewData["Admins"] = adminsList;
            var teachersList = _dataBase.Teachers;
            ViewData["Teachers"] = teachersList;
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
            teacher.Id = int.Parse(HttpContext.Session.GetString(sessionId));
            var generalTeacher = _dataBase.Admins.Where(x => x.Id == teacher.Id)
                                                .FirstOrDefault();

            ViewData["Teacher"] = generalTeacher;

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
