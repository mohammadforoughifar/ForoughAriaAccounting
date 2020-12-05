using System.Data;
using System.Threading.Tasks;
using Backend_Toplearn.Config;
using Backend_Toplearn.Config.JWT_Service;
using Backend_Toplearn.Model.BAL;
using Backend_Toplearn.Model.DAL;
using Backend_Toplearn.Model.Dictionary;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace Backend_Toplearn.Controllers
{
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly AppSettings _appSettings;
        private readonly IJwtService jwtService;
        public UserController(IOptions<AppSettings> appSettings, IJwtService jwtService)
        {
            _appSettings = appSettings.Value;
            this.jwtService = jwtService;
        }
        Helper hlp = new Helper();
        Tbl_User tblUser = new Tbl_User();

        [HttpGet("[action]")]
        public async Task<IActionResult> Select()
        {
            return new JsonResult(await tblUser.Select());
        }
        // [PermissionAuthorize(Permissions.User.PostInsert)]
        [HttpPost("[action]")]
        public async Task<IActionResult> Register([FromBody] User.Fields Insert)
        {
            return new JsonResult(await tblUser.Register(Insert));
        }
        [AllowAnonymous]
        [HttpPost("[action]")]
        public async Task<IActionResult> Login([FromBody] User.Fields user)
        {
            DataTable dt = hlp.ToDataTable(await tblUser.Login(user));

            //var tokenHandler = new JwtSecurityTokenHandler();
            //var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            //var claims = new ClaimsIdentity();
            //DataTable dtAccess = hlp.ToDataTable(await tblAccessLevel.Select(user.Email));
            //if (dtAccess.Rows.Count > 0)
            //{
            //    for (int i = 0; i < dtAccess.Rows.Count; i++)
            //    {
            //        ArrayList PermissionList = new ArrayList();
            //        PermissionList.Add(P_AccessLevel.PostShow + dtAccess.Rows[i]["PostShow"].ToString());
            //        PermissionList.Add(P_AccessLevel.PostInsert + dtAccess.Rows[i]["PostInsert"].ToString());
            //        PermissionList.Add(P_AccessLevel.PostUpdate + dtAccess.Rows[i]["PostUpdate"].ToString());
            //        PermissionList.Add(P_AccessLevel.PostDelete + dtAccess.Rows[i]["PostDelete"].ToString());
            //        PermissionList.Add(P_AccessLevel.CourseShow + dtAccess.Rows[i]["CourseShow"].ToString());
            //        PermissionList.Add(P_AccessLevel.CourseInsert + dtAccess.Rows[i]["CourseInsert"].ToString());
            //        PermissionList.Add(P_AccessLevel.CourseUpdate + dtAccess.Rows[i]["CourseUpdate"].ToString());
            //        PermissionList.Add(P_AccessLevel.CourseDelete + dtAccess.Rows[i]["CourseDelete"].ToString());
            //        object[] PermissionObject = PermissionList.ToArray();
            //        foreach (string Permission in PermissionObject)
            //        {
            //            claims.AddClaims(new[]
            //            {
            //                new Claim(Permissions.Permission,Permission),
            //            });
            //        }
            //    }
            //}
            //var tokenDescriptor = new SecurityTokenDescriptor
            //{
            //    Subject = claims,
            //    Expires = DateTime.Now.AddMinutes(5),
            //    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
            //        SecurityAlgorithms.HmacSha256Signature)
            //};
            //var token = tokenHandler.CreateToken(tokenDescriptor);
            var tk = "";
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    user.FullName = dt.Rows[i][Dictionary_Fields.FullName].ToString();
                    user.Roles = dt.Rows[i][Dictionary_Fields.Roles].ToString();
                }
                tk = jwtService.Generate(user);
            }
            else if (dt.Rows.Count == 0)
            {
                tk = "";
            }
            return Content(tk);
        }
    }
}