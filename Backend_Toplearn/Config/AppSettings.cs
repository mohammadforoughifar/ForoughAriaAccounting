namespace Backend_Toplearn.Config
{
    public class AppSettings
    {
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public int NotBeforeMinutes { get; set; }
        public int ExpirationMinutes { get; set; }
        public string Secret { get; set; }
    }
}