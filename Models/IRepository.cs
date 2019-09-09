using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkillsTest.Models
{
    public interface IRepository<T> : IDisposable
    {
        Task<IEnumerable<T>> GetListAsync();
        Task<T> GetAsync(int id);
        //Adds certain element
        Task<T> AddAsync(T elem);
        //Deletes certain element
        Task DeleteAsync(int id);
        //Save changes
        Task SaveAsync();
        Task<T> UpdateAsync(T elem);
    }
}
