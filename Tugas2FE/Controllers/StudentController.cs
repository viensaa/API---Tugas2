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
            //mengambil data
            ViewData["user"] = TempData["user"] ?? TempData["user"];
            //mengirim data
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
                return RedirectToAction("Login" , "Account");
                }
            ViewData["pesan"] = TempData["pesan"] ?? TempData["pesan"];
            IEnumerable<Student> model;
            model = await _studentDAL.GetAll(myToken);
            return View(model);
        }

        //insert data        
        public IActionResult Create()
        {
            //mengambil data
            ViewData["user"] = TempData["user"] ?? TempData["user"];
            //mengirim data
            TempData["user"] = ViewData["user"];
            return View();
        }
        [HttpPost]
        public async Task<ActionResult>Create(Student student)
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
                var result = await _studentDAL.Insert(student,myToken);
                TempData["pesan"] = $"<div class='alert alert-success alert-dismissible fade show'><button type='button' class='btn-close' data-bs-dismiss='alert'></button> Berhasil menambahkan data Student Dengan Id {result.id}</div>";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ViewData["pesan"] = $"<div class='alert alert-success alert-dismissible fade show'><button type='button' class='btn-close' data-bs-dismiss='alert'></button> Error: {ex.Message}</div>";
                return View();
            }
        }

        //update
        public async Task<IActionResult>Update(int id)
        {
            //mengambil data
            ViewData["user"] = TempData["user"] ?? TempData["user"];
            //mengirim data
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
            var model = await _studentDAL.GetById(id,myToken);
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult>Update(Student student)
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
                var result = await _studentDAL.Update(student,myToken);
                TempData["pesan"] = $"<div class='alert alert-success alert-dismissible fade show'><button type='button' class='btn-close' data-bs-dismiss='alert'></button> Berhasil Mengubah Data Student dengan ID {result.id}</div>";
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
            //mengambil data
            ViewData["user"] = TempData["user"] ?? TempData["user"];
            //mengirim data
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
            var model = await _studentDAL.GetById(id,myToken);
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

                await _studentDAL.Delete(id,myToken);
                TempData["pesan"] = $"<div class='alert alert-success alert-dismissible fade show'><button type='button' class='btn-close' data-bs-dismiss='alert'></button> Berhasil Menghapus Data</div>";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return View();
            }
        }

        public async Task<IActionResult> StudentCourseById(int id)
        {
            //mengambil data
            ViewData["user"] = TempData["user"] ?? TempData["user"];
            //mengirim data
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
            var result = await _studentDAL.StudentCourseById(id,myToken);
            return View(result);
        }

    }
}
