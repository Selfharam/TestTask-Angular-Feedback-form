using Microsoft.Data.SqlClient;
using System;
using System.Data;
using System.Threading.Tasks;

namespace ExaAngular
{
    public class DbConnect
    {
        public SqlConnection connection;
        
        public DbConnect()
        {
            string connectionString = "Server=DESKTOP-24S5SPT\\MSSQLSERVER1;Database=test;User Id=test;Password=test;Encrypt=False;Trusted_Connection=False";
            connection = new SqlConnection(connectionString);
        }

        public void InsertToSql(string insertString)
        {
            SqlCommand command = new SqlCommand();

            command.CommandText = insertString;
            command.Connection = connection;
            command.Connection.Open();
            command.ExecuteNonQuery();
            command.Connection.Close();
        }

        public bool IsContact(string dataString)//проверка phone + Email
        {
            SqlCommand command = new SqlCommand();

            command.CommandText = dataString;
            command.Connection = connection;
            command.Connection.Open();
            command.ExecuteNonQuery();

            SqlDataReader reader = command.ExecuteReader();
            if (reader.HasRows)
            {
                command.Connection.Close();
                return false;
            }
            command.Connection.Close();
            return true;


        }

        public int amountId(string TableName)//кол-во строк в таблице + 1
        {
            int i = 1;
            SqlCommand command = new SqlCommand();

            command.CommandText = "SELECT * FROM " + TableName;
            command.Connection = connection;
            command.Connection.Open();
            command.ExecuteNonQuery();

            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read()) i++;

            command.Connection.Close();
            return i;


        }

        public int GetUserId(string PE)//
        {
            int id;
            SqlCommand command = new SqlCommand();

            command.CommandText = "SELECT Id FROM Contacts Where " + PE;
            command.Connection = connection;
            command.Connection.Open();
            command.ExecuteNonQuery();

            SqlDataReader reader = command.ExecuteReader();
            reader.Read();
            id = reader.GetInt32(0);
            command.Connection.Close();
            return id;


        }

        public int GetThemeId(string PE)//
        {
            int id;
            SqlCommand command = new SqlCommand();

            command.CommandText = "SELECT Id FROM Themes Where Theme = '" + PE + "'";
            command.Connection = connection;
            command.Connection.Open();
            command.ExecuteNonQuery();

            SqlDataReader reader = command.ExecuteReader();
            reader.Read();
            id = reader.GetInt32(0);
            command.Connection.Close();
            return id;


        }


        public List<string> GetThemes()//
        {

            List<string> Themes = new List<string>();
            SqlCommand command = new SqlCommand();

            command.CommandText = "SELECT * FROM Themes";
            command.Connection = connection;
            command.Connection.Open();
            command.ExecuteNonQuery();

            SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                Themes.Add(reader.GetString(1));
            }

            command.Connection.Close();
            return Themes;
        }
    }
}