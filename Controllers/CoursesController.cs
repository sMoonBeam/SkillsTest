using SkillsTest.Models;
using SkillsTest.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using SkillsTest.Models.Repository;

namespace SkillsTest.Controllers
{
    public class CoursesController : Controller
    {
        private CoursesRepository _courseRepository;
        private StudentRepository _studentRepository;
        private EnrollmentRepository _enrollmentRepository;

        public async Task<ActionResult> Index(string order = "Ascending Course", string theme = "All Themes")
        {
            ViewBag.order = order;
            ViewBag.theme = theme;
            IEnumerable<Course> courses = null;
            using (_courseRepository = new CoursesRepository())
            {
                courses = await _courseRepository.GetListWithEnrollmentsAndThemesAsync();
                if (theme != "All Themes")
                {
                    courses = from x in courses where x.CourseTheme.ThemeName == theme select x;
                }
                switch (order)
                {
                    case "Ascending Course":
                        {
                            courses = from x in courses orderby x.Title select x;
                            break;
                        }
                    case "Descending Course":
                        {
                            courses = from x in courses orderby x.Title descending select x;
                            break;
                        }
                    case "Ascending Students":
                        {
                            courses = from x in courses orderby x.Enrollments.Count select x;
                            break;
                        }
                    case "Descending Students":
                        {
                            courses = from x in courses orderby x.Enrollments.Count descending select x;
                            break;
                        }
                    case "Ascending Monthes":
                        {
                            courses = from x in courses orderby x.TotalMonths() select x;
                            break;
                        }
                    case "Descending Monthes":
                        {
                            courses = from x in courses orderby x.TotalMonths() descending select x;
                            break;
                        }
                }
            }
            return View(courses.ToList()); ;
        }

        public async Task<ActionResult> SpecificCourse(int id)
        {
            Course result = null;
            using (_courseRepository = new CoursesRepository())
            {
                var allCourses = await _courseRepository.GetListWithEnrollmentsAndThemesAsync();
                result = allCourses.FirstOrDefault(f => f.CourseID == id);
            }
            return View(result);
        }

        public async Task<ActionResult> Register(int id)
        {
            Course registerCourse = null;
            Student registerStudent = null;

            using (_courseRepository = new CoursesRepository())
            {
                registerCourse = await _courseRepository.GetAsync(id);
            }
            using (_studentRepository = new StudentRepository())
            {
                var allStudents = await _studentRepository.GetListWithUserAsync();
                registerStudent = allStudents.FirstOrDefault(f => f.User.UserName == HttpContext.User.Identity.Name);
            }
            using (_enrollmentRepository = new EnrollmentRepository())
            {
                Enrollment newEnrollment = new Enrollment() { CourseID = registerCourse.CourseID, StudentID = registerStudent.StudentID };
                var allEnrollments = await _enrollmentRepository.GetListAsync();
                if (!allEnrollments.Contains(newEnrollment))
                {
                    await _enrollmentRepository.AddAsync(newEnrollment);
                }
                else
                {
                    return new HttpStatusCodeResult(404);
                }
            }


            return RedirectToAction("SpecificCourse/" + id);
        }
    }
}