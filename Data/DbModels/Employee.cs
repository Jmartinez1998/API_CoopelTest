using System.ComponentModel.DataAnnotations;

namespace ApiCoppel.Data.DbModels
{
    public class Employee
    {
        public Employee()
        {
            this.Movements = new HashSet<MovementEmployee>();
        }

        [Key]
        public int Id { get; set; }
        [Required]
        public int EmployeeNumber { get; set; }
        [Required]
        [StringLength(maximumLength: 25, ErrorMessage = "El campo {0} no debe tener mas de {1} caracteres!")]
        public string Name { get; set; }
        public string LastName { get; set; }
        public int RoleId { get; set; }
        public Role Role { get; set; }
        public virtual ICollection<MovementEmployee> Movements { get; set; }
    }
}
