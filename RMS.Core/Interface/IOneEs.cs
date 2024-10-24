using System;

namespace RMS.Core.Interface
{
    public interface IOneEs
    {
        bool IsUse { get; }
        DateTime? Date { get; set; }
        string Comment { get; set; }
    }
}
