using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using MySql.Data.MySqlClient;
using System.Data.SqlClient;

namespace FreelancersApp
{
    class Connect
    {

        public readonly MySqlConnection MySQLconnection = new MySqlConnection("server=localhost;port=3306;username=root;database=test");
        public readonly SqlConnection SQLconnection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\DBFreelancers.mdf;Integrated Security=True");


        // MySQL ПОДКЛЮЧЕНИЯ К ОНЛАЙН БАЗЕ!
        public void MySQLopenConnection()
        {
            if (MySQLconnection.State == System.Data.ConnectionState.Closed)
                MySQLconnection.Open();
        }

        public void MySQLcloseConnection()
        {
            if (MySQLconnection.State == System.Data.ConnectionState.Open)
                MySQLconnection.Close();
        }

        public MySqlConnection GetMySQLConnection()
        {
            return MySQLconnection;
        }

         // SQL ПОДКЛЮЧЕНИЯ К ЛОКАЛЬНОЙ БАЗЕ ДАННЫХ!

        public void SQLopenConnection()
        {
            if (SQLconnection.State == System.Data.ConnectionState.Closed)
                SQLconnection.Open();
        }

        public void SQLcloseConnection()
        {
            if (SQLconnection.State == System.Data.ConnectionState.Open)
                SQLconnection.Close();
        }

        public SqlConnection GetSQLConnection()
        {
            return SQLconnection;
        }
    }
}
