using BusinessObject.Enums;
using BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interfaces
{
    public interface IProjectDocumentTemplateRepository : ICrudBaseRepository<ProjectDocumentTemplate, int>
    {
        public ProjectDocumentTemplate getByType(DocumentTemplateType type);
    }
}
