using SkillsTest.Models.Entities;
using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;

namespace SkillsTest.Models.Repository
{
    public class InstructorRepository : IRepository<Instructor>
    {
        public InstructorRepository()
        { }
        public async Task<Instructor> AddAsync(Instructor elem)
        {
            Instructor result = null;
            using (var instructorContext = new ApplicationDbContext())
            {
                result = instructorContext.Instructors.Add(elem);
                await instructorContext.SaveChangesAsync();
            }
            return result;
        }

        public async Task DeleteAsync(int id)
        {
            using (var instructorContext = new ApplicationDbContext())
            {
                var instructor = await instructorContext.Instructors.FirstOrDefaultAsync(f => f.InstructorID == id);

                instructorContext.Entry(instructor).State = EntityState.Deleted;

                await instructorContext.SaveChangesAsync();
            }
        }

        public void Dispose()
        {
            using (var instructorContext = new ApplicationDbContext())
            {
                instructorContext.Dispose();
            }
        }

        public async Task<Instructor> GetAsync(int id)
        {
            Instructor result = null;
            using (var instructorContext = new ApplicationDbContext())
            {
                result = await instructorContext.Instructors.FirstOrDefaultAsync(f => f.InstructorID == id);
            }

            return result;
        }

        public async Task<Instructor> GetAsync(string id)
        {
            Instructor result = null;
            using (var instructorContext = new ApplicationDbContext())
            {
                result = await instructorContext.Instructors.Include(f=>f.Courses).FirstOrDefaultAsync(f => f.User.Id == id);
            }

            return result;
        }

        public async Task<List<Enrollment>> GetInstructorEnrollmentsAsync(string id)
        {
            List<Enrollment> res = new List<Enrollment>();
            using (var instructorContext = new ApplicationDbContext())
            {
                var result = await instructorContext.Instructors.Include(f=>f.Courses).FirstOrDefaultAsync(f => f.User.Id == id);
                foreach (var Item in result.Courses)
                {
                    foreach (var enroll in Item.Enrollments) {
                        res.Add(enroll);
                    }
                }
            }

            return res;
        }

        public async Task<IEnumerable<Instructor>> GetListAsync()
        {
            var result = new List<Instructor>();

            using (var instructorContext = new ApplicationDbContext())
            {
                result = await instructorContext.Instructors.ToListAsync();
            }

            return result;
        }

        public async Task<IEnumerable<Instructor>> GetListWithCoursesAsync()
        {
            var result = new List<Instructor>();

            using (var instructorContext = new ApplicationDbContext())
            {
                result = await instructorContext.Instructors.Include(f => f.Courses).ToListAsync();
            }

            return result;
        }

        public async Task SaveAsync()
        {
            using (var instructorContext = new ApplicationDbContext())
            {
                await instructorContext.SaveChangesAsync();
            }
        }

        public async Task<Instructor> UpdateAsync(Instructor elem)
        {
            using (var instructorContext = new ApplicationDbContext())
            {
                instructorContext.Entry(elem).State = EntityState.Modified;

                await instructorContext.SaveChangesAsync();
            }

            return elem;
        }
    }
}