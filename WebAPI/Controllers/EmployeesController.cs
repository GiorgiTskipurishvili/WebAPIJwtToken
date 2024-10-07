using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Model;
using WebAPI.Package;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        [Authorize]
        [HttpGet]
        public List<Employee> GetEmployee()
        {
            PKG_EMP package = new PKG_EMP();
            List<Employee> list_employees = new List<Employee>();
            list_employees = package.get_employees();
            return list_employees;
        }


        [HttpPost]
        public void SaveEmployee(Employee employee)
        {
            PKG_EMP package = new PKG_EMP();
            package.save_employee(employee);
        }


        [HttpPut]
        public void UpdateEmployee(Employee employee)
        {
            PKG_EMP package = new PKG_EMP();
            package.update_employee(employee);
        }
        [HttpPut("{id}")]
        public IActionResult UpdateEmployeeById(int id, [FromBody] Employee employee)
        {
            if (id != employee.ID)
            {
                return BadRequest("Employee ID mismatch");
            }

            try
            {
                PKG_EMP package = new PKG_EMP();
                package.update_employee(employee);

                return Ok("Employee updated successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }




        //[HttpDelete]
        //public void DeleteEmployee(Employee employee) 
        //{
        //    PKG_EMP package = new PKG_EMP();
        //    package.delete_employee(employee);
        //}

        [HttpDelete("{id}")]
        public void DeleteEmployee(int id)
        {
            PKG_EMP package = new PKG_EMP();
            Employee employee = new Employee();
            employee.ID = id;
            package.delete_employee(employee);
        }


        [HttpGet("{id}")]
        public Employee GetEmployeeById(int id)
        {
            PKG_EMP package = new PKG_EMP();
            Employee employee = new Employee();
            employee.ID = id;
            return package.get_emp_by_id(employee);
        }

    }
}
