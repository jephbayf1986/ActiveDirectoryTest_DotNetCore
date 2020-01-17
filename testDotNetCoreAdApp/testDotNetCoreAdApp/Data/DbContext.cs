using Microsoft.EntityFrameworkCore;
using testDotNetCoreAdApp.Models;

namespace testDotNetCoreAdApp.Data
{
    public class ApplicationDbContext : DbContext
    {
        /// <summary>
        /// Database Context - Constructor
        /// </summary>
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        /// <summary>
        /// SwearWord - Database Reference
        /// </summary>
        public DbSet<SwearWord> SwearWord { get; set; }
    }
}