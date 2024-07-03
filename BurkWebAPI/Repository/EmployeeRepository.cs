using BurkWebAPI.Data;
using BurkWebAPI.Interfaces;
using BurkWebAPI.Models;

namespace BurkWebAPI.Repository
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly DataContext _context;

        public EmployeeRepository(DataContext context)
        {
            _context = context;
        }

        public ICollection<Employee> GetEmployees()
        {
            return [.. _context.Employee.OrderBy(p => p.FirstName)];
        }
    }
}