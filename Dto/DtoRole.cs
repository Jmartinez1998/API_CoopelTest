using System.Reflection.Metadata;

namespace ApiCoppel.Dto
{
    public class DtoRole
    {
        public int Id { get; set; }
        public string RoleName { get; set; }
        public int Bonus { get; set; }
        public bool IsActive { get; set; }
    }
}
