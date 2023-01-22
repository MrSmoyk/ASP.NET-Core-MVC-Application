using Domain.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Servise.Interfaces
{
    public interface IStudentService
    {
        Task<StudentVM> GetFromId(int id);

        Task<List<StudentVM>> GetAll();

        Task<StudentVM> Create(StudentVM vievModel);

        Task<bool> Delete(int id);

        Task<bool> Update(StudentVM vievModel);

        Task<List<StudentVM>> GetAllFromGroup(int id);

        Task<List<SelectListItem>> GetGroupsToSelect();
    }
}
