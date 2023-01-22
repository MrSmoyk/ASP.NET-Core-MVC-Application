using AutoFixture;
using DAL;
using DAL.Repositories;
using Domain.Entity;
using System.Collections.Generic;
using Tests.DAL.Tests.FakeDb;
using Xunit;

namespace Tests.DAL.Tests.RepositoriesTests
{
    public class StudentReposytoryTests
    {
        private readonly ApplicationDbContext _context;
        private readonly StudentRepository _studentRepository;

        public StudentReposytoryTests()
        {
            _context = FakeDbContext.GetDbContext();
            _studentRepository = new StudentRepository(_context);
        }


        [Fact]
        public async void StudentRepository_GetById_Sucsess()
        {
            var student = new Fixture().Create<Student>();
            var id = student.Id;
            await _studentRepository.Create(student);

            var result = await _studentRepository.GetById(id);

            Assert.Equal(student.Id, id);
            Assert.Equal(student.FirstName, result.FirstName);
            Assert.Equal(student.LastName, result.LastName);
        }

        [Fact]
        public async void StudentRepository_GetAll_Sucsess()
        {
            var expected1 = new List<Student>();

            var result = await _studentRepository.GetAll();

            Assert.Equal(expected1.GetType(), result.GetType());
        }

        [Fact]
        public async void StudentRepository_Create_Sucsess()
        {
            var student = new Fixture().Create<Student>();
            var expected = student;

            var result = await _studentRepository.Create(student);

            Assert.Equal(expected, result);
        }

        [Fact]
        public async void StudentRepository_Delite_Sucsess()
        {
            var student = new Fixture().Create<Student>();
            var expected = student;
            await _studentRepository.Create(student);
            var result = await _studentRepository.Delete(student);

            Assert.Equal(expected, result);
        }

        [Fact]
        public async void StudentRepository_Uppdate_Sucsess()
        {
            int id = 1;
            var student = await _studentRepository.GetById(id);

            student.FirstName = "SoS";
            var expected = student;

            var result = await _studentRepository.Update(student);

            Assert.Equal(expected.FirstName, result.FirstName);
        }

        [Fact]
        public async void StudentRepository_GetAllFromCourse_Sucsess()
        {
            int id = 1;
            var group = new Group { Id = 1, CourseId = 1, GroupName = "SR-01" };

            var student = new Student { LastName = "Пестов", FirstName = "Игорь", GroupId = 1, Id = 1, Group = group };


            var expected = new List<Student>()
            {
              student,
              new Student{LastName="Токар",FirstName="Чеслав",GroupId=1,Id = 3},
              new Student{LastName="Полищук",FirstName="Павел",GroupId=1,Id = 2},
              new Student{LastName="Филатов",FirstName="Никита",GroupId=1,Id = 4},
            };

            var result = await _studentRepository.GetAllFromGroup(id);

            Assert.NotNull(result);
            Assert.Equal(expected[0].GroupId, result[0].GroupId);
            Assert.Equal(expected[1].GroupId, result[1].GroupId);
            Assert.True(expected[2].GroupId == result[2].GroupId);
        }
    }
}
