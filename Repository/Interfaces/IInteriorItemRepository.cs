﻿using BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interfaces
{
    public interface IInteriorItemRepository : ICrudBaseRepository<InteriorItem, Guid>
    {
        IEnumerable<InteriorItem> GetByCategory(int id);
        bool CheckCodeExisted(string code);
    }
}
