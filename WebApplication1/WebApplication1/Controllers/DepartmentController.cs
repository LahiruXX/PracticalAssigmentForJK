using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;
using WebApplication1.Models;

namespace WebApplication1.Controllers
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
        [HttpGet]

        public JsonResult Get()
        {
            string query = @"select DepartmentId,DepartmentName from dbo.Department";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("EmployeeAppCon");

            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();

                using(SqlCommand mycommand = new SqlCommand(query, myCon))
                {
                    myReader = mycommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }

            }

            return new JsonResult("Get Successfully");
        }

        [HttpPost]

        public JsonResult Post(Department dep )
        {
            string query = @"insert into dbo.Department
                            values(@DepartmentName)";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("EmployeeAppCon");

            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();

                using(SqlCommand mycommand = new SqlCommand(query, myCon))
                {
                    mycommand.Parameters.AddWithValue("@DepartmentName",dep.DepartmentName);
                    myReader = mycommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }

            }

            return new JsonResult("Add Successfully");
        } 
        
        [HttpPut]

        public JsonResult Put(Department dep )
        {
            string query = @"update dbo.Department
                            set DepartmentName = @DepartmentName
                            where DepartmentId = @DepartmentId";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("EmployeeAppCon");

            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();

                using(SqlCommand mycommand = new SqlCommand(query, myCon))
                {
                    mycommand.Parameters.AddWithValue("@DepartmentId",dep.DepartmentId);
                    mycommand.Parameters.AddWithValue("@DepartmentName",dep.DepartmentName);
                    myReader = mycommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }

            }

            return new JsonResult("Update Successfully");
        }  

        [HttpDelete("{id}")]

        public JsonResult Delete(int id )
        {
            string query = @"delete from dbo.Department                          
                            where DepartmentId = @DepartmentId";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("EmployeeAppCon");

            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();

                using(SqlCommand mycommand = new SqlCommand(query, myCon))
                {
                    mycommand.Parameters.AddWithValue("@DepartmentId",id);

                    myReader = mycommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }

            }

            return new JsonResult("Delete Successfully");
        }
    }
}
