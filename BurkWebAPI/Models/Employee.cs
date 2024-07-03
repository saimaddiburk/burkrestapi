namespace BurkWebAPI.Models
{
    public class Employee
    {
        public int EmployeeID { get; set; }

        public required string FirstName { get; set; }

        public required string LastName { get; set; }

        public DateTime? BirthDate { get; set; }

        public DateTime? HireDate { get; set; }

        public string? Email { get; set; }

        public string? PhoneNumber { get; set; }
    }
}