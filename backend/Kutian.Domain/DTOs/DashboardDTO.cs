using System;
using System.Collections.Generic;
using static Kutian.Domain.Entities.Dashboard;

namespace Kutian.Domain.DTOs
{
    public class DashboardDTO
    {
        public Guid DashboardId { get; set; }
        public Guid UserId { get; set; }
        public string Name { get; set; }
        public ICollection<DashboardCard> DashboardCards { get; set; } = new HashSet<DashboardCard>();
    }
}