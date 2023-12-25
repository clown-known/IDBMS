using BusinessObject.Models;

namespace IDBMS_API.DTOs.Response
{
    public class DashboardReponse
    {
/*        public decimal yearlyIncome { get; set; }
        public decimal lastYearIncome { get; set; }

        public decimal monthlyIncome { get; set; }
        public decimal lastMonthIncome { get; set; }*/


        public List<Project> recentProjects { get; set; }

        public List<TaskReport> recentReports { get; set; }
    }
}
