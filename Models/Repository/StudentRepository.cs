using SkillsTest.Models.Entities;
using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;

namespace SkillsTest.Models.Repository
{
    public class StudentRepository : IRepository<Student>
    {
        public StudentRepository()
        { }
        
        public async Task<Student> AddAsync(Student elem)
        {
            Student result = null;
            using (var studentContext = new ApplicationDbContext())
            {
                result = studentContext.Students.Add(elem);
                await studentContext.SaveChangesAsync();
            }
            return result;
        }

        public async Task DeleteAsync(int id)
        {
            using (var studentContext = new ApplicationDbContext())
            {
                var student = await studentContext.Students.FirstOrDefaultAsync(f => f.StudentID == id);

                studentContext.Entry(student).State = EntityState.Deleted;

                await studentContext.SaveChangesAsync();
            }
        }

        public void Dispose()
        {
            using (var studentContext = new ApplicationDbContext())
            {
                studentContext.Dispose();
            }
        }

        public async Task<List<Course>> GetAsync(string id)
        {
            List<Course> res = new List<Course>();
            using (var studentContext = new ApplicationDbContext())
            {
                var result = await studentContext.Students.FirstOrDefaultAsync(f => f.UserID == id);
                foreach (var Item in result.Enrollments)
                {
                    res.Add(Item.Course);
                }
            }

            return res;
        }

        public async Task<IEnumerable<Student>> GetListAsync()
        {
            var result = new List<Student>();

            using (var studentContext = new ApplicationDbContext())
            {
                result = await studentContext.Students.ToListAsync();
            }

            return result;
        }

        public async Task<IEnumerable<Student>> GetListWithUserAsync()
        {
            var result = new List<Student>();

            using (var studentContext = new ApplicationDbContext())
            {
                result = await studentContext.Students.Include(f=>f.User).ToListAsync();
            }

            return result;
        }

        public async Task<IEnumerable<Student>> GetListWithUserAndEnrollmentsAsync()
        {
            var result = new List<Student>();

            using (var studentContext = new ApplicationDbContext())
            {
                result = await studentContext.Students.Include(f => f.User).Include(f=>f.Enrollments).ToListAsync();
            }

            return result;
        }


        public async Task SaveAsync()
        {
            using (var studentContext = new ApplicationDbContext())
            {
                await studentContext.SaveChangesAsync();
            }
        }

        public async Task<Student> UpdateAsync(Student elem)
        {
            using (var studentContext = new ApplicationDbContext())
            {
                studentContext.Entry(elem).State = EntityState.Modified;

                await studentContext.SaveChangesAsync();
            }

            return elem;
        }

        public async Task<Student> GetAsync(int id)
        {
            Student result = null;
            using (var studentContext = new ApplicationDbContext())
            {
                result = await studentContext.Students.FirstOrDefaultAsync(f=>f.StudentID == id);
            }
            return result;
        }
    }
}