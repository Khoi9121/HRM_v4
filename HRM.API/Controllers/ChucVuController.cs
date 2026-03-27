using Microsoft.AspNetCore.Mvc;
using HRM.Application.DTOs;
using HRM.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
namespace HRM.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ChucVuController : ControllerBase
    {
        private readonly IChucVuService _service;

        public ChucVuController(IChucVuService service)
            => _service = service;

        /// <summary>Lấy danh sách tất cả chức vụ</summary>
        [HttpGet]
        public async Task<IActionResult> GetAll()
            => Ok(await _service.GetAllAsync());

        /// <summary>Lấy chức vụ theo Id</summary>
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await _service.GetByIdAsync(id);
            return result is null ? NotFound() : Ok(result);
        }

        /// <summary>Tạo chức vụ mới</summary>
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateChucVuDto dto)
        {
            var result = await _service.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }

        /// <summary>Cập nhật chức vụ</summary>
        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateChucVuDto dto)
            => Ok(await _service.UpdateAsync(id, dto));

        /// <summary>Xóa mềm chức vụ</summary>
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _service.DeleteAsync(id);
            return NoContent();
        }
    }
}
