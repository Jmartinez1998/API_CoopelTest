using ApiCoppel.Data.Context;
using ApiCoppel.Data.DbModels;
using ApiCoppel.Dto;
using ApiCoppel.Repository.Interface;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace ApiCoppel.Repository
{
    public class MovementEmployeeRepository : IMovementEmployee
    {
        private readonly DataContext _db;
        private readonly DtoCalculatePayment paymnt;

        public MovementEmployeeRepository(DataContext context)
        {
            _db = context;
            paymnt = new DtoCalculatePayment();
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
        public async Task<DtoCalculatePayment> GetPayment(int employeeId, string Month)
        {
            List<int> numbers = new List<int>();
            int meses_trabajados;
            int hras;
            int TotalEntregasxMes;
            int bonos;
            double retencion = 0.9;
            List<MovementEmployee> movementsdb = await _db.MovementEmployee.Where(x => x.EmployeeId.Equals(employeeId) & x.Month.Equals(Month))
                .Include(c => c.Employee)
                .ToListAsync();
            Employee typeEmpl =  await _db.Employee.Include(x => x.Role).Where(x => x.Id.Equals(employeeId)).SingleAsync();
            if (movementsdb.Count > 0)
            {
                foreach (var movement in movementsdb)
                {
                    numbers.Add(movement.TotalDelivery);
                }
                TotalEntregasxMes = numbers.Sum();
                meses_trabajados = movementsdb.Count;
                //Obtiene Hras trabajadas en el / los meses
                hras = meses_trabajados*4 * 6 *8;
                if (typeEmpl.Role.RoleName.Equals("Chofer") || typeEmpl.Role.RoleName.Equals("Cargador"))
                {
                    bonos = typeEmpl.Role.Bonus;
                    bonos = bonos * TotalEntregasxMes;
                }
                else
                {
                    bonos = 0;
                }
                double sueldo_base = 30 * hras;
                //- retencion o subtotal
                retencion = sueldo_base + bonos * .9;
                //Checa si sobrepara los 10kxmes
                if(retencion > 10000)
                {
                    retencion = retencion * .3;
                }
                double vales_desp = retencion * .4;
                double GralTTotal = vales_desp;

                paymnt.EmployeeId = employeeId;
                paymnt.Hours = hras;
                paymnt.TotalDelivery = TotalEntregasxMes;
                paymnt.TotalBonous = bonos;
                paymnt.Retention = retencion;
                paymnt.GralTotal = sueldo_base + bonos;
            }
            return paymnt;
        }

    }
}
