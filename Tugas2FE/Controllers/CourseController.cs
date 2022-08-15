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

        //insert data        
        public IActionResult Create()
        {

            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Create(Course course)
        {
            try
            {
                var result = await _courseDAL.Insert(course);
                TempData["pesan"] = $"<div class='alert alert-success alert-dismissible fade show'><button type='button' class='btn-close' data-bs-dismiss='alert'></button> Berhasil menambahkan data Course Dengan Id {result.courseID}</div>";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ViewData["pesan"] = $"<div class='alert alert-success alert-dismissible fade show'><button type='button' class='btn-close' data-bs-dismiss='alert'></button> Error: {ex.Message}</div>";
                return View();
            }
        }


        //update
        public async Task<IActionResult> Update(int id)
        {
            var model = await _courseDAL.GetById(id);
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Update(Course course)
        {
            try
            {
                var result = await _courseDAL.Update(course);
                TempData["pesan"] = $"<div class='alert alert-success alert-dismissible fade show'>" +
                    $"<button type='button' class='btn-close' data-bs-dismiss='alert'></button> Berhasil Mengubah Data Course dengan ID {result.courseID}</div>";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {

                return View();
            }
        }

        //delete
        public async Task<IActionResult> Delete(int id)
        {
            var model = await _courseDAL.GetById(id);
            return View(model);
        }

        [ActionName("Delete")]
        [HttpPost]
        public async Task<IActionResult> DeletePost(int id)
        {
            try
            {
                await _courseDAL.Delete(id);
                TempData["pesan"] = $"<div class='alert alert-success alert-dismissible fade show'><button type='button' class='btn-close' data-bs-dismiss='alert'></button> Berhasil Menghapus Data</div>";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return View();
            }
        }

        //menu detail unutk melihat coursenya di ambil oleh siapa saja
        public async Task<IActionResult> CourseStudentById(int id)
        {
            var model =await _courseDAL.CourseWithStudent(id);
            return View(model);
        }

    }
}
