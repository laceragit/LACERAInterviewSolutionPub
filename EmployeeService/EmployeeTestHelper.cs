using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeService
{
    public static class EmployeeTestHelper
    {
        public static IList<Employee> PrepareTestEmployeeList()
        {
            IList<Employee> testEmployees = new List<Employee>();

            testEmployees.Add(new Employee()
            {
                Id = 1,
                EmployeeName = "John Smith",
                Birthdate = new DateTime(year: 1974, month: 12, day: 1),
                Salary = 50000m,
                DateHired = new DateTime(year: 2005, month: 1, day: 1),
                Invalid = false
            });

            testEmployees.Add(new Employee()
            {
                Id = 2,
                EmployeeName = "Karl Johnson",
                Birthdate = new DateTime(year: 1980, month: 3, day: 3),
                Salary = 45000m,
                DateHired = new DateTime(year: 2009, month: 4, day: 4),
                Invalid = false
            });

            testEmployees.Add(new Employee()
            {
                Id = 3,
                EmployeeName = "Mark Stowell",
                Birthdate = DateTime.MinValue,
                Salary = 0m,
                DateHired = new DateTime(year: 2001, month: 3, day: 31),
                Invalid = true
            });

            testEmployees.Add(new Employee()
            {
                Id = 4,
                EmployeeName = "Tony Spronan",
                Birthdate = new DateTime(year: 1962, month: 9, day: 30),
                Salary = 0m,
                DateHired = new DateTime(year: 2000, month: 3, day: 31),
                Invalid = true
            });

            testEmployees.Add(new Employee()
            {
                Id = 5,
                EmployeeName = "Jan Seveg",
                Birthdate = new DateTime(year: 1988, month: 6, day: 1),
                Salary = 80000m,
                DateHired = DateTime.MinValue,
                Invalid = true
            });

            testEmployees.Add(new Employee()
            {
                Id = 6,
                EmployeeName = "Mark Twain",
                Birthdate = new DateTime(year: 1968, month: 7, day: 2),
                Salary = 55532m,
                DateHired = new DateTime(year: 2006, month: 1, day: 15),
                Invalid = false
            });

            testEmployees.Add(new Employee()
            {
                Id = 7,
                EmployeeName = "Wendall Smith",
                Birthdate = new DateTime(year: 1972, month: 8, day: 9),
                Salary = 0m,
                DateHired = DateTime.MinValue,
                Invalid = true
            });

            return testEmployees;
        }
    }
}
