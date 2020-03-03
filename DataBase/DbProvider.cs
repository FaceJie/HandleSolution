using Dos.ORM;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataBase
{
    public class DbProvider
    {
        internal static DatabaseType databaseType = 0;//默认sql
        internal static string connectionStr = string.Empty;
        internal static string isOpenOrg = string.Empty;
        public static void UseDb(string _databaseType, string _connectionStr,string _isOpenOrg)
        {
            switch (_databaseType)
            {
                case "0":
                    databaseType = DatabaseType.SqlServer;
                    break;
                case "1":
                    databaseType = DatabaseType.MsAccess;
                    break;
                case "2":
                    databaseType = DatabaseType.SqlServer9;
                    break;
                case "3":
                    databaseType = DatabaseType.Oracle;
                    break;
                case "4":
                    databaseType = DatabaseType.Sqlite3;
                    break;
                case "5":
                    databaseType = DatabaseType.MySql;
                    break;
                default:
                    databaseType = DatabaseType.SqlServer;
                    break;
            }
            connectionStr = _connectionStr;
            isOpenOrg = _isOpenOrg;
        }
    }

    public class DbDetailsSetting
    {
        /// <summary>
        /// 数据库类型
        /// </summary>
        public string databaseType { get; set; }

        /// <summary>
        /// 连接字符串
        /// </summary>
        public string connectionStr { get; set; }

        /// <summary>
        /// 是否开启日志
        /// </summary>
        public string isOpenOrg { get; set; }
    }
    public class DbSetting
    {
        public DbDetailsSetting ConnectionString { get; set; }
    }
}
