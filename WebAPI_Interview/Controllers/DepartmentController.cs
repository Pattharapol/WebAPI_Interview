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
    public class DepartmentController : ControllerBase
    {

        private readonly IConfiguration _configuration;

        public DepartmentController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        #region Get Department
        [HttpGet]
        public JsonResult Get()
        {
            DataTable table = new DataTable();
            string sqlConnection = _configuration.GetConnectionString("EmployeeConn");
            string sql = @"SELECT DepartmentId, DepartmentName FROM mytestdb.Department";

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

        #region Post Department
        [HttpPost]
        public JsonResult Post(Department dep)
        {
            DataTable table = new DataTable();
            string sqlConnection = _configuration.GetConnectionString("EmployeeConn");
            string sql = @"INSERT INTO mytestdb.Department (DepartmentName) VALUES (@DepartmentName)";

            MySqlDataReader myReader;
            using (MySqlConnection con = new MySqlConnection(sqlConnection))
            {
                con.Open();

                using (MySqlCommand cmd = new MySqlCommand(sql, con))
                {
                    cmd.Parameters.AddWithValue("@DepartmentName", dep.DepartmentName);
                    myReader = cmd.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    con.Close();
                }
            }
            return new JsonResult("Added Successfully");
        }
        #endregion

        #region Put Department
        [HttpPut]
        public JsonResult Put(Department dep)
        {
            DataTable table = new DataTable();
            string sqlConnection = _configuration.GetConnectionString("EmployeeConn");
            string sql = @"UPDATE mytestdb.Department SET mytestdb.Department.DepartmentName = @DepartmentName 
                                        WHERE mytestdb.Department.DepartmentId = @DepartmentId";

            MySqlDataReader myReader;
            using (MySqlConnection con = new MySqlConnection(sqlConnection))
            {
                con.Open();

                using (MySqlCommand cmd = new MySqlCommand(sql, con))
                {
                    cmd.Parameters.AddWithValue("@DepartmentId", dep.DepartmentId);
                    cmd.Parameters.AddWithValue("@DepartmentName", dep.DepartmentName);
                    myReader = cmd.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    con.Close();
                }
            }
            return new JsonResult("Updated Successfully");
        }
        #endregion

        #region Delete Department
        [HttpDelete("{id}")]
        public JsonResult Delete(int id)
        {
            DataTable table = new DataTable();
            string sqlConnection = _configuration.GetConnectionString("EmployeeConn");
            string sql = @"DELETE FROM mytestdb.Department WHERE mytestdb.Department.DepartmentId = @DepartmentId";

            MySqlDataReader myReader;
            using (MySqlConnection con = new MySqlConnection(sqlConnection))
            {
                con.Open();

                using (MySqlCommand cmd = new MySqlCommand(sql, con))
                {
                    cmd.Parameters.AddWithValue("@DepartmentId", id);
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
