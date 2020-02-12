using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

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
        }

        private void CheckBoxAutoAppMode_Checked(object sender, RoutedEventArgs e)
        {
            if (CheckBoxAutoAppMode.IsChecked.Value)
            {
                //label1.Text = @"Автоматический режим включен!";
                GlobalVariables.AutoAppMode = true;
            }
        }

        private void CheckBoxAutoAppMode_Unchecked(object sender, RoutedEventArgs e)
        {
            if (CheckBoxAutoAppMode.IsChecked.Value == false)
            {
                //label1.Text = @"Автоматический режим включен!";
                GlobalVariables.AutoAppMode = false;
            }
        }
    }
}
