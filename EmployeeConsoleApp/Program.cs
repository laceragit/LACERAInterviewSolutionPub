using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using EmployeeService;

namespace EmployeeConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            EmployeeService.IEmployeeService employeeService = new EmployeeService.EmployeeService();
            IList<EmployeeService.Employee> employees = new List<Employee>();

            using (FileStream stream = File.Open(args[0], FileMode.Open))
            {
                employees = employeeService.GenerateEmployeeListFromCSV(stream: stream);
            }
                
            foreach (Employee employee in employees)
            {
                Console.WriteLine(employee.ToString());
            }

            Console.ReadLine();
        }
    }
}
