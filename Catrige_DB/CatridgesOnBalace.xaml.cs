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
    ///     Логика взаимодействия для AddNew.xaml
    /// </summary>
    public partial class CatridgesOnBalace : Page
    {
        private int _i;

        public CatridgesOnBalace()
        {
            InitializeComponent();
            DataPickerTodayDate.Text = DateTime.Today.ToShortDateString();
            LbErrorMessage.Visibility = Visibility.Hidden;
            FillOrganComboBox();
            FillModelComboBox();

            var timer = new DispatcherTimer
            {
                Interval = new TimeSpan(0, 0, 1) //in Hour, Minutes, Second.
            };
            timer.Tick += Timer_Tick;
            timer.Start();

            DataLoad();
        }

        private void DataLoad()
        {
            //MySqlConnection connection = new MySqlConnection(ConnectionString.ToString());
            //connection.Open();
            //MySqlCommand cmd =
            //    new MySqlCommand(
            //        "Select * from Cartridges", connection);
            //MySqlDataAdapter adp = new MySqlDataAdapter(cmd);
            //DataSet ds = new DataSet();
            //adp.Fill(ds, "LoadDataBinding");
            //CatrigesOnBalance.DataContext = ds.Tables[0];
            //connection.Close();

            const string strQuery = "Select * from Cartridges";
            try
            {
                using (var connection = new MySqlConnection(ConnectionString.ToString()))
                {
                    connection.Open();
                    using (var cmd = new MySqlCommand(strQuery, connection))
                    {
                        MySqlDataAdapter adp = new MySqlDataAdapter(cmd);
                        DataSet ds = new DataSet();
                        adp.Fill(ds, "LoadDataBinding");
                        CatrigesOnBalance.DataContext = ds;
                        connection.Close();
                        //DgRefillLog.Items.SortDescriptions.Add(new SortDescription("Order", ListSortDirection.Ascending));
                        //MessageBox.Show("Данные обновлены!");
                    }
                }
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void BtDelete_Click(object sender, EventArgs e)
        {
            using (var connection = new MySqlConnection(ConnectionString.ToString()))
            {
                connection.Open();
                if (string.IsNullOrWhiteSpace(TextBoxSealToDelete.Text))
                {
                    var i = Convert.ToInt32(CatrigesOnBalance.CurrentRow.Cells["Seal"].FormattedValue.ToString());
                    var sql = /*"DELETE FROM Refill WHERE Seal = " + i + */" DELETE FROM Cartridges WHERE Seal = " + i;
                    MessageBox.Show(@"Вы удалили картридж с пломбой: " +
                                    CatrigesOnBalance.CurrentRow.Cells["Seal"].FormattedValue);
                    var cmd = new MySqlCommand(sql, connection);
                    cmd.ExecuteNonQuery();
                    CatrigesOnBalance.Rows.RemoveAt(CatrigesOnBalance.SelectedCells[0].RowIndex);

                    //DataLoad();
                }
                else
                {
                    const string sql = "DELETE FROM Cartridges Where Seal = @Seal";
                    var cmd = new MySqlCommand(sql, connection);
                    cmd.Parameters.AddWithValue("Seal", TextBoxSealToDelete.Text);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show(@"Удаление завершено!");
                    DataLoad();
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

        private void BtAddNew_Click(object sender, EventArgs e)
        {
            if (int.TryParse(TextBoxSeal.Text, out var i) == false || string.IsNullOrEmpty(ComboBoxModel.Text) ||
                string.IsNullOrEmpty(ComboBoxOrganizacions.Text))
                MessageBox.Show("Заполните все поля!");
            else
            {
                using (var connection = new MySqlConnection(ConnectionString.ToString()))
                {
                    //connection.Open();
                    //var command = new MySqlCommand(
                    //    $"insert into `Cartridges`(`Seal`,`Model`, `Organization`, `Data`) values('{i}', '{ComboBoxModel.Text}', '{ComboBoxOrganizacions.Text}', '{DataPickerTodayDate.Text}')",
                    //    connection);
                    //command.ExecuteNonQuery();
                    //connection.Close();
                    //MessageBox.Show("Картридж успешно добавлен!");

                    connection.Open();

                    //проверка на наличие картриджа
                    const string sqltmp = "SELECT Seal FROM Cartridges WHERE Seal=@Seal";
                    MySqlCommand checkCartridgeExist = new MySqlCommand(sqltmp, connection);
                    checkCartridgeExist.Parameters.AddWithValue("@Seal", i);
                    int cartridgeExist = (int)checkCartridgeExist.ExecuteScalar();
                    //проверка на наличие картриджа

                    if (cartridgeExist > 0)
                    {

                        MessageBox.Show(@"Картридж уже добавлен!");

                    }
                    else
                    {
                        //cartridge doesn't exist.
                        string insertQuery = "INSERT INTO `Cartridges`(`Seal`,`Model`, `Organization`, `Data`) VALUES ('{i}', '{ComboBoxModel.Text}', '{ComboBoxOrganizacions.Text}', '{DataPickerTodayDate.Text}')";

                        connection.Open();
                        var cmd = new MySqlCommand(insertQuery, connection);
                        cmd.Parameters.AddWithValue("@Seal", i);
                        cmd.Parameters.AddWithValue("@Model", ComboBoxModel.Text);
                        cmd.Parameters.AddWithValue("@Organization", ComboBoxOrganizacions.Text);
                        cmd.Parameters.AddWithValue("@Info", TextBoxInfo.Text);
                        cmd.Parameters.AddWithValue("@Data", DataPickerTodayDate.Text);
                        try
                        {
                            cmd.ExecuteNonQuery();
                            MessageBox.Show(@"Картридж успешно добавлен!");
                        }
                        catch (MySqlException ex)
                        {
                            MessageBox.Show(ex.Message);
                        }

                        MessageBox.Show(@"Картридж успешно добавлен!");
                        connection.Close();
                    }
                }
            }
        }

        private void FillOrganComboBox()
        {
            using (var connection = new MySqlConnection(ConnectionString.ToString()))
            {
                var adapter = new MySqlDataAdapter("SELECT Organization FROM Organization ORDER BY Organization",
                    connection);
                connection.Open();
                var dt = new DataTable();
                adapter.Fill(dt);
                ComboBoxOrganizacions.ItemsSource = dt.DefaultView;
                ComboBoxOrganizacions.DisplayMemberPath = "Organization";
                //ComboBoxOrgan.SelectedValuePath = "Phone";
                adapter.Dispose();
                connection.Close();
            }
        }

        private void FillModelComboBox()
        {
            using (var connection = new MySqlConnection(ConnectionString.ToString()))
            {
                var adapter = new MySqlDataAdapter("SELECT Model, Vendor FROM cartridgesmodels ORDER BY Model",
                    connection);
                connection.Open();
                var dt = new DataTable();
                adapter.Fill(dt);
                ComboBoxModel.ItemsSource = dt.DefaultView;
                ComboBoxModel.DisplayMemberPath = "Model";
                //ComboBoxModel.SelectedValuePath = "vendor";
                adapter.Dispose();
                connection.Close();
            }
        }
    }
}