using Kutian.Domain.Entities;

namespace Kutian.Tests.Builders.Models
{
    public class CustomerBuilder
    {
        private readonly Customer _customer;

        public CustomerBuilder() => _customer = new Customer();

        public Customer Build() => _customer;
    }
}