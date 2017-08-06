using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data;
using ObjectModel;
using System.Data;
using System.Data.Common;
using System.Configuration;
using MySql.Data.MySqlClient;

namespace DataAccessLayer
{
    public class MySqlDAL
    {
        MySqlConnection connection;
        MySqlCommand command;
        public MySqlDAL()
        {
            connection = new MySqlConnection("server=localhost;uid=root;database=personalwebsite");
            command = new MySqlCommand();
            command.Connection = connection;
            connection.Open();
        }

        public int ExecuteNonQuery(string commandName, CommandType commandType, params MySqlParameter[] parameters)
        {
            command.CommandText = commandName;
            command.CommandType = commandType;
            parameters.ToList().ForEach(paramter => { command.Parameters.Add(paramter); });
            return command.ExecuteNonQuery();
        }

        public DataTable ExecuteReader(string commandName, CommandType commandType, params MySqlParameter[] parameters)
        {
            DataTable table = new DataTable();
            command.CommandText = commandName;
            command.CommandType = commandType;
            parameters.ToList().ForEach(paramter => { command.Parameters.Add(paramter); });
            table.Load(command.ExecuteReader());
            return table;
        }

        public MySqlParameter CreateParameter(string paramterName, MySqlDbType paramterType, object paramterValue)
        {
            MySqlParameter parameter = new MySqlParameter();
            parameter.ParameterName = paramterName;
            parameter.Value = paramterValue;
            parameter.MySqlDbType = paramterType;
            return parameter;
        }

        ~MySqlDAL()
        {
            connection.Close();
        }
    }
}
