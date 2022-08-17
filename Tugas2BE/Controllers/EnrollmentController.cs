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
        public async Task<IEnumerable<EnrollmentDetailDTO>> Get()
        {
            var results = await _enrollmentDAL.GetAll();
            // var readData = _mapper.Map<IEnumerable<EnrollmentDetailDTO>>(results);
            List<EnrollmentDetailDTO> readData = new List<EnrollmentDetailDTO>();
           foreach (var item in results)
            {
                readData.Add(new EnrollmentDetailDTO
                {
                    EnrollmentID = item.EnrollmentID,
                    FirstMidName = item.Student.FirstMidName,
                    LastName = item.Student.LastName,
                    Title = item.Course.Title,
                    Credits = item.Course.Credits,
                    EnrollmentDate = item.Student.EnrollmentDate
                });                
            }
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
