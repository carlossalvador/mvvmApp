using app.Model;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace app.Data
{
    class UsersTable
    {
        private DBConnect dbConn = new DBConnect();
        private String table = "users";

        public List<User> Select()
        {
            List<User> users = new List<User>();
            try
            {
                string query = "SELECT * FROM " + table;

                if (dbConn.OpenConnection() == true)
                {
                    MySqlCommand cmd = new MySqlCommand(query, dbConn.serverConn);
                    MySqlDataReader dataReader = cmd.ExecuteReader();

                    while (dataReader.Read())
                    {
                        users.Add(new User{
                            ID = Int32.Parse(dataReader["ID"].ToString()),
                            nombre = dataReader["nombre"].ToString(),
                            password = dataReader["password"].ToString(),
                            fecha = DateTime.Parse(dataReader["fecha"].ToString())
                        });
                    }

                    dataReader.Close();
                    dbConn.CloseConnection();

                    return users;
                }
                else
                {
                    MessageBox.Show("Error en la conexion de DB");
                    return users;
                }
            }
            catch (MySqlException ex)
            {
                switch (ex.Number)
                {
                    case 1146:
                        MessageBox.Show("No hay tabla");
                        CreateDatabaseSchema();
                        break;
                    default:
                        MessageBox.Show(ex.ToString());
                        break;
                }
                return users;
            }

        }

        private void CreateDatabaseSchema()
        {
            
            string query = "CREATE TABLE IF NOT EXISTS `users` (" +
            "`ID` INT AUTO_INCREMENT," +
            "`nombre` VARCHAR(60)," +
            "`password` VARCHAR(255)," +
            "`fecha` DATE," +
            "PRIMARY KEY(ID));";
            MySqlCommand cmd = new MySqlCommand(query, dbConn.serverConn);
            cmd.ExecuteNonQuery();
            dbConn.CloseConnection();
            MessageBox.Show("Se creo la tabla usuarios");
        }
    }
}
