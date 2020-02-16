using System.Windows;
using System.Windows.Controls;
using  static Catrige_DB.GlobalVariables;

namespace Catrige_DB
{
    /// <summary>
    /// Логика взаимодействия для Settings.xaml
    /// </summary>
    public partial class Settings : Page
    {
        public Settings()
        {
            InitializeComponent();
            TextBoxServerIp.Text = ServerIp;
            TextBoxServerLogin.Text = ServerUserId;
            TextBoxServerPassword.Text = ServerPassword;
            TextBoxServerDataBase.Text = ServerDatabase;
        }

        private void CheckBoxAutoAppMode_Checked(object sender, RoutedEventArgs e)
        {
            if (CheckBoxAutoAppMode.IsChecked.Value)
            {
                //label1.Text = @"Автоматический режим включен!";
                AutoAppMode = true;
            }
        }

        private void CheckBoxAutoAppMode_Unchecked(object sender, RoutedEventArgs e)
        {
            if (CheckBoxAutoAppMode.IsChecked.Value == false)
            {
                //label1.Text = @"Автоматический режим включен!";
                AutoAppMode = false;
            }
        }

        private void TextBoxServerIp_TextChanged(object sender, TextChangedEventArgs e)
        {
            ServerIp = TextBoxServerIp.Text;
        }

        private void TextBoxServerLogin_TextChanged(object sender, TextChangedEventArgs e)
        {
            ServerUserId = TextBoxServerLogin.Text;
        }

        private void TextBoxServerDataBase_TextChanged(object sender, TextChangedEventArgs e)
        {
            ServerDatabase = TextBoxServerDataBase.Text;
        }

        private void TextBoxServerPassword_TextChanged(object sender, TextChangedEventArgs e)
        {
            ServerPassword = TextBoxServerPassword.Text;
        }
    }
}
