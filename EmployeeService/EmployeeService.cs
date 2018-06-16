using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.VisualBasic.FileIO;

namespace EmployeeService
{
    public class EmployeeService : IEmployeeService
    {
        public IList<Employee> GenerateEmployeeListFromCSV(Stream stream)
        {
            IList<Employee> employees = new List<Employee>();
            using (TextFieldParser parser = new TextFieldParser(stream, System.Text.Encoding.GetEncoding("iso-8859-1")))
            {
                parser.TextFieldType = FieldType.Delimited;
                parser.SetDelimiters(", ");
                bool headerRowRead = false;
                int idCtr = 1;
                while (!parser.EndOfData)
                {
                    //Processing row
                    string[] fields = parser.ReadFields();

                    if (!headerRowRead)
                    {
                        headerRowRead = true;
                        continue;
                    }

                    DateTime parsedBirthdate = DateTime.MinValue;
                    decimal parsedSalary = 0m;
                    DateTime parsedDateHired = DateTime.MinValue;
                    Employee employee = new Employee();

                    employee.Id = idCtr;

                    //We need to remove the double quotes surrounding the employee name...
                    //\u0093 is the double-quote on the left of the employee name...
                    //\u0094 is the double-quote on the right of the employee name...
                    employee.EmployeeName = fields[0].Replace("\u0093", "").Replace("\u0094", "").Replace("\"", "");

                    employee.Invalid = fields.Length != 4;
                    employee.Invalid = fields.Length < 2 || !DateTime.TryParse(fields[1], out parsedBirthdate) || employee.Invalid;
                    employee.Invalid = fields.Length < 3 || !decimal.TryParse(fields[2], out parsedSalary) || !(parsedSalary > 0.0m) || employee.Invalid;
                    employee.Invalid = fields.Length < 4 || !DateTime.TryParse(fields[3], out parsedDateHired) || employee.Invalid;


                    employee.Birthdate = parsedBirthdate;
                    employee.Salary = parsedSalary;
                    employee.DateHired = parsedDateHired;

                    employees.Add(employee);

                    idCtr++;
                }
            }

            return employees;
        }
    }
}