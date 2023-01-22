using Domain.Entity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DAL.Interfases
{
    public interface ICourseRepository
    {
        Task<List<Course>> GetAll();

        Task<Course> GetById(int id);

        Task<Course> Create(Course entity);

        Task<Course> Delete(Course entity);

        Task<Course> Update(Course entity);
    }
}
