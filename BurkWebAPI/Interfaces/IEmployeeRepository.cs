using BurkWebAPI.Dto;
using BurkWebAPI.Models;

namespace BurkWebAPI.Interfaces
{
    public interface IEmployeeRepository
    {
        ICollection<Employee> GetEmployees();

        Employee GetEmployee(int employeeID);

        bool EmployeeExists(int employeeID);

        bool CreateEmployee(EmployeeDto employeeDto);

        bool UpdateEmployee(int employeeID, EmployeeDto employeeDto);

        bool DeleteEmployee(int employeeID);
    }
}