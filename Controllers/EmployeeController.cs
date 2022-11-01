using ApiCoppel.Dto;
using ApiCoppel.Repository;
using ApiCoppel.Repository.Interface;
using Microsoft.AspNetCore.Mvc;

namespace ApiCoppel.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployee employeeRepository;
        public EmployeeController(IEmployee _employeeRepository)
        {
            employeeRepository = _employeeRepository;
        }

        // GET: api/<EmployeeController>/
        [HttpGet]
        public async Task<IActionResult> Get()
        {
           var res = await employeeRepository.GetEmployeesAsync();
            return Ok(res);
        }

        // POST: api/<EmployeeController>/
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] DtoEmployee employee)
        {
            try
            {
                DtoEmployee newemployee = await employeeRepository.NewEmployee(employee);
                return Ok(newemployee);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // PUT: api/<EmployeeController>/
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] DtoEmployee employee)
        {
            try
            {
                DtoEmployee newemployee = await employeeRepository.Edit(employee);
                return Ok(newemployee);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

    }
}
