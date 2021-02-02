using System;
using Kutian.Utilities.Abstractions;

namespace Kutian.Utilities.Core.Models
{
    public class MachineDateTime : IDateTime
    {
        public DateTime UtcNow => DateTime.UtcNow;
    }
}