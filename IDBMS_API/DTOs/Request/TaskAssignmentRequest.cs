using BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDBMS_API.DTOs.Request
{
    public class TaskAssignmentRequest
    {
        [Required]
        public Guid UserId { get; set; }

        [Required]
        public Guid ProjectTaskId { get; set; }
        
        [Required]
        public Guid ProjectId { get; set; }
    }
}
