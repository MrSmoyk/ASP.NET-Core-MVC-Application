using AutoMapper;
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
    public class StudentServiceTests
    {
        public readonly StudentService _studentService;

        public StudentServiceTests()
        {
            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(typeof(Profiles));
            });
            var mapper = mockMapper.CreateMapper();
            var _mapperS = new MapperService(mapper);
            var _context = FakeDbContext.GetDbContext();
            var _groupRepository = new GroupRepository(_context);
            var _courseService = A.Fake<ICourseService>();
            var _studentRepository = new StudentRepository(_context);
            var __logger = A.Fake<ILogger<GroupService>>();
            var _logger = A.Fake<ILogger<StudentService>>();
            var _groupServise = new GroupService(_groupRepository, _courseService,
                _studentRepository, _mapperS, __logger);

            _studentService = new StudentService(_studentRepository, _groupServise, _mapperS, _logger);
        }

        [Fact]
        public async void StudentService_InjectingGroupsVmToStudentsVm_WithValidList_Sucsess()
        {
            var students = new List<Student>()
            {
              new Student{LastName="Пестов",FirstName="Игорь",GroupId=1},
              new Student{LastName="Полищук",FirstName="Павел",GroupId=1},
              new Student{LastName="Токар",FirstName="Чеслав",GroupId=1},
              new Student{LastName="Филатов",FirstName="Никита",GroupId=1},
            };

            var result = await _studentService.InjectingGroupsVmToStudentsVm(students);

            Assert.NotNull(result);
            Assert.Equal(students.Count, result.Count);
            Assert.Equal(students[0].GroupId, result[0].GroupId);
            Assert.Equal(students[0].LastName, result[0].LastName);
        }

        [Fact]
        public async void StudentService_Create_WithValidList_Sucsess()
        {
            var studentsVM = new List<StudentVM>()
            {
              new StudentVM{LastName="Пестов",FirstName="Игорь",GroupId=1}
            };

            var result = await _studentService.Create(studentsVM[0]);

            Assert.NotNull(result);
            Assert.Equal(studentsVM[0].GroupId, result.GroupId);
            Assert.Equal(studentsVM[0].FirstName, result.FirstName);
        }

        [Fact]
        public async void StudentServise_Delete_WithValidId_Sucsess()
        {
            int id = 25;
            var studentVM = new StudentVM() { LastName = "Пестов", FirstName = "Игорь", GroupId = 1, };

            await _studentService.Create(studentVM);

            var result = await _studentService.Delete(id);

            Assert.True(result);
        }

        [Fact]
        public async void StudentServise_Delete_With_Zero_Id_Sucsess()
        {
            int id = 0;

            var result = await _studentService.Delete(id);

            Assert.False(result);
        }

        [Fact]
        public async void StudentServise_GetAll_Sucsess()
        {
            var expected = new List<StudentVM>()
            {
              new StudentVM{LastName="Пестов",FirstName="Игорь",GroupId=1 , Id = 6},
              new StudentVM{LastName="Полищук",FirstName="Павел",GroupId=1},
              new StudentVM{LastName="Токар",FirstName="Чеслав",GroupId=1},
              new StudentVM{LastName="Филатов",FirstName="Никита",GroupId=1},
              new StudentVM{LastName="Коцюбинский",FirstName="Добрыня",GroupId=2},
              new StudentVM{LastName="Исаков",FirstName="Болеслав",GroupId=2},
              new StudentVM{LastName="Лыткин",FirstName="Оскар",GroupId=2},
              new StudentVM{LastName="Силин",FirstName="Роман",GroupId=2},
              new StudentVM{LastName="Власов",FirstName="Владлен",GroupId=3},
              new StudentVM{LastName="Горшков",FirstName="Артур",GroupId=3},
              new StudentVM{LastName="Предыбайло",FirstName="Герман",GroupId=3},
              new StudentVM{LastName="Худобяк",FirstName="Сава",GroupId=3},
              new StudentVM{LastName="Бородай",FirstName="Гавриил",GroupId=4},
              new StudentVM{LastName="Кабанов",FirstName="Антон",GroupId=4},
              new StudentVM{LastName="Комаров",FirstName="Леонид",GroupId=4},
              new StudentVM{LastName="Грабчак",FirstName="Игорь",GroupId=4},
              new StudentVM{LastName="Бобылёв",FirstName="Юрий",GroupId=5},
              new StudentVM{LastName="Федосеев",FirstName="Сава",GroupId=5},
              new StudentVM{LastName="Колесник",FirstName="Геннадий",GroupId=5},
              new StudentVM{LastName="Городецкий",FirstName="Никодим",GroupId=5},
              new StudentVM{LastName="Крюков",FirstName="Фёдор",GroupId=6},
              new StudentVM{LastName="Воронцов",FirstName="Лев",GroupId=6},
              new StudentVM{LastName="Кулаков",FirstName="Богдан",GroupId=6},
              new StudentVM{LastName="Доронин",FirstName="Елисей",GroupId=6}
            };

            var result = await _studentService.GetAll();

            Assert.NotNull(result);
            Assert.NotEmpty(result);
            Assert.Equal(expected.Count, result.Count);
            Assert.Equal(expected[0].Id, result[0].Id);
            Assert.True(expected[2].GetType() == result[2].GetType());
        }

        [Fact]
        public async void StudentServise_GetAllFromGroup_Sucsess()
        {
            int id = 1;
            var expected = new List<StudentVM>()
            {
              new StudentVM{LastName="Пестов",FirstName="Игорь",GroupId=1},
              new StudentVM{LastName="Полищук",FirstName="Павел",GroupId=1},
              new StudentVM{LastName="Токар",FirstName="Чеслав",GroupId=1},
              new StudentVM{LastName="Филатов",FirstName="Никита",GroupId=1}
            };


            var result = await _studentService.GetAllFromGroup(id);

            Assert.NotNull(result);
            Assert.NotEmpty(result);
            Assert.Equal(expected.Count, result.Count);
            Assert.True(expected[2].GetType() == result[2].GetType());
        }

        [Fact]
        public async void StudentServise_GetFromId_Sucsess()
        {
            int id = 1;
            var expected = new List<StudentVM>()
            {
              new StudentVM{Id = 1, LastName="Пестов",FirstName="Игорь", GroupId=1},
            };


            var result = await _studentService.GetFromId(id);

            Assert.NotNull(result);
            Assert.Equal(expected[0].FirstName, result.FirstName);
            Assert.Equal(expected[0].Id, result.Id);
            Assert.Equal(expected[0].GroupId, result.GroupId);
            Assert.True(expected[0].GetType() == result.GetType());
        }

        [Fact]
        public async void StudentServise_Update_Sucsess()
        {
            int id = 3;
            var expected = new StudentVM { LastName = "C#111123dksf;k", FirstName = "121234gsdyf8ysd", GroupId = 1, Id = id };

            var result = await _studentService.Update(expected);
            var result2 = await _studentService.GetFromId(id);

            Assert.True(result);
            Assert.Equal(result2.LastName, expected.LastName);
            Assert.Equal(result2.FirstName, expected.FirstName);
            Assert.Equal(result2.GroupId, expected.GroupId);
        }
    }
}
