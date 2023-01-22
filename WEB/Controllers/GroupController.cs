using Domain.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Servise.Interfaces;
using System.Threading.Tasks;

namespace WEB.Controllers
{
    public class GroupController : Controller
    {
        public readonly IGroupService _groupService;

        public GroupController(IGroupService groupService)
        {
            _groupService = groupService;
        }

        public async Task<IActionResult> GetAllGroups(int id)
        {
            if (id == 0)
            {
                return View(await _groupService.GetAll());
            }

            return View(await _groupService.GetAllFromCourse(id));
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            ViewBag.Courses = await _groupService.GetCoursesToSelect();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(GroupVM groupVM)
        {
            if (ModelState.IsValid)
            {
                await _groupService.Create(groupVM);
            }
            return RedirectToAction("GetAllGroups");
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            var groupVM = await _groupService.GetFromId((int)id);
            return View(groupVM);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            bool result = await _groupService.Delete(id);
            if (result == false)
            {
                return RedirectToAction("ErrorScreen");
            }
            return RedirectToAction("GetAllGroups");
        }

        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var groupVM = await _groupService.GetFromId(id);
            ViewBag.Courses = await _groupService.GetCoursesToSelect();
            return View(groupVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(GroupVM groupVM)
        {
            if (ModelState.IsValid)
            {
                await _groupService.Update(groupVM);
            }
            return RedirectToAction("GetAllGroups");
        }

        public IActionResult ErrorScreen()
        {
            return View();
        }
    }
}
