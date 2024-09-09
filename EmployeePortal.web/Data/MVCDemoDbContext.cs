using EmployeePortal.web.Models;
using EmployeePortal.web.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace EmployeePortal.web.Data
{
    public class MVCDemoDbContext : DbContext
    {
        public MVCDemoDbContext(DbContextOptions options) : base(options)
        {
        }


        public DbSet<Employee> Employees { get; set; }
    }
}
