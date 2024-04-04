using EmpFullStack.Models;
using Microsoft.EntityFrameworkCore;
using System.Data.Common;

namespace EmpFullStack.Data
{
    public class FullstackDbContext : DbContext

    {
        public FullstackDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Emp> Employees { get; set; }
    }
}
