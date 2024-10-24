using System;
using System.Xml;
using System.Xml.XPath;

namespace RMS.UI
{
    class cls_XpoSettings
    {
        public cls_XpoSettings()
        {
            ConnectionString = string.Empty;
            WebServiceFlag = false;
            DataBase = string.Empty;
            DataSource = string.Empty;
            DBName = string.Empty;
            UserName = string.Empty;
            UserPassword = string.Empty;
        }

        public string ConnectionString { get; set; }
        public bool WebServiceFlag { get; set; }
        private string DataBase;
        private string DataSource;
        private string DBName;
        private string UserName;
        private string UserPassword;
        private int? Port;


        public bool LoadXpoSettingsDB(string path_to_xml, string db_name = "", string data_source = "", string user_name = "", string user_password = "", string server_mappath = "", int? port = null)
        {
            bool fl_return = false;
            XmlDocument XMLRead = new XmlDocument();

            try
            {
                try
                {
                    //string str_path_to_xml = Server.MapPath("settings/SettingsBD.xml");
                    XMLRead.Load(path_to_xml); // Set XMLRead object's path value
                    string s_xpath = string.Empty;
                    string fl_ServerMapPath = string.Empty;
                    s_xpath = (db_name == String.Empty) ? "Settings/DataBases/DataBase[@default='true']" : "Settings/DataBases/DataBase[@name='" + db_name + "']";

                    //cls_SimpleLog sl = new cls_SimpleLog();
                    //sl.do_simple_log(null, "server_mappath: " + server_mappath + Environment.NewLine);

                    XmlNodeList XMLItems = XMLRead.SelectNodes(s_xpath); //"Settings/DataBases/DataBase"); // Create instance of XmlNodeList class

                    //sl.do_simple_log(null, "XMLItems: " + XMLItems.Count + Environment.NewLine);

                    foreach (XmlNode node in XMLItems) // Loop Node for the child node items
                    {
                        DataBase = node.Attributes["name"].Value; // xmlAttr.Value;
                        fl_ServerMapPath = node["ServerMapPath"].InnerText;
                        DataSource = ((fl_ServerMapPath == "true") ? server_mappath : string.Empty) + ((data_source == String.Empty) ? node["DataSource"].InnerText : data_source);
                        DBName = node["DBName"].InnerText;
                        ConnectionString = node["ConnectionString"].InnerText;
                        UserName = (user_name == String.Empty) ? node["User"].InnerText : user_name;
                        UserPassword = (user_password == String.Empty) ? node["Password"].InnerText : user_password;

                        if (node["Port"] != null && !string.IsNullOrWhiteSpace(node["Port"].InnerText))
                        {
                            Port = (port == null) ? Convert.ToInt32(node["Port"].InnerText) : port;
                        }
                        
                        break;
                    }

                    if (ConnectionString == String.Empty)
                    {
                        switch (DataBase)
                        {
                            case "SQLServer":
                                //this.ConnectionString = "Data Source={DataSource};Initial Catalog={DBName};Integrated Security=True";
                                if (UserName == String.Empty)
                                    ConnectionString = DevExpress.Xpo.DB.MSSqlConnectionProvider.GetConnectionString("{DataSource}", "{DBName}");
                                else
                                    ConnectionString = DevExpress.Xpo.DB.MSSqlConnectionProvider.GetConnectionString("{DataSource}", "{UserName}", "{UserPassword}", "{DBName}");
                                break;

                            case "MySQL":
                                //this.ConnectionString = "Data Source={DataSource};Initial Catalog={DBName};Integrated Security=True";
                                if (!string.IsNullOrWhiteSpace(UserName))
                                    ConnectionString = DevExpress.Xpo.DB.MySqlConnectionProvider.GetConnectionString("{DataSource}", Convert.ToInt32(Port), "{UserName}", "{UserPassword}", "{DBName}");
                                break;

                            case "SQLite":
                                //ConnectionString = "Data Source={DataSource};Version=3;New=True;Compress=True";
                                if (UserPassword == String.Empty)
                                    ConnectionString = DevExpress.Xpo.DB.SQLiteConnectionProvider.GetConnectionString("{DataSource}");
                                else
                                    ConnectionString = DevExpress.Xpo.DB.SQLiteConnectionProvider.GetConnectionString("{DataSource}", "{UserPassword}");
                                DataSource = System.IO.Path.GetFullPath(DataSource);

                                break;
                            case "Access":
                                ConnectionString = DevExpress.Xpo.DB.AccessConnectionProvider.GetConnectionString("{DataSource}", "{UserName}", "{UserPassword}");

                                DataSource = System.IO.Path.GetFullPath(DataSource);
                                //this.ConnectionString = DevExpress.Xpo.DB.AccessConnectionProvider.GetConnectionString(Server.MapPath("App_Data\\ContactManagement.mdb"));
                                break;

                            case "WebService":
                                ConnectionString = DataSource;
                                WebServiceFlag = true;
                                break;

                            default:
                                break;
                        }
                    }

                    ConnectionString = ConnectionString.Replace("{DataSource}", DataSource);
                    ConnectionString = ConnectionString.Replace("{DBName}", DBName);
                    ConnectionString = ConnectionString.Replace("{UserName}", UserName);
                    ConnectionString = ConnectionString.Replace("{UserPassword}", UserPassword);

                    //sl.do_simple_log(null, "path_to_xml: " + path_to_xml + "\r\nDataBase: " + DataBase + "\r\nconn: " + ConnectionString + "\r\nFrom cls_XpoSettings\r\n");

                    fl_return = true;
                }
                catch (XPathException ex)
                {
                    const string errorMessage = "Ошибка в задании выражения XPath!" + "\r\n" + "Соответствующие данные в документе не найдены!" + "\r\n" + "Попробуйте задать другое выражение!";
                    throw new Exception(errorMessage + Environment.NewLine + ex.Message);
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return fl_return;
        }
    }
}

