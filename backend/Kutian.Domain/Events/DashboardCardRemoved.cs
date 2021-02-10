using System;

namespace Kutian.Domain.Events
{
    public class DashboardCardRemoved
    {
        public DashboardCardRemoved(Guid dashboardCardId) => DashboardCardId = dashboardCardId;

        public Guid DashboardCardId { get; }
    }
}