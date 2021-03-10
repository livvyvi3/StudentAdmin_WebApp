using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudentAdmin_WebApp.Models
{
    public class Student
    {
        //public int student_id { get; set; }
        public string student_name { get; set; }
        public string specialization { get; set; }
        public string qualification { get; set; }
        public int courses { get; set; }
    }
}