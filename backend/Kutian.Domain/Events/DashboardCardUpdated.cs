using System;

namespace Kutian.Domain.Events
{
    public class DashboardCardUpdated
    {
        public DashboardCardUpdated(Guid dashboardCardId, dynamic options)
        {
            DashboardCardId = dashboardCardId;
            Options = options;
        }

        public Guid DashboardCardId { get; set; }
        public dynamic Options { get; set; }
    }
}