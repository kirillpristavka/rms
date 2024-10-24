using System.ComponentModel;

namespace RMS.Core.Enumerator
{
    /// <summary>
    /// Тип общего словаря.
    /// </summary>
    public enum GeneralVocabularyType
    {
        /// <summary>
        /// Вариант получения первичных документов.
        /// </summary>
        [Description("Вариант получения первичных документов")]
        OptionObtainingPrimaryDocuments = 1,

        /// <summary>
        /// Вариант требований.
        /// </summary>
        [Description("Вариант требований")]
        OptionDemand = 2
    }
}