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
    public class GroupControllerTests
    {
        public readonly GroupController _groupController;
        public readonly IGroupService _groupService;

        public GroupControllerTests()
        {
            _groupService = A.Fake<IGroupService>();
            _groupController = new GroupController(_groupService);
        }

        [Fact]
        public async void GroupController_GetAllGroups_ZeroId_Sucsess()
        {
            int id = 0;
            var courses = A.Fake<List<GroupVM>>();
            A.CallTo(() => _groupService.GetAll()).Returns(courses);

            var result = await _groupController.GetAllGroups(id);

            Assert.NotNull(result);
            result.Should().BeOfType<ViewResult>();
        }

        [Fact]
        public async void GroupController_GetAllGroups_ValidId_Sucsess()
        {
            int id = 1;
            var courses = A.Fake<List<GroupVM>>();
            A.CallTo(() => _groupService.GetAllFromCourse(id)).Returns(courses);

            var result = await _groupController.GetAllGroups(id);

            Assert.NotNull(result);
            result.Should().BeOfType<ViewResult>();
        }

        [Fact]
        public async void GroupController_CreateGET_Sucsess()
        {
            var coursesVM = new List<SelectListItem>();

            A.CallTo(() => _groupService.GetCoursesToSelect()).Returns(coursesVM);

            var result = await _groupController.Create() as ViewResult;

            Assert.NotNull(result);
            result.Should().BeOfType<ViewResult>();
            result.ViewData.Should().NotBeNull();
            result.ViewData.Should().BeOfType<ViewDataDictionary>();
        }

        [Fact]
        public async void GroupController_DeliteGET_WithValidId_Sucsess()
        {
            int? id = 1;
            var groupVM = new GroupVM()
            {
                Id = 1,
                CourseId = 2,
                GroupName = "SR-01"
            };

            A.CallTo(() => _groupService.GetFromId(1)).Returns(groupVM);

            var result = await _groupController.Delete(id) as ViewResult;

            Assert.NotNull(result);
            result.Model.Should().NotBeNull();
            result.Model.Should().BeOfType<GroupVM>();
            result.Should().BeOfType<ViewResult>();
        }

        [Fact]
        public async void GroupController_DeliteGET_WithInvalidId_Fail()
        {
            int? id = 0;
            var result = await _groupController.Delete(id);


            Assert.NotNull(result);
            result.Should().BeOfType<ViewResult>();
        }

        [Fact]
        public async void GroupController_DeliteGET_WithInvalidDataModel_Fail()
        {
            int? id = 1;

            GroupVM group = null;

            A.CallTo(() => _groupService.GetFromId((int)id)).Returns(group);

            var result = await _groupController.Delete(id);

            Assert.NotNull(result);
            result.Should().BeOfType<ViewResult>();

        }

        [Fact]
        public async void GroupController_DelitePost_With_ZeroId_Fail()
        {
            int id = 0;
            var result = await _groupController.Delete(id);

            Assert.NotNull(result);
            result.Should().BeOfType<RedirectToActionResult>();
        }

        [Fact]
        public async void GroupController_DelitePost_With_InvalidId_Fail()
        {
            int id = 1;
            A.CallTo(() => _groupService.Delete(id)).Returns(false);

            var result = await _groupController.Delete(id);

            Assert.NotNull(result);
            result.Should().BeOfType<RedirectToActionResult>();
        }

        [Fact]
        public async void GroupController_DelitePost_With_ValidId_Sucsess()
        {
            int id = 7;
            A.CallTo(() => _groupService.Delete(id)).Returns(true);

            var result = await _groupController.Delete(id);

            Assert.NotNull(result);
            result.Should().BeOfType<RedirectToActionResult>();
        }

        [Fact]
        public async void GroupController_UpdateGET_WithValidId_Sucsess()
        {
            int id = 1;
            var groupVM = new GroupVM()
            {
                Id = 1,
                CourseId = 2,
                GroupName = "SR-0101"
            };

            A.CallTo(() => _groupService.GetFromId(1)).Returns(groupVM);

            var result = await _groupController.Update(id) as ViewResult;

            Assert.NotNull(result);
            result.Model.Should().NotBeNull();
            result.Model.Should().BeOfType<GroupVM>();
            result.Should().BeOfType<ViewResult>();
        }

        [Fact]
        public async void GroupController_UpdateGET_WithInvalidId_Fail()
        {
            int id = 0;
            var result = await _groupController.Update(id);


            Assert.NotNull(result);
            result.Should().BeOfType<ViewResult>();
        }

        [Fact]
        public async void GroupController_UpdateGET_WithInvalidDataModel_Fail()
        {
            int id = 1;

            GroupVM group = null;

            A.CallTo(() => _groupService.GetFromId(id)).Returns(group);

            var result = await _groupController.Update(id);

            Assert.NotNull(result);
            result.Should().BeOfType<ViewResult>();

        }

        [Fact]
        public async void GroupController_UpdatePOST_WithValidData_Sucsess()
        {
            var groupVM = new GroupVM()
            {
                Id = 1,
                CourseId = 2,
                GroupName = "SR-01123"
            };

            A.CallTo(() => _groupService.Update(groupVM)).Returns(true);

            var result = await _groupController.Update(groupVM) as RedirectToActionResult;

            result.Should().NotBeNull();
            result.Should().BeOfType<RedirectToActionResult>();
            result.ActionName.Should().Contain("GetAllGroups");
        }

    }
}
