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
    public partial class AddNew : Page
    {
        public AddNew()
        {
            InitializeComponent();
            DataPickerTodayDate.Text = DateTime.Today.ToShortDateString();
            FillOrganComboBox();
            FillModelComboBox();
        }

        private void BtAddNew_Click(object sender, EventArgs e)
        {
            if (int.TryParse(TextBoxSeal.Text, out var i) == false || ComboBoxModel.Text.Length < 3 ||
                /*ComboBoxOrgan.Text == ""*/ ComboBoxOrgan.Text == string.Empty)
                MessageBox.Show("Заполните все поля!");
            else
                using (var connection = new MySqlConnection(ConnectionString.ToString()))
                {
                    connection.Open();
                    var command = new MySqlCommand(
                        $"insert into `Cartridges`(`Seal`,`Model`, `Organization`, `Data`) values('{i}', '{ComboBoxModel.Text}', '{ComboBoxOrgan.Text}', '{DataPickerTodayDate.Text}')",
                        connection);
                    command.ExecuteNonQuery();
                    connection.Close();
                    MessageBox.Show("Картридж успешно добавлен!");
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
                ComboBoxOrgan.ItemsSource = dt.DefaultView;
                ComboBoxOrgan.DisplayMemberPath = "Organization";
                //ComboBoxOrgan.SelectedValuePath = "Phone";
                adapter.Dispose();
                connection.Close();
            }
        }

        private void FillModelComboBox()
        {
            using (var connection = new MySqlConnection(ConnectionString.ToString()))
            {
                var adapter = new MySqlDataAdapter("SELECT model, vendor FROM cartridgesmodels ORDER BY model",
                    connection);
                connection.Open();
                var dt = new DataTable();
                adapter.Fill(dt);
                ComboBoxModel.ItemsSource = dt.DefaultView;
                ComboBoxModel.DisplayMemberPath = "model";
                //ComboBoxModel.SelectedValuePath = "vendor";
                adapter.Dispose();
                connection.Close();
            }
        }

        private void BtShowCartridges_Click(object sender, EventArgs e)
        {
            FrameShowCatriges.NavigationService.Navigate(new Uri("ShowRefillLog.xaml", UriKind.Relative));
        }
    }
}