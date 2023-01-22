using Domain.ViewModels;
using FakeItEasy;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Servise.Interfaces;
using System.Collections.Generic;
using WEB.Controllers;
using Xunit;

namespace Tests.WEB.Tests
{
    public class StudentControllerTests
    {
        public readonly StudentController _studentController;
        public readonly IStudentService _studentService;

        public StudentControllerTests()
        {
            _studentService = A.Fake<IStudentService>();
            _studentController = new StudentController(_studentService);
        }

        [Fact]
        public async void StudentController_GetAllGroups_ZeroId_Sucsess()
        {
            int id = 0;
            var students = A.Fake<List<StudentVM>>();
            A.CallTo(() => _studentService.GetAll()).Returns(students);

            var result = await _studentController.GetAllStudents(id);

            Assert.NotNull(result);
            result.Should().BeOfType<ViewResult>();
        }

        [Fact]
        public async void StudentController_GetAllGroups_ValidId_Sucsess()
        {
            int id = 1;
            var students = A.Fake<List<StudentVM>>();
            A.CallTo(() => _studentService.GetAllFromGroup(id)).Returns(students);

            var result = await _studentController.GetAllStudents(id);

            Assert.NotNull(result);
            result.Should().BeOfType<ViewResult>();
        }

        [Fact]
        public async void StudentController_CreateGET_Sucsess()
        {
            var groupsVM = new List<SelectListItem>();


            A.CallTo(() => _studentService.GetGroupsToSelect()).Returns(groupsVM);

            var result = await _studentController.Create() as ViewResult;

            Assert.NotNull(result);
            result.Should().BeOfType<ViewResult>();
            result.ViewData.Should().NotBeNull();
            result.ViewData.Should().BeOfType<ViewDataDictionary>();
        }

        [Fact]
        public async void StudentController_DeliteGET_WithValidId_Sucsess()
        {
            int? id = 1;
            var studentVM = new StudentVM()
            {
                LastName = "Пестов",
                FirstName = "Игорь",
                GroupId = 1
            };

            A.CallTo(() => _studentService.GetFromId(1)).Returns(studentVM);

            var result = await _studentController.Delete(id) as ViewResult;

            Assert.NotNull(result);
            result.Model.Should().NotBeNull();
            result.Model.Should().BeOfType<StudentVM>();
            result.Should().BeOfType<ViewResult>();
        }

        [Fact]
        public async void StudentController_DeliteGET_WithInvalidId_Fail()
        {
            int? id = 0;
            var result = await _studentController.Delete(id);

            Assert.NotNull(result);
            result.Should().BeOfType<ViewResult>();
        }

        [Fact]
        public async void StudentController_DeliteGET_WithInvalidDataModel_Fail()
        {
            int? id = 1;

            StudentVM student = null;

            A.CallTo(() => _studentService.GetFromId((int)id)).Returns(student);

            var result = await _studentController.Delete(id);

            Assert.NotNull(result);
            result.Should().BeOfType<ViewResult>();

        }

        [Fact]
        public async void StudentController_DelitePost_With_InvalidId_Fail()
        {
            int id = 1;
            A.CallTo(() => _studentService.Delete(id)).Returns(false);

            var result = await _studentController.Delete(id);

            Assert.NotNull(result);
            result.Should().BeOfType<RedirectToActionResult>();
        }

        [Fact]
        public async void StudentController_DelitePost_With_ValidId_Sucsess()
        {
            int id = 25;
            A.CallTo(() => _studentService.Delete(id)).Returns(true);

            var result = await _studentController.Delete(id);

            Assert.NotNull(result);
            result.Should().BeOfType<RedirectToActionResult>();
        }

        [Fact]
        public async void StudentController_UpdateGET_WithValidId_Sucsess()
        {
            int id = 1;
            var studentVM = new StudentVM()
            {
                LastName = "Пестов",
                FirstName = "Игорь",
                GroupId = 1
            };

            A.CallTo(() => _studentService.GetFromId(1)).Returns(studentVM);

            var result = await _studentController.Update(id) as ViewResult;

            Assert.NotNull(result);
            result.Model.Should().NotBeNull();
            result.Model.Should().BeOfType<StudentVM>();
            result.Should().BeOfType<ViewResult>();
        }

        [Fact]
        public async void StudentController_UpdateGET_WithInvalidId_Fail()
        {
            int id = 0;
            var result = await _studentController.Update(id);

            Assert.NotNull(result);
            result.Should().BeOfType<ViewResult>();
        }

        [Fact]
        public async void StudentController_UpdateGET_WithInvalidDataModel_Fail()
        {
            int id = 1;

            StudentVM student = null;

            A.CallTo(() => _studentService.GetFromId((int)id)).Returns(student);

            var result = await _studentController.Update(id);

            Assert.NotNull(result);
            result.Should().BeOfType<ViewResult>();

        }

        [Fact]
        public async void StudentController_UpdatePOST_WithValidData_Sucsess()
        {
            var studentVM = new StudentVM()
            {
                LastName = "Пестовasdf",
                FirstName = "Игорьsadasdf",
                GroupId = 1
            };

            A.CallTo(() => _studentService.Update(studentVM)).Returns(true);

            var result = await _studentController.Update(studentVM) as RedirectToActionResult;

            result.Should().NotBeNull();
            result.Should().BeOfType<RedirectToActionResult>();
            result.ActionName.Should().Contain("GetAllStudents");
        }
    }
}
