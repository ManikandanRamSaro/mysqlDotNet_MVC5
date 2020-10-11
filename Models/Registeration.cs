using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MysqlConnect.Models
{
    public class Registeration
    {
        public int sno { get; set; }        
        public string userName { get; set; }
        public string empid { get; set; }
        public string mobile { get; set; }
        public string email { get; set; }
        public string dateofJoin { get; set; }
        public string createdDate { get; set; }
        public string statuss { get; set; }
    }
}