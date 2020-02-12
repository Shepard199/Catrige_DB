using MySql.Data.MySqlClient;

namespace Catrige_DB
{
    internal class GlobalVariables
    {
        public static bool IsAdmin = false;
        public static bool AutoAppMode = false;
        public static string UserStatus;

        //MessageBox.Show($"MySQL version : {connection.ServerVersion}");

        public static string ServerIp = "localhost";
        public static uint ServerPort = 3306;
        public static string UserId = "root";
        public static string Password = "zxcv";
        public static string Database = "Cartridges";

        //public static double MainFrameWidth = -1;
        //public static double MainFrameHeight = -1;

        //public static double MainWindowWidth = -1;
        //public static double MainWindowHeight = -1;


        public static MySqlConnectionStringBuilder ConnectionString = new MySqlConnectionStringBuilder
        {
            Server = ServerIp,
            Port = ServerPort,
            UserID = UserId,
            Password = Password,
            Database = Database
        };

        //private static readonly MySqlConnectionStringBuilder _cs = new MySqlConnectionStringBuilder
        //{
        //    Server = ServerIp,
        //    Port = Convert.ToUInt32(ServerPort),
        //    UserID = UserId,
        //    Password = Password,
        //    Database = Database
        //};

        //public void GetUserStatus()
        //{
        //    if (admin == true) // подключение к базе данных и получение статуса пользователя "[Администратор/Пользователь]"
        //    {
        //        IsAdmin = true;
        //        UserStatus = " режим - [Администратор]";
        //    }
        //    else
        //    {
        //        IsAdmin = false;
        //        UserStatus = " режим - [Пользователь]";
        //    }
        //}
    }
}