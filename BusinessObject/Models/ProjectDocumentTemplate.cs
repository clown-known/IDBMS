using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject.Models
{
    public class ProjectDocumentTemplate
    {
        [Key]
        public Guid Id { get; set; }
    }
}
