using Domain.Entity;
using Domain.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Servise.Interfaces
{
    public interface IGroupService
    {
        Task<GroupVM> GetFromId(int id);

        Task<List<GroupVM>> GetAll();

        Task<GroupVM> Create(GroupVM vievModel);

        Task<bool> Delete(int id);

        Task<bool> Update(GroupVM vievModel);

        Task<List<GroupVM>> GetAllFromCourse(int id);

        Task<List<SelectListItem>> GetCoursesToSelect();

        Task<List<GroupVM>> InjectingCoursesVmToGroupsVm(List<Group> groups);
    }
}
