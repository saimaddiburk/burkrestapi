using BurkWebAPI.Data;
using BurkWebAPI.Dto;
using BurkWebAPI.Interfaces;
using BurkWebAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace BurkWebAPI.Repository
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly DataContext _context;

        public EmployeeRepository(DataContext context)
        {
            _context = context;
        }

        public bool CreateEmployee(EmployeeDto employee)
        {
            string p0 = "CreateEmployee";

            try
            {
                _context.Database.ExecuteSqlInterpolated($@"EXEC {p0}
                    @FirstName={employee.FirstName},
                    @LastName={employee.LastName},
                    @BirthDate={(employee.BirthDate.HasValue ? employee.BirthDate.Value.ToString("yyyyMMdd") : string.Empty)},
                    @HireDate={(employee.HireDate.HasValue ? employee.HireDate.Value.ToString("yyyyMMdd") : string.Empty)},
                    @Email={(!string.IsNullOrEmpty(employee.Email) ? employee.Email : string.Empty)},
                    @PhoneNumber={(!string.IsNullOrEmpty(employee.PhoneNumber) ? employee.PhoneNumber : string.Empty)}");
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        public bool EmployeeExists(int employeeID)
        {
            return _context.Employee.Any(p => p.EmployeeID == employeeID);
        }

        public Employee GetEmployee(int employeeID)
        {
            return _context.Employee.Where(p => p.EmployeeID == employeeID).FirstOrDefault();
        }

        public ICollection<Employee> GetEmployees()
        {
            return [.. _context.Employee.OrderBy(p => p.FirstName)];
        }
    }
}