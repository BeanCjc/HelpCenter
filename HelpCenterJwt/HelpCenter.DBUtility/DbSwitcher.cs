using Dapper;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.SQLite;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using HelpCenter.Common.Config;

namespace HelpCenter.DBUtility
{
    public class DbSwitcher
    {
        public static SimpleCRUD.Dialect _dbtype;
        public static IConfiguration Configuration { get; }

        static DbSwitcher()
        {
            _dbtype = SimpleCRUD.Dialect.MySQL;
        }

        public static void SetDialect(SimpleCRUD.Dialect dbType)
        {
            _dbtype = dbType;
            SimpleCRUD.SetDialect(dbType);
        }

        //获取打开连接
        public static IDbConnection GetOpenConnection()
        {
            IDbConnection connection;
            if (_dbtype == SimpleCRUD.Dialect.SQLite)
            {
                connection = new SQLiteConnection(ConfigHelper.GetSectionValue("ConnectionStrings:SQLiteConnectionString"));
                SimpleCRUD.SetDialect(SimpleCRUD.Dialect.SQLite);
            }
            else if (_dbtype == SimpleCRUD.Dialect.MySQL)
            {
                
                connection = new MySqlConnection(ConfigHelper.GetSectionValue("ConnectionStrings:MySQLConnectionString"));
                SimpleCRUD.SetDialect(SimpleCRUD.Dialect.MySQL);
            }
            else
            {
                connection =
                    new SqlConnection(ConfigHelper.GetSectionValue("ConnectionStrings:SQLServerConnectionString"));
                SimpleCRUD.SetDialect(SimpleCRUD.Dialect.SQLServer);
            }

            if (connection.State != ConnectionState.Open)
            {
                connection.Open();
            }
            return connection;
        }
    }
}
