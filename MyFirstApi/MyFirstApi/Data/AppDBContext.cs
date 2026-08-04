using Microsoft.EntityFrameworkCore;
using MyFirstApi.Entities;

namespace MyFirstApi.Data
{
    /// <summary>
    /// 
    /// </summary>
    public class AppDBContext:DbContext
    {
        /// <summary>
        /// Intiate AppDBContext class for Database related operations.
        /// DbContextOptions<AppDBContext>:=> Represents the options used to configure the AppDBContext instance.
        /// </summary>
        /// <param name="options"> object of DbContextOptions<AppDBContext></param>
        public AppDBContext(DbContextOptions<AppDBContext> options):base(options)
        {
            
        }
        /*
         * DbSet<User> AccountUser { get; set; }:=> Represents the collection of User entities in the database. 
         * This property allows you to query and manipulate User records using Entity Framework Core.
         * DbSet<Employee> Employees { get; set; }:=> Represents the collection of Employee entities in the database. 
         * This property allows you to query and manipulate Employee records using Entity Framework Core.
         */
        public DbSet<User> AccountUser { get; set; }       
        public DbSet<Employee> Employees { get; set; }

    }
}
