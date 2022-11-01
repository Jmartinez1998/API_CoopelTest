using ApiCoppel.Dto;

namespace ApiCoppel.Repository.Interface
{
    public interface IEmployee
    {
        //Show
        Task<List<DtoEmployee>> GetEmployeesAsync();
        //Create
        Task<DtoEmployee> NewEmployee(DtoEmployee employee);
        //Updt
        Task<DtoEmployee> Edit(DtoEmployee employee);
    }
}
