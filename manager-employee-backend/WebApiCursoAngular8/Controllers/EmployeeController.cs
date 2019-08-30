using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using WebApiCursoAngular8.Models;

namespace WebApiCursoAngular8.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : Controller
    {
        private readonly IConfiguration _configuration;

        public EmployeeController(IConfiguration configuration)
        {
            _configuration = configuration;
        }


        [HttpGet]
        public ActionResult<IEnumerable<Employee>> Get()
        {


            DataTable table = new DataTable();

            string query = @"
                               Select EmployeeID, EmployeeName, Department, MailID, DOJ FROM dbo.Employees
                        ";


            using (var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            using (var cmd = new SqlCommand(query, conn))
            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.Text;
                da.Fill(table);
            }

            return Ok(table);
        }

        [HttpPost]
        public async Task<ActionResult> SaveDepartment(Employee emp)
        {

            try
            {
                DataTable table = new DataTable();

                string query = $@"
                               INSERT INTO  dbo.Employees VALUES (
                                    '{ emp.EmployeeName }',
                                    '{ emp.Department}',
                                    '{ emp.MailID }',
                                    '{ emp.DOJ}'
                               )";


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
        public async Task<ActionResult> UpdateEmployee(Employee emp)
        {

            try
            {
                DataTable table = new DataTable();

                string query = $@"
                               UPDATE dbo.Employees SET 
                                    EmployeeName  = '{ emp.EmployeeName }',
                                    Department ='{ emp.Department}',
                                    MailID = '{ emp.MailID }',
                                    DOJ = '{ emp.DOJ}'
                               WHERE EmployeeID = {emp.EmployeeId}";


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
        public async Task<ActionResult> DeleteEmployee(int id)
        {

            try
            {
                DataTable table = new DataTable();

                string query = $@"
                               DELETE FROM dbo.Employees
                               WHERE EmployeeID = {id}";


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