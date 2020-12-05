using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Backend_Toplearn.Model.BAL;
using Backend_Toplearn.Model.Dictionary;
using Backend_Toplearn.Utility;

namespace Backend_Toplearn.Model.DAL
{
    public class Tbl_Courses
    {
        Helper hlp = new Helper();
        public static string Pro = Procedure.dbo(Dictionary_Fields.Courses);
        public async Task<IEnumerable<Courses.Select>> Select()
        {
            List<Courses.Select> course = new List<Courses.Select>();
            SqlCommand commnd = new SqlCommand();
            var dicData = new Dictionary<string, string>();
            dicData[Dictionary_Fields.StatementType] = Dictionary_Fields.Select;
            course = await hlp.DataReaderMapToList<Courses.Select>(commnd, Pro, dicData);
            return course;
        } 
        public async Task<IEnumerable<Courses.Select>> SelectCourse(Courses.Fields fieldList)
        {
            List<Courses.Select> course = new List<Courses.Select>();
            SqlCommand commnd = new SqlCommand();
            var dicData = new Dictionary<string, string>();
            dicData[Dictionary_Fields.CourseId] = fieldList.CourseId;
            dicData[Dictionary_Fields.StatementType] = Dictionary_Fields.SelectCourse;
            course = await hlp.DataReaderMapToList<Courses.Select>(commnd, Pro, dicData);
            return course;
        }
        public async Task Insert(Courses.Fields fieldList)
        {
            SqlCommand commnd = new SqlCommand();
            var dicData = new Dictionary<string, object>();
            dicData[Dictionary_Fields.Title] = fieldList.Title;
            dicData[Dictionary_Fields.ImageUrl] = fieldList.filename;
            dicData[Dictionary_Fields.Price] = StringExtensions.RemovePoint(fieldList.Price);
            dicData[Dictionary_Fields.Descriptions] = fieldList.Descriptions;
            dicData[Dictionary_Fields.CourseTime] = fieldList.CourseTime;
            dicData[Dictionary_Fields.IsActive] = fieldList.IsActive;
            dicData[Dictionary_Fields.StatementType] = Dictionary_Fields.Insert;
            await hlp.Operations(commnd, Pro, dicData);
            await commnd.ExecuteNonQueryAsync();
        }
        public async Task Update(Courses.Fields fieldList)
        {
            SqlCommand commnd = new SqlCommand();
            var dicData = new Dictionary<string, object>();
            dicData[Dictionary_Fields.CourseId] = fieldList.CourseId;
            dicData[Dictionary_Fields.Title] = fieldList.Title;
            dicData[Dictionary_Fields.ImageUrl] = fieldList.filename;
            dicData[Dictionary_Fields.Price] = StringExtensions.RemovePoint(fieldList.Price);
            dicData[Dictionary_Fields.Descriptions] = fieldList.Descriptions;
            dicData[Dictionary_Fields.CourseTime] = fieldList.CourseTime;
            dicData[Dictionary_Fields.IsActive] = fieldList.IsActive;
            dicData[Dictionary_Fields.StatementType] = Dictionary_Fields.Update;
            await hlp.Operations(commnd, Pro, dicData);
            await commnd.ExecuteNonQueryAsync();
        }
        public async Task Delete(int CourseId)
        {
            SqlCommand commnd = new SqlCommand();
            var dicData = new Dictionary<string, object>();
            dicData[Dictionary_Fields.CourseId] = CourseId;
            dicData[Dictionary_Fields.StatementType] = Dictionary_Fields.Delete;
            await hlp.Operations(commnd, Pro, dicData);
            await commnd.ExecuteNonQueryAsync();
        }
    }
}