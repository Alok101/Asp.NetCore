using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASPNetCoreApplicationPractice.Models
{
    public class BOEmployeeRepository : IEmployeeRepository
    {
        private List<Employee> listEmployee;
        public BOEmployeeRepository()
        {
            listEmployee = new List<Employee>()
            {
                new Employee() { Id = 8, Name = "Alok8 Gautam", Address = "India8", Department = Department.None, Salary = 7200000 },
                new Employee() { Id = 1, Name = "Alok1 Gautam", Address = "India1", Department = Department.BSC, Salary = 1200000 },
                new Employee() { Id = 2, Name = "Alok2 Gautam", Address = "India2", Department = Department.CE, Salary = 2200000 },
                new Employee() { Id = 3, Name = "Alok3 Gautam", Address = "India3", Department = Department.CSE, Salary = 3200000 },
                new Employee() { Id = 4, Name = "Alok4 Gautam", Address = "India4", Department = Department.ECE, Salary = 4200000 },
                new Employee() { Id = 5, Name = "Alok5 Gautam", Address = "India5", Department = Department.EEE, Salary = 5200000 },
                new Employee() { Id = 6, Name = "Alok6 Gautam", Address = "India6", Department = Department.ME, Salary = 6200000 },
                new Employee() { Id = 7, Name = "Alok7 Gautam", Address = "India7", Department = Department.MSC, Salary = 7200000 }
            };
        }
        public Employee GetEmployeeById(int id)
        {
            return listEmployee.FirstOrDefault(x => x.Id == id);
        }
        public IEnumerable<Employee> GetAllEmployee()
        {
            return listEmployee;
        }

        public Employee AddNewEmployee(Employee employee)
        {
            employee.Id = listEmployee.Max(x => x.Id) + 1;
            listEmployee.Add(employee);
            return employee;
        }

        public Employee Update(Employee employeeChanges)
        {
            Employee employee = listEmployee.FirstOrDefault(x => x.Id == employeeChanges.Id);
            if (employee != null)
            {
                employee.Name = employeeChanges.Name;
                employee.Salary = employeeChanges.Salary;
                employee.Department = employeeChanges.Department;
                employee.Address = employeeChanges.Address;
            }
            return employee;
        }

        public Employee Delete(int id)
        {
            Employee employee=listEmployee.FirstOrDefault(x => x.Id==id);
            if (employee != null)
            {
                listEmployee.Remove(employee);
            }
            return employee;
        }
    }
}
