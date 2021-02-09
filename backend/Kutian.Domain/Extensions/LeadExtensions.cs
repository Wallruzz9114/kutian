using Kutian.Domain.DTOs;
using Kutian.Domain.Entities;

namespace Kutian.Domain.Extensions
{
    public static class LeadExtensions
    {
        public static LeadDTO ToDTO(this Lead lead)
        {
            return new LeadDTO { };
        }
    }
}