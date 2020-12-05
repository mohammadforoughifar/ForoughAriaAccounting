using Microsoft.AspNetCore.Http;

namespace Backend_Toplearn.Model.BAL
{
    public abstract class Field_List
    {
        public string UserID { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Passwords { get; set; }
        public string CourseId { get; set; }
        public string Title { get; set; }
        public IFormFile ImageUrl { get; set; }
        public string Price { get; set; }
        public string Descriptions { get; set; }
        public string IsActive { get; set; }
        public string CourseTime { get; set; }
        public string RoleID { get; set; }
        public string Roles { get; set; }
        public string  filename { get; set; }
    }
}