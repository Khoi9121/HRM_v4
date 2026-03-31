using HRM.Application.Common;
using HRM.Application.DTOs;
using HRM.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using HRM.Domain.Interfaces;
namespace HRM.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ThongBaoController : ControllerBase
    {
        private readonly IThongBaoService _service;
        public ThongBaoController(IThongBaoService s) => _service = s;

        /// <summary>Lấy danh sách thông báo có phân trang và filter</summary>
        /// <remarks>
        /// Ví dụ: GET api/thongbao?page=1&pageSize=20&daDoc=false&sortDesc=true
        /// </remarks>
        [HttpGet]
        // SỬA TẠI ĐÂY: Đổi ThongBaoPaginationParams thành ThongBaoQueryParameters
        public async Task<IActionResult> GetPaged([FromQuery] ThongBaoQueryParameters p)
            => Ok(await _service.GetPagedAsync(p));

        /// <summary>Lấy 1 thông báo theo Id</summary>
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var r = await _service.GetByIdAsync(id);
            return r is null ? NotFound() : Ok(r);
        }

        /// <summary>Tạo thông báo mới</summary>
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateThongBaoDto dto)
        {
            var r = await _service.CreateAsync(dto);
            // r.Id là Guid, CreatedAtAction sẽ tạo header Location trỏ đến GetById
            return CreatedAtAction(nameof(GetById), new { id = r.Id }, r);
        }

        /// <summary>Cập nhật thông báo</summary>
        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateThongBaoDto dto)
            => Ok(await _service.UpdateAsync(id, dto));

        /// <summary>Xóa mềm thông báo</summary>
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _service.DeleteAsync(id);
            return NoContent();
        }

        /// <summary>Đánh dấu đã đọc — hỗ trợ batch nhiều thông báo</summary>
        [HttpPatch("danh-dau-da-doc")]
        public async Task<IActionResult> DanhDauDaDoc([FromBody] DanhDauDocDto dto)
        {
            await _service.DanhDauDaDocAsync(dto);
            return NoContent();
        }

        /// <summary>Đếm số thông báo chưa đọc của 1 người dùng</summary>
        [HttpGet("chua-doc/{nguoiNhanId:guid}")]
        public async Task<IActionResult> DemChuaDoc(Guid nguoiNhanId)
            => Ok(new { count = await _service.DemChuaDocAsync(nguoiNhanId) });
    }
}
