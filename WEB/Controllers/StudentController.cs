using Domain.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Servise.Interfaces;
using System.Threading.Tasks;

namespace WEB.Controllers
{
    public class StudentController : Controller
    {
        public readonly IStudentService _studentService;

        public StudentController(IStudentService studentService)
        {
            _studentService = studentService;
        }

        public async Task<IActionResult> GetAllStudents(int id)
        {
            if (id == 0)
            {
                return View(await _studentService.GetAll());
            }
            return View(await _studentService.GetAllFromGroup(id));
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            ViewBag.Groups = await _studentService.GetGroupsToSelect();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(StudentVM studentVM)
        {
            if (ModelState.IsValid)
            {
                await _studentService.Create(studentVM);
            }
            return RedirectToAction("GetAllStudents");
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            var studentVM = await _studentService.GetFromId((int)id);
            return View(studentVM);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            await _studentService.Delete(id);
            return RedirectToAction("GetAllStudents");
        }

        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var studentVM = await _studentService.GetFromId(id);
            ViewBag.Groups = await _studentService.GetGroupsToSelect();
            return View(studentVM);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(StudentVM studentVM)
        {
            if (ModelState.IsValid)
            {
                await _studentService.Update(studentVM);
            }
            return RedirectToAction("GetAllStudents");

        }

    }
}
