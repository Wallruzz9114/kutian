using Kutian.Domain.DTOs;
using Kutian.Domain.Entities;

namespace Kutian.Domain.Extensions
{
    public static class CustomerExtensions
    {
        public static CustomerDTO ToDTO(this Customer customer) => new CustomerDTO { };
    }
}