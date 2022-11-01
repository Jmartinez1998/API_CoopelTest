using ApiCoppel.Data.Context;
using ApiCoppel.Data.DbModels;
using ApiCoppel.Dto;
using ApiCoppel.Repository.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data.SqlTypes;

namespace ApiCoppel.Repository
{
    public class RoleRepository : IRole
    {
        private readonly DataContext _db;
        public RoleRepository(DataContext context)
        {
            _db = context;
        }
        public async Task<List<DtoRole>> GetRolesAsync()
        {
            List<Role> roledb = await _db.Role.Include(x => x.Employee).ToListAsync();
            List<DtoRole> roles = roledb.Select(roles => new DtoRole()
            {
                Id = roles.Id,
                RoleName = roles.RoleName,
                Bonus = roles.Bonus,
                IsActive = roles.IsActive,
            }).ToList();
            return roles;
        }
        public async Task<DtoRole> NewRol(DtoRole role)
        {
            await _db.Database.ExecuteSqlRawAsync("exec InsertRole @RoleName, @Bonus, @IsActive",
                new SqlParameter("@RoleName", role.RoleName),
                new SqlParameter("@Bonus", role.Bonus),
                new SqlParameter("@IsActive", role.IsActive));
            return role;
        }
        public async Task<DtoRole> Edit(DtoRole role)
        {
            await _db.Database.ExecuteSqlRawAsync("exec UpdateRoles @Id, @RoleName, @Bonus, @IsActive",
                new SqlParameter("@Id", role.Id),
                new SqlParameter("@RoleName", role.RoleName),
                new SqlParameter("@Bonus", role.Bonus),
                new SqlParameter("@IsActive", role.IsActive));
            return role;
        }
    }
}
