using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject.DTOs.Request
{
    public class ConstructionTaskCategoryRequest
    {
        public string Name { get; set; } = default!;

        public string? Description { get; set; }

        public string IconImageUrl { get; set; } = default!;

        public bool IsDeleted { get; set; }
    }
}
