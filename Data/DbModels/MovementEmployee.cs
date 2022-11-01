using System.ComponentModel.DataAnnotations;

namespace ApiCoppel.Data.DbModels
{
    public class MovementEmployee
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "This field is rquired!")]
        public int EmployeeId { get; set; }
        public virtual Employee? Employee { get; set; }
        [Required]
        public string Month { get; set; }
        public int TotalDelivery { get; set; }
    }
}
