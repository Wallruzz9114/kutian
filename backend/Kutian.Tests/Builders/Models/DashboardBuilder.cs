using Kutian.Domain.Entities;

namespace Kutian.Tests.Builders.Models
{
    public class DashboardBuilder
    {
        private readonly Dashboard _dashboard;

        public DashboardBuilder() => _dashboard = new Dashboard();

        public Dashboard Build() => _dashboard;
    }
}