namespace Backend_Toplearn.Model.BAL
{
    public class User
    {
        public class Fields : Field_List
        {

        }
        public class Select
        {
            public int UserID { get; set; }
            public string FullName { get; set; }
            public string Email { get; set; }
        }
        public class Logins
        {
            public int counts { get; set; }
            public string FullName { get; set; }
            public string Roles { get; set; }
        }
    }
}