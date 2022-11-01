using ApiCoppel.Dto;
using Microsoft.AspNetCore.Mvc;

namespace ApiCoppel.Repository.Interface
{
    public interface IRole
    {
        //Show
        Task<List<DtoRole>> GetRolesAsync();
        //Create
        Task<DtoRole> NewRol(DtoRole role);
        //Edit
        Task<DtoRole> Edit(DtoRole role);
    }
}
