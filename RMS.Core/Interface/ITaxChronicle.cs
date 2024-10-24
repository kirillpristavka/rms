using RMS.Core.Enumerator;
using System;

namespace RMS.Core.Interface
{
    public interface ITaxChronicle
    {
        bool IsUse { get; }
        DateTime? Date { get; set; }
        string Comment { get; set; }
        Availability? Availability { get; set; }
    }
}
