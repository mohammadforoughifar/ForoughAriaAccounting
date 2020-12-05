using System;
using System.Collections;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Backend_Toplearn.Utility;
using Microsoft.AspNetCore.Mvc;

namespace Backend_Toplearn.Message
{
    public  class SqlMessage
    {
        public string Code { get; set; }
        public string Message { get ; set; }
        public string Message1 { get ; set; }
        public string Message2 { get ; set; }
    }
}