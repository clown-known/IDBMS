using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject.Models
{
    public class TaskAssignment
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public Guid UserId { get; set; }
        public User User { get; set; } = new();

        [Required]
        public Guid ProjectTaskId { get; set; }
        public ProjectTask ProjectTask { get; set; } = new();

        [Required]
        public Guid ProjectId { get; set; }
    }
}
