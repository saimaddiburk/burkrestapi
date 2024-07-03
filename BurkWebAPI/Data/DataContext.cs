using BurkWebAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace BurkWebAPI.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        public DbSet<Employee> Employee { get; set; }
    }
}