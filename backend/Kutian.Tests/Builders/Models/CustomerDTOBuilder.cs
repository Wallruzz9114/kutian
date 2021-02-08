using Kutian.Domain.DTOs;

namespace Kutian.Tests.Builders.Models
{
    public class CustomerDTOBuilder
    {
        private readonly CustomerDTO _customerDTO;

        public CustomerDTOBuilder() => _customerDTO = new CustomerDTO();

        public CustomerDTO Build() => _customerDTO;
    }
}