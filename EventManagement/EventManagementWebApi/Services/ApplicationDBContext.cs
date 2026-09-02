using EventManagementWebApi.Models;
using Microsoft.EntityFrameworkCore;

// use the actual namespace where Event is defined
namespace EventManagementWebApi.Services
{
    public class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options)
            : base(options)
        {

        }

        // Step 2.1.6:Add DbSet<T> properties here, e.g.:
        /* required modifier: The required modifier indicates that the field or property it applies to must be initialized by an 
         * object initializer. Any expression that initializes a new instance of the type must initialize all required members. 
         * The required modifier is available starting with C# 11.
         * */
        public required DbSet<Event> Events { get; set; }
    }
}
