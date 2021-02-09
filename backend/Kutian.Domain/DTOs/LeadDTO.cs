using System;

namespace Kutian.Domain.DTOs
{
    public class LeadDTO
    {
        public Guid LeadId { get; private set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string EmailAddress { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime? Deleted { get; set; }
    }
}