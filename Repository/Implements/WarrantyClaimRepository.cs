using BusinessObject.Models;
using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Implements
{
    public class WarrantyClaimRepository : IWarrantyClaimRepository
    {
        public void DeleteById(Guid id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<WarrantyClaim> GetAll()
        {
            throw new NotImplementedException();
        }

        public WarrantyClaim? GetById(Guid id)
        {
            throw new NotImplementedException();
        }

        public WarrantyClaim? Save(WarrantyClaim entity)
        {
            throw new NotImplementedException();
        }

        public void Update(WarrantyClaim entity)
        {
            throw new NotImplementedException();
        }
    }
}
