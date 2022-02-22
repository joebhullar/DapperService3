using Dapper;
using DapperService3.Models;
using DapperService3.Repository;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace DapperService3.DAL
{
    public class EmployeeDAL: IEmployeeRepository
    {
        private readonly IConfiguration configuration;
        public IDbConnection connection
        {
            get
            {
                return new SqlConnection(configuration.GetConnectionString("MyConnectionString"));
            }
        }

        public EmployeeDAL(IConfiguration _configuration)
        {
            this.configuration = _configuration;
        }

        public int AddEmployee(Employee employee)
        {
            int rowAffected = 0;
            using (IDbConnection con = connection)
            {
                con.Open();
                var param = new DynamicParameters();
                param.Add("@EmpName", employee.EmpName);
                param.Add("@EmpAge", employee.EmpAge);
                param.Add("@EmpGender", employee.EmpGender);
                param.Add("@EmpAddress", employee.EmpAddress);
                param.Add("@EmpContactNo", employee.EmpContactNo);
                rowAffected = con.Execute("AddNewEmpDetails", param, commandType: CommandType.StoredProcedure);
                con.Close();
            }
            return rowAffected;
        }

        public int DeleteEmployee(int empId)
        {
            int rowAffected = 0;
            using (IDbConnection con = connection)
            {
                con.Open();
                var param = new DynamicParameters();
                param.Add("@EmpId", empId);
                rowAffected = con.Execute("DeleteEmpDetails", param, commandType: CommandType.StoredProcedure);
                con.Close();
            }
            return rowAffected;
        }

        public List<Employee> GetAllEmployees()
        {
            using (IDbConnection con = connection)
            {
                string query = "select * from employee";
                con.Open();
                var result = con.Query<Employee>(query);
                // con.Close();
                return result.ToList<Employee>();

            }
        }

        public Employee GetEmployeeId(int empId)
        {
            using (IDbConnection con = connection)
            {
                string query = "SELECT EmpId, EmpName, EmpAge, EmpGender, EmpAddress, EmpContactNo from Employee where EmpId=@EmpId";
                con.Open();
                var result = con.QueryFirst<Employee>(query, new { EmpId = empId });
                return result;
            }
        }
        public int UpdateEmployee(Employee employee)
        {
            int rowAffected = 0;
            using (IDbConnection con = connection)
            {
                con.Open();
                var param = new DynamicParameters();
                param.Add("@EmpId", employee.EmpId);
                param.Add("@EmpName", employee.EmpName);
                param.Add("@EmpAge", employee.EmpAge);
                param.Add("@EmpGender", employee.EmpGender);
                param.Add("@EmpAddress", employee.EmpAddress);
                param.Add("@EmpContactNo", employee.EmpContactNo);
                rowAffected = con.Execute("UpdateEmpDetails", param, commandType: CommandType.StoredProcedure);
                con.Close();
            }
            return rowAffected;
        }



    }
}
