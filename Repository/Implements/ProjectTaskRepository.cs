using BusinessObject.Models;
using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Implements
{
    public class ProjectTaskRepository : IProjectTaskRepository
    {
        public void DeleteById(Guid id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ProjectTask> GetAll()
        {
            throw new NotImplementedException();
        }

        public ProjectTask? GetById(Guid id)
        {
            throw new NotImplementedException();
        }

        public ProjectTask? Save(ProjectTask entity)
        {
            throw new NotImplementedException();
        }

        public void Update(ProjectTask entity)
        {
            throw new NotImplementedException();
        }
    }
}
