using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Tugas2BE.DTO;
using Tugas2BE.Interface;

namespace Tugas2BE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private IUser _userDAL;

        public UserController(IUser user)
        {
            _userDAL = user;
        }

        //registration
        [AllowAnonymous]
        [HttpPost]
        public async Task<ActionResult> Registration(CreateUserDTO createUserDTO)
        {
            try
            {
                await _userDAL.Registration(createUserDTO);
                return Ok($"Registrasi  {createUserDTO.Username} Berhasil");
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        //login
        [AllowAnonymous]
        [HttpPost("Login")]
        public async Task<ActionResult<UserDTO>> Authenticate(CreateUserDTO createUserDTO)
        {
            try
            {
                var user = await _userDAL.Authenticate(createUserDTO.Username, createUserDTO.Password);
                if (user == null)
                    return BadRequest("Username or Password Wrong!");

                return Ok(user);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

    }
}
