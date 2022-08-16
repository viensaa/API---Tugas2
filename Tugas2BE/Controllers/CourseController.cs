using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Tugas2BE.DTO;
using Tugas2BE.Interface;
using Tugas2BE.Models;

namespace Tugas2BE.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CourseController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ICourse _courseDAL;

        public CourseController(IMapper mapper,ICourse course)
        {
            _mapper = mapper;
            _courseDAL = course;
        }

        //insert
        [HttpPost]
        [ActionName(nameof(Post))]
        public async Task<ActionResult> Post(CourseCreateDTO courseCreateDTO)
        {
            try
            {
                var NewCourse = _mapper.Map<Course>(courseCreateDTO);
                var result = await _courseDAL.Insert(NewCourse);
                var ReadData = _mapper.Map<CourseDTO>(result);
                return CreatedAtAction(nameof(Post), new { CourseID = result.CourseID }, ReadData);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        //get All
        [HttpGet]
        public async Task<IEnumerable<CourseDTO>> Get()
        {
            var results = await _courseDAL.GetAll();
            var readData = _mapper.Map<IEnumerable<CourseDTO>>(results);
            return readData;
        }

        //CourseWith Student
        [HttpGet("WithStudent")]
        public async Task<IEnumerable<CourseWithStudentDTO>> CourseStudent()
        {
            var results = await _courseDAL.CourseByStudent();
            List<CourseWithStudentDTO> readData = new List<CourseWithStudentDTO>();
            foreach(var result in results)
            {
                List<StudentDTO> studentDTOs = new List<StudentDTO>();
                foreach(var student in result.Enrollments)
                {
                    studentDTOs.Add(new StudentDTO
                    {
                        FirstMidName = student.Student.FirstMidName,
                        LastName = student.Student.LastName,                                                                       
                    });
                }
                readData.Add(new CourseWithStudentDTO
                {
                    CourseID = result.CourseID,
                    Title = result.Title,
                    Credits = result.Credits,
                    Students = studentDTOs
                });
            }
            return readData;
        }


        //byTitle
        [HttpGet("Title/{title}")]
        public async Task<IEnumerable<CourseDTO>> Title(string title)
        {
            var results = await _courseDAL.ByTitle(title);
            var readData = _mapper.Map<IEnumerable<CourseDTO>>(results);
            return readData;
        }
        //delete
        [HttpDelete]
        public async Task<ActionResult>Delete(int id)
        {
            try
            {
                await _courseDAL.DeleteById(id);
                return Ok("Data Berhasil di Hapus");
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }
        [HttpGet("{id}")]
        public async Task<CourseDTO>ById(int id)
        {
            var result = await _courseDAL.GetById(id);
            var readData = _mapper.Map<CourseDTO>(result);
            return readData;
        }
        [HttpPut]
        public async Task<ActionResult>Put(CourseDTO courseDTO)
        {
            try
            {
                var updateUser = _mapper.Map<Course>(courseDTO);
                var result = await _courseDAL.Update(updateUser);
                return Ok(updateUser);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        //CourseWith Student By Id
        [HttpGet("WithStudentByid/{id}")]
        public async Task<CourseWithStudentDTO> CourseStudent(int id)
        {
            var result = await _courseDAL.CourseStudentById(id);
            CourseWithStudentDTO readData = new CourseWithStudentDTO();
            
                List<StudentDTO> studentDTOs = new List<StudentDTO>();
                foreach (var student in result.Enrollments)
                {
                    studentDTOs.Add(new StudentDTO
                    {
                        FirstMidName = student.Student.FirstMidName,
                        LastName = student.Student.LastName,
                    });
                }
                readData = new CourseWithStudentDTO
                {
                    CourseID = result.CourseID,
                    Title = result.Title,
                    Credits = result.Credits,
                    Students = studentDTOs
                };
            
            return readData;
        }

    }
}
