using BurkWebAPI.Interfaces;
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

        [HttpGet(Name = "GetEmployees")]

        public IActionResult GetEmployees()
        {
            var employees = _employeeRepository.GetEmployees();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(employees);
        }
    }
}
