using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASPNetCoreApplicationPractice.Models
{
    public interface IEmployeeRepository
    {
        Employee GetEmployeeById(int id);
        IEnumerable<Employee> GetAllEmployee();
        Employee AddNewEmployee(Employee employee);
        Employee Update(Employee employeeChanges);
        Employee Delete(int id);
    }
}
