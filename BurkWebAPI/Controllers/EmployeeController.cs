using BurkWebAPI.Dto;
using BurkWebAPI.Interfaces;
using BurkWebAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace BurkWebAPI.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class EmployeeController : Controller
    {
        private readonly IEmployeeRepository _employeeRepository;

        public EmployeeController(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Employee>))]
        public IActionResult GetEmployees()
        {
            var employees = _employeeRepository.GetEmployees();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(employees);
        }

        [HttpGet("{employeeID}")]
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

        [HttpPost]
        public IActionResult CreateEmployee([FromBody] EmployeeDto employee)
        {
            if (_employeeRepository.CreateEmployee(employee))
            {
                return Ok("Employee created successfully");
            }
            else
            {
                return BadRequest("Failed to create employee");
            }
        }
    }
}