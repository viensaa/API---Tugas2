using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Tugas2BE.Data.Interface;
using Tugas2BE.Domain;
using Tugas2BE.DTO;

namespace Tugas2BE.Controllers
{
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

    }
}
