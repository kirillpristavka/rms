using System.ComponentModel;

namespace RMS.Core.Enumerator
{
    /// <summary>
    /// Протокол шифрования.
    /// </summary>
    public enum EncryptionProtocol
    {
        /// <summary>
        /// SSL.
        /// </summary>
        [Description("SSL")]
        SSL = 0,

        /// <summary>
        /// TLS.
        /// </summary>
        [Description("TLS")]
        TLS = 1,
    }
}
