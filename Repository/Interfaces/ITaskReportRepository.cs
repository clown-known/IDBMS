﻿using BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interfaces
{
    public interface ITaskReportRepository : ICrudBaseRepository<TaskReport, Guid>
    {
        IEnumerable<TaskReport> GetByTaskId(Guid id);
        IEnumerable<TaskReport> GetRecentReports();
        IEnumerable<TaskReport> GetRecentReportsByUserId(Guid id);
    }
}
