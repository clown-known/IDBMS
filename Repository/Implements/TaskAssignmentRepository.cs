using BusinessObject.Models;
using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Implements
{
    public class TaskAssignmentRepository : ITaskAssignmentRepository
    {
        public void DeleteById(Guid id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<TaskAssignment> GetAll()
        {
            throw new NotImplementedException();
        }

        public TaskAssignment? GetById(Guid id)
        {
            throw new NotImplementedException();
        }

        public TaskAssignment? Save(TaskAssignment entity)
        {
            throw new NotImplementedException();
        }

        public void Update(TaskAssignment entity)
        {
            throw new NotImplementedException();
        }
    }
}
