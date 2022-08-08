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
    public class StudentController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IStudent _studentDAL;

        public StudentController(IMapper mapper,IStudent student)
        {
            _mapper = mapper;
            _studentDAL = student;
        }

        //menampilakn seluruh data       
        [HttpGet]
        public async Task<IEnumerable<StudentDTO>> Get()
        {
            var results = await _studentDAL.GetAll();
            var ReadData = _mapper.Map<IEnumerable<StudentDTO>>(results);
            return ReadData;
        }
        //byFname
        [HttpGet("FName/{Fname}")]
        public async Task<IEnumerable<StudentDTO>>FName(string Fname)
        {
            var results = await _studentDAL.ByFirstName(Fname);
            var readData = _mapper.Map<IEnumerable<StudentDTO>>(results);
            return readData;
        }
        //byLname
        [HttpGet("LName/{Lname}")]
        public async Task<IEnumerable<StudentDTO>> LName(string Lname)
        {
            var results = await _studentDAL.ByLastName(Lname);
            var readData = _mapper.Map<IEnumerable<StudentDTO>>(results);
            return readData;
        }


        //getbyid
        [HttpGet("{id}")]
        public async Task<StudentDTO>Get(int id)
        {
            var result = await _studentDAL.GetById(id);
            var readData = _mapper.Map<StudentDTO>(result);
            return readData;
        }


        //insert data
        [HttpPost]
        public async Task<ActionResult> Post(StudentCreateDTO studentCreateDTO)
        {
            try
            {
                var newStudent = _mapper.Map<Student>(studentCreateDTO);
                var result = await _studentDAL.Insert(newStudent);
                var ReadData = _mapper.Map<StudentDTO>(result);
                return CreatedAtAction("Get", new { Id = result.ID }, ReadData);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        //delete
        [HttpDelete]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                await _studentDAL.DeleteById(id);
                return Ok("Data Berhasil di Hapus");
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        //update
        [HttpPut]
        public async Task<ActionResult> Put(StudentDTO studentDTO)
        {
            try
            {
                var updateUser = _mapper.Map<Student>(studentDTO);
                var result = await _studentDAL.Update(updateUser);
                return Ok(updateUser);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


    }
}
