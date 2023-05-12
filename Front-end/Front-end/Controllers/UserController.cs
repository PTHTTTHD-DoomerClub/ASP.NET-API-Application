using Microsoft.AspNetCore.Mvc;
using Front_end.Models;
using System.Net;
using System.Net.Http.Headers;
using Microsoft.Net.Http.Headers;
using Microsoft.AspNetCore.Authentication;
using Microsoft.VisualBasic;
using System.Web;
using System.Net.Http;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;
using System.Text;
using Newtonsoft.Json.Linq;
using System.Text.Json.Nodes;
using System.IdentityModel.Tokens.Jwt;

namespace Front_end.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UserController : Controller
    {
        static HttpClient client = new HttpClient();

        public UserController()
        {
            //System.Diagnostics.Debug.WriteLine("An instance of UserController has been created!");
            if (client.BaseAddress == null)
            {
                client.BaseAddress = new Uri("https://localhost:7020/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));
            }
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var accessToken = Request.Headers[HeaderNames.Authorization];
            System.Diagnostics.Debug.WriteLine("Check key: " + accessToken);
            //var accessToken = await HttpContext.GetTokenAsync("access_token");
            return View("RegistUser");
        }

        [HttpGet("Instructor")]
        public async Task<IActionResult> InstructorPage()
        {
            var accessToken = Request.Headers[HeaderNames.Authorization];
            System.Diagnostics.Debug.WriteLine("Check key: " + accessToken);
            //var accessToken = await HttpContext.GetTokenAsync("access_token");
            return View("Instructor");
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromForm] string username, [FromForm] string password)
        {
            HttpResponseMessage response = await client.PostAsJsonAsync("/api/Login/Login", new { username = username, password = password });
            System.Diagnostics.Debug.WriteLine("Check return: " + response.StatusCode + " - " + response.RequestMessage);

            if (response.IsSuccessStatusCode)
            {
                var token = await response.Content.ReadAsAsync<JObject>();
                if (token.TryGetValue("token", out JToken nameToken))
                {
                    System.Diagnostics.Debug.WriteLine("Check result: " + (string)nameToken);
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", (string)nameToken);

                    //Check user type.
                    var handler = new JwtSecurityTokenHandler();
                    var jwtSecurityToken = handler.ReadJwtToken((string)nameToken);
                    //System.Diagnostics.Debug.WriteLine("Check role: " + jwtSecurityToken.Claims.First(x => x.Type == "Role").Value);
                    if (jwtSecurityToken.Claims.First(x => x.Type == "Role").Value == "Instructor")
                    {
                        return View("Instructor");
                    }
                }
            }
            return View("RegistUser");
        }

        [HttpPost("Regist")]
        [ActionName("Regist")]
        public async Task<HttpResponseMessage> RegistUser([FromForm] User value)
        {
            //HttpContext.Request.Form["Username"];
            var accessToken = Request.Headers[HeaderNames.Authorization];
            System.Diagnostics.Debug.WriteLine("Check token: " + accessToken.ToString());
            //var accessToken = await HttpContext.GetTokenAsync("access_token");
            if (value != null)
            {
                System.Diagnostics.Debug.WriteLine("Checking user value: "
                    + value.Email + " - " + value.Username + " - " + value.Password);
                HttpResponseMessage response = await client.PostAsJsonAsync("/api/Login/Regist", value);

                System.Diagnostics.Debug.WriteLine("Check return: " + response.StatusCode + " - " + response.RequestMessage);
                if (response.IsSuccessStatusCode)
                {
                    
                }
                return response;
            }
            else
            {
                return new HttpResponseMessage(HttpStatusCode.BadRequest);
            };
        }
    }
}
