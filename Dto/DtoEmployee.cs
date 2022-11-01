namespace ApiCoppel.Dto
{
    public class DtoEmployee
    {
        public int Id { get; set; }
        public int EmployeeNumber { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public int RoleId { get; set; }
        public DtoRole ?Role { get; set; }
    }
}
