using DevExpress.Xpo;
using RMS.Core.Controllers;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;

namespace RMS.Core.Model
{
    public class Attachment : XPObject
    {
        public Attachment() { }
        public Attachment(Session session) : base(session) { }
        public Attachment(Session session, string fullPathToFile) : base(session)
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

        /// <summary>
        /// Полное имя файла [Пример: text.txt].
        /// </summary>
        [Size(512)]
        [DisplayName("Файл")]
        public string FullFileName { get; set; }

        /// <summary>
        /// Расширение файла из полного именования.
        /// </summary>
        [MemberDesignTimeVisibility(false)]
        public string FullFileNameExtension
        {
            get
            {
                var result = default(string);
                
                try
                {
                    if (!string.IsNullOrWhiteSpace(FullFileName))
                    {
                        var item = FullFileName.Split('.')?.LastOrDefault();
                        if (item != null)
                        {
                            result =  $".{item}";
                        }
                    }
                }
                catch (Exception ex)
                {
                    RMS.Core.Controllers.LoggerController.WriteLog(ex?.ToString());
                }
                
                return result;
            }
        }

        /// <summary>
        /// Имя файла [Пример: text].
        /// </summary>
        [Size(512)]
        [DisplayName("Имя файла")]
        [MemberDesignTimeVisibility(false)]
        public string FileName { get; set; }

        /// <summary>
        /// Расширение файла [Пример: .txt].
        /// </summary>
        [Size(512)]
        [MemberDesignTimeVisibility(false)]
        public string FileExtension { get; set; }

        /// <summary>
        /// Дата создания файла.
        /// </summary>
        [MemberDesignTimeVisibility(false)]
        public DateTime FileCreationDate { get; set; }

        /// <summary>
        /// Кодировка файла.
        /// </summary>
        [MemberDesignTimeVisibility(false)]
        public Encoding FileEncoding { get; set; }

        /// <summary>
        /// Файл в байтах.
        /// </summary>
        [MemberDesignTimeVisibility(false)]
        [Delayed(true)]
        public byte[] FileByte
        {
            get { return GetDelayedPropertyValue<byte[]>(nameof(FileByte)); }
            set { SetDelayedPropertyValue<byte[]>(nameof(FileByte), value); }
        }

        public override string ToString()
        {
            return FullFileName;
        }

        public bool WriteFile(string path, bool isTempFile = false)
        {

            if (!string.IsNullOrWhiteSpace(path))
            {
                if (isTempFile)
                {
                    System.IO.File.WriteAllBytes(path, this.FileByte);
                    _ = $@"Файл {FullFileName} успешно сохранен в {path}";
                    return true;
                }

                if (Directory.Exists(path))
                {
                    var fullPath = $"{path}\\{FullFileName}";

                    if (!System.IO.File.Exists(fullPath))
                    {
                        System.IO.File.WriteAllBytes(fullPath, this.FileByte);
                        _ = $@"Файл {FullFileName} успешно сохранен в {path}";
                        return true;
                    }
                    else
                    {
                    }
                }
                else
                {
                }
            }
            else
            {
            }

            return false;
        }

        public Encoding GetFileEncoding(string fullPathToFile)
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
    }
}
