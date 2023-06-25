using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using SimpraBitirme.BusinessLayer.Abstract;
using SimpraBitirme.EntityLayer.Concrete;
using SimpraBitirme.EntityLayer.Dto.Authenticate;
using SimpraBitirme.EntityLayer.Dto.User;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using SimpraBitirme.Constant;
using Microsoft.AspNetCore.Authorization;
using Azure;

namespace SimpraBitirme.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IConfiguration _configuration;

        public AuthController(IUserService userService, IConfiguration configuration)
        {
            _userService = userService;
            _configuration = configuration;
        }
        [HttpPost("Admin")]
        public string AdminRegister([FromBody] UserRequest userPostRequest)
        {
            userPostRequest.Role = ApplicationConstants.AdminRole;
            var response = _userService.Add(userPostRequest);
            if (response.Success)
            {
                return "başarılı";
            }
            return response.Message;
        }
        [HttpPost("User")]
        public string UserRegister([FromBody] UserRequest userPostRequest)
        {
            userPostRequest.Role = ApplicationConstants.UserRole;
            var response = _userService.Add(userPostRequest);
            if (response.Success)
            {
                return "başarılı";
            }
            return response.Message;
        }
        [HttpPost("Login")]
        public string Login([FromBody] AuthenticateRequest request)
        {
            var response = _userService.Login(request);
            if (response.Success)
            {
                var kullanici = _userService.GetUserTokenByID(response.Response.Id);
                string token = CreateToken(kullanici);
                return token;
            }
            else
             return response.Message;
        }
        [Authorize(Roles = "A")]

        [HttpDelete("id")]
        public bool DeleteUser(int id)
        {
            var response = _userService.Delete(id);
            return response;
        }

        [Authorize]
        [HttpGet("Id")]
        public UserResponse GetByUserId(int Id)
        {
            var response = _userService.GetByID(Id);
            return response;
        }
        private string CreateToken(UserToken user)
        {
            List<Claim> claims = new List<Claim>();
            claims.Add(new Claim(ClaimTypes.Name, user.Id.ToString()));

            claims.Add(new Claim(ClaimTypes.Role, user.Role));
            
            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(
                _configuration.GetSection("AppSettings:Token").Value));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: creds);

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);
            var identity = new ClaimsIdentity(claims,
                CookieAuthenticationDefaults.AuthenticationScheme);

            HttpContext.SignInAsync(
              CookieAuthenticationDefaults.AuthenticationScheme,
              new ClaimsPrincipal(identity));
            return jwt;
        }
    }
}
