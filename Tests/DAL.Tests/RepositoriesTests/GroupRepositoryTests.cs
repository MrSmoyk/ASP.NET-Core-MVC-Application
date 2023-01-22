using AutoFixture;
using DAL;
using DAL.Repositories;
using Domain.Entity;
using System.Collections.Generic;
using Tests.DAL.Tests.FakeDb;
using Xunit;

namespace Tests.DAL.Tests.RepositoriesTests
{
    public class GroupRepositoryTests
    {
        private readonly ApplicationDbContext _context;
        private readonly GroupRepository _groupRepository;

        public GroupRepositoryTests()
        {
            _context = FakeDbContext.GetDbContext();
            _groupRepository = new GroupRepository(_context);
        }


        [Fact]
        public async void GroupRepository_GetById_Sucsess()
        {
            var group = new Fixture().Create<Group>();
            var id = group.Id;
            await _groupRepository.Create(group);

            var result = await _groupRepository.GetById(id);

            Assert.Equal(group.Id, id);
            Assert.Equal(group.GroupName, result.GroupName);
            Assert.Equal(group.CourseId, result.CourseId);
        }

        [Fact]
        public async void GroupRepository_GetAll_Sucsess()
        {
            var expected1 = new List<Group>();

            var result = await _groupRepository.GetAll();

            Assert.Equal(expected1.GetType(), result.GetType());
        }

        [Fact]
        public async void GroupRepository_Create_Sucsess()
        {
            var group = new Fixture().Create<Group>();
            var expected = group;

            var result = await _groupRepository.Create(group);

            Assert.Equal(expected, result);
        }

        [Fact]
        public async void GroupRepository_Delite_Sucsess()
        {
            var group = new Group { CourseId = 1, GroupName = "PR-03" };
            var expected = group;
            await _groupRepository.Create(group);
            var result = await _groupRepository.Delete(group);

            Assert.Equal(expected, result);
        }

        [Fact]
        public async void GroupRepository_Uppdate_Sucsess()
        {
            int id = 1;
            var group = await _groupRepository.GetById(id);

            group.GroupName = "GoG";
            var expected = group;

            var result = await _groupRepository.Update(group);

            Assert.Equal(expected.GroupName, result.GroupName);
        }

        [Fact]
        public async void GroupRepository_GetAllFromCourse_Sucsess()
        {
            int id = 1;
            var course = new Course() { Id = 1, CourseName = "C# Start", Description = "Задания курса очень простые, сложность повышается постепенно и в каждом задании вам нужно добиться только того, чтобы программа сработала." };
            var group = new Group { Course = course, Id = 3, CourseId = 1, GroupName = "PR-03" };
            var expected = new List<Group>()
            {
                group,
                new Group{ CourseId = 1, GroupName = "PR-04" },
                new Group{ CourseId = 1, GroupName = "PR-05" },
            };

            var result = await _groupRepository.GetAllFromCourse(id);

            Assert.NotNull(result);
            Assert.Equal(expected[0].CourseId, result[0].CourseId);
            Assert.Equal(expected[1].CourseId, result[1].CourseId);
            Assert.True(result[2].CourseId == result[2].CourseId);
            Assert.Equal(group.GroupName, result[0].GroupName);
        }

    }
}
