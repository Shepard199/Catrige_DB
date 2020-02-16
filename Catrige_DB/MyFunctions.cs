using System.Data.SqlClient;
using MySql.Data.MySqlClient;
using static Catrige_DB.GlobalVariables;

namespace Catrige_DB
{
    class MyFunctions
    {
        public static void GetConnectionsStatus()
        {
            using (MySqlConnection connection = new MySqlConnection(ConnectionString.ToString()))
            {
                try
                {
                    connection.Open();
                }
                catch (SqlException ex)
                {
                    switch (ex.Number)
                    {
                        case 18456: // Can't login
                            // Do something
                            ConnectStatus = ex.Number;
                            break;
                        default:
                            ConnectStatus = 0;
                            break;
                    }
                }
            }
        }
    }
}
