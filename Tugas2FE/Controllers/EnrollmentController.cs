using Microsoft.AspNetCore.Mvc;
using Tugas2FE.Interfaces;
using Tugas2FE.ViewModels;

namespace Tugas2FE.Controllers
{
    public class EnrollmentController : Controller
    {
        private readonly IEnrollment _enrollmentDAL;

        public EnrollmentController(IEnrollment  enrollment)
        {
            _enrollmentDAL = enrollment;
        }
        //getdALL
        public async Task<IActionResult> Index()
        {
            ViewData["pesan"] = TempData["pesan"] ?? TempData["pesan"];
            IEnumerable<EnrollmentDetail> models;
            models = await _enrollmentDAL.GetAll();
            return View(models);
        }
    }
}
