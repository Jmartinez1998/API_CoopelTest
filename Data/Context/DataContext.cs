using ApiCoppel.Data.DbModels;
using Microsoft.EntityFrameworkCore;

namespace ApiCoppel.Data.Context
{
    public class DataContext : DbContext
    {
        protected readonly IConfiguration _Configuration;
        public DataContext(IConfiguration configuration)
        {
            _Configuration = configuration;
        }
        public DbSet<Role> Role { get; set; }

        public DbSet<Employee> Employee { get; set; }
        public DbSet<MovementEmployee> MovementEmployee { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            // connecion sql
            options.UseSqlServer(_Configuration.GetConnectionString("DefaultConnection"));
        }
    }
}
