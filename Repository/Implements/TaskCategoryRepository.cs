using BusinessObject.Models;
using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Implements
{
    public class TaskCategoryRepository : ITaskCategoryRepository
    {
        public void DeleteById(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<TaskCategory> GetAll()
        {
            throw new NotImplementedException();
        }

        public TaskCategory? GetById(int id)
        {
            throw new NotImplementedException();
        }

        public TaskCategory? Save(TaskCategory entity)
        {
            throw new NotImplementedException();
        }

        public void Update(TaskCategory entity)
        {
            throw new NotImplementedException();
        }
    }
}
