using System;

namespace Kutian.Utilities.Abstractions
{
    public interface IDateTime
    {
        DateTime UtcNow { get; }
    }
}