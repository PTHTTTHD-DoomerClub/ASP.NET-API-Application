using Azure.Identity;
using Backend_APIs.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Net.Http.Headers;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;
using JwtRegisteredClaimNames = System.IdentityModel.Tokens.Jwt.JwtRegisteredClaimNames;

namespace Backend_APIs.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : Controller
    {
        private IConfiguration _config;
        private readonly ModelContext _context;
        // GET: LoginController
        public LoginController(IConfiguration config, ModelContext context)
        {
            _context = context;
            _config = config;
        }

        [HttpPost("Login")]
        [ActionName("Login")]
        public IActionResult Login([FromBody] Dictionary<string, string> data)
        {
            UserModel login = new UserModel();
            login.Username = data["username"];
            login.Password = data["password"];
            login.Role = "User";
            IActionResult response = Unauthorized();

            var user = AuthenticateUser(login);
            if (user != null)
            {
                var tokenStr = GenerateJSONWebToken(user);
                response = Ok(new { token = tokenStr });
            }
            return response;
        }

        //[Authorize]
        [HttpPost("Regist")]
        [ActionName("Regist")]
        public HttpResponseMessage RegistUser(UserModel value)
        {
            //var accessToken = Request.Headers[HeaderNames.Authorization];
            //System.Diagnostics.Debug.WriteLine("Check key: " + accessToken);
            //System.Diagnostics.Debug.WriteLine("Someone is registing!");
            /*var token = "[encoded jwt]";
            var handler = new JwtSecurityTokenHandler();
            var jwtSecurityToken = handler.ReadJwtToken(token);*/
            if (value != null)
            {
                System.Diagnostics.Debug.WriteLine("Checking user value: "
                    + value.Email + " - " + value.Username + " - " + value.Password);
                try
                {
                    value.Role = "User";
                    _context.Users.Add(value);
                    var result = _context.SaveChanges();
                    var response = new HttpResponseMessage(HttpStatusCode.Created)
                    {
                        Content = new StringContent("Success code")
                    };
                    return response;
                }
                catch (DataException /* dex */)
                {
                    //Log the error (uncomment dex variable name and add a line here to write a log.
                    ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
                    return new HttpResponseMessage(HttpStatusCode.BadRequest);
                }
            }
            else
            {
                return new HttpResponseMessage(HttpStatusCode.BadRequest);
            };
        }

        private UserModel AuthenticateUser(UserModel login)
        {
            var user = _context.Users.Where(x => x.Username == login.Username && x.Password == login.Password).FirstOrDefault();
            return user;
        }
        
        private string GenerateJSONWebToken(UserModel userinfo)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            //System.Diagnostics.Debug.WriteLine("Check key: " + securityKey.);
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, userinfo.Username),
                new Claim(JwtRegisteredClaimNames.Email, userinfo.Email),
                //new Claim("Role", "User"),
                new Claim("Role", userinfo.Role),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Issuer"],
                claims,
                expires: DateTime.Now.AddMinutes(120),
                signingCredentials: credentials);

            var encodetoken = new JwtSecurityTokenHandler().WriteToken(token);
            return encodetoken;
        }
    }
}
