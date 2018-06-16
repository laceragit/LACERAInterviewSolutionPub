using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using EmployeeService;

namespace EmployeeService.Test
{
    [TestClass]
    public class EmployeeServiceTest
    {
        [TestMethod]
        public void TestReadEmployeeCSVFile()
        {
            //Control test...
            IList<Employee> expectedEmployees = EmployeeTestHelper.PrepareTestEmployeeList();

            IEmployeeService service = new EmployeeService();
            IList<Employee> actualEmployees = new List<Employee>();

            using (FileStream stream = File.Open(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "employees.csv"), FileMode.Open))
            {
                actualEmployees = service.GenerateEmployeeListFromCSV(
                    stream: stream);
            }

            Assert.AreEqual(expected: expectedEmployees.Count, actual: actualEmployees.Count);

            for(int i = 0; i < expectedEmployees.Count; i++)
            {
                Assert.AreEqual(expected: expectedEmployees[i].Id, actual: actualEmployees[i].Id);
                Assert.AreEqual(expected: expectedEmployees[i].EmployeeName, actual: actualEmployees[i].EmployeeName);
                Assert.AreEqual(expected: expectedEmployees[i].Birthdate, actual: actualEmployees[i].Birthdate);
                Assert.AreEqual(expected: expectedEmployees[i].Salary, actual: actualEmployees[i].Salary);
                Assert.AreEqual(expected: expectedEmployees[i].DateHired, actual: actualEmployees[i].DateHired);
                Assert.AreEqual(expected: expectedEmployees[i].Invalid, actual: actualEmployees[i].Invalid);
            }

        }
    }
}
