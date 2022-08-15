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

        //insert
        //insert data        
        public IActionResult Create()
        {

            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Create(Enrollment enrollment)
        {
            try
            {
                var result = await _enrollmentDAL.Insert(enrollment);
                TempData["pesan"] = $"<div class='alert alert-success alert-dismissible fade show'><button type='button' class='btn-close' data-bs-dismiss='alert'></button> Berhasil Melakukan enrollment  Dengan Id {result.EnrollmentID}</div>";
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
            var model = await _enrollmentDAL.GetById(id);
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Update(Enrollment enrollment)
        {
            try
            {
                var result = await _enrollmentDAL.Update(enrollment);
                TempData["pesan"] = $"<div class='alert alert-success alert-dismissible fade show'><button type='button' class='btn-close' data-bs-dismiss='alert'></button> Berhasil Mengubah Data Course dengan ID {result.EnrollmentID}</div>";
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
            var model = await _enrollmentDAL.GetById(id);
            return View(model);
        }

        [ActionName("Delete")]
        [HttpPost]
        public async Task<IActionResult> DeletePost(int id)
        {
            try
            {
                await _enrollmentDAL.Delete(id);
                TempData["pesan"] = $"<div class='alert alert-success alert-dismissible fade show'><button type='button' class='btn-close' data-bs-dismiss='alert'></button> Berhasil Menghapus Data</div>";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return View();
            }
        }

    }
}
