using BusinessObject.Enums;
using BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDBMS_API.DTOs.Request
{
    public class ProjectParticipationRequest
    {
        [Required]
        public Guid UserId { get; set; }

        [Required]
        public ParticipationRole Role { get; set; }

        [Required]
        public Guid ProjectId { get; set; }
    }
}
