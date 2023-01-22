using DAL.Interfases;
using Domain.Entity;
using Domain.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using Servise.AutoMapper;
using Servise.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Servise.Implementations
{
    public class GroupService : IGroupService
    {
        public readonly IGroupRepository _groupRepository;
        public readonly IStudentRepository _studentRepository;

        public readonly ICourseService _courseService;
        public readonly IMapperService _mapperService;
        public readonly ILogger<GroupService> _logger;

        public GroupService(IGroupRepository groupRepository,
            ICourseService courseService,
            IStudentRepository studentRepository,
            IMapperService mapperService,
            ILogger<GroupService> logger)
        {
            _groupRepository = groupRepository;
            _studentRepository = studentRepository;

            _courseService = courseService;
            _mapperService = mapperService;
            _logger = logger;
        }

        public async Task<List<GroupVM>> InjectingCoursesVmToGroupsVm(List<Group> groups)
        {
            var groupsVM = _mapperService.GroupListToGroupVmList(groups);

            foreach (var group in groupsVM)
            {
                var course = await _courseService.GetFromId(group.CourseId);
                group.Course = course;
            }

            return groupsVM;
        }

        public async Task<List<SelectListItem>> GetCoursesToSelect()
        {
            List<SelectListItem> coursesToSelect = new List<SelectListItem>();
            var courses = await _courseService.GetAll();
            foreach (var course in courses)
            {
                coursesToSelect.Add(new SelectListItem
                {
                    Text = course.CourseName,
                    Value = course.Id.ToString()
                });
            }
            return coursesToSelect;
        }

        public async Task<GroupVM> Create(GroupVM vievModel)
        {
            try
            {
                var toCreate = _mapperService.GroupVmToGroup(vievModel);
                await _groupRepository.Create(toCreate);
                return vievModel;

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"GroupService.Create error: {ex.Message}");
                return new GroupVM { GroupName = "Object creation error!" };
            }

        }

        public async Task<bool> Delete(int id)
        {
            try
            {
                if (id == 0)
                {
                    throw new ArgumentException(nameof(id));
                }

                var group = await _groupRepository.GetById(id);
                if (group == null)
                {
                    throw new NullReferenceException(nameof(group));
                }
                var students = await _studentRepository.GetAllFromGroup(id);
                if (students.Count != 0)
                {
                    return false;
                }
                await _groupRepository.Delete(group);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"GroupService.Delete error: {ex.Message}");
                return false;
            }

        }

        public async Task<List<GroupVM>> GetAll()
        {
            try
            {
                var groups = await _groupRepository.GetAll();
                if (groups == null)
                {
                    throw new NullReferenceException(nameof(groups));
                }
                var groupsVM = await InjectingCoursesVmToGroupsVm(groups);
                return groupsVM;

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"GroupService.GetAll error: {ex.Message}");
                return new List<GroupVM>();
            }

        }

        public async Task<List<GroupVM>> GetAllFromCourse(int id)
        {
            try
            {
                var groups = await _groupRepository.GetAllFromCourse(id);
                if (groups == null)
                {
                    throw new NullReferenceException(nameof(groups));
                }
                var groupsVM = await InjectingCoursesVmToGroupsVm(groups);
                return groupsVM;

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"GroupService.GetAllFromCourse error: {ex.Message}");
                return new List<GroupVM>();
            }
        }

        public async Task<GroupVM> GetFromId(int id)
        {
            try
            {
                if (id == 0)
                {
                    throw new ArgumentException(nameof(id));
                }

                var group = await _groupRepository.GetById(id);

                if (group == null)
                {
                    throw new NullReferenceException(nameof(group));
                }

                var groupVM = _mapperService.GroupToGroupVm(group);

                var course = await _courseService.GetFromId(groupVM.CourseId);
                groupVM.Course = course;

                return groupVM;

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"GroupService.GetFromId error: {ex.Message}");
                return new GroupVM { GroupName = "Object not found!" };
            }
        }

        public async Task<bool> Update(GroupVM vievModel)
        {
            try
            {
                var toUppdate = _mapperService.GroupVmToGroup(vievModel);
                await _groupRepository.Update(toUppdate);
                return true;

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"GroupService.Update error: {ex.Message}");
                return false;
            }

        }

    }
}
