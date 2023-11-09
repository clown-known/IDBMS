using BusinessObject.Models;
using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Implements
{
    public class TaskReportRepository : ITaskReportRepository
    {
        public void DeleteById(Guid id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<TaskReport> GetAll()
        {
            throw new NotImplementedException();
        }

        public TaskReport? GetById(Guid id)
        {
            throw new NotImplementedException();
        }

        public TaskReport? Save(TaskReport entity)
        {
            throw new NotImplementedException();
        }

        public void Update(TaskReport entity)
        {
            throw new NotImplementedException();
        }
    }
}
