using Domain.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Servise.Interfaces;
using System.Threading.Tasks;

namespace WEB.Controllers
{
    public class CourseController : Controller
    {
        public readonly ICourseService _courseService;

        public CourseController(ICourseService courseService)
        {
            _courseService = courseService;
        }

        public async Task<IActionResult> GetAllCourses()
        {
            var coursesVM = await _courseService.GetAll();
            return View(coursesVM);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CourseVM courseVM)
        {
            if (ModelState.IsValid)
            {
                await _courseService.Create(courseVM);
            }
            return RedirectToAction("GetAllCourses");
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            var courseVM = await _courseService.GetFromId((int)id);
            return View(courseVM);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            await _courseService.Delete(id);
            return RedirectToAction("GetAllCourses");
        }

        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var courseVM = await _courseService.GetFromId(id);
            return View(courseVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(CourseVM courseVM)
        {
            if (ModelState.IsValid)
            {
                await _courseService.Update(courseVM);
            }
            return RedirectToAction("GetAllCourses");
        }
    }
}
