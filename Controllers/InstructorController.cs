using SkillsTest.Models.Entities;
using SkillsTest.Models.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace SkillsTest.Controllers
{
    public class InstructorController : Controller
    {

        private InstructorRepository _instructorRepository;

        // GET: Instructor
        public async Task<ActionResult> Index()
        {
            IEnumerable<Instructor> instructors = null;
            using (_instructorRepository = new InstructorRepository())
            {
                instructors = await _instructorRepository.GetListAsync();
            }
            return View(instructors);
        }
        public async Task<ActionResult> SpecificInstructor(int id)
        {
            IEnumerable<Instructor> result = null;
            using (_instructorRepository = new InstructorRepository())
            {
                var allInstructors = await _instructorRepository.GetListWithCoursesAsync();
                result = from x in allInstructors where x.InstructorID == id select x;
            }
            return View(result.ToList()[0]);
        }
    }
}