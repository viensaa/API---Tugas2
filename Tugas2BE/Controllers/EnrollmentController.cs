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
    public class EnrollmentController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IEnrollment _enrollmentDAL;

        public EnrollmentController(IMapper mapper,IEnrollment enrollment)
        {
            _mapper = mapper;
            _enrollmentDAL = enrollment;

        }
        //get All
        [HttpGet]
        public async Task<IEnumerable<EnrollmentDTO>> Get()
        {
            var results = await _enrollmentDAL.GetAll();
            var readData = _mapper.Map<IEnumerable<EnrollmentDTO>>(results);
            return readData;
        }
        //insert
        [HttpPost]
        public async Task<ActionResult> Post(EnrollmentCreateDTO enrollmentCreateDTO)
        {
            try
            {
                var newEnrollment = _mapper.Map<Enrollment>(enrollmentCreateDTO);
                var result = await _enrollmentDAL.Insert(newEnrollment);
                var readData = _mapper.Map<EnrollmentDTO>(result);
                return CreatedAtAction("Get", new { Id = result.EnrollmentID }, readData);
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
                await _enrollmentDAL.DeleteById(id);
                return Ok("Data Berhasil di Hapus");
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }
        [HttpGet("{id}")]
        public async Task<EnrollmentDTO> ById(int id)
        {
            var result = await _enrollmentDAL.GetById(id);
            var readData = _mapper.Map<EnrollmentDTO>(result);
            return readData;
        }
        //update
        [HttpPut]
        public async Task<ActionResult> Put(EnrollmentDTO courseDTO)
        {
            try
            {
                var updateUser = _mapper.Map<Enrollment>(courseDTO);
                var result = await _enrollmentDAL.Update(updateUser);
                return Ok(updateUser);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

    }
}
