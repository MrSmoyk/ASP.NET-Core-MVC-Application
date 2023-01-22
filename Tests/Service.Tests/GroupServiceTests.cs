using AutoMapper;
using DAL;
using DAL.Interfases;
using DAL.Repositories;
using Domain.Entity;
using Domain.ViewModels;
using FakeItEasy;
using Microsoft.Extensions.Logging;
using Servise.AutoMapper;
using Servise.Implementations;
using Servise.Interfaces;
using System.Collections.Generic;
using Tests.DAL.Tests.FakeDb;
using Xunit;

namespace Tests.Service.Tests
{
    public class GroupServiceTests
    {
        public readonly IGroupService _groupService;
        public readonly IGroupRepository _groupRepository;
        public readonly ApplicationDbContext _context;
        public readonly IStudentRepository _studentRepository;
        public readonly ICourseRepository _courseRepository;
        public readonly ICourseService _courseService;
        public readonly ILogger<CourseService> __logger;
        public readonly ILogger<GroupService> _logger;
        public GroupServiceTests()
        {
            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(typeof(Profiles));
            });
            var mapper = mockMapper.CreateMapper();
            var _mapperS = new MapperService(mapper);
            _context = FakeDbContext.GetDbContext();
            _groupRepository = new GroupRepository(_context);
            _studentRepository = new StudentRepository(_context);
            _courseRepository = new CourseRepository(_context);
            __logger = A.Fake<ILogger<CourseService>>();
            _logger = A.Fake<ILogger<GroupService>>();
            _courseService = new CourseService(_courseRepository, _mapperS, __logger);

            _groupService = new GroupService(_groupRepository, _courseService, _studentRepository, _mapperS, _logger);
        }

        [Fact]
        public async void GroupService_InjectingCoursesVmToGroupsVm_WithValidList_Sucsess()
        {
            var groups = new List<Group>()
            {
                new Group{ CourseId = 2, GroupName = "SR-01" },
                new Group{ CourseId = 2, GroupName = "SR-02" },
                new Group{ CourseId = 1, GroupName = "PR-03" },
                new Group{ CourseId = 1, GroupName = "PR-04" },
                new Group{ CourseId = 1, GroupName = "PR-05" },
                new Group{ CourseId = 3, GroupName = "UR-06" }
            };

            var result = await _groupService.InjectingCoursesVmToGroupsVm(groups);

            Assert.NotNull(result);
            Assert.Equal(groups.Count, result.Count);
            Assert.Equal(groups[0].CourseId, result[0].CourseId);
            Assert.Equal(groups[1].CourseId, result[1].Course.Id);
        }

        [Fact]
        public async void GroupService_Create_WithValidList_Sucsess()
        {
            var groupsVM = new List<GroupVM>()
            {
                new GroupVM{Id = 7, CourseId = 2, GroupName = "SR-01" },
            };

            var result = await _groupService.Create(groupsVM[0]);

            Assert.NotNull(result);
            Assert.Equal(groupsVM[0].CourseId, result.CourseId);
            Assert.Equal(groupsVM[0].GroupName, result.GroupName);
        }

        [Fact]
        public async void GroupServise_Delete_WithValidId_Sucsess()
        {
            var groupsVM = new List<GroupVM>()
            {
                new GroupVM{CourseId = 2, GroupName = "SR-01" },
                new GroupVM{CourseId = 2, GroupName = "SR-01" },
                new GroupVM{CourseId = 2, GroupName = "SR-01" },
            };

            await _groupService.Create(groupsVM[0]);
            await _groupService.Create(groupsVM[1]);
            await _groupService.Create(groupsVM[2]);


            var result1 = await _groupService.Delete(7);
            var result2 = await _groupService.Delete(8);
            var result3 = await _groupService.Delete(9);

            Assert.True(result1);
            Assert.True(result2);
            Assert.True(result3);
        }

        [Fact]
        public async void GroupServise_Delete_With_Invalid_Id_Fail()
        {
            int id = 1;

            var result = await _groupService.Delete(id);

            Assert.False(result);
        }

        [Fact]
        public async void GroupServise_Delete_With_Zero_Id_Fail()
        {
            int id = 0;

            var result = await _groupService.Delete(id);

            Assert.False(result);
        }

        [Fact]
        public async void GroupServise_GetAll_Sucsess()
        {
            var expected = new List<GroupVM>()
            {
                new GroupVM {Id = 3, CourseId = 2, GroupName = "SR-01" },
                new GroupVM {Id = 2, CourseId = 2, GroupName = "SR-02" },
                new GroupVM {Id = 3, CourseId = 1, GroupName = "PR-03" },
                new GroupVM {Id = 4, CourseId = 1, GroupName = "PR-04" },
                new GroupVM {Id = 5, CourseId = 1, GroupName = "PR-05" },
                new GroupVM {Id = 6, CourseId = 3, GroupName = "UR-06" }
            };

            var result = await _groupService.GetAll();

            Assert.NotNull(result);
            Assert.NotEmpty(result);
            Assert.Equal(expected.Count, result.Count);
            Assert.Equal(expected[0].Id, result[0].Id);
            Assert.Equal(expected[1].GroupName, result[1].GroupName);
            Assert.True(expected[2].GetType() == result[2].GetType());
        }

        [Fact]
        public async void GroupServise_GetAllFromCourse_Sucsess()
        {
            int id = 1;
            var expected = new List<GroupVM>()
            {
                new GroupVM {Id = 1, CourseId = 1, GroupName = "PR-03" },
                new GroupVM {Id = 2, CourseId = 1, GroupName = "PR-04" },
                new GroupVM {Id = 3, CourseId = 1, GroupName = "PR-05" },
            };


            var result = await _groupService.GetAllFromCourse(id);

            Assert.NotNull(result);
            Assert.NotEmpty(result);
            Assert.Equal(expected.Count, result.Count);
            Assert.Equal(expected[1].GroupName, result[1].GroupName);
            Assert.True(expected[2].GetType() == result[2].GetType());
        }

        [Fact]
        public async void GroupServise_GetFromId_Sucsess()
        {
            int id = 1;
            var expected = new List<GroupVM>()
            {
                new GroupVM {Id = 1, CourseId = 2, GroupName = "SR-01" },
            };


            var result = await _groupService.GetFromId(id);

            Assert.NotNull(result);
            Assert.Equal(expected[0].GroupName, result.GroupName);
            Assert.Equal(expected[0].Id, result.Id);
            Assert.Equal(expected[0].CourseId, result.CourseId);
            Assert.True(result.GetType() == result.GetType());
        }

        [Fact]
        public async void GroupServise_Update_Sucsess()
        {
            int id = 1;
            var expected = new GroupVM() { Id = id, CourseId = 2, GroupName = "C#111" };

            var result = await _groupService.Update(expected);
            var result2 = await _groupService.GetFromId(id);

            Assert.True(result);
            Assert.Equal(result2.GroupName, expected.GroupName);
            Assert.Equal(result2.CourseId, expected.CourseId);

        }
    }
}
