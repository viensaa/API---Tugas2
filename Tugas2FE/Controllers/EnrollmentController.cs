using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Tugas2FE.Interfaces;
using Tugas2FE.ViewModels;

namespace Tugas2FE.Controllers
{
    public class EnrollmentController : Controller
    {
        private readonly IEnrollment _enrollmentDAL;
        private readonly ICourse _courseDAL;
        private readonly IStudent _studentDAL;

        public EnrollmentController(IEnrollment  enrollment,ICourse course,IStudent student)
        {
            _enrollmentDAL = enrollment;
            _courseDAL = course;
            _studentDAL = student;
        }
        //getdALL
        public async Task<IActionResult> Index()
        {
            //mendapat TOKEN
            string myToken = string.Empty;
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("token")))
            {
                myToken = HttpContext.Session.GetString("token");

            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
            ViewData["pesan"] = TempData["pesan"] ?? TempData["pesan"];

            IEnumerable<EnrollmentDetail> models;
            models = await _enrollmentDAL.GetAll(myToken);
            return View(models);
        }

        //insert
        //insert data        
        public async Task<IActionResult> Create()
        {

            string myToken = string.Empty;
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("token")))
            {
                myToken = HttpContext.Session.GetString("token");

            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
            ViewBag.Course =new SelectList(await _courseDAL.GetAll(myToken), "courseID", "title");
            ViewBag.Student =new SelectList(await _studentDAL.GetAll(myToken), "id", "lastName");
            return View();
        }
        
        [HttpPost]
        public async Task<ActionResult> Create(Enrollment enrollment)
        {
            string myToken = string.Empty;
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("token")))
            {
                myToken = HttpContext.Session.GetString("token");

            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
            try
            {
                ViewBag.Course = new SelectList(await _courseDAL.GetAll(myToken), "courseID", "title");
                ViewBag.Student = new SelectList(await _studentDAL.GetAll(myToken), "id", "lastName");
                var result = await _enrollmentDAL.Insert(enrollment,myToken);
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
            string myToken = string.Empty;
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("token")))
            {
                myToken = HttpContext.Session.GetString("token");

            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
            var model = await _enrollmentDAL.GetById(id,myToken);
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Update(Enrollment enrollment)
        {
            try
            {
                string myToken = string.Empty;
                if (!string.IsNullOrEmpty(HttpContext.Session.GetString("token")))
                {
                    myToken = HttpContext.Session.GetString("token");

                }
                else
                {
                    return RedirectToAction("Login", "Account");
                }
                var result = await _enrollmentDAL.Update(enrollment,myToken);
                TempData["pesan"] = $"<div class='alert alert-success alert-dismissible fade show'>" +
                    $"<button type='button' class='btn-close' data-bs-dismiss='alert'></button> Berhasil Mengubah Data Course dengan ID {result.EnrollmentID}</div>";
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
            string myToken = string.Empty;
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("token")))
            {
                myToken = HttpContext.Session.GetString("token");

            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
            var model = await _enrollmentDAL.GetById(id,myToken);
            return View(model);
        }

        [ActionName("Delete")]
        [HttpPost]
        public async Task<IActionResult> DeletePost(int id)
        {
            try
            {
                string myToken = string.Empty;
                if (!string.IsNullOrEmpty(HttpContext.Session.GetString("token")))
                {
                    myToken = HttpContext.Session.GetString("token");

                }
                else
                {
                    return RedirectToAction("Login", "Account");
                }
                await _enrollmentDAL.Delete(id,myToken);
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
