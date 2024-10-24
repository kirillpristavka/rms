using DevExpress.Xpo;
using System;
using System.IO;

namespace RMS.Core.Model.Mail
{
    public class LetterAttachment : Attachment
    {
        public LetterAttachment() { }
        public LetterAttachment(Session session) : base(session) { }
        public LetterAttachment(Session session, string fullPathToFile) : base(session)
        {
            if (string.IsNullOrWhiteSpace(fullPathToFile))
            {
                throw new ArgumentNullException(nameof(fullPathToFile), "Путь к файлу не может быть пустым.");
            }

            if (!System.IO.File.Exists(fullPathToFile))
            {
                throw new ArgumentNullException(nameof(fullPathToFile), "Файл по заданному пути не существует.");
            }

            FileEncoding = GetFileEncoding(fullPathToFile);
            FullFileName = Path.GetFileName(fullPathToFile);
            FileName = Path.GetFileNameWithoutExtension(fullPathToFile);
            FileCreationDate = System.IO.File.GetCreationTime(fullPathToFile);
            FileByte = System.IO.File.ReadAllBytes(fullPathToFile);
            FileExtension = Path.GetExtension(fullPathToFile);
        }

        [Size(2048)]
        [MemberDesignTimeVisibility(false)]
        public string Link { get; set; }
        
        [Association]
        [MemberDesignTimeVisibility(false)]
        public Letter Letter { get; set; }

        public override string ToString()
        {
            return FullFileName; 
        }        
    }
}