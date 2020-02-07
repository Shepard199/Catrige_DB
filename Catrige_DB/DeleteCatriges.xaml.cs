using System;
using System.Data;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Threading;
using MySql.Data.MySqlClient;
using static Catrige_DB.GlobalVariables;

namespace Catrige_DB
{
    /// <inheritdoc>
    ///     <cref></cref>
    /// </inheritdoc>
    /// <summary>
    ///     Логика взаимодействия для DeleteCatriges.xaml
    /// </summary>
    public partial class DeleteCatriges : Page
    {
        private int _i;

        public DeleteCatriges()
        {
            InitializeComponent();
            DispatcherTimer timer = new DispatcherTimer
            {
                Interval = new TimeSpan(0, 0, 1) //in Hour, Minutes, Second.
            };
            timer.Tick += Timer_Tick;
            timer.Start();
            DataLoad();
            //CatBdDataGrid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            //CatBdDataGrid.SelectionMode = CatBdDataGrid.SelectionMode.FullRowSelect;
        }

        private void DataLoad()
        {
            MySqlConnection connection = new MySqlConnection(ConnectionString.ToString());
            connection.Open();
            MySqlCommand cmd =
                new MySqlCommand(
                    "Select * from Cartridges", connection);
            MySqlDataAdapter adp = new MySqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            adp.Fill(ds, "LoadDataBinding");
            CatBdDataGrid.DataContext = ds.Tables[0];
            connection.Close();
        }

        private void BtDelete_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(TbSeal.Text))
            {
                using (MySqlConnection connection = new MySqlConnection(ConnectionString.ToString()))
                {
                    connection.Open();
                    MySqlCommand cmd =
                        new MySqlCommand(
                            "DELETE FROM Cartridges Where Seal = @Seal", connection);
                    MySqlDataAdapter adp = new MySqlDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    adp.Fill(ds, "LoadDataBinding");
                    CatBdDataGrid.DataContext = ds.Tables[0];
                    connection.Close();
                    MessageBox.Show("Удаление завершено!");
                    DataLoad();
                }
            }
            else
            {
                LbErrorMessage.Visibility = Visibility.Visible;
                //timer.Enabled = true;
                //timer.Start();
                _i = 0;
            }
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            if (_i < 25)
                if (_i % 2 == 1)
                {
                    LbErrorMessage.Foreground = Brushes.Black; /* ErrorMessage.Visible = false;*/
                    _i++;
                }
                else
                {
                    LbErrorMessage.Foreground = Brushes.Red; /*ErrorMessage.Visible = true;*/
                    _i++;
                }
            else
                LbErrorMessage.Visibility = Visibility.Hidden;
        }
    }
}