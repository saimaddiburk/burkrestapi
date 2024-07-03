using BurkWebAPI.Models;

namespace BurkWebAPI.Interfaces
{
    public interface IEmployeeRepository
    {
        ICollection<Employee> GetEmployees();
    }
}
