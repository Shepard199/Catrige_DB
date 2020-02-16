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
            LbErrorMessage.Visibility = Visibility.Hidden;
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
            using (MySqlConnection connection = new MySqlConnection(ConnectionString.ToString()))
            {

                try
                {
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

                catch (MySqlException ex)
                {
                    MessageBox.Show(ex.Message + WindowTitle);
                }
            }

        }

        private void BtDelete_Click(object sender, EventArgs e)
        {
            //if (!string.IsNullOrWhiteSpace(TbSeal.Text))
            //{
            //    using (MySqlConnection connection = new MySqlConnection(ConnectionString.ToString()))
            //    {
            //        connection.Open();
            //        MySqlCommand cmd =
            //            new MySqlCommand(
            //                "DELETE FROM Cartridges Where Seal = @Seal", connection);
            //        MySqlDataAdapter adp = new MySqlDataAdapter(cmd);
            //        DataSet ds = new DataSet();
            //        adp.Fill(ds, "LoadDataBinding");
            //        CatBdDataGrid.DataContext = ds.Tables[0];
            //        connection.Close();
            //        MessageBox.Show("Удаление завершено!");
            //        DataLoad();
            //    }
            //}
            //else
            //{
            //    LbErrorMessage.Visibility = Visibility.Visible;
            //    //timer.Enabled = true;
            //    //timer.Start();
            //    _i = 0;
            //}

            using (var connection = new MySqlConnection(ConnectionString.ToString()))
            {
                connection.Open();
                if (!string.IsNullOrWhiteSpace(TbSeal.Text))
                {
                    const string sql = "DELETE FROM Cartridges Where Seal = @Seal";
                    var cmd = new MySqlCommand(sql, connection);
                    cmd.Parameters.AddWithValue("Seal", TbSeal.Text);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show(@"Удаление завершено!");
                    DataLoad();
                }
                else
                {
                    var i = Convert.ToInt32(dataGridView1.CurrentRow.Cells["Seal"].FormattedValue.ToString());
                    var sql = /*"DELETE FROM Refill WHERE Seal = " + i + */" DELETE FROM Cartridges WHERE Seal = " + i;
                    MessageBox.Show(@"Вы удалили картридж с пломбой: " +
                                    dataGridView1.CurrentRow.Cells["Seal"].FormattedValue);
                    var cmd = new MySqlCommand(sql, connection);
                    cmd.ExecuteNonQuery();
                    dataGridView1.Rows.RemoveAt(dataGridView1.SelectedCells[0].RowIndex);

                    //DataLoad();
                }
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