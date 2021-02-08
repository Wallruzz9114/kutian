using System.Collections.Generic;
using Kutian.Domain.DTOs;

namespace Kutian.Domain.ViewModels
{
    public class GetOrdersResponse
    {
        public List<OrderDTO> Orders { get; set; }
    }
}