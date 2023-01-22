using AutoFixture;
using DAL;
using DAL.Repositories;
using Domain.Entity;
using System.Collections.Generic;
using Tests.DAL.Tests.FakeDb;
using Xunit;

namespace Tests.DAL.Tests.RepositoriesTests
{
    public class CourseRepositoryTests
    {
        private readonly ApplicationDbContext _context;
        private readonly CourseRepository _courseRepository;

        public CourseRepositoryTests()
        {
            _context = FakeDbContext.GetDbContext();
            _courseRepository = new CourseRepository(_context);
        }


        [Fact]
        public async void CourseRepository_GetById_Sucsess()
        {
            var course = new Course()
            {
                Id = 1,
                CourseName = "C# Start",
                Description = "Задания курса очень простые, сложность повышается постепенно и в каждом задании вам нужно добиться только того, чтобы программа сработала."
            };
            var id = course.Id;

            var result = await _courseRepository.GetById(id);

            Assert.Equal(course.CourseName, result.CourseName);
            Assert.Equal(course.Id, result.Id);
            Assert.Equal(course.Description, result.Description);
        }

        [Fact]
        public async void CourseRepository_GetAll_Sucsess()
        {
            var course = new Course()
            {
                Id = 1,
                CourseName = "C# Start",
                Description = "Задания курса очень простые, сложность повышается постепенно и в каждом задании вам нужно добиться только того, чтобы программа сработала."
            };
            var expected1 = new List<Course>()
            {
                course,
                new Course()
                {
                    Id = 2,
                    CourseName="C#/.NET",
                    Description = "Курс по C#/.Net предназначен для тех, кто хочет получить перспективную и высокооплачиваемую работу C# разработчика."
                },
                new Course()
                {
                    Id = 3,
                    CourseName="UNITY",
                    Description = "Курс по Unity3D предназначен для тех, кто обладает базовыми знаниями программирования на С# и хочет получить перспективную и высокооплачиваемую работу Unity3D-разработчика."
                }

            };



            var result = await _courseRepository.GetAll();

            Assert.Equal(expected1.GetType(), result.GetType());
            Assert.Equal(course.CourseName, result[0].CourseName);
            Assert.Equal(expected1[1].Id, result[1].Id);
            Assert.Equal(expected1[2].Description, result[2].Description);

        }

        [Fact]
        public async void CourseRepository_Create_Sucsess()
        {
            var expected = new Course()
            {
                Id = 4,
                CourseName = "C# Start",
                Description = "Задания курса очень простые, сложность повышается постепенно и в каждом задании вам нужно добиться только того, чтобы программа сработала."
            };
            var course = expected;
            var result = await _courseRepository.Create(course);

            Assert.Equal(expected, result);
        }

        [Fact]
        public async void CourseRepository_Delite_Sucsess()
        {
            var course = new Fixture().Create<Course>();
            var expected = course;
            await _courseRepository.Create(course);
            var result = await _courseRepository.Delete(course);

            Assert.Equal(expected, result);
        }

        [Fact]
        public async void CourseRepository_Uppdate_Sucsess()
        {
            int id = 1;
            var course = await _courseRepository.GetById(id);


            course.CourseName = "CoC";
            var expected = course;

            var result = await _courseRepository.Update(course);

            Assert.Equal(expected, result);
        }




    }
}
