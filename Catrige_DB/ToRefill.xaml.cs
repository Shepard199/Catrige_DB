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
    ///     Логика взаимодействия для ToRefill.xaml
    /// </summary>
    public partial class ToRefill : Page
    {
        //private const string Sql = "INSERT INTO [Refill] (Seal, For_repair) VALUES (@Seal, @For_repair)";

        private string _repairStatus = string.Empty;

        public ToRefill()
        {
            InitializeComponent();
            CheckBoxForRepairFix();
            //DataLoad();
            LbTodayData.Content = DateTime.Today.ToShortDateString();
            FrameToRefill.NavigationService.Navigate(new Uri("ShowRefillLog.xaml", UriKind.Relative));
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
                        const string sqltmp = "SELECT Seal FROM Cartridges WHERE Seal=@Seal";
                        MySqlCommand cmdtmp = new MySqlCommand(sqltmp, connection);
                        cmdtmp.Parameters.AddWithValue("@Seal", i);
                        MySqlDataReader reader = cmdtmp.ExecuteReader();
                        if (reader.HasRows) return;
                        reader.Close();
                        MySqlCommand cmd = new MySqlCommand(sql, connection);
                        cmd.Parameters.AddWithValue("@Seal", i);
                        cmd.Parameters.AddWithValue("@Accepted", DateTime.Today.ToShortDateString());
                        cmd.Parameters.AddWithValue("@Note", _repairStatus);
                        cmd.ExecuteNonQuery();
                        MessageBox.Show(@"Картридж успешно добавлен!");
                    }
                }

                catch (MySqlException ex)
                {
                    MessageBox.Show(ex.Message + @" 123");
                }
            }
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
        }

        private void CheckBoxForRepair_Unchecked(object sender, RoutedEventArgs e)
        {
            CheckBoxForRepair.Content = "Ремон не требуется";
            _repairStatus = "Ремон не требуется";
        }

        private void ButtonAddToRefill_Click(object sender, RoutedEventArgs e)
        {
            AddCartridge();
        }

        //private static void DataLoad()
        //{
        //    var connection = new SqlConnection(ConnectionString);
        //    connection.Open();
        //    var cmd = new SqlCommand(Sql, connection);
        //}
    }
}