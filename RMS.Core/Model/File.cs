using DevExpress.Xpo;
using RMS.Core.Enumerator;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace RMS.Core.Model
{
    public class File : XPObject, IEquatable<File>
    {
        public File() { }
        public File(Session session) : base(session) { }

        public File(Session session, string fullPathToFile) : base(session)
        {
            Create(fullPathToFile);
        }

        public void Create(string fullPathToFile)
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
        }

        public string WriteFile(string path)
        {

            if (!string.IsNullOrWhiteSpace(path))
            {
                if (Directory.Exists(path))
                {
                    var fullFileName = FullFileName ?? FileName;
                    var fullPath = $"{path}\\{fullFileName}";
                    
                    if (!System.IO.File.Exists(fullPath))
                    {
                        System.IO.File.WriteAllBytes(fullPath, this.FileByte);
                        _ = $@"Файл {fullFileName} успешно сохранен в {path}";
                    }
                    return fullPath;
                }
                else
                {
                }
            }
            else
            {
            }

            return default;
        }

        private Encoding GetFileEncoding(string fullPathToFile)
        {
            if (string.IsNullOrWhiteSpace(fullPathToFile))
            {
                throw new ArgumentNullException(nameof(fullPathToFile), "Путь к файлу не может быть пустым.");
            }

            var encoding = default(Encoding);

            var tempFile = Path.GetTempFileName();
            System.IO.File.Copy(fullPathToFile, tempFile, true);

            using (var fileStream = new FileStream(tempFile, FileMode.Open))
            {
                using (var streaReader = new StreamReader(fileStream, true))
                {
                    encoding = streaReader.CurrentEncoding;
                }
            }

            System.IO.File.Delete(tempFile);
            return encoding;
        }

        /// <summary>
        /// Полное имя файла [Пример: text.txt].
        /// </summary>
        [DisplayName("Полное имя файла")]
        public string FullFileName { get; set; }

        /// <summary>
        /// Имя файла [Пример: text].
        /// </summary>
        [DisplayName("Имя файла")]
        public string FileName { get; set; }

        /// <summary>
        /// Расширение файла [Пример: .txt].
        /// </summary>
        public FileExtension FileExtension { get; set; }

        /// <summary>
        /// Дата создания файла.
        /// </summary>
        public DateTime FileCreationDate { get; set; } = DateTime.Now;
                
        /// <summary>
        /// Кодировка файла.
        /// </summary>
        public Encoding FileEncoding { get; set; }

        /// <summary>
        /// Файл в байтах.
        /// </summary>
        public byte[] FileByte { get; set; }

        public void Edit(File file)
        {
            if (file is null)
            {
                return;
            }

            FullFileName = file.FullFileName;
            FileName = file.FileName;
            FileExtension = file.FileExtension;
            FileCreationDate = file.FileCreationDate;
            FileEncoding = file.FileEncoding;
            FileByte = file.FileByte;

            Save();
        }
        
        public override string ToString()
        {            
            return FileName;
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as File);
        }

        public bool Equals(File other)
        {
            return other != null &&
                   FileName == other.FileName &&
                   EqualityComparer<byte[]>.Default.Equals(FileByte, other.FileByte);
        }

        public override int GetHashCode()
        {
            int hashCode = 1468050511;
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(FileName);
            hashCode = hashCode * -1521134295 + EqualityComparer<byte[]>.Default.GetHashCode(FileByte);
            return hashCode;
        }
    }
}