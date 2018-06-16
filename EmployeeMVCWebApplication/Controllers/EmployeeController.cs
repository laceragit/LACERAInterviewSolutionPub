using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Web;
using System.Web.Mvc;
using System.Data.SQLite;


using EmployeeMVCWebApplication.Models;

namespace EmployeeMVCWebApplication.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly EmployeeService.IEmployeeService _employeeService;

        public EmployeeController(EmployeeService.IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        // GET: Employee
        public ActionResult Employees()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Employees(EmployeeFileModel model)
        {
            if (ModelState.IsValid)
            {
                IList<EmployeeService.Employee> employees = _employeeService.GenerateEmployeeListFromCSV(stream: model.File.InputStream);

                //Now we need to save to a database...
                string databaseFilename = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "employeeDatabase.sqlite");
                string databaseConnectionString = "Data Source=" + databaseFilename + ";Version=3;";

                //First create the database and table if it does not already exist...
                InitializeCleanDatabase(databaseFilename, databaseConnectionString);

                //Now let's store the employee data into the database...
                InsertEmployeesDataIntoDatabase(databaseFilename, databaseConnectionString, employees);

                //Now, let's redirect to a different action to read from the database and display the result....
                return RedirectToAction("Index");

            }

            return View("Error");
        }

        public ActionResult Index()
        {
            string databaseFilename = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "employeeDatabase.sqlite");
            string databaseConnectionString = "Data Source=" + databaseFilename + ";Version=3;";

            IList<EmployeeModel> employeeModels = GetAllEmployeesFromDatabase(databaseFilename, databaseConnectionString);

            return View("Index", employeeModels);
        }

        [NonAction]
        public bool InitializeCleanDatabase(string databaseFilename, string databaseConnectionString)
        {
            bool result = true;

            if (System.IO.File.Exists(databaseFilename))
            {
                System.IO.File.Delete(databaseFilename);
            }

            //First create the database and table if it does not already exist...
            try
            {
                SQLiteConnection.CreateFile(databaseFilename);

                string sqlCreateTableCommand = @"CREATE TABLE Employee 
                                                (Id integer primary key autoincrement, 
                                                EmployeeName varchar(30), 
                                                Birthdate varchar(12), 
                                                Salary decimal(10,2), 
                                                DateHired varchar(12),
                                                Status varchar(7))";

                using (SQLiteConnection connectionToCreateTable = new SQLiteConnection(databaseConnectionString))
                {
                    SQLiteCommand command = connectionToCreateTable.CreateCommand();
                    command.CommandText = sqlCreateTableCommand;
                    connectionToCreateTable.Open();
                    command.ExecuteNonQuery();
                    connectionToCreateTable.Close();
                }
            }
            catch (Exception)
            {
                result = false;
            }

            return result;
        }

        [NonAction]
        public int InsertEmployeesDataIntoDatabase(string databaseFilename, string databaseConnectionString, IList<EmployeeService.Employee> employees)
        {
            //Now let's store the employee data into the database...

            int numberOfRowsInserted = 0;

            string sqlStringPattern = @"INSERT INTO Employee(EmployeeName, Birthdate, Salary, DateHired, Status) 
                                            VALUES(@EmployeeName, @Birthdate, @Salary, @DateHired, @Status)";
            using (SQLiteConnection connection = new SQLiteConnection(databaseConnectionString))
            {
                SQLiteCommand command = connection.CreateCommand();
                connection.Open();
                foreach (EmployeeService.Employee employee in employees)
                {
                    command.CommandText = sqlStringPattern;

                    command.Parameters.AddWithValue("@EmployeeName", employee.EmployeeName);
                    command.Parameters.AddWithValue("@Birthdate", employee.Birthdate.ToString("MM/dd/yyyy"));
                    command.Parameters.AddWithValue("@Salary", employee.Salary);
                    command.Parameters.AddWithValue("@DateHired", employee.DateHired.ToString("MM/dd/yyyy"));
                    command.Parameters.AddWithValue("@Status", employee.Invalid ? "invalid" : "valid");

                    numberOfRowsInserted += command.ExecuteNonQuery();
                }
                connection.Close();
            }

            return numberOfRowsInserted;
        }

        [NonAction]
        public IList<EmployeeModel> GetAllEmployeesFromDatabase(string databaseFilename, string databaseConnectionString)
        {
            IList<EmployeeModel> employeeModels = new List<EmployeeModel>();

            //We should get the repository...
            

            string sql = "SELECT EmployeeName, Birthdate, Salary, DateHired, Status FROM Employee";
            using (SQLiteConnection connection = new SQLiteConnection(databaseConnectionString))
            {
                SQLiteCommand command = connection.CreateCommand();
                command.CommandText = sql;

                connection.Open();

                using (SQLiteDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        EmployeeModel employeeModel = new EmployeeModel();
                        employeeModel.Name = reader.GetString(0);
                        employeeModel.Birthdate = reader.GetString(1);
                        employeeModel.Salary = reader.GetDecimal(2);
                        employeeModel.DateHired = reader.GetString(3);
                        employeeModel.Status = reader.GetString(4);

                        employeeModels.Add(employeeModel);
                    }

                    reader.Close();
                }

                connection.Close();
            }
            return employeeModels;
        }
    }
}