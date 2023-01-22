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
    public class StudentService : IStudentService
    {
        public readonly IStudentRepository _studentRepository;

        public readonly IGroupService _groupService;
        public readonly IMapperService _mapperService;
        public readonly ILogger<StudentService> _logger;

        public StudentService(IStudentRepository studentRepository,
            IGroupService groupService,
            IMapperService mapperService,
            ILogger<StudentService> logger)
        {
            _studentRepository = studentRepository;

            _groupService = groupService;
            _mapperService = mapperService;
            _logger = logger;
        }

        public async Task<List<SelectListItem>> GetGroupsToSelect()
        {
            List<SelectListItem> groupsToSelect = new List<SelectListItem>();
            var groups = await _groupService.GetAll();
            foreach (var group in groups)
            {
                groupsToSelect.Add(new SelectListItem
                {
                    Text = group.GroupName,
                    Value = group.Id.ToString()
                });
            }
            return groupsToSelect;
        }

        public async Task<List<StudentVM>> InjectingGroupsVmToStudentsVm(List<Student> students)
        {
            var studentsVM = _mapperService.StudentListToStudentVmList(students);

            foreach (var student in studentsVM)
            {
                var groupVM = await _groupService.GetFromId(student.GroupId);
                student.Group = groupVM;
            }

            return studentsVM;

        }

        public async Task<StudentVM> Create(StudentVM vievModel)
        {
            try
            {
                var toCreate = _mapperService.StudentVmToStudent(vievModel);
                await _studentRepository.Create(toCreate);
                return vievModel;

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"StudentService.Create error: {ex.Message}");
                return new StudentVM { FirstName = "Object creation error!", LastName = "Object creation error!" };
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
                var student = await _studentRepository.GetById(id);
                if (student == null)
                {
                    throw new NullReferenceException(nameof(student));
                }
                await _studentRepository.Delete(student);
                return true;

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"StudentService.Delete error: {ex.Message}");
                return false;
            }


        }

        public async Task<List<StudentVM>> GetAll()
        {
            try
            {
                var students = await _studentRepository.GetAll();
                if (students == null)
                {
                    throw new NullReferenceException(nameof(students));
                }
                var studentsVM = await InjectingGroupsVmToStudentsVm(students);
                return studentsVM;

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"StudentService.GetAll error: {ex.Message}");
                return new List<StudentVM>();
            }

        }

        public async Task<StudentVM> GetFromId(int id)
        {
            try
            {
                if (id == 0)
                {
                    throw new ArgumentException(nameof(id));
                }
                var student = await _studentRepository.GetById(id);
                if (student == null)
                {
                    throw new NullReferenceException(nameof(student));
                }
                var studentVM = _mapperService.StudentToStudentVm(student);
                var groupVM = await _groupService.GetFromId(student.GroupId);
                studentVM.Group = groupVM;
                return studentVM;

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"StudentService.GetFromId error: {ex.Message}");
                return new StudentVM { FirstName = "Object not found!", LastName = "Object not found!" };
            }

        }

        public async Task<bool> Update(StudentVM vievModel)
        {
            try
            {
                var toUppdate = _mapperService.StudentVmToStudent(vievModel);
                await _studentRepository.Update(toUppdate);
                return true;

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"StudentService.Update error: {ex.Message}");
                return false;
            }

        }

        public async Task<List<StudentVM>> GetAllFromGroup(int id)
        {
            try
            {
                var students = await _studentRepository.GetAllFromGroup(id);
                if (students == null)
                {
                    throw new NullReferenceException(nameof(students));
                }
                var studentsVM = await InjectingGroupsVmToStudentsVm(students);
                return studentsVM;

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"StudentService.GetAllFromGroup error: {ex.Message}");
                return new List<StudentVM>();
            }

        }


    }
}
