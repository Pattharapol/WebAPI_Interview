using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using System.Data;
using WebAPI_Interview.Models;

namespace WebAPI_Interview.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public EmployeeController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        #region Get Employee
        [HttpGet]
        public JsonResult Get()
        {
            DataTable table = new DataTable();
            string sqlConnection = _configuration.GetConnectionString("EmployeeConn");
            string sql = @"SELECT * FROM mytestdb.employee";

            MySqlDataReader myReader;
            using (MySqlConnection con = new MySqlConnection(sqlConnection))
            {
                con.Open();

                using (MySqlCommand cmd = new MySqlCommand(sql, con))
                {
                    myReader = cmd.ExecuteReader();
                    table.Load(myReader);

                    myReader.Close();
                    con.Close();
                }

            }

            return new JsonResult(table);

        }
        #endregion

        #region Post Employee
        [HttpPost]
        public JsonResult Post(Employee emp)
        {
            DataTable table = new DataTable();
            string sqlConnection = _configuration.GetConnectionString("EmployeeConn");
            string sql = @"INSERT INTO mytestdb.employee (EmployeeName, Department, DateOfJoining) VALUES (@EmployeeName, @Department, @DateOfJoining)";

            MySqlDataReader myReader;
            using (MySqlConnection con = new MySqlConnection(sqlConnection))
            {
                con.Open();

                using (MySqlCommand cmd = new MySqlCommand(sql, con))
                {
                    cmd.Parameters.AddWithValue("@EmployeeName", emp.EmployeeName);
                    cmd.Parameters.AddWithValue("@Department", emp.Department);
                    cmd.Parameters.AddWithValue("@DateOfJoining", emp.DateOfJoining);
                    myReader = cmd.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    con.Close();
                }
            }
            return new JsonResult("Added Successfully");
        }
        #endregion

        #region Put Employee
        [HttpPut]
        public JsonResult Put(Employee emp)
        {
            DataTable table = new DataTable();
            string sqlConnection = _configuration.GetConnectionString("EmployeeConn");
            string sql = @"UPDATE mytestdb.employee SET mytestdb.employee.employeeName = @EmployeeName,  
                                        mytestdb.employee.Department = @Department, 
                                        mytestdb.employee.DateOfJoining = @DateOfJoining 
                                        WHERE mytestdb.employee.EmployeeId = @EmployeeId";

            MySqlDataReader myReader;
            using (MySqlConnection con = new MySqlConnection(sqlConnection))
            {
                con.Open();

                using (MySqlCommand cmd = new MySqlCommand(sql, con))
                {
                    cmd.Parameters.AddWithValue("@EmployeeId", emp.EmployeeId);
                    cmd.Parameters.AddWithValue("@EmployeeName", emp.EmployeeName);
                    cmd.Parameters.AddWithValue("@Department", emp.Department);
                    cmd.Parameters.AddWithValue("@DateOfJoining", emp.DateOfJoining);
                    myReader = cmd.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    con.Close();
                }
            }
            return new JsonResult("Updated Successfully");
        }
        #endregion

        #region Delete Employee
        [HttpDelete("{id}")]
        public JsonResult Delete(int id)
        {
            DataTable table = new DataTable();
            string sqlConnection = _configuration.GetConnectionString("EmployeeConn");
            string sql = @"DELETE FROM mytestdb.Employee WHERE mytestdb.Employee.EmployeeId = @EmployeeID";

            MySqlDataReader myReader;
            using (MySqlConnection con = new MySqlConnection(sqlConnection))
            {
                con.Open();

                using (MySqlCommand cmd = new MySqlCommand(sql, con))
                {
                    cmd.Parameters.AddWithValue("@EmployeeID", id);
                    myReader = cmd.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    con.Close();
                }
            }
            return new JsonResult("Deleted Successfully");
        }
        #endregion
    }
}
