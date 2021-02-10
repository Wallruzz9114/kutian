using System;

namespace Kutian.Domain.Events
{
    public class DashboardCardAdded
    {
        public DashboardCardAdded(Guid dashboardCardId, Guid cardId, dynamic options)
        {
            DashboardCardId = dashboardCardId;
            CardId = cardId;
            Options = options;
        }

        public Guid DashboardCardId { get; set; }
        public Guid CardId { get; set; }
        public dynamic Options { get; set; }
    }
}