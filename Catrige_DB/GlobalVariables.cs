using MySql.Data.MySqlClient;

namespace Catrige_DB
{
    internal class GlobalVariables
    {
        public static bool IsAdmin = false;
        public static bool AutoAppMode = false;
        public static string UserStatus;
        public static int ConnectStatus;

        //MessageBox.Show($"MySQL version : {connection.ServerVersion}");

        //public static string ServerIp = "localhost";
        public static string ServerIp = "172.27.0.32";
        public static uint ServerPort = 3306;
<<<<<<< HEAD
        public static string ServerUserId = "root";
        public static string ServerPassword = "zxcv";
        public static string ServerDatabase = "Cartridges";
=======
        public static string UserId = "root";
        //public static string Password = "zxcv";
        public static string Password = "";
        public static string Database = "Cartridges";
>>>>>>> e406b67614d8143bb78224c7738daebea5d443c3

        //public static double MainFrameWidth = -1;
        //public static double MainFrameHeight = -1;

        //public static double MainWindowWidth = -1;
        //public static double MainWindowHeight = -1;


        public static MySqlConnectionStringBuilder ConnectionString = new MySqlConnectionStringBuilder
        {
            Server = ServerIp,
            Port = ServerPort,
            UserID = ServerUserId,
            Password = ServerPassword,
            Database = ServerDatabase
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