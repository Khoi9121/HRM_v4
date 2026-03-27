using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRM.Domain.Entities
{
    public class PhongBan : BaseEntity
    {
        public string TenPhongBan {  get; set; }
        public string? MoTa {  get; set; }
    }
}
