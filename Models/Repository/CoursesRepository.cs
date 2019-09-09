using SkillsTest.Models.Entities;
using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;

namespace SkillsTest.Models.Repository
{
    public class CoursesRepository : IRepository<Course>
    {
        public CoursesRepository()
        { }
        
        public async Task<Course> AddAsync(Course elem)
        {
            Course result = null;
            using (var coursesContext = new ApplicationDbContext())
            {
                result = coursesContext.Courses.Add(elem);
                await coursesContext.SaveChangesAsync();
            }
            return result;
        }

        public async Task DeleteAsync(int id)
        {
            using (var coursesContext = new ApplicationDbContext())
            {
                var course = await coursesContext.Courses.FirstOrDefaultAsync(f => f.CourseID == id);

                coursesContext.Entry(course).State = EntityState.Deleted;

                await coursesContext.SaveChangesAsync();
            }
        }

        public void Dispose()
        {
            using (var coursesContext = new ApplicationDbContext())
            {
                coursesContext.Dispose();
            }
        }

        public async Task<Course> GetAsync(int id)
        {
            Course result = null;
            using (var coursesContext = new ApplicationDbContext())
            {
                result = await coursesContext.Courses.FirstOrDefaultAsync(f => f.CourseID == id);
            }

            return result;
        }

        public async Task<IEnumerable<Course>> GetListAsync()
        {
            var result = new List<Course>();

            using (var coursesContext = new ApplicationDbContext())
            {
                result = await coursesContext.Courses.ToListAsync();
            }

            return result;
        }

        public async Task<IEnumerable<Course>> GetListWithEnrollmentsAndThemesAsync()
        {
            var result = new List<Course>();

            using (var coursesContext = new ApplicationDbContext())
            {
                result = await coursesContext.Courses.Include(f => f.Enrollments).Include(f=>f.CourseTheme).ToListAsync();
            }
            return result;
        }

        public async Task SaveAsync()
        {
            using (var coursesContext = new ApplicationDbContext())
            {
                await coursesContext.SaveChangesAsync();
            }
        }

        public async Task<Course> UpdateAsync(Course elem)
        {
            using (var coursesContext = new ApplicationDbContext())
            {
                coursesContext.Entry(elem).State = EntityState.Modified;

                await coursesContext.SaveChangesAsync();
            }

            return elem;
        }
    }
}