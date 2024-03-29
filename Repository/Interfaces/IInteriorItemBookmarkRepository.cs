﻿using BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interfaces
{
    public interface IInteriorItemBookmarkRepository : ICrudBaseRepository<InteriorItemBookmark, Guid>
    {
        IEnumerable<InteriorItemBookmark> GetByUserId(Guid userId);
    }
}
