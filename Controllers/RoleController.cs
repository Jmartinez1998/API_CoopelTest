using ApiCoppel.Dto;
using ApiCoppel.Repository.Interface;
using Microsoft.AspNetCore.Mvc;

namespace ApiCoppel.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RoleController : ControllerBase
    {
        private readonly IRole roleRepository;
        public RoleController(IRole _roleRepository)
        {
            roleRepository = _roleRepository;
        }

        // GET: api/<RoleController>/
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await roleRepository.GetRolesAsync());
        }

        // POST: api/<RoleController>/
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] DtoRole role)
        {
            try
            {
                DtoRole rol = await roleRepository.NewRol(role);
                return Ok(rol);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // PUT: api/<RoleController>/
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] DtoRole newrole)
        {
            try
            {
                DtoRole rol = await roleRepository.Edit(newrole);
                return Ok(rol);
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }

    }
}
