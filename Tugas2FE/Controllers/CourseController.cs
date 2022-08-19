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
            //mendapat TOKEN
            string myToken = string.Empty;
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("token")))
            {
                myToken = HttpContext.Session.GetString("token");

            }
            else
            {
                TempData["pesan"] = $"<div class='alert alert-danger alert-dismissible fade show'>" +
                    $"<button type='button' class='btn-close' data-bs-dismiss='alert'></button> Session Habis Harap Login Kembali</div>";
                return RedirectToAction("Login", "Account");
            }
            ViewData["pesan"] = TempData["pesan"] ?? TempData["pesan"];
            ViewData["user"] = TempData["user"] ?? TempData["user"];
            TempData["user"] = ViewData["user"];
            IEnumerable<Course> models;
            models = await _courseDAL.GetAll(myToken);
            return View(models);
        }

        //insert data        
        public IActionResult Create()
        {
            ViewData["user"] = TempData["user"] ?? TempData["user"];
            TempData["user"] = ViewData["user"];
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Create(Course course)
        {
            
            try
            {
                //mendapat TOKEN
                string myToken = string.Empty;
                if (!string.IsNullOrEmpty(HttpContext.Session.GetString("token")))
                {
                    myToken = HttpContext.Session.GetString("token");

                }
                else
                {
                    TempData["pesan"] = $"<div class='alert alert-danger alert-dismissible fade show'>" +
                    $"<button type='button' class='btn-close' data-bs-dismiss='alert'></button> Session Habis Harap Login Kembali</div>";
                    return RedirectToAction("Login", "Account");
                }
                var result = await _courseDAL.Insert(course,myToken);
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
            ViewData["user"] = TempData["user"] ?? TempData["user"];
            TempData["user"] = ViewData["user"];
            //mendapat TOKEN
            string myToken = string.Empty;
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("token")))
            {
                myToken = HttpContext.Session.GetString("token");

            }
            else
            {
                TempData["pesan"] = $"<div class='alert alert-danger alert-dismissible fade show'>" +
                    $"<button type='button' class='btn-close' data-bs-dismiss='alert'></button> Session Habis Harap Login Kembali</div>";
                return RedirectToAction("Login", "Account");
            }
            var model = await _courseDAL.GetById(id,myToken);
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Update(Course course)
        {

            try
            {
                //mendapat TOKEN
                string myToken = string.Empty;
                if (!string.IsNullOrEmpty(HttpContext.Session.GetString("token")))
                {
                    myToken = HttpContext.Session.GetString("token");

                }
                else
                {
                    TempData["pesan"] = $"<div class='alert alert-danger alert-dismissible fade show'>" +
                    $"<button type='button' class='btn-close' data-bs-dismiss='alert'></button> Session Habis Harap Login Kembali</div>";
                    return RedirectToAction("Login", "Account");
                }
                var result = await _courseDAL.Update(course,myToken);
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
            ViewData["user"] = TempData["user"] ?? TempData["user"];
            TempData["user"] = ViewData["user"];
            //mendapat TOKEN
            string myToken = string.Empty;
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("token")))
            {
                myToken = HttpContext.Session.GetString("token");

            }
            else
            {
                TempData["pesan"] = $"<div class='alert alert-danger alert-dismissible fade show'>" +
                    $"<button type='button' class='btn-close' data-bs-dismiss='alert'></button> Session Habis Harap Login Kembali</div>";
                return RedirectToAction("Login", "Account");
            }
            var model = await _courseDAL.GetById(id,myToken);
            return View(model);
        }

        [ActionName("Delete")]
        [HttpPost]
        public async Task<IActionResult> DeletePost(int id)
        {
            try
            {
                //mendapat TOKEN
                string myToken = string.Empty;
                if (!string.IsNullOrEmpty(HttpContext.Session.GetString("token")))
                {
                    myToken = HttpContext.Session.GetString("token");

                }
                else
                {
                    TempData["pesan"] = $"<div class='alert alert-danger alert-dismissible fade show'>" +
                    $"<button type='button' class='btn-close' data-bs-dismiss='alert'></button> Session Habis Harap Login Kembali</div>";
                    return RedirectToAction("Login", "Account");
                }
                await _courseDAL.Delete(id,myToken);
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
            ViewData["user"] = TempData["user"] ?? TempData["user"];
            TempData["user"] = ViewData["user"];
            //mendapat TOKEN
            string myToken = string.Empty;
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("token")))
            {
                myToken = HttpContext.Session.GetString("token");

            }
            else
            {
                TempData["pesan"] = $"<div class='alert alert-danger alert-dismissible fade show'>" +
                    $"<button type='button' class='btn-close' data-bs-dismiss='alert'></button> Session Habis Harap Login Kembali</div>";
                return RedirectToAction("Login", "Account");
            }
            var model =await _courseDAL.CourseWithStudent(id,myToken);
            return View(model);
        }

    }
}
