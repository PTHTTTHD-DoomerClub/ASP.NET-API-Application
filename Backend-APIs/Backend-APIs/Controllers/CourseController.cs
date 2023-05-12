using Microsoft.AspNetCore.Mvc;
using Backend_APIs.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Net.Http.Headers;
using Microsoft.IdentityModel.Tokens;
using System.Net;
using Backend_APIs.Helpers;
using Newtonsoft.Json.Linq;
using System.Reflection;
using System.Data.Entity;
using System.Linq;

namespace Backend_APIs.Controllers
{
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CourseController : Controller
    {
        private readonly ModelContext _context;
        public CourseController(ModelContext context)
        {
            _context = context;
        }
        [HttpGet]
        public IEnumerable<CourseModel> Get()
        {
            var accessToken = Request.Headers[HeaderNames.Authorization];
            System.Diagnostics.Debug.WriteLine("Check key: " + accessToken);
            var position = 0;
            return _context.Courses.Skip(position).ToList();
        }

        [HttpGet("test")]
        public async Task<ActionResult> Upload(string folder, string fileName)
        {
            string path = Path.Combine(@"E:\\Gits\\PTHTTTHD\\ASP.NET-API-Application\\Backend-APIs\\Backend-APIs", @"Storage\ro.png");
            var fileUrl = await DropboxAPI.UploadFileAsync(folder, fileName, path);
            ViewBag.FileUrl = fileUrl;
            System.Diagnostics.Debug.WriteLine("Check URL of posted file: " + fileUrl);

            //Delete file from local storage after upload.
            //System.IO.File.Delete(path);
            return Ok(new { });
        }

        [HttpPost("CreateCourse")]
        public async Task<ActionResult> AddNewCourse(CourseModel course)
        {
            //var accessToken = Request.Headers[HeaderNames.Authorization];
            //System.Diagnostics.Debug.WriteLine("Check key: " + accessToken);
            _context.Courses.Add(course);
            var state = await _context.SaveChangesAsync();
            if (state > 0)
            {
                return CreatedAtAction(nameof(course), new { id = course.CourseID }, course);
            }
            return BadRequest();
        }

        [HttpGet("TestGetCourses")]
        public async Task<ActionResult> GetTest()
        {
            //var accessToken = Request.Headers[HeaderNames.Authorization];
            //System.Diagnostics.Debug.WriteLine("Check key: " + accessToken);
            var preRequires = _context.Courses.Where(c => c.CourseID == 2).FirstOrDefault();
            //_context.Entry(preRequires).Reference(c => c.PrerequisiteCourses).Load();
            _context.Entry(preRequires).Collection(c => c.PrerequisiteCourses).Load();
            return  Ok(new {preRequires });
        }
    }
}
