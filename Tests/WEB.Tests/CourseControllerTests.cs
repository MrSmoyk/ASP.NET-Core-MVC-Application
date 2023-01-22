using Domain.ViewModels;
using FakeItEasy;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Servise.Interfaces;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using WEB.Controllers;
using Xunit;

namespace Tests.WEB.Tests
{
    public class CourseControllerTests
    {
        public readonly CourseController _courseController;
        public readonly ICourseService _courseService;

        public CourseControllerTests()
        {
            _courseService = A.Fake<ICourseService>();
            _courseController = new CourseController(_courseService);
        }

        [Fact]
        public async void CourseController_GetAllCourses_Sucsess()
        {
            var courses = A.Fake<List<CourseVM>>();
            A.CallTo(() => _courseService.GetAll()).Returns(courses);

            var result = await _courseController.GetAllCourses();

            Assert.NotNull(result);
            result.Should().BeOfType<ViewResult>();

        }

        [Fact]
        public async void CourseController_GetAllCourses_WithValidData_Sucsess()
        {
            var courses = new List<CourseVM>();
            A.CallTo(() => _courseService.GetAll()).Returns(courses);

            var result = await _courseController.GetAllCourses() as ViewResult;

            Assert.NotNull(result);
            result.Model.Should().NotBeNull();
            result.Model.Should().BeOfType<List<CourseVM>>();
        }


        [Fact]
        public async void CourseController_CreateGET_Sucsess()
        {
            var result = await _courseController.GetAllCourses();

            Assert.NotNull(result);
            result.Should().BeOfType<ViewResult>();
        }


        [Fact]
        public async void CourseController_CreatePOST_WithValidData_Sucsess()
        {
            var courseVM = new CourseVM()
            {
                Id = 1,
                CourseName = "C# Start",
                Description = "Задания курса очень простые, сложность повышается постепенно и в каждом задании вам нужно добиться только того, чтобы программа сработала."
            };

            A.CallTo(() => _courseService.Create(courseVM)).Returns(courseVM);

            var result = await _courseController.Create(courseVM) as RedirectToActionResult;

            result.Should().NotBeNull();
            result.Should().BeOfType<RedirectToActionResult>();
            result.ActionName.Should().Contain("GetAllCourses");
        }

        [Fact]
        public void CourseController_CreatePOST_ValidationChek()
        {
            var courseVM = new CourseVM()
            {
                CourseName = null,
                Description = null
            };

            var context = new ValidationContext(courseVM, null, null);
            var result = new List<ValidationResult>();
            var invalidModel = Validator.TryValidateObject(courseVM, context, result);

            result.Should().NotBeNull();
            result.Should().HaveCount(2);
            Assert.False(invalidModel);
        }


        [Fact]
        public async void CourseController_CreatePOST_WithInvalidData()
        {
            var courseVM = new CourseVM()
            {
                CourseName = null,
                Description = null
            };

            _courseController.ModelState.AddModelError("test", "test");
            var result = await _courseController.Create(courseVM);

            result.Should().NotBeNull();
        }


        [Fact]
        public async void CourseController_DeliteGET_WithValidId_Sucsess()
        {
            int? id = 1;
            var courseVM = new CourseVM()
            {
                Id = 1,
                CourseName = "C# Start",
                Description = "Задания курса очень простые, сложность повышается постепенно и в каждом задании вам нужно добиться только того, чтобы программа сработала."
            };

            A.CallTo(() => _courseService.GetFromId(1)).Returns(courseVM);

            var result = await _courseController.Delete(id) as ViewResult;

            Assert.NotNull(result);
            result.Model.Should().NotBeNull();
            result.Model.Should().BeOfType<CourseVM>();
            result.Should().BeOfType<ViewResult>();
        }

        [Fact]
        public async void CourseController_DeliteGET_WithInvalidId_Fail()
        {
            int? id = 0;
            var result = await _courseController.Delete(id);

            Assert.NotNull(result);
            result.Should().BeOfType<ViewResult>();
        }

        [Fact]
        public async void CourseController_DeliteGET_WithInvalidDataModel_Fail()
        {
            int? id = 1;

            CourseVM course = null;

            A.CallTo(() => _courseService.GetFromId((int)id)).Returns(course);

            var result = await _courseController.Delete(id);

            Assert.NotNull(result);
            result.Should().BeOfType<ViewResult>();

        }

        [Fact]
        public async void CourseController_DelitePost_WithInvalidId_Fail()
        {
            int id = 0;
            var result = await _courseController.Delete(id);

            Assert.NotNull(result);
            result.Should().BeOfType<RedirectToActionResult>();
        }

        [Fact]
        public async void CourseController_DelitePost_WithValidId_Fail()
        {
            int id = 1;
            var result = await _courseController.Delete(id);

            Assert.NotNull(result);
            result.Should().BeOfType<RedirectToActionResult>();
        }

        [Fact]
        public async void CourseController_UpdateGET_WithValidId_Sucsess()
        {
            int id = 1;
            var courseVM = new CourseVM()
            {
                Id = 1,
                CourseName = "C# Start",
                Description = "Задания курса очень простые, сложность повышается постепенно и в каждом задании вам нужно добиться только того, чтобы программа сработала."
            };

            A.CallTo(() => _courseService.GetFromId(1)).Returns(courseVM);

            var result = await _courseController.Update(id) as ViewResult;

            Assert.NotNull(result);
            result.Model.Should().NotBeNull();
            result.Model.Should().BeOfType<CourseVM>();
            result.Should().BeOfType<ViewResult>();
        }

        [Fact]
        public async void CourseController_UpdateGET_WithInvalidId_Fail()
        {
            int id = 0;
            var result = await _courseController.Update(id);

            Assert.NotNull(result);
            result.Should().BeOfType<ViewResult>();
        }

        [Fact]
        public async void CourseController_UpdateGET_WithInvalidDataModel_Fail()
        {
            int id = 1;

            CourseVM course = null;

            A.CallTo(() => _courseService.GetFromId(id)).Returns(course);

            var result = await _courseController.Update(id);

            Assert.NotNull(result);
            result.Should().BeOfType<ViewResult>();

        }

        [Fact]
        public async void CourseController_UpdatePOST_WithValidData_Sucsess()
        {
            var courseVM = new CourseVM()
            {
                Id = 1,
                CourseName = "C# Start",
                Description = "Задания курса очень простые, сложность повышается постепенно и в каждом задании вам нужно добиться только того, чтобы программа сработала."
            };

            A.CallTo(() => _courseService.Update(courseVM)).Returns(true);

            var result = await _courseController.Update(courseVM) as RedirectToActionResult;

            result.Should().NotBeNull();
            result.Should().BeOfType<RedirectToActionResult>();
            result.ActionName.Should().Contain("GetAllCourses");
        }



    }
}
