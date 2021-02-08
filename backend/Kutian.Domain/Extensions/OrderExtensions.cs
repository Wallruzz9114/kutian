using Kutian.Domain.DTOs;
using Kutian.Domain.Entities;

namespace Kutian.Utilities.Core.Extensions
{
    public static class OrderExtensions
    {
        public static OrderDTO ToDTO(this Order order) => new OrderDTO { };
    }
}