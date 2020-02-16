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
    ///     Логика взаимодействия для AddNew.xaml
    /// </summary>
    public partial class CatridgesOnBalace : Page
    {
        private string _selectedRowIndex;
        private string _selectedSeal;
        public static int LastTableRowNumber;

        public CatridgesOnBalace()
        {
            InitializeComponent();
            DataPickerTodayDate.Text = DateTime.Today.ToShortDateString();
            FillOrganComboBox();
            FillModelComboBox();

            DataLoad();
        }

        private void DataLoad()
        {
            const string strQuery =
                "SELECT cart.Order, cart.Seal, cart.Model, cart.Organization, cart.Info, cart.Data, cart.Status, cart.LSC, cartmod.Vendor, cartmod.Toner, cartmod.ImageUrl FROM cartridges cart INNER JOIN cartridgesmodels cartmod ON cartmod.Model= cart.Model ORDER BY cart.Order";
            try
            {
                using (MySqlConnection connection = new MySqlConnection(ConnectionString.ToString()))
                using (MySqlCommand cmd = connection.CreateCommand())
                {
                    connection.Open();
                    //cmd.CommandText =
                    //    "SELECT cart.Order, cart.Seal, cart.Model, cart.Organization, cart.Info, cart.Data, cart.Status, cart.LSC, cartmod.Vendor, cartmod.Toner, cartmod.ImageUrl FROM cartridges cart INNER JOIN cartridgesmodels cartmod ON cartmod.Model= cart.Model";                    
                    cmd.CommandText = strQuery;
                    MySqlDataAdapter adap = new MySqlDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    adap.Fill(ds, "LoadDataBinding");
                    DataGridCatrigesOnBalance.DataContext = ds;
                    connection.Close();
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

            GetLastTableRowNumber();
        }

        private static void GetLastTableRowNumber()
        {
            using (MySqlConnection con = new MySqlConnection(ConnectionString.ToString()))
            {
                con.Open();
                string query = "SELECT `cartridges`.`Order` FROM `cartridges` ORDER BY `cartridges`.`Order` DESC LIMIT 1";
                using (MySqlCommand sda = new MySqlCommand(query, con))
                {
                    MySqlDataReader data = sda.ExecuteReader();
                    if (data.Read())
                    {
                        ////textBox2.Text = data.GetValue(0).ToString();
                        LastTableRowNumber = (int) data.GetValue(0);
                        //MessageBox.Show(LastTableRowNumber);
                    }
                }
                con.Close();
                con.Dispose();
            }
        }

        private void BtDelete_Click(object sender, EventArgs e)
        {
            string message = $"Удалить картридж с пломбой { _selectedSeal} ?";
            string caption = "Удаление картриджа";
            MessageBoxButton buttons = MessageBoxButton.YesNo;
            MessageBoxImage icon = MessageBoxImage.Question;
            if (MessageBox.Show(message, caption, buttons, icon) == MessageBoxResult.OK)
            {

                using (MySqlConnection connection = new MySqlConnection(ConnectionString.ToString()))
                {
                    connection.Open();
                    if (string.IsNullOrWhiteSpace(TextBoxSealToDelete.Text))
                    {
                        string sql = "DELETE FROM Cartridges WHERE Seal = " + _selectedSeal;
                        MessageBox.Show(@"Вы удалили картридж с пломбой: " + _selectedSeal);
                        MySqlCommand cmd = new MySqlCommand(sql, connection);
                        cmd.ExecuteNonQuery();
                        //DataGridCatrigesOnBalance.Items.RemoveAt(Convert.ToInt32(_selectedRowIndex));
                        DataLoad();
                    }
                    else
                    {
                        const string sql = "DELETE FROM Cartridges Where Seal = @Seal";
                        MySqlCommand cmd = new MySqlCommand(sql, connection);
                        cmd.Parameters.AddWithValue("Seal", TextBoxSealToDelete.Text);
                        cmd.ExecuteNonQuery();
                        MessageBox.Show(@"Удаление завершено!");
                        //DataGridCatrigesOnBalance.Items.RemoveAt(Convert.ToInt32(_selectedRowIndex)); // ???
                        DataLoad(); // Check for work
                    }
                }
            }
        }

        private void BtAddNew_Click(object sender, EventArgs e)
        {
            if (int.TryParse(TextBoxSeal.Text, out int i) == false || string.IsNullOrEmpty(ComboBoxModel.Text) ||
                string.IsNullOrEmpty(ComboBoxOrganizacions.Text))
                MessageBox.Show("Заполните все поля!");
            else
                using (MySqlConnection connection = new MySqlConnection(ConnectionString.ToString()))
                {
                    connection.Open();
                    //проверка на наличие картриджа
                    const string sqltmp = "SELECT Seal FROM Cartridges WHERE Seal=@Seal";
                    MySqlCommand checkCartridgeExist = new MySqlCommand(sqltmp, connection);
                    checkCartridgeExist.Parameters.AddWithValue("@Seal", i);
                    //int cartridgeExist = (int) checkCartridgeExist.ExecuteScalar();
                    int cartridgeExist = Convert.ToInt32(checkCartridgeExist.ExecuteScalar());
                    //проверка на наличие картриджа

                    if (cartridgeExist > 0)
                    {
                        MessageBox.Show($"Картридж с пломбой {TextBoxSeal.Text} уже существует!");
                    }
                    else
                    {
                        //cartridge doesn't exist.
                        const string insertQuery = "INSERT INTO Cartridges (cartridges.Order, Seal, Model, cartridges.Organization, Info, Data) " +
                                                   "VALUES (@Order, @Seal, @Model, @Organization, @Info, @Data)";

                        //connection.Open();
                        MySqlCommand cmd = new MySqlCommand(insertQuery, connection);
                        cmd.Parameters.AddWithValue("@Order", LastTableRowNumber + 1);
                        cmd.Parameters.AddWithValue("@Seal", i);
                        cmd.Parameters.AddWithValue("@Model", ComboBoxModel.Text);
                        cmd.Parameters.AddWithValue("@Organization", ComboBoxOrganizacions.Text);
                        cmd.Parameters.AddWithValue("@Info", TextBoxInfo.Text);
                        cmd.Parameters.AddWithValue("@Data", DataPickerTodayDate.Text);
                        try
                        {
                            cmd.ExecuteNonQuery();
                            MessageBox.Show(@"Картридж успешно добавлен!");
                            DataLoad();
                        }
                        catch (MySqlException ex)
                        {
                            MessageBox.Show(ex.Message);
                        }
                        connection.Close();
                    }
                }
        }

        private void FillOrganComboBox()
        {
            using (MySqlConnection connection = new MySqlConnection(ConnectionString.ToString()))
            {
                MySqlDataAdapter adapter = new MySqlDataAdapter("SELECT Organization FROM Organization ORDER BY Organization",
                    connection);
                connection.Open();
                DataTable dt = new DataTable();
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
            using (MySqlConnection connection = new MySqlConnection(ConnectionString.ToString()))
            {
                MySqlDataAdapter adapter = new MySqlDataAdapter("SELECT Model, Vendor FROM cartridgesmodels ORDER BY Model",
                    connection);
                connection.Open();
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                ComboBoxModel.ItemsSource = dt.DefaultView;
                ComboBoxModel.DisplayMemberPath = "Model";
                //ComboBoxModel.SelectedValuePath = "vendor";
                adapter.Dispose();
                connection.Close();
            }
        }

        private void DgCartriges_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataRowView drv = (DataRowView) DataGridCatrigesOnBalance.SelectedItem;
            //LabelSeal.Content = "Пломба №: ";

            TextBoxSealToDelete.Text = string.Empty;
            if (drv != null)
            {
                string result = drv["Seal"].ToString();
                TextBoxSealToDelete.Text = result;
                _selectedSeal = result;
                _selectedRowIndex = DataGridCatrigesOnBalance.SelectedIndex.ToString();
                //LabelSeal.Content = "Пломба №: " + result;
            }
        }

        private void TxtName_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox t = (TextBox) sender;
            string filter = t.Text;
            if (t.Name == "txtId")
            {
                //MessageBox.Show(filter);
                //blv.Filter = ($"Seal = '{0}'", filter).ToString();
            }
        }

        private void DataGridMenuItemDeleteCartridge_Click(object sender, RoutedEventArgs e)
        {
            //MessageBox.Show(_selectedRowIndex + " " + _selectedSeal);

            string message = $"Удалить картридж с пломбой { _selectedSeal} ?";
            string caption = "Удаление картриджа";
            MessageBoxButton buttons = MessageBoxButton.YesNo;
            MessageBoxImage icon = MessageBoxImage.Question;
            if (MessageBox.Show(message, caption, buttons, icon) == MessageBoxResult.OK)
            {
                using (MySqlConnection connection = new MySqlConnection(ConnectionString.ToString()))
                {
                    connection.Open();
                    const string sql = "DELETE FROM Cartridges Where Seal = @Seal";
                    MySqlCommand cmd = new MySqlCommand(sql, connection);
                    cmd.Parameters.AddWithValue("Seal", _selectedSeal);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show(@"Вы удалили картридж с пломбой: " + _selectedSeal);
                    DataLoad();
                }
            }
        }

        private void ButtonPrintDataGrid_Click(object sender, RoutedEventArgs e)
        {
            string message = "Распечать список?";
            string caption = "Печать списка";
            MessageBoxButton buttons = MessageBoxButton.YesNo;
            MessageBoxImage icon = MessageBoxImage.Question;
            if (MessageBox.Show(message, caption, buttons, icon) == MessageBoxResult.OK)
            {
                PrintDialog printDlg = new PrintDialog();
                printDlg.PrintVisual(DataGridCatrigesOnBalance, "Grid Printing.");
            }
        }
    }


    //class Cartrige
    //{
    //    public string Model { get; set; }
    //    public string Name { get; set; }
    //    public string ImageUrls { get; set; }
    //}
}