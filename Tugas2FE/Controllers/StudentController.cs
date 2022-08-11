using Microsoft.AspNetCore.Mvc;
using Tugas2FE.Interfaces;
using Tugas2FE.ViewModels;

namespace Tugas2FE.Controllers
{
    public class StudentController : Controller
    {
        private readonly IStudent _studentDAL;

        public StudentController(IStudent student)
        {
            _studentDAL = student;
        }

        public async  Task<IActionResult> Index()
        {
            ViewData["pesan"] = TempData["pesan"] ?? TempData["pesan"];
            IEnumerable<Student> model;
            model = await _studentDAL.GetAll();
            return View(model);
        }

        //insert data
        
        public IActionResult Create()
        {

            return View();
        }
        [HttpPost]
        public async Task<ActionResult>Create(Student student)
        {
            try
            {
                var result = await _studentDAL.Insert(student);
                TempData["pesan"] = $"<div class='alert alert-success alert-dismissible fade show'><button type='button' class='btn-close' data-bs-dismiss='alert'></button> Berhasil menambahkan data Student Dengan Id {result.id}</div>";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ViewData["pesan"] = $"<div class='alert alert-success alert-dismissible fade show'><button type='button' class='btn-close' data-bs-dismiss='alert'></button> Error: {ex.Message}</div>";
                return View();
            }
        }
    }
}
