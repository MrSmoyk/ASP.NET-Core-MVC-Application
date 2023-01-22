using AutoMapper;
using DAL;
using DAL.Repositories;
using Domain.ViewModels;
using FakeItEasy;
using Microsoft.Extensions.Logging;
using Servise.AutoMapper;
using Servise.Implementations;
using System.Collections.Generic;
using Tests.DAL.Tests.FakeDb;
using Xunit;

namespace Tests.Service.Tests
{
    public class CourseServiceTests
    {
        private readonly ApplicationDbContext _context;
        private readonly CourseRepository _courseRepository;
        private readonly CourseService _courseService;
        private readonly MapperService _mapperS;
        private readonly ILogger<CourseService> _logger;

        public CourseServiceTests()
        {
            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(typeof(Profiles));
            });
            var mapper = mockMapper.CreateMapper();

            _context = FakeDbContext.GetDbContext();
            _courseRepository = new CourseRepository(_context);
            _mapperS = new MapperService(mapper);
            _logger = A.Fake<ILogger<CourseService>>();
            _courseService = new CourseService(_courseRepository, _mapperS, _logger);
        }

        [Fact]
        public async void CourseService_GetAll_Sucsess()
        {
            var courseVM = new CourseVM()
            {
                Id = 1,
                CourseName = "C# Start",
                Description = "Задания курса очень простые, сложность повышается постепенно и в каждом задании вам нужно добиться только того, чтобы программа сработала."
            };
            var expected = new List<CourseVM>()
            {
                courseVM,
                new CourseVM()
                {
                    Id = 2,
                    CourseName="C#/.NET",
                    Description = "Курс по C#/.Net предназначен для тех, кто хочет получить перспективную и высокооплачиваемую работу C# разработчика."
                },
                new CourseVM()
                {
                    Id = 3,
                    CourseName="UNITY",
                    Description = "Курс по Unity3D предназначен для тех, кто обладает базовыми знаниями программирования на С# и хочет получить перспективную и высокооплачиваемую работу Unity3D-разработчика."
                }
            };

            var result = await _courseService.GetAll();

            Assert.Equal(expected.Count, result.Count);
            Assert.Equal(expected[0].CourseName, result[0].CourseName);
            Assert.Equal(expected[1].Id, result[1].Id);
            Assert.Equal(expected[2].Description, result[2].Description);

        }


        [Fact]
        public async void CourseService_GetFromId_Sucsess()
        {
            var expected = new CourseVM()
            {
                Id = 1,
                CourseName = "C# Start",
                Description = "Задания курса очень простые, сложность повышается постепенно и в каждом задании вам нужно добиться только того, чтобы программа сработала."
            };

            int id = 1;

            var result = await _courseService.GetFromId(id);

            Assert.Equal(expected.CourseName, result.CourseName);
            Assert.Equal(expected.Id, result.Id);
            Assert.Equal(expected.Description, result.Description);
        }

        [Fact]
        public async void CourseService_Create_Sucsess()
        {
            var expected = new CourseVM()
            {
                Id = 4,
                CourseName = "C# Start",
                Description = "Задания курса очень простые, сложность повышается постепенно и в каждом задании вам нужно добиться только того, чтобы программа сработала."
            };


            var result = await _courseService.Create(expected);

            Assert.Equal(expected.CourseName, result.CourseName);
            Assert.Equal(expected.Id, result.Id);
            Assert.Equal(expected.Description, result.Description);
        }

        [Fact]
        public async void CourseService_Delite_Sucsess()
        {
            int id = 1;

            var result = await _courseService.Delete(id);

            Assert.True(result);
        }

        [Fact]
        public async void CourseService_Delite_WithZeroId_Fail()
        {
            int id = 0;

            var result = await _courseService.Delete(id);

            Assert.False(result);
        }

        [Fact]
        public async void CourseService_Update_Sucsess()
        {
            int id = 1;
            var expected = new CourseVM() { Id = id, CourseName = "C#", Description = "1234" };

            var result = await _courseService.Update(expected);

            var result2 = await _courseService.GetFromId(id);

            Assert.True(result);
            Assert.Equal(result2.CourseName, expected.CourseName);
            Assert.Equal(result2.Description, expected.Description);
        }

    }
}
