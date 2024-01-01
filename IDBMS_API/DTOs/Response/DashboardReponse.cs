using BusinessObject.Models;

namespace IDBMS_API.DTOs.Response
{
    public class DashboardReponse
    {
        public int numOngoingTasks { get; set; }
        public int numOngoingProjects { get; set; }

        public List<Project> recentProjects { get; set; }

        public List<TaskReport> recentReports { get; set; }

        // list report up theo thang
    }
}
