using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeService
{
    public interface IEmployeeService
    {
        IList<Employee> GenerateEmployeeListFromCSV(Stream stream);
    }
}
