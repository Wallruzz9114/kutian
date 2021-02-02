using System;
using System.Collections.Generic;
using Kutian.Utilities.Abstractions;

namespace Kutian.Utilities.Store.Models
{
    public class Snapshot
    {
        public Guid SnapshotId { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public IDictionary<string, HashSet<AggregateRoot>> Data { get; set; } = new Dictionary<string, HashSet<AggregateRoot>>();
    }
}