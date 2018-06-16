using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace EmployeeMVCWebApplication.Models
{
    public class EmployeeModel
    {
        public string Name { get; set; }

        public string Birthdate { get; set; }

        public decimal Salary { get; set; }

        public string DateHired { get; set; }

        public string Status { get; set; }
    }
}