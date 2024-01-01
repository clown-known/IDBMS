using BusinessObject.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDBMS_API.DTOs.Request
{
    public class UserRoleRequest
    {
        [Required]
        public CompanyRole Role { get; set; }
        [Required]
        public Guid UserId { get; set; }
    }
}
