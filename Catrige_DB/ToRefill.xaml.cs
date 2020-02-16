using System;
using System.Data;
using System.Windows;
using System.Windows.Controls;
using MySql.Data.MySqlClient;
using static Catrige_DB.GlobalVariables;

namespace Catrige_DB
{
    /// <inheritdoc>
    ///     <cref></cref>
    /// </inheritdoc>
    /// <summary>
    ///     Логика взаимодействия для ToRefill.xaml
    /// </summary>
    public partial class ToRefill : Page
    {
        private string _repairStatus = string.Empty;
        private MySqlDataAdapter _adp;
        private DataSet _ds;

        public ToRefill()
        {
            InitializeComponent();
            CheckBoxForRepairFix();
            LoadRefillLog();
            LbTodayData.Content = DateTime.Today.ToShortDateString();
        }

        private void AddCartridge()
        {
            using (MySqlConnection connection = new MySqlConnection(ConnectionString.ToString()))
            {

                try
                {
                    if (!string.IsNullOrWhiteSpace(TextBoxSeal.Text) && TextBoxSeal.Text.Length > 2)
                    {
                        const string sql = "INSERT INTO Refill (Seal, Accepted, Note, Filled) " +
                                           "VALUES (@Seal, @Accepted, @Note, '')";
                        int.TryParse(TextBoxSeal.Text, out int i);
                        connection.Open();
                        //проверка на наличие картриджа

                        const string sqltmp = "SELECT Seal FROM Refill WHERE Seal=@Seal";
                        MySqlCommand checkCartridgeExist = new MySqlCommand(sqltmp, connection);
                        checkCartridgeExist.Parameters.AddWithValue("@Seal", i);
                        int cartridgeExist = (int)checkCartridgeExist.ExecuteScalar();

                        if (cartridgeExist > 0)
                        {
                            //cartridge exist

                            MessageBox.Show(@"Картридж уже добавлен!");

                        }
                        else
                        {
                            //cartridge doesn't exist.

                            var cmd = new MySqlCommand(sql, connection);
                            cmd.Parameters.AddWithValue("@Seal", i);
                            cmd.Parameters.AddWithValue("@Accepted", DateTime.Today.ToShortDateString());
                            if (!string.IsNullOrEmpty(TextBoxForRepair.Text))
                            {
                                cmd.Parameters.AddWithValue("@Note", _repairStatus + ": " + TextBoxForRepair.Text);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@Note", _repairStatus);
                            }
                            cmd.ExecuteNonQuery();
                            MessageBox.Show(@"Картридж успешно добавлен!"); 
                            connection.Close();

                        }
                    }
                }
                catch (MySqlException ex)
                {
                    MessageBox.Show(ex.Message + WindowTitle);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message + WindowTitle);
                }
            }
        }

        public void LoadRefillLog()
        {
            const string strQuery =
                "SELECT Refill.Order, Cartridges.Seal, Cartridges.Organization, Cartridges.Model, Refill.Accepted, Refill.Filled, Refill.Replaced, Refill.Note " +
                "FROM Refill, Cartridges WHERE Cartridges.Seal = Refill.Seal";
            try
            {
                using (MySqlConnection connection = new MySqlConnection(ConnectionString.ToString()))
                {
                    connection.Open();
                    using (MySqlCommand cmd = new MySqlCommand(strQuery, connection))
                    {
                        _adp = new MySqlDataAdapter(cmd);
                        _ds = new DataSet();
                        _adp.Fill(_ds, "LoadDataBinding");
                        DgRefillLog.DataContext = _ds;
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

        private void UpdateDb()
        {
            MySqlCommandBuilder comandbuilder = new MySqlCommandBuilder(_adp);
            _adp.Update(_ds, "LoadDataBinding");
            MessageBox.Show("Данные обновлены!");
        }

        private void UpdateRefillingLog_Click(object sender, RoutedEventArgs e)
        {
            UpdateDb();
            //LoadRefillLog();
        }
        private void CheckBoxForRepairFix()
        {
            if (CheckBoxForRepair.IsChecked != false) return;
            CheckBoxForRepair.IsChecked = true;
            CheckBoxForRepair.IsChecked = false;
        }

        private void CheckBoxForRepair_Checked(object sender, RoutedEventArgs e)
        {
            CheckBoxForRepair.Content = "Требуется ремонт";
            _repairStatus = "Требуется ремонт";
            TextBoxForRepair.IsEnabled = true;
        }

        private void CheckBoxForRepair_Unchecked(object sender, RoutedEventArgs e)
        {
            CheckBoxForRepair.Content = "Ремон не требуется";
            _repairStatus = "Ремон не требуется";
            TextBoxForRepair.IsEnabled = false;
            TextBoxForRepair.Text = string.Empty;
        }

        private void ButtonAddToRefill_Click(object sender, RoutedEventArgs e)
        {
            AddCartridge();
            TextBoxSeal.Text = string.Empty;
            TextBoxForRepair.Text = string.Empty;
        }

        //private static void DataLoad()
        //{
        //    var connection = new SqlConnection(ConnectionString.ToString());
        //    connection.Open();
        //    var cmd = new SqlCommand(Sql, connection);
        //}
    }
}