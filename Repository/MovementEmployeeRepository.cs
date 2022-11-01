using ApiCoppel.Data.Context;
using ApiCoppel.Data.DbModels;
using ApiCoppel.Dto;
using ApiCoppel.Repository.Interface;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace ApiCoppel.Repository
{
    public class MovementEmployeeRepository : IMovementEmployee
    {
        private readonly DataContext _db;
        public MovementEmployeeRepository(DataContext context)
        {
            _db = context;
        }
        public async Task<List<DtoMovementEmployee>> GetMovements()
        {
            List<MovementEmployee> movementsdb = await _db.MovementEmployee.Include(c => c.Employee).ToListAsync();
            List<DtoMovementEmployee> movement = movementsdb.Select(movement => new DtoMovementEmployee()
            {
                Id = movement.Id,
                EmployeeId = movement.EmployeeId,
                Month = movement.Month,
                TotalDelivery = movement.TotalDelivery,
                Employee = new DtoEmployee()
                {
                    Id = movement.EmployeeId,
                    EmployeeNumber = movement.Employee.EmployeeNumber,
                    Name = movement.Employee.Name,
                    LastName = movement.Employee.LastName
                }
            }).ToList();
            return movement;
        }
        public async Task<DtoMovementEmployee> NewMovement(DtoMovementEmployee movement)
        {
            await _db.Database.ExecuteSqlRawAsync("exec InsertMovement @EmployeeId, @Month, @TotalDelivery",
                new SqlParameter("@EmployeeId", movement.EmployeeId),
                new SqlParameter("@Month", movement.Month),
                new SqlParameter("@TotalDelivery", movement.TotalDelivery));
            return movement;
        }
    }
}
