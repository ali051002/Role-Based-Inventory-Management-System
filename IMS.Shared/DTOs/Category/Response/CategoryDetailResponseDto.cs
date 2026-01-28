using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMS.Shared.DTOs.Category.Response
{
    public class CategoryDetailResponseDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public bool IsActive { get; set; }
        public int ProductCount { get; set; }
        public int OutOfStockProducts { get; set; }
    }
}
