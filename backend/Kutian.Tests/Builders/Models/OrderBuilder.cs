using Kutian.Domain.Entities;

namespace Kutian.Tests.Builders.Models
{
    public class OrderBuilder
    {
        private readonly Order _order;

        public OrderBuilder() => _order = new Order();

        public Order Build() => _order;
    }
}