using System.IO;
using System.Threading.Tasks;
using Backend_Toplearn.Model.BAL;
using Backend_Toplearn.Model.DAL;
using Backend_Toplearn.Utility;
using Microsoft.AspNetCore.Mvc;

namespace Backend_Toplearn.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoursesController : ControllerBase
    {
       
        Helper hlp = new Helper();
        Utilities utility = new Utilities();
        Tbl_Courses tblCourses = new Tbl_Courses();
        [HttpGet("[action]")]
        public async Task<IActionResult> Select()
        {
            return new JsonResult(await tblCourses.Select());
        }
        [HttpGet("[action]")]
        public async Task<IActionResult> SelectCourse([FromQuery] Courses.Fields fields)
        {
            return new JsonResult(await tblCourses.SelectCourse(fields));
        }
        [HttpPost("Insert")]
        public async Task<IActionResult> OnInsertAsync([FromForm] Courses.Fields fields)
        {
            string path = Path.Combine( "wwwroot/Course/");
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            string img = utility.Generate_Unique_Number() +fields.ImageUrl.FileName;
            var file = Path.Combine("wwwroot/Course/" + img);
            using (var fileStream = new FileStream(file, FileMode.Create))
            {
                await fields.ImageUrl.CopyToAsync(fileStream);
            }
            //بررسی اینکه از قبل فایل موجود هستش حذف شود.
            //string fullPath = Path.Combine(_environment.ContentRootPath, "wwwroot/Course/" + NamePicture);
            //if (System.IO.File.Exists(fullPath))
            //{
            //    System.IO.File.Delete(fullPath);
            //}
            fields.filename = img;
            await tblCourses.Insert(fields);
            return Ok();
        }
        [HttpPut("Update")]
        public async Task<IActionResult> OnUpdateAsync([FromForm] Courses.Fields fields)
        {
            if (fields.ImageUrl != null)
            {
                string path = Path.Combine("wwwroot/Course/");
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                string img = utility.Generate_Unique_Number() + fields.ImageUrl.FileName;
                var file = Path.Combine("wwwroot/Course/" + img);
                using (var fileStream = new FileStream(file, FileMode.Create))
                {
                    await fields.ImageUrl.CopyToAsync(fileStream);
                }

                //بررسی اینکه از قبل فایل موجود هستش حذف شود.
                //string fullPath = Path.Combine(_environment.ContentRootPath, "wwwroot/Course/" + NamePicture);
                //if (System.IO.File.Exists(fullPath))
                //{
                //    System.IO.File.Delete(fullPath);
                //}
                fields.filename = img;
            }
            else if (fields.ImageUrl==null)
            {
                fields.filename = "";
            }
            await tblCourses.Update(fields);
            return Ok();
        }
        [HttpDelete("[action]")]
        public async Task<IActionResult> Delete([FromQuery] int CourseId)
        {
            await tblCourses.Delete(CourseId);
            return Ok();
        }
    }
}