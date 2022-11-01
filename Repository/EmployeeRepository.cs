using ApiCoppel.Data.Context;
using ApiCoppel.Data.DbModels;
using ApiCoppel.Dto;
using ApiCoppel.Repository.Interface;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace ApiCoppel.Repository
{
    public class EmployeeRepository : IEmployee
    {
        private readonly DataContext _db;
        public EmployeeRepository(DataContext context)
        {
            _db = context;
        }
        public async Task<List<DtoEmployee>> GetEmployeesAsync()
        {
            List<Employee> employeedb = await _db.Employee.Include(x => x.Role).ToListAsync();
            List<DtoEmployee> employenes = new List<DtoEmployee>();
            
            List<DtoEmployee> employee = employeedb.Select(employee => new DtoEmployee()
            {
                Id = employee.Id,
                EmployeeNumber = employee.EmployeeNumber,
                Name = employee.Name,
                LastName = employee.LastName,
                RoleId = employee.RoleId,
                Role = new DtoRole()
                {
                    Id = employee.Role.Id,
                    RoleName = employee.Role.RoleName,
                    Bonus = employee.Role.Bonus,
                    IsActive = employee.Role.IsActive
                }
            }).ToList();
            return employee;
        }

        public async Task<DtoEmployee> NewEmployee(DtoEmployee employee)
        {
            await _db.Database.ExecuteSqlRawAsync("exec InsertEmployee @EmployeeNumber, @Name, @LastName, @RoleId",
                new SqlParameter("@EmployeeNumber", employee.EmployeeNumber),
                new SqlParameter("@Name", employee.Name),
                new SqlParameter("@LastName", employee.LastName),
                new SqlParameter("@RoleId", employee.RoleId));
            return employee;
        }

        public async Task<DtoEmployee> Edit(DtoEmployee employee)
        {
            await _db.Database.ExecuteSqlRawAsync("exec UpdateEmployee @Id, @EmployeeNumber, @Name, @LastName, @RoleId",
                new SqlParameter("@Id", employee.Id),
                new SqlParameter("@EmployeeNumber", employee.EmployeeNumber),
                new SqlParameter("@Name", employee.Name),
                new SqlParameter("LastName", employee.LastName),
                new SqlParameter("RoleId", employee.RoleId));
            return employee;
        }
    }
}
