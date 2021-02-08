using System;
using System.Collections.Generic;

namespace Kutian.Domain.DTOs
{
    public class OrderDTO
    {
        public Guid OrderId { get; private set; }
        public decimal Total { get; set; }
        public ICollection<LineItemDto> LineItems { get; set; }

        public record LineItemDto { }
    }
}