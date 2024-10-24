using DevExpress.Xpo;
using DevExpress.Xpo.DB;
using DevExpress.Xpo.Metadata;
using RMS.Core.Model;
using RMS.Setting.Model.ColorSettings;
using RMS.Setting.Model.CustomerSettings;
using System;
using System.IO;

namespace RMS.UI
{
    /// <summary>
    /// Соединение с базой данных.
    /// </summary>
    public class DatabaseConnection
    {
        /// <summary>
        /// Строка подключения для сессии с базой данных.
        /// </summary>
        public static string WorkConnectionString { get; private set; }

        /// <summary>
        /// Строка подключения для сессии с базой данных пользовательских настроек.
        /// </summary>
        public static string LocalConnectionString { get; private set; }

        public DatabaseConnection()
        {
            GetConnection();
            
            XpoDefault.Session = WorkSession;
            XpoDefault.DataLayer = WorkSession.DataLayer;
        }

        /// <summary>
        /// Получение активных соединений для работы с базами данных.
        /// </summary>
        public void GetConnection()
        {
            GetLocalConnection();
            GetWorkConnection();
        }

        /// <summary>
        /// Получение активных соединений для работы с базами данных.
        /// </summary>
        public async void GetConnectionAsync()
        {
            await GetLocalConnectionAsync();
            await GetWorkConnectionAsync();
        }

        /// <summary>
        /// Получить строку подключения и активную сессию для работы с базой данных пользовательских настроек.  
        /// </summary>
        private void GetLocalConnection(string nameBase = null)
        {
            if (string.IsNullOrWhiteSpace(nameBase))
            {
                nameBase = "LocalSettingsDB.s3db";
            }

            var fullPath = Path.GetFullPath(nameBase);
            LocalConnectionString = SQLiteConnectionProvider.GetConnectionString(fullPath);

            using (var session = GetSessionSimpleDataLayer(LocalConnectionString))
            {
                session.CreateObjectTypeRecords(typeof(CustomerSettings));
                session.CreateObjectTypeRecords(typeof(ColorStatus));
                session.CreateObjectTypeRecords(typeof(set_LocalSettings));
                session.UpdateSchema();
            }

            LocalSession = GetSessionThreadSafeDataLayer(LocalConnectionString);
            LocalSession.Disposed += Session_Disposed;
        }

        /// <summary>
        /// Получить строку подключения и активную сессию для работы с базой данных пользовательских настроек.  
        /// </summary>
        private async System.Threading.Tasks.Task GetLocalConnectionAsync(string nameBase = null)
        {
            await System.Threading.Tasks.Task.Run(() =>
            {
                GetLocalConnection(nameBase);
            });
        }

        /// <summary>
        /// Получить строку подключения и активную сессию для работы с базой данных.  
        /// </summary>
        private void GetWorkConnection()
        {
            cls_XpoSettings oXpoSettingsDB = new cls_XpoSettings();
            string s_db_name = string.Empty; // "SQLite"; // "SQLServer"; // "Access";
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

            using (var session = GetSessionSimpleDataLayer(WorkConnectionString))
            {
                session.CreateObjectTypeRecords();
                session.UpdateSchema();
            }

            WorkSession = GetSessionThreadSafeDataLayer(WorkConnectionString);
            WorkSession.Disposed += Session_Disposed;
        }

        /// <summary>
        /// Получить строку подключения и активную сессию для работы с базой данных.  
        /// </summary>
        private async System.Threading.Tasks.Task GetWorkConnectionAsync()
        {
            await System.Threading.Tasks.Task.Run(() =>
            {
                GetWorkConnection();
            });
        }

        /// <summary>
        /// Активная сессия с базой данных.
        /// </summary>
        public static Session WorkSession { get; set; }

        /// <summary>
        /// Активная локальная сессия с базой данных пользовательских настроек.
        /// </summary>
        public static Session LocalSession { get; private set; }
        
        /// <summary>
        /// Активный пользователь системы.
        /// </summary>
        public static User User { get; private set; }

        /// <summary>
        /// Запомнить активного пользователя.
        /// </summary>
        /// <param name="oid">Уникальный идентификатор пользователя.</param>
        public static async System.Threading.Tasks.Task RememberWorkUser(int oid)
        {
            User = await WorkSession.GetObjectByKeyAsync<User>(oid, true);
        }

        /// <summary>
        /// Запомнить активного пользователя.
        /// </summary>
        /// <param name="user">Активный пользователь.</param>
        public static async System.Threading.Tasks.Task RememberWorkUser(User user)
        {
            User = await WorkSession.GetObjectByKeyAsync<User>(user.Oid, true);
        }
        
        private void Session_Disposed(object sender, EventArgs e)
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
        /// засейвенный Xpo-connection_string (или на WebService или локальный на SQLServer)
        /// </summary>
        public string Connection_stringSQL { get; set; }

        /// <summary>
        /// Получение сессии
        /// </summary>
        /// <param name="url">если Empty, то используется ранее определенный (oXpo.Connection_string)</param>
        /// <returns>Session</returns>
        public Session GetSessionThreadSafeDataLayer(string connectionString = null)
        {
            if (!string.IsNullOrWhiteSpace(connectionString))
            {
                Connection_stringSQL = connectionString;
            }
            
            var connectionPoolString = XpoDefault.GetConnectionPoolString(Connection_stringSQL, -1, -1);

            IDataStore dataStore = XpoDefault.GetConnectionProvider(connectionPoolString, AutoCreateOption.None);
            XPDictionary dictionary = new ReflectionDictionary();

            var threadSafeDataLayer = new ThreadSafeDataLayer(dictionary, dataStore);
            var session = new Session(threadSafeDataLayer, null)
            {
                LockingOption = LockingOption.None,
                TrackPropertiesModifications = true
            };

            session.Disposed += Session_Disposed;
            
            return session;
        }         
        
        private Session GetSessionSimpleDataLayer(string connectionString)
        {
            IDataStore dataStore = XpoDefault.GetConnectionProvider(connectionString, AutoCreateOption.DatabaseAndSchema);
            XPDictionary dictionary = new ReflectionDictionary();
            
            //var simpleDataLayer = new SimpleDataLayer(dataStore);
            var simpleDataLayer = new SimpleDataLayer(dictionary, dataStore);
            var session = new Session(simpleDataLayer, null)
            {
                LockingOption = LockingOption.None,
                TrackPropertiesModifications = true
            };

            return session;
        }

        private static Session _workSession;
        public static string BaseConnectionString { get; set; }
        /// <summary>
        /// Получение сессии
        /// </summary>
        /// <param name="url">если Empty, то используется ранее определенный (oXpo.Connection_string)</param>
        /// <returns>Session</returns>
        public static Session GetWorkSession(string connectionString = null)
        {
            if (_workSession is null)
            {
                if (!string.IsNullOrWhiteSpace(connectionString))
                {
                    BaseConnectionString = connectionString;
                }

                var connectionPoolString = XpoDefault.GetConnectionPoolString(BaseConnectionString, -1, -1);

                IDataStore dataStore = XpoDefault.GetConnectionProvider(connectionPoolString, AutoCreateOption.None);
                XPDictionary dictionary = new ReflectionDictionary();

                var threadSafeDataLayer = new ThreadSafeDataLayer(dictionary, dataStore);
                _workSession = new Session(threadSafeDataLayer, null)
                {
                    LockingOption = LockingOption.None,
                    TrackPropertiesModifications = true
                };
            }

            return _workSession;            
        }
    }
}
