using DevExpress.Xpo;
using DevExpress.Xpo.DB;
using DevExpress.Xpo.Metadata;
using RMS.Core.TG.Core.Models;
using System;
using System.IO;

namespace TelegramBotRMS.Core.Models.Core
{
    /// <summary>
    /// Соединение с базой данных.
    /// </summary>
    public class DatabaseConnection
    {
        /// <summary>
        /// Строка подключения для сессии с базой данных.
        /// </summary>
        private static string WorkConnectionString { get; set; }

        /// <summary>
        /// Получить строку подключения и активную сессию для работы с базой данных.  
        /// </summary>
        private static void GetWorkConnection()
        {
            Settings oXpoSettingsDB = new Settings();
            string s_db_name = string.Empty;
            string s_data_source = string.Empty, s_user_name = string.Empty, s_user_password = string.Empty;
            string s_path_to_settings = $@"{Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location)}\settings\XpoSettingsDB.xml";
            string path_to_db = string.Empty;
            if (oXpoSettingsDB.LoadXpoSettingsDB(s_path_to_settings, s_db_name, s_data_source, s_user_name, s_user_password, path_to_db))
            {
                WorkConnectionString = oXpoSettingsDB.ConnectionString;
            }
            else
            {
                throw new Exception("Error for read Settings File!");
            }
        }

        private static Session session;
        /// <summary>
        /// Активная сессия с базой данных.
        /// </summary>
        public static Session GetWorkSession(Session inSession = null)
        {
            if (inSession != null && inSession.IsConnected)
            {
                session = inSession;
                return session;
            }
            
            if (session != null && session.IsConnected)
            {
                return session;
            }

            GetWorkConnection();

            GetSessionSimpleDataLayer(WorkConnectionString);
            session = GetSessionThreadSafeDataLayer(WorkConnectionString);
            session.Disposed += Session_Disposed;

            return session;
        }
        
        private static void Session_Disposed(object sender, EventArgs e)
        {
            var session = sender as Session;

            if (session != null)
            {
                session = null;
                
                MySql.Data.MySqlClient.MySqlConnection.ClearAllPools();
                System.Data.SqlClient.SqlConnection.ClearAllPools();
            }
        }

        /// <summary>
        /// Получение сессии
        /// </summary>
        /// <returns>Session</returns>
        private static void GetSessionSimpleDataLayer(string connectionString = null)
        {
            if (string.IsNullOrWhiteSpace(connectionString))
            {
                connectionString = WorkConnectionString;
            }

            var connectionPoolString = XpoDefault.GetConnectionPoolString(connectionString, -1, -1);

            IDataStore dataStore = XpoDefault.GetConnectionProvider(connectionPoolString, AutoCreateOption.DatabaseAndSchema);
            XPDictionary dictionary = new ReflectionDictionary();

            var threadSafeDataLayer = new SimpleDataLayer(dictionary, dataStore);
            var session = new Session(threadSafeDataLayer, null)
            {
                LockingOption = LockingOption.None,
                TrackPropertiesModifications = true
            };

            session.CreateObjectTypeRecords(typeof(TGUser));
            session.CreateObjectTypeRecords(typeof(TGMessage));
            session.CreateObjectTypeRecords(typeof(TGLog));
            session.UpdateSchema();
            session.Disposed += Session_Disposed;

            session?.Disconnect();
            session?.Dispose();
        }

        /// <summary>
        /// Получение сессии
        /// </summary>
        /// <returns>Session</returns>
        private static Session GetSessionThreadSafeDataLayer(string connectionString = null)
        {
            if (string.IsNullOrWhiteSpace(connectionString))
            {
                connectionString = WorkConnectionString;
            }
            
            var connectionPoolString = XpoDefault.GetConnectionPoolString(connectionString, -1, -1);

            IDataStore dataStore = XpoDefault.GetConnectionProvider(connectionPoolString, AutoCreateOption.None);
            XPDictionary dictionary = new ReflectionDictionary();

            var threadSafeDataLayer = new ThreadSafeDataLayer(dictionary, dataStore);
            var session = new Session(threadSafeDataLayer, null)
            {
                LockingOption = LockingOption.None,
                TrackPropertiesModifications = true
            };

            XpoDefault.DataLayer = session.DataLayer;
            XpoDefault.Session = session;
            
            session.Disposed += Session_Disposed;
            
            return session;
        }
    }
}
