using BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject.DTOs.Request
{
    public class InteriorItemBookmarkRequest
    {
        [Required]
        public Guid UserId { get; set; }

        [Required]
        public Guid InteriorItemId { get; set; }
    }
}
