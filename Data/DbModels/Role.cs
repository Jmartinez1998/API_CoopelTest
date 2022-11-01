using System.ComponentModel.DataAnnotations;

namespace ApiCoppel.Data.DbModels
{
    public class Role
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "This field is rquired!")]
        public string RoleName { get; set; }
        public int Bonus { get; set; }
        public bool IsActive { get; set; }
        public Employee Employee { get; set; }
    }
}
