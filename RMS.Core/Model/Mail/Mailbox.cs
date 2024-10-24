using DevExpress.Data.Filtering;
using DevExpress.Xpo;
using RMS.Core.Enumerator;
using System;
using System.Diagnostics;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace RMS.Core.Model.Mail
{
    /// <summary>
    /// Почтовый ящик.
    /// </summary>
    public class Mailbox : XPObject
    {
        public Mailbox() { }

        public Mailbox(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();

            if (MailboxSetup is null)
            {
                MailboxSetup = new MailboxSetup(Session);
            }
        }

        /// <summary>
        /// Используется или не используется.
        /// </summary>
        [MemberDesignTimeVisibility(false)]
        public StateMailbox StateMailbox { get; set; }

        /// <summary>
        /// Почтовый адрес.
        /// </summary>
        [DisplayName("Почтовый адрес"), Size(256)]
        public string MailingAddress { get; set; }

        /// <summary>
        /// Пароль.
        /// </summary>
        [MemberDesignTimeVisibility(false), Size(128)]
        public string Password { get; set; }

        /// <summary>
        /// Логин пользователя.
        /// </summary>
        [DisplayName("Логин пользователя"), Size(32)]
        public string Login { get; set; }

        [DisplayName("Комментарий"), Size(1024)]
        public string Comment { get; set; }

        /// <summary>
        /// Почтовый адрес для копий.
        /// </summary>
        [Size(512)]
        [DisplayName("Почтовый адрес")]
        [MemberDesignTimeVisibility(false)]
        public string MailingAddressCopy { get; set; }

        [MemberDesignTimeVisibility(false)]
        public string UserId { get; set; }

        [MemberDesignTimeVisibility(false)]
        public string AccessToken { get; set; }

        /// <summary>
        /// Настойка почтового ящика.
        /// </summary>
        [Aggregated]
        [Persistent]
        [MemberDesignTimeVisibility(false)]
        public MailboxSetup MailboxSetup { get; private set; }

        //private XPCollection<Letter> letters;
        //public XPCollection<Letter> GetLetter(bool isUpdate = false)
        //{
        //    try
        //    {
        //        if (letters is null || isUpdate)
        //        {
        //            letters = new XPCollection<Letter>(Session, new BinaryOperator(nameof(Letter.Mailbox), Oid));                    
        //        }
                
        //        return letters;
        //    }
        //    catch (Exception ex)
        //    {
        //        RMS.Core.Controllers.LoggerController.WriteLog(ex?.ToString());
        //    }

        //    return letters;
        //}
        
        /// <summary>
        /// Шифрование информации.
        /// </summary>
        /// <param name="plainText"></param>
        /// <param name="password"></param>
        /// <param name="salt"></param>
        /// <param name="hashAlgorithm"></param>
        /// <param name="passwordIterations"></param>
        /// <param name="initialVector"></param>
        /// <param name="keySize"></param>
        /// <returns></returns>
        public static string Encrypt(string plainText,
            string password = "ilel@list.ru",
            string salt = "Kosher",
            string hashAlgorithm = "SHA1",
            int passwordIterations = 2,
            string initialVector = "OFRna73m*Csd_2=s@1Y",
            int keySize = 256)
        {
            if (string.IsNullOrEmpty(plainText))
            {
                return default;
            }

            byte[] initialVectorBytes = Encoding.ASCII.GetBytes(initialVector);
            byte[] saltValueBytes = Encoding.ASCII.GetBytes(salt);
            byte[] plainTextBytes = Encoding.UTF8.GetBytes(plainText);

            PasswordDeriveBytes derivedPassword = new PasswordDeriveBytes(password, saltValueBytes, hashAlgorithm, passwordIterations);
            byte[] keyBytes = derivedPassword.GetBytes(keySize / 8);
            RijndaelManaged symmetricKey = new RijndaelManaged();
            symmetricKey.Mode = CipherMode.CBC;

            byte[] cipherTextBytes = null;

            using (ICryptoTransform encryptor = symmetricKey.CreateEncryptor(keyBytes, initialVectorBytes))
            {
                using (MemoryStream memStream = new MemoryStream())
                {
                    using (CryptoStream cryptoStream = new CryptoStream(memStream, encryptor, CryptoStreamMode.Write))
                    {
                        cryptoStream.Write(plainTextBytes, 0, plainTextBytes.Length);
                        cryptoStream.FlushFinalBlock();
                        cipherTextBytes = memStream.ToArray();
                        memStream.Close();
                        cryptoStream.Close();
                    }
                }
            }

            symmetricKey.Clear();
            return Convert.ToBase64String(cipherTextBytes);
        }

        /// <summary>
        /// Дешифрирование информации. 
        /// </summary>
        /// <param name="cipherText"></param>
        /// <param name="password"></param>
        /// <param name="salt"></param>
        /// <param name="hashAlgorithm"></param>
        /// <param name="passwordIterations"></param>
        /// <param name="initialVector"></param>
        /// <param name="keySize"></param>
        /// <returns></returns>
        public static string Decrypt(string cipherText,
            string password = "ilel@list.ru",
            string salt = "Kosher",
            string hashAlgorithm = "SHA1",
            int passwordIterations = 2,
            string initialVector = "OFRna73m*Csd_2=s@1Y",
            int keySize = 256)
        {
            if (string.IsNullOrEmpty(cipherText))
                return string.Empty;

            byte[] initialVectorBytes = Encoding.ASCII.GetBytes(initialVector);
            byte[] saltValueBytes = Encoding.ASCII.GetBytes(salt);
            byte[] cipherTextBytes = Convert.FromBase64String(cipherText);

            PasswordDeriveBytes derivedPassword = new PasswordDeriveBytes(password, saltValueBytes, hashAlgorithm, passwordIterations);
            byte[] keyBytes = derivedPassword.GetBytes(keySize / 8);

            RijndaelManaged symmetricKey = new RijndaelManaged();
            symmetricKey.Mode = CipherMode.CBC;

            byte[] plainTextBytes = new byte[cipherTextBytes.Length];
            int byteCount = 0;

            using (ICryptoTransform decryptor = symmetricKey.CreateDecryptor(keyBytes, initialVectorBytes))
            {
                using (MemoryStream memStream = new MemoryStream(cipherTextBytes))
                {
                    using (CryptoStream cryptoStream = new CryptoStream(memStream, decryptor, CryptoStreamMode.Read))
                    {
                        byteCount = cryptoStream.Read(plainTextBytes, 0, plainTextBytes.Length);
                        memStream.Close();
                        cryptoStream.Close();
                    }
                }
            }

            symmetricKey.Clear();
            return Encoding.UTF8.GetString(plainTextBytes, 0, byteCount);
        }
        
        public override string ToString()
        {
            return MailingAddress;
        }
    }
}
