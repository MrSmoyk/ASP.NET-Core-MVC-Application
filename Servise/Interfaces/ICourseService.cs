using Domain.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Servise.Interfaces
{
    public interface ICourseService
    {
        Task<CourseVM> GetFromId(int id);

        Task<List<CourseVM>> GetAll();

        Task<CourseVM> Create(CourseVM vievModel);

        Task<bool> Delete(int id);

        Task<bool> Update(CourseVM vievModel);
    }
}
