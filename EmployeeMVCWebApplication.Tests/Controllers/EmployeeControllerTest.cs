using System;
using System.Text;
using System.Collections.Generic;
using System.IO;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using EmployeeMVCWebApplication.Controllers;
using EmployeeMVCWebApplication.Models;

namespace EmployeeMVCWebApplication.Tests.Controllers
{
    /// <summary>
    /// Summary description for EmployeeControllerTest
    /// </summary>
    [TestClass]
    public class EmployeeControllerTest
    {        

        public EmployeeControllerTest()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        //
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        //[ClassInitialize()]
        //public static void MyClassInitialize(TestContext testContext) {
        //    testContext.Properties.Add("Controller", new EmployeeController(new EmployeeService.EmployeeService()));

        //    string databaseFilename = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "employeeDatabase.sqlite");
        //    testContext.Properties.Add("DatabaseFilename", databaseFilename);
        //    testContext.Properties.Add("DatabaseConnectionString", "Data Source=" + databaseFilename + ";Version=3;");


        //}
        //
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        //Use TestInitialize to run code before running each test
        [TestInitialize()]
        public void EmployeeControllerTestInitialize() {
            TestContext.Properties.Add("Controller", new EmployeeController(new EmployeeService.EmployeeService()));

            string databaseFilename = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "employeeDatabase.sqlite");
            TestContext.Properties.Add("DatabaseFilename", databaseFilename);
            TestContext.Properties.Add("DatabaseConnectionString", "Data Source=" + databaseFilename + ";Version=3;");
        }
        //
        // Use TestCleanup to run code after each test has run
        [TestCleanup()]
        public void EmployeeControllerTestCleanup() {
            TestContext.Properties.Clear();
        }

        #endregion

        [TestMethod]
        public void Employees()
        {
            // Arrange
            //EmployeeController controller = new EmployeeController(new EmployeeService.EmployeeService());

            // Act
            ViewResult result = (TestContext.Properties["Controller"] as EmployeeController).Employees() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void Index()
        {
            // Arrange
            //EmployeeController controller = new EmployeeController(new EmployeeService.EmployeeService());

            // Act
            ViewResult result = (TestContext.Properties["Controller"] as EmployeeController).Index() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void TestInitializeCleanDatabase()
        {
            bool expectedResult = true;
            bool actualResult = (TestContext.Properties["Controller"] as EmployeeController)
                .InitializeCleanDatabase(TestContext.Properties["DatabaseFilename"].ToString(), 
                    TestContext.Properties["DatabaseConnectionString"].ToString());

            Assert.AreEqual(expected: expectedResult, actual: actualResult);
        }

        [TestMethod]
        public void TestInsertEmployeesDataIntoDatabase()
        {
            IList<EmployeeService.Employee> employees = EmployeeService.EmployeeTestHelper.PrepareTestEmployeeList();

            int expectedNumberInserted = 7;
            int actualNumberInserted = (TestContext.Properties["Controller"] as EmployeeController)
                .InsertEmployeesDataIntoDatabase(TestContext.Properties["DatabaseFilename"].ToString(),
                    TestContext.Properties["DatabaseConnectionString"].ToString(), employees);

            Assert.AreEqual(expected: expectedNumberInserted, actual: actualNumberInserted);
        }

        [TestMethod]
        public void TestGetAllEmployeesFromDatabase()
        {
            IList<EmployeeModel> expectedEmployees = new List<EmployeeModel>();

            expectedEmployees.Add(new EmployeeModel()
            {
                
                Name = "John Smith",
                Birthdate = "12/01/1974",
                Salary = 50000m,
                DateHired = "01/01/2005",
                Status = "valid"
            });

            expectedEmployees.Add(new EmployeeModel()
            {
                Name = "Karl Johnson",
                Birthdate = "03/03/1980",
                Salary = 45000m,
                DateHired = "04/04/2009",
                Status = "valid"
            });

            expectedEmployees.Add(new EmployeeModel()
            {
                Name = "Mark Stowell",
                Birthdate = "01/01/0001",
                Salary = 0m,
                DateHired = "03/31/2001",
                Status = "invalid"
            });

            expectedEmployees.Add(new EmployeeModel()
            {
                Name = "Tony Spronan",
                Birthdate = "09/30/1962",
                Salary = 0m,
                DateHired = "03/31/2000",
                Status = "invalid"
            });

            expectedEmployees.Add(new EmployeeModel()
            {
                Name = "Jan Seveg",
                Birthdate = "06/01/1988",
                Salary = 80000m,
                DateHired = "01/01/0001",
                Status = "invalid"
            });

            expectedEmployees.Add(new EmployeeModel()
            {
                Name = "Mark Twain",
                Birthdate = "07/02/1968",
                Salary = 55532m,
                DateHired = "01/15/2006",
                Status = "valid"
            });

            expectedEmployees.Add(new EmployeeModel()
            {
                Name = "Wendall Smith",
                Birthdate = "08/09/1972",
                Salary = 0m,
                DateHired = "01/01/0001",
                Status = "invalid"
            });

            IList<EmployeeModel> actualEmployees = (TestContext.Properties["Controller"] as EmployeeController)
                .GetAllEmployeesFromDatabase(TestContext.Properties["DatabaseFilename"].ToString(),
                    TestContext.Properties["DatabaseConnectionString"].ToString());

            Assert.AreEqual(expected: expectedEmployees.Count, actual: actualEmployees.Count);

            for (int i = 0; i < expectedEmployees.Count; i++)
            {
                Assert.AreEqual(expected: expectedEmployees[i].Name, actual: actualEmployees[i].Name);
                Assert.AreEqual(expected: expectedEmployees[i].Birthdate, actual: actualEmployees[i].Birthdate);
                Assert.AreEqual(expected: expectedEmployees[i].Salary, actual: actualEmployees[i].Salary);
                Assert.AreEqual(expected: expectedEmployees[i].DateHired, actual: actualEmployees[i].DateHired);
                Assert.AreEqual(expected: expectedEmployees[i].Status, actual: actualEmployees[i].Status);
            }
        }
    }
}
