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

        public bool EmployeeExists(int employeeID)
        {
            return _context.Employee.Any(p => p.EmployeeID == employeeID);
        }

        public ICollection<Employee> GetEmployees()
        {
            return [.. _context.Employee.OrderBy(p => p.FirstName)];
        }

        public Employee GetEmployee(int employeeID)
        {
            return _context.Employee.Where(p => p.EmployeeID == employeeID).FirstOrDefault()!;
        }

        public bool CreateEmployee(EmployeeDto employeeDto)
        {
            string p0 = "CreateEmployee";

            try
            {
                _context.Database.ExecuteSqlInterpolated($@"EXEC {p0}
                    @FirstName={employeeDto.FirstName},
                    @LastName={employeeDto.LastName},
                    @BirthDate={(employeeDto.BirthDate.HasValue ? employeeDto.BirthDate.Value.ToString("yyyyMMdd") : string.Empty)},
                    @HireDate={(employeeDto.HireDate.HasValue ? employeeDto.HireDate.Value.ToString("yyyyMMdd") : string.Empty)},
                    @Email={(!string.IsNullOrEmpty(employeeDto.Email) ? employeeDto.Email : string.Empty)},
                    @PhoneNumber={(!string.IsNullOrEmpty(employeeDto.PhoneNumber) ? employeeDto.PhoneNumber : string.Empty)}");
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        public bool UpdateEmployee(int employeeID, EmployeeDto employeeDto)
        {
            string p0 = "UpdateEmployee";

            try
            {
                _context.Database.ExecuteSqlInterpolated($@"EXEC {p0}
                    @EmployeeID={employeeID},
                    @FirstName={employeeDto.FirstName},
                    @LastName={employeeDto.LastName},
                    @BirthDate={(employeeDto.BirthDate.HasValue ? employeeDto.BirthDate.Value.ToString("yyyyMMdd") : string.Empty)},
                    @HireDate={(employeeDto.HireDate.HasValue ? employeeDto.HireDate.Value.ToString("yyyyMMdd") : string.Empty)},
                    @Email={(!string.IsNullOrEmpty(employeeDto.Email) ? employeeDto.Email : string.Empty)},
                    @PhoneNumber={(!string.IsNullOrEmpty(employeeDto.PhoneNumber) ? employeeDto.PhoneNumber : string.Empty)}");
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        public bool DeleteEmployee(int employeeID)
        {
            string p0 = "DeleteEmployee";

            try
            {
                _context.Database.ExecuteSqlInterpolated($@"EXEC {p0} @EmployeeID={employeeID}");
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }
    }
}