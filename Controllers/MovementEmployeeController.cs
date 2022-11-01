using ApiCoppel.Dto;
using ApiCoppel.Repository;
using ApiCoppel.Repository.Interface;
using Microsoft.AspNetCore.Mvc;

namespace ApiCoppel.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MovementEmployeeController : ControllerBase
    {
        private readonly IMovementEmployee _MovementRepository;
        public MovementEmployeeController(IMovementEmployee MovementRepository)
        {
            _MovementRepository = MovementRepository;
        }
        // GET: api/<MovementEmployeeController>/
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var res = await _MovementRepository.GetMovements();
            return Ok(res);
        }

        [HttpGet("{id}/{month}")]
        public async Task<IActionResult> Payment(int id, string month)
        {
            var res = await _MovementRepository.GetPayment(id, month);
            return Ok(res);
        }
        // POST: api/<MovementEmployeeController>/
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] DtoMovementEmployee movement)
        {
            try
            {
                DtoMovementEmployee newmovement = await _MovementRepository.NewMovement(movement);
                return Ok(newmovement);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
