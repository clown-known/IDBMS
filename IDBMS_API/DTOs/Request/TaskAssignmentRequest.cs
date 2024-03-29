﻿using BusinessObject.Models;
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
        public List<Guid> ProjectParticipationId { get; set; }

        [Required]
        public List<Guid> ProjectTaskId { get; set; }
    }
}
