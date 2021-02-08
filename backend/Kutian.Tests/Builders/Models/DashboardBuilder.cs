using System;
using Kutian.Domain.Entities;

namespace Kutian.Tests.Builders.Models
{
    public class DashboardBuilder
    {
        private readonly Dashboard _dashboard;

        public DashboardBuilder(string name, Guid userId) => _dashboard = new Dashboard(name, userId);

        public Dashboard Build() => _dashboard;
    }
}