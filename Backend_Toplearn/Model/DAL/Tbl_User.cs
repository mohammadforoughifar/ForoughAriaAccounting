using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Backend_Toplearn.Message;
using Backend_Toplearn.Model.BAL;
using Backend_Toplearn.Model.Dictionary;
using Backend_Toplearn.Utility;
using Microsoft.AspNetCore.Mvc;

namespace Backend_Toplearn.Model.DAL
{
    public class Tbl_User
    {
        Helper hlp = new Helper();
        SqlMessage sqlMessage = new SqlMessage();
        public static string Pro = Procedure.dbo(Dictionary_Fields.User);
        public async Task<IEnumerable<User.Select>> Select()
        {
            List<User.Select> Users = new List<User.Select>();
            SqlCommand commnd = new SqlCommand();
            var dicData = new Dictionary<string, string>();
            dicData[Dictionary_Fields.StatementType] = Dictionary_Fields.Select;
            Users = await hlp.DataReaderMapToList<User.Select>(commnd, Pro, dicData);
            return Users;
        }
        public async Task<IActionResult> Register(User.Fields fieldList)
        {
            SqlCommand commnd = new SqlCommand();
            var dicData = new Dictionary<string, object>();
            dicData[Dictionary_Fields.FullName] = fieldList.FullName;
            dicData[Dictionary_Fields.Email] = fieldList.Email;
            dicData[Dictionary_Fields.Passwords] = fieldList.Passwords;
            dicData[Dictionary_Fields.StatementType] = Dictionary_Fields.Register;
            await hlp.Operations(commnd, Pro, dicData);
            SqlDataReader reader = await commnd.ExecuteReaderAsync();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    sqlMessage.Code = reader.GetString(0);
                    sqlMessage.Message = StringExtensions.Msg(reader.GetString(1));
                }
            }
            else
            {
                Console.WriteLine("رکوردی یافت نشد");
            }
            reader.Close();
            return new JsonResult(sqlMessage);
        }
        public async Task<IEnumerable<User.Logins>> Login(User.Fields user)
        {
            List<User.Logins> Users = new List<User.Logins>();
            SqlCommand commnd = new SqlCommand();
            var dicData = new Dictionary<string, string>();
            dicData[Dictionary_Fields.StatementType] = Dictionary_Fields.Login;
            dicData[Dictionary_Fields.Email] = user.Email;
            dicData[Dictionary_Fields.Passwords] = user.Passwords;
            Users = await hlp.DataReaderMapToList<User.Logins>(commnd, Pro, dicData);
            return Users;
        }
    }
}