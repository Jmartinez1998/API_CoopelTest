using ApiCoppel.Data.DbModels;
using System.ComponentModel.DataAnnotations;

namespace ApiCoppel.Dto
{
    public class DtoMovementEmployee
    {
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public  DtoEmployee? Employee { get; set; }
        public string Month { get; set; }
        public int TotalDelivery { get; set; }
    }
}
