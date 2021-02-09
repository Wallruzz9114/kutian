using System.Collections.Generic;
using Kutian.Domain.DTOs;

namespace Kutian.Domain.ViewModels
{
    public class GetLeadsResponse
    {
        public List<LeadDTO> Leads { get; set; }
    }
}