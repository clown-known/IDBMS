﻿using BusinessObject.Enums;
using BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDBMS_API.DTOs.Request
{
    public class ProjectTaskRequest
    {

        [Required]
        public string Name { get; set; } = default!;

        public string? Description { get; set; }

        [Required]
        public CalculationUnit CalculationUnit { get; set; }

        [Required]
        [Column(TypeName = "money")]
        public decimal PricePerUnit { get; set; }

        [Required]
        public double UnitInContract { get; set; }

        [Required]
        public bool IsIncurred { get; set; }

        public DateTime? StartedDate { get; set; }

        public int EstimateBusinessDay { get; set; }

        public Guid? ParentTaskId { get; set; }

        public int? TaskCategoryId { get; set; }

        [Required]
        public Guid ProjectId { get; set; }

        public Guid? PaymentStageId { get; set; }

        public int? TaskDesignId { get; set; }

        public Guid? RoomId { get; set; }
    }
}
