using SkillsTest.Models.Entities;
using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;

namespace SkillsTest.Models.Repository
{
    public class ThemeRepository : IRepository<Theme>
    {
        public ThemeRepository()
        { }

        public async Task<Theme> AddAsync(Theme elem)
        {
            Theme result = null;
            using (var themeContext = new ApplicationDbContext())
            {
                result = themeContext.Themes.Add(elem);
                await themeContext.SaveChangesAsync();
            }
            return result;
        }

        public async Task DeleteAsync(int id)
        {
            using (var themeContext = new ApplicationDbContext())
            {
                var theme = await themeContext.Themes.FirstOrDefaultAsync(f => f.ThemeId == id);

                themeContext.Entry(theme).State = EntityState.Deleted;

                await themeContext.SaveChangesAsync();
            }
        }

        public void Dispose()
        {
            using (var themeContext = new ApplicationDbContext())
            {
                themeContext.Dispose();
            }
        }

        public async Task<Theme> GetAsync(int id)
        {
            Theme result = null;
            using (var themeContext = new ApplicationDbContext())
            {
                result = await themeContext.Themes.FirstOrDefaultAsync(f => f.ThemeId == id);
            }

            return result;
        }

        public async Task<IEnumerable<Theme>> GetListAsync()
        {
            var result = new List<Theme>();

            using (var themeContext = new ApplicationDbContext())
            {
                result = await themeContext.Themes.ToListAsync();
            }

            return result;
        }

        public async Task SaveAsync()
        {
            using (var themeContext = new ApplicationDbContext())
            {
                await themeContext.SaveChangesAsync();
            }
        }

        public async Task<Theme> UpdateAsync(Theme elem)
        {
            using (var themeContext = new ApplicationDbContext())
            {
                themeContext.Entry(elem).State = EntityState.Modified;

                await themeContext.SaveChangesAsync();
            }

            return elem;
        }
    }
}