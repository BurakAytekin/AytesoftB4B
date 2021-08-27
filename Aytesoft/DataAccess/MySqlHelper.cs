using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Data;

namespace DataAccess
{
    public class MySqlHelper
    {
        private static string GetConnectionString()
        {
            return ConfigurationManager.ConnectionStrings["MySQLConnectionString"].ConnectionString;
        }

        public static MySqlCommand GenerateCommand(string procedurename)
        {
            MySqlCommand cmd = new MySqlCommand(procedurename, new MySqlConnection(GetConnectionString()));
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection.Open();
            return cmd;
        }
    }
}
