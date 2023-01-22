using Domain.Entity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DAL.Interfases
{
    public interface IGroupRepository
    {
        Task<List<Group>> GetAll();

        Task<Group> GetById(int id);

        Task<Group> Create(Group entity);

        Task<Group> Delete(Group entity);

        Task<Group> Update(Group entity);

        Task<List<Group>> GetAllFromCourse(int id);
    }
}
