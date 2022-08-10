using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Tugas2BE.DTO;
using Tugas2BE.Helpers;
using Tugas2BE.Interface;

namespace Tugas2BE.DAL
{
    public class UserDAL : IUser
    {
        private readonly UserManager<IdentityUser> _userManager;
        private AppSettings _appSettings;

        public UserDAL(UserManager<IdentityUser> userManager,
            IOptions<AppSettings> options)
        {
            _userManager = userManager;
            _appSettings = options.Value;
        }
        public async Task<UserDTO> Authenticate(string username, string password)
        {
            var currentUser = await _userManager.FindByNameAsync(username);
            var userResult = await _userManager.CheckPasswordAsync(currentUser, password);
            if (!userResult)
                throw new Exception("Authentikasi Gagal");

            var user = new UserDTO
            {
                Username = username
            };
            List<Claim> claims = new List<Claim>();
            //menambhakn informasi user di dalam token
            claims.Add(new Claim(ClaimTypes.Name, user.Username));

            //mambuat token(generate token)
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            user.Token = tokenHandler.WriteToken(token);
            return user;
        }

        public Task<IEnumerable<UserDTO>> GetAll()
        {
            throw new NotImplementedException();
        }

        public async Task Registration(CreateUserDTO user)
        {
            try
            {
                var newUser = new IdentityUser
                {
                    UserName = user.Username,
                    Email = user.Username
                };
                var result = await _userManager.CreateAsync(newUser, user.Password);
                if (!result.Succeeded)
                {
                    //pakai string builder karena tidak ienumarable
                    StringBuilder sb = new StringBuilder();
                    var errors = result.Errors;
                    foreach (var error in errors)
                    {
                        sb.Append($"{error.Code} - {error.Description} \n");
                    }
                    throw new Exception($"Error : {sb.ToString()}");
                }

            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }
    }
}
