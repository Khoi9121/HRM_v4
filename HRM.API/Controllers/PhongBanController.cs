using Microsoft.AspNetCore.Mvc;
using HRM.Application.DTOs;
using HRM.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
namespace HRM.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PhongBanController : ControllerBase
    {
        private readonly IPhongBanService _service;

        public PhongBanController(IPhongBanService service)
            => _service = service;

        // GET api/phongban
        [HttpGet]
        public async Task<IActionResult> GetAll()
            => Ok(await _service.GetAllAsync());

        // GET api/phongban/{id}
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await _service.GetByIdAsync(id);
            return result is null ? NotFound() : Ok(result);
        }

        // POST api/phongban
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreatePhongBanDto dto)
        {
            var result = await _service.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }

        // PUT api/phongban/{id}
        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdatePhongBanDto dto)
            => Ok(await _service.UpdateAsync(id, dto));

        // DELETE api/phongban/{id}
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _service.DeleteAsync(id);
            return NoContent();
        }
    }
}
