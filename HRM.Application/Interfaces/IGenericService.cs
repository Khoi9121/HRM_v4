using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRM.Application.Interfaces
{
    public interface IGenericService<TDto, TCreate, TUpdate>
    {
        Task<IEnumerable<TDto>> GetAllAsync();
        Task<TDto?> GetByIdAsync(Guid id);
        Task<TDto> CreateAsync(TCreate dto);
        Task<TDto> UpdateAsync(Guid id, TUpdate dto);
        Task DeleteAsync(Guid id);
    }
}
