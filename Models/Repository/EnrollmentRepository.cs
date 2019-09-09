using SkillsTest.Models.Entities;
using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;

namespace SkillsTest.Models.Repository
{
    public class EnrollmentRepository : IRepository<Enrollment>
    {
        public EnrollmentRepository()
        { }

        public async Task<Enrollment> AddAsync(Enrollment elem)
        {
            Enrollment result = null;
            using (var enrollmentContext = new ApplicationDbContext())
            {
                result = enrollmentContext.Enrollments.Add(elem);
                await enrollmentContext.SaveChangesAsync();
            }
            return result;
        }

        public async Task DeleteAsync(int id)
        {
            using (var enrollmentContext = new ApplicationDbContext())
            {
                var enrollment = await enrollmentContext.Enrollments.FirstOrDefaultAsync(f => f.EnrollmentID == id);

                enrollmentContext.Entry(enrollment).State = EntityState.Deleted;

                await enrollmentContext.SaveChangesAsync();
            }
        }

        public void Dispose()
        {
            using (var enrollmentContext = new ApplicationDbContext())
            {
                enrollmentContext.Dispose();
            }
        }

        public async Task<Enrollment> GetAsync(int id)
        {
            Enrollment result = null;
            using (var enrollmentContext = new ApplicationDbContext())
            {
                result = await enrollmentContext.Enrollments.FirstOrDefaultAsync(f => f.EnrollmentID == id);
            }

            return result;
        }

        public async Task<Course> GetListEnrollment(int id)
        {
            Course result = null;
            using (var enrollmentContext = new ApplicationDbContext())
            {
                result = await enrollmentContext.Courses.Include(f => f.Enrollments).FirstOrDefaultAsync(f => f.CourseID == id);
                
            }
            return result;
        }

        public async Task<IEnumerable<Enrollment>> GetListAsync()
        {
            var result = new List<Enrollment>();

            using (var enrollmentContext = new ApplicationDbContext())
            {
                result = await enrollmentContext.Enrollments.ToListAsync();
            }

            return result;
        }

        public async Task SaveAsync()
        {
            using (var enrollmentContext = new ApplicationDbContext())
            {
                await enrollmentContext.SaveChangesAsync();
            }
        }

        public async Task<Enrollment> UpdateAsync(Enrollment elem)
        {
            using (var enrollmentContext = new ApplicationDbContext())
            {
                enrollmentContext.Entry(elem).State = EntityState.Modified;

                await enrollmentContext.SaveChangesAsync();
            }

            return elem;
        }
    }
}