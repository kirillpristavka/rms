namespace RMS.Core.Interface
{
    public interface IStatus
    {
        int? Index { get; set; }
        int? IndexIcon { get; set; }
        string Name { get; set; }
        string Description { get; set; }
        string Color { get; set; }
        bool IsDefault { get; set; }
        bool IsProtectionDelete { get; set; }
    }
}
