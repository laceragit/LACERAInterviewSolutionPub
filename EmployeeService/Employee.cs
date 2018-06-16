using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeService
{
    public class Employee
    {
        public int Id { get; set; }

        public string EmployeeName { get; set; }

        public DateTime Birthdate { get; set; }

        public decimal Salary { get; set; }

        public DateTime DateHired { get; set; }

        public bool Invalid { get; set; }

        public override string ToString()
        {
            return "Id: " + Id
                + "\r\nEmployeeName: " + EmployeeName
                + "\r\nBirthdate:  " + Birthdate.ToString("MM/dd/yyyy")
                + "\r\nSalary:  " + Salary
                + "\r\nDateHired: " + DateHired.ToString("MM/dd/yyyy")
                + "\r\nInvalid: " + (Invalid ? "invalid" : "valid")
                + "\r\n\r\n";
        }
    }
}
