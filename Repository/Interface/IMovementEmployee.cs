using ApiCoppel.Dto;

namespace ApiCoppel.Repository.Interface
{
    public interface IMovementEmployee
    {
        Task<List<DtoMovementEmployee>> GetMovements();
        //Create
        Task<DtoMovementEmployee> NewMovement(DtoMovementEmployee movement);
    }
}
