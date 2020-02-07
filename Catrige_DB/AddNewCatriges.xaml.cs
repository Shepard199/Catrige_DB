using System;
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
        }

        //private readonly string _sql =
        //    "INSERT INTO [Cartridges] (Seal, Model, Organization, Data) VALUES (@Seal, @Model, @Organization, @Data)";

        private void BtAddNew_Click(object sender, EventArgs e)
        {
            if (int.TryParse(TextBoxSeal.Text, out int i) == false || TextBoxModel.Text.Length < 3 ||
                ComboBoxOrgan.Text == "")
                MessageBox.Show("Заполните все поля!");
            else
                using (MySqlConnection connection = new MySqlConnection(ConnectionString.ToString()))
                {
                    connection.Open();
                    MySqlCommand command = new MySqlCommand(
                        $"insert into `Cartridges`(`Seal`,`Model`, `Organization`, `Data`) values('{i}', '{TextBoxModel.Text}', '{ComboBoxOrgan.Text}', '{DpShosCatriges.Text}')",
                        connection);
                    command.ExecuteNonQuery();
                    connection.Close();
                    MessageBox.Show("Картридж успешно добавлен!");
                    //try
                    //{
                    //    int n = cmd.ExecuteNonQuery();
                    //}
                    //catch (SqlException ex)
                    //{
                    //    System.Windows.Forms.MessageBox.Show(ex.Message);
                    //}
                }
        }

        private void BtShowCartridges_Click(object sender, EventArgs e)
        {
            FrameShowCatriges.NavigationService.Navigate(new Uri("ShowRefillLog.xaml", UriKind.Relative));
        }
    }
}