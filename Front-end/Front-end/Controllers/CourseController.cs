using Microsoft.AspNetCore.Mvc;
using Front_end.Models;
//using System.Web.Http;
using System.Net;
using System.Net.Http.Headers;
using Microsoft.Net.Http.Headers;

namespace Front_end.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CourseController : Controller
    {
        static HttpClient client = new HttpClient();
        Course[] courses;

        public CourseController() {
            client.BaseAddress = new Uri("https://localhost:7020/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
        }

        [HttpGet("/Course2")]
        public IEnumerable<Course> GetAllCourses()
        {
            System.Diagnostics.Debug.WriteLine("Get all course!");
            var accessToken = Request.Headers[HeaderNames.Authorization];
            System.Diagnostics.Debug.WriteLine("Check token: " + accessToken);
            return courses;
        }

        /*public Course GetCourseByID(int id)
        {
            
            var product = courses.FirstOrDefault((p) => p.CourseID == id);
            if (product == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            return product;
        }*/
        [HttpGet("/Course")]
        //public async Task<Course> GetCourseByPageAsync()
        public async Task<ActionResult> GetCourseByPageAsync()
        {
            var accessToken = (string)Request.Headers[HeaderNames.Authorization];
            System.Diagnostics.Debug.WriteLine("Check token: " + accessToken);
            if (accessToken != null)
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken.Replace("Bearer ", ""));
            //Course course = null;
            IEnumerable<Course>? courses = null;
            HttpResponseMessage response = await client.GetAsync("/api/Course/");

            System.Diagnostics.Debug.WriteLine("Check return: " + response.StatusCode + " - " + response.RequestMessage);
            if (response.IsSuccessStatusCode)
            {
                courses = response.Content.ReadAsAsync<IEnumerable<Course>>().Result;
                //course = response.Content.ReadAsAsync<Course>().Result;
                foreach(var c in courses)
                {
                   System.Diagnostics.Debug.WriteLine("Check result: " + c.Title);
                }
            }
            //View();
            ViewData["Courses"] = courses;
            return View("Course");
        }

        //[HttpGet]
        public IEnumerable<Course> GetCoursesFromPage(int pageNumber)
        {
            return courses/*.Where(
                (p) => string.Equals(p.Category, category,
                    StringComparison.OrdinalIgnoreCase));*/;
        }

        //[HttpGet]
        public async Task<IActionResult> Index()
        {
            var accessToken = Request.Headers[HeaderNames.Authorization];
            System.Diagnostics.Debug.WriteLine("Check key: " + accessToken);
            //var accessToken = await HttpContext.GetTokenAsync("access_token");
            return View();
        }
    }
}
