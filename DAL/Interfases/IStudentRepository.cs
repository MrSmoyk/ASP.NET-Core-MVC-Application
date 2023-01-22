using Domain.Entity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DAL.Interfases
{
    public interface IStudentRepository
    {
        Task<List<Student>> GetAll();

        Task<Student> GetById(int id);

        Task<Student> Create(Student entity);

        Task<Student> Delete(Student entity);

        Task<Student> Update(Student entity);

        Task<List<Student>> GetAllFromGroup(int id);
    }
}
