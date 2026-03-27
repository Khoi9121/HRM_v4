using HRM.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRM.Application.Common
{
    public class PaginationParams
    {
        private const int MaxPageSize = 100;
        private int _pageSize = 20;

        public int Page { get; set; } = 1;
        public int PageSize
        {
            get => _pageSize;
            set => _pageSize = value > MaxPageSize ? MaxPageSize : value;
        }
    }

    public class ThongBaoPaginationParams : PaginationParams
    {
        public Guid? NguoiNhanId { get; set; }
        public bool? DaDoc { get; set; }
        public LoaiThongBao? LoaiThongBao { get; set; }
        public MucDoUuTien? MucDoUuTien { get; set; }
        public string? Search { get; set; }
        public string SortBy { get; set; } = "createdAt";
        public bool SortDesc { get; set; } = true;
    }
}
