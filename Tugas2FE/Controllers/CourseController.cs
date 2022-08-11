using Microsoft.AspNetCore.Mvc;
using Tugas2FE.Interfaces;
using Tugas2FE.ViewModels;

namespace Tugas2FE.Controllers
{
    public class CourseController : Controller
    {
        private readonly ICourse _courseDAL;

        public CourseController(ICourse course)
        {
            _courseDAL = course;
        }

        public async Task<IActionResult> Index()
        {
            ViewData["pesan"] = TempData["pesan"] ?? TempData["pesan"];
            IEnumerable<Course> models;
            models = await _courseDAL.GetAll();
            return View(models);
        }
    }
}
