using BusinessObject.Models;
using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Implements
{
    public class SiteRepository : ISiteRepository
    {
        public void DeleteById(Guid id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Site> GetAll()
        {
            throw new NotImplementedException();
        }

        public Site? GetById(Guid id)
        {
            throw new NotImplementedException();
        }

        public Site? Save(Site entity)
        {
            throw new NotImplementedException();
        }

        public void Update(Site entity)
        {
            throw new NotImplementedException();
        }
    }
}
