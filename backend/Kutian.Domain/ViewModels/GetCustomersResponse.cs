using System.Collections.Generic;
using Kutian.Domain.DTOs;

namespace Kutian.Domain.ViewModels
{
    public class GetCustomersResponse
    {
        public List<CustomerDTO> Customers { get; set; }
    }
}