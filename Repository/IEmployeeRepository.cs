using DapperService3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DapperService3.Repository
{
    public interface IEmployeeRepository
    {
        Employee GetEmployeeId(int empId);
        int AddEmployee(Employee employee);
        int UpdateEmployee(Employee employee);
        int DeleteEmployee(int empId);
        List<Employee> GetAllEmployees();
    }
}
