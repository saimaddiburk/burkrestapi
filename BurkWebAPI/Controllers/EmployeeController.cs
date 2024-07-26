using BurkWebAPI.Dto;
using BurkWebAPI.Interfaces;
using BurkWebAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace BurkWebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeeController : Controller
    {
        private readonly IEmployeeRepository _employeeRepository;

        public EmployeeController(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        [HttpGet("GetEmployees")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Employee>))]
        public IActionResult GetEmployees()
        {
            var employees = _employeeRepository.GetEmployees();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(employees);
        }

        [HttpGet("GetEmployee/{employeeID}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Employee>))]
        [ProducesResponseType(400)]
        public IActionResult GetEmployee(int employeeID)
        {
            if (!_employeeRepository.EmployeeExists(employeeID))
            {
                return NotFound();
            }

            var employees = _employeeRepository.GetEmployee(employeeID);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(employees);
        }

        [HttpPost("CreateEmployee")]
        public IActionResult CreateEmployee([FromBody] EmployeeDto employeeDto)
        {
            if (_employeeRepository.CreateEmployee(employeeDto))
            {
                return Ok("Employee created successfully");
            }
            else
            {
                return BadRequest("Failed to create employee");
            }
        }

        [HttpPut("UpdateEmployee/{employeeID}")]
        public IActionResult UpdateEmployee(int employeeID, [FromBody]EmployeeDto employeeDto)
        {
            if (_employeeRepository.UpdateEmployee(employeeID, employeeDto))
            {
                return Ok("Employee updated successfully");
            }
            else
            {
                return BadRequest("Failed to update employee");
            }
        }

        [HttpDelete("DeleteEmployee/{employeeID}")]
        public IActionResult DeleteEmployee(int employeeID)
        {
            if (_employeeRepository.DeleteEmployee(employeeID))
            {
                return Ok("Employee deleted successfully");
            }
            else
            {
                return BadRequest("Failed to delete employee");
            }
        }
    }
}