using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Protocols;
using WebApiCursoAngular8.Models;

namespace WebApiCursoAngular8.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : Controller
    {
        private readonly IConfiguration _configuration;

        public DepartmentController(IConfiguration configuration)
        {
            _configuration = configuration;
        }


        [HttpGet]
        public ActionResult<IEnumerable<Department>> Get()
        {


            DataTable table = new DataTable();

            string query = @"
                               Select DepartmentID, DepartmentName FROM dbo.Departments
                        ";


            using(var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            using(var cmd = new SqlCommand(query, conn))             
            using(var da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.Text;
                da.Fill(table);
            }

            return Ok(table);
        }

        [HttpPost]
        public async Task<ActionResult> SaveDepartment(Department dep)
        {

            try
            {
                DataTable table = new DataTable();

                string query = $@"
                               INSERT INTO  dbo.Departments VALUES ('{ dep.DepartmentName }')";


                using (var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                using (var cmd = new SqlCommand(query, conn))
                using (var da = new SqlDataAdapter(cmd))
                {
                    cmd.CommandType = CommandType.Text;
                    da.Fill(table);
                }

                return Ok(Json("added Successfully"));
            }
            catch (Exception ex)
            {

                return BadRequest("ops, algo inesperado aconteceu");
            }
            
        }

        [HttpPut]
        public async Task<ActionResult> UpdateDepartment(Department dep)
        {

            try
            {
                DataTable table = new DataTable();

                string query = $@"
                               UPDATE dbo.Departments SET 
                               departmentName = '{ dep.DepartmentName }' 
                               WHERE departmentID = {dep.DepartmentID}";


                using (var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                using (var cmd = new SqlCommand(query, conn))
                using (var da = new SqlDataAdapter(cmd))
                {
                    cmd.CommandType = CommandType.Text;
                    da.Fill(table);
                }

                return Ok(Json("updated Successfully"));
            }
            catch (Exception ex)
            {

                return BadRequest("ops, algo inesperado aconteceu");
            }

        }


        [HttpDelete]
        public async Task<ActionResult> DeleteDepartment(int id)
        {

            try
            {
                DataTable table = new DataTable();

                string query = $@"
                               DELETE FROM dbo.Departments
                               WHERE DepartmentID = {id}";


                using (var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                using (var cmd = new SqlCommand(query, conn))
                using (var da = new SqlDataAdapter(cmd))
                {
                    cmd.CommandType = CommandType.Text;
                    da.Fill(table);
                }

                return Ok(Json("deleted Successfully"));
            }
            catch (Exception ex)
            {

                return BadRequest("ops, algo inesperado aconteceu");
            }

        }

    }
}