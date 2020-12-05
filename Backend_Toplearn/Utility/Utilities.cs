using System;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Net;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Xml;

namespace Backend_Toplearn.Utility
{
    public class Utilities
    {
        public static string GetLocalIpAddress()
        {
            string hostName = Dns.GetHostName();
            IPHostEntry ip = Dns.GetHostEntry(hostName);
            string IpAddress = Convert.ToString(ip.AddressList[2]);
            return IpAddress.ToString();
        }
        public bool checkInternetCon()
        {
            try
            {
                using (var client = new WebClient())
                using (client.OpenRead("https://www.google.com/"))
                {
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }
        public void showdate(string date)
        {
            PersianCalendar pc = new PersianCalendar();
            date = pc.GetYear(DateTime.Now).ToString("0000") + "/" + pc.GetMonth(DateTime.Now).ToString("00") + "/" +
            pc.GetDayOfMonth(DateTime.Now).ToString("00");
            return;
        }
        public string miladi2shamsi(DateTime _date)
        {
            //کد مربوط به تاریخ به صورت ماه و روز
            PersianCalendar pc = new PersianCalendar();

            StringBuilder sb = new StringBuilder();

            sb.Append(pc.GetYear(_date).ToString("0000"));

            sb.Append("/");

            sb.Append(pc.GetMonth(_date).ToString("00"));

            sb.Append("/");

            sb.Append(pc.GetDayOfMonth(_date).ToString("00"));

            sb.Append(" امروز :");

            //sb.Append(pc.GetDayOfWeek(_date).ToString());

            string s = pc.GetDayOfWeek(_date).ToString();

            switch (s.ToUpper())
            {

                case "SATURDAY":

                    sb.Append(" شنبه");

                    break;

                case "SUNDAY":

                    sb.Append(" يكشنبه");

                    break;

                case "MONDAY":

                    sb.Append(" دوشنبه");

                    break;
                case "TUESDAY":
                    sb.Append(" سه شنبه");
                    break;

                case "WEDNESDAY":
                    sb.Append(" چهار شنبه");
                    break;
                case "THURSDAY":
                    sb.Append(" بنچ شنبه");
                    break;
                case "FRIDAY":

                    sb.Append(" جمعه");

                    break;

            }

            return sb.ToString();
        }
        [DllImport("wininet.dll")]
        private static extern bool InternetGetConnectedState(out int description, int reservedValue);

        public bool IsConnectedToInternet()
        {
            bool flag;
            int desc;
            flag = InternetGetConnectedState(out desc, 0);
            return flag;
        }


        public string datemiladi(DateTime _Datemildadi)
        {
            DateTime dateTime = DateTime.Now;
            string date = dateTime.Year + "-" + dateTime.Month + "-" + dateTime.Day;
            return date.ToString();
        }
        public string TimeFull(DateTime _time)
        {
            //کد مربوط به زمان کامل به صورت Web Server and TimeZone
            DateTime dateTime;
            //if (IsConnectedToInternet())
            //{
            //    XmlDocument document = new XmlDocument();
            //    document.Load("http://nist.time.gov/actualtime.cgi?lzbc=siqm9b");
            //    if (document.DocumentElement != null)
            //    {
            //        var timeAttribute = document.DocumentElement.GetAttributeNode("time");
            //        if (timeAttribute != null && timeAttribute.Value != null)
            //        {
            //            string time = timeAttribute.Value;
            //            long milliseconds = Convert.ToInt64(time) / 1000;
            //            dateTime = new DateTime(1970, 1, 1).AddMilliseconds(milliseconds).ToLocalTime();
            //            return dateTime.ToString("HH:mm:ss");
            //        }
            //    }
            //}

            var timeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById("Iran Standard Time");
            dateTime = TimeZoneInfo.ConvertTime(DateTime.Now, timeZoneInfo);
            return dateTime.ToString("HH:mm:ss");
        }
        public string dateint(DateTime _dateint)
        {
            DateTime dateTime;
            if (IsConnectedToInternet())
            {
                XmlDocument document = new XmlDocument();
                document.Load("http://nist.time.gov/actualtime.cgi?lzbc=siqm9b");
                if (document.DocumentElement != null)
                {
                    var timeAttribute = document.DocumentElement.GetAttributeNode("time");
                    if (timeAttribute != null && timeAttribute.Value != null)
                    {
                        string time = timeAttribute.Value;
                        long milliseconds = Convert.ToInt64(time) / 1000;
                        dateTime = new DateTime(1970, 1, 1).AddMilliseconds(milliseconds).ToLocalTime();
                        return dateTime.ToString("yyyy-mm-dd");
                    }
                }
            }
            PersianCalendar pc = new PersianCalendar();
            string date = pc.GetYear(DateTime.Now) + "/" + pc.GetMonth(DateTime.Now) + "/" +
                          pc.GetDayOfMonth(DateTime.Now);
            return date.ToString();
            //var timeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById("Iran Standard Time");
            //dateTime = TimeZoneInfo.ConvertTime(DateTime.Now, timeZoneInfo);
            //return dateTime.ToString("Iran Standard Time");
        }
        public string DateAndTimeBackup(DateTime _DateAndTimeBackup)
        {
            DateTime dateTime;
            if (IsConnectedToInternet())
            {
                XmlDocument document = new XmlDocument();
                document.Load("http://nist.time.gov/actualtime.cgi?lzbc=siqm9b");
                if (document.DocumentElement != null)
                {
                    var timeAttribute = document.DocumentElement.GetAttributeNode("time");
                    if (timeAttribute != null && timeAttribute.Value != null)
                    {
                        string time = timeAttribute.Value;
                        long milliseconds = Convert.ToInt64(time) / 1000;
                        dateTime = new DateTime(1970, 1, 1).AddMilliseconds(milliseconds).ToLocalTime();
                        return dateTime.ToString("yyyy-mm-dd HH-mm-ss");
                    }
                }
            }
            PersianCalendar pc = new PersianCalendar();
            string date = pc.GetYear(DateTime.Now) + "/" + pc.GetMonth(DateTime.Now) + "/" + pc.GetDayOfMonth(DateTime.Now) + " " + pc.GetHour(DateTime.Now) + "-" + pc.GetMinute(DateTime.Now) + "-" + pc.GetSecond(DateTime.Now);
            return date.ToString();
            //var timeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById("Iran Standard Time");
            //dateTime = TimeZoneInfo.ConvertTime(DateTime.Now, timeZoneInfo);
            //return dateTime.ToString("yyyy-mm-dd HH-mm-ss");
        }

        private static int hour;
        private static int minute;
        public int Hour
        {
            get { return hour; }
            set { hour = value; }
        }
        public int Minute
        {
            get { return minute; }
            set { minute = value; }
        }
        public static string shamsi(DateTime sdate)
        {
            //تبدیل تاریخ میلادی به شمسی
            try
            {
                //PersianCalendar pc = new PersianCalendar();
                //string year = Convert.ToString(pc.GetYear(sdate));
                //string month = pc.GetMonth(sdate).ToString("00");
                //string day = pc.GetDayOfMonth(sdate).ToString("00");
                //string _hour = pc.GetHour(sdate).ToString("00");
                //string _minute = pc.GetMinute(sdate).ToString("00");
                //string all = year + "/" + month + "/" + day;
                //return all;

                System.Globalization.PersianCalendar shamsi = new System.Globalization.PersianCalendar();
                DateTime sh;
                string strdate = null;
                strdate = DateTime.Now.ToString("yyyy/MM/dd");
                sh = DateTime.Parse(strdate);
                int ysh = shamsi.GetYear(sh);
                int msh = shamsi.GetMonth(sh);
                int dsh = shamsi.GetDayOfMonth(sh);
                string all = ysh + "/" + msh + "/" + dsh;
                return all;
            }
            catch { return ""; }
        }
        public static string Miladi(string year, string month, string day)
        {
            //تبدیل تاریخ شمسی به میلادی
            try
            {
                PersianCalendar pc = new PersianCalendar();
                DateTime dt1 = pc.ToDateTime(int.Parse(year), int.Parse(month), int.Parse(day), 0, 0, 0, 0);
                return dt1.Year.ToString("0000") + "/" + dt1.Month.ToString("00") + "/" + dt1.Day.ToString("00");
            }
            catch
            { return ""; }
        }
        public static bool Valid(string InputDate)
        {
            //بررسی صحت تاریخ وارد شده "این بلاک از تاریخ جاری جلوتر را هم معتبر نمیداند
            string year = InputDate.Substring(0, 4);
            string month = InputDate.Substring(5, 2);
            string day = InputDate.Substring(8, 2);
            //string _hour = InputDate.Substring(11, 2);
            //string _minute = InputDate.Substring(14, 2);
            if (Miladi(year, month, day) != "")
                if (DateTime.Parse(Miladi(year, month, day)) < DateTime.Now)
                    return true;
                else return false;
            else return false;
        }
        public static long DateDiffToMinutes(DateTime d1, DateTime d2)
        {
            //اختلاف بین دو تاریخ به تعداد دقایق
            TimeSpan span = d2.Subtract(d1);
            return Int64.Parse(Math.Round(span.TotalMinutes).ToString());
        }
        public static long DateDiffToDays(DateTime d1, DateTime d2)
        {
            //اختلاف دو تاریخ به روز
            TimeSpan span = d2.Subtract(d1);
            return Int64.Parse(Math.Round(span.TotalDays).ToString());
        }
        public static bool check(string DBName)
        {
            //بررسی اینکه دیتابیس وارد شده آیا در اس کیو ال سرور اتچ هست یا نه
            bool re = false;
            try
            {
                //در صورتی که سرور شما نام دیگری دارد به جای نام سرور زیر وارد کنید
                SqlConnection sq = new SqlConnection("server=.\\SQLExpress;trusted_connection=yes;");
                SqlDataAdapter adapt = new SqlDataAdapter("Exec sp_helpdb", sq);
                DataSet set = new DataSet();
                adapt.Fill(set);
                DataView view = new DataView(set.Tables[0]);
                view.Sort = "name";
                int res = view.Find(DBName);
                if (res >= 0)
                    re = true;
                else if (res == -1)
                    re = false;
            }
            catch
            {
                re = false;
            }
            return re;
        }
        public static void deleteDirectory(string target_dir)
        {
            //حذف فایل ها و پوشه ی وارد شده در قسمت نشانی ورودی متد
            string[] files = Directory.GetFiles(target_dir);
            string[] dirs = Directory.GetDirectories(target_dir);
            foreach (string file in files)
            {
                File.SetAttributes(file, FileAttributes.Normal);
                File.Delete(file);
            }

            foreach (string dir in dirs)
            {
                deleteDirectory(dir);
            }
            Directory.Delete(target_dir, false);
        }
        public string Generate_Unique_Number()
        {
            var bytes = new byte[4];
            var rng = RandomNumberGenerator.Create();
            rng.GetBytes(bytes);
            uint random = BitConverter.ToUInt32(bytes, 0) % 100000000;
            return String.Format("{0:D8}", random);
        }
    }
}