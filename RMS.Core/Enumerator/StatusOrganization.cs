using System.ComponentModel;

namespace RMS.Core.Enumerator
{
    /// <summary>
    /// Статус организации.
    /// </summary>
    public enum StatusOrganization
    {
        /// <summary>
        /// Действующая.
        /// </summary>
        [Description("Действующая")]
        ACTIVE = 0,

        /// <summary>
        /// Ликвидируется.
        /// </summary>
        [Description("Ликвидируется")]
        LIQUIDATING = 1,

        /// <summary>
        /// Ликвидирована.
        /// </summary>
        [Description("Ликвидирована")]
        LIQUIDATED = 2,

        /// <summary>
        /// В процессе присоединения к другому юридическому лицу, с последующей ликвидацией.
        /// </summary>
        [Description("Реорганизация")]
        REORGANIZING = 3,

        /// <summary>
        /// Банкротство.
        /// </summary>
        [Description("Банкротство")]
        BANKRUPT = 4
    }
}
