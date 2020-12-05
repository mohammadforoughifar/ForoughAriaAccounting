namespace Backend_Toplearn.Model.Dictionary
{
    public class Procedure
    {
        public static string dbo(string procedure)
        {
            return "[dbo].[Pro_"+procedure+"]";
        }
    }
}