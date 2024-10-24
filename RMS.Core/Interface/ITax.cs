using RMS.Core.Enumerator;
using System;

namespace RMS.Core.Interface
{
    public interface ITax
    {
        bool IsUse { get; }
        DateTime? Date { get; set; }
        string Comment { get; set; }
        Availability? Availability { get; set; }
    }
}
