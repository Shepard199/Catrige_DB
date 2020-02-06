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
    ///     Логика взаимодействия для ShowRefillLog.xaml
    /// </summary>
    public partial class ShowRefillLog : Page
    {
        private MySqlDataAdapter _adp;
        private DataSet _ds;

        public ShowRefillLog()
        {
            InitializeComponent();
            LoadRefillLog();
            //DgRefillLog.SelectionUnit = DataGridSelectionUnit.FullRow;

            //WindowWidth = MainFrameWidth;
            //WindowHeight = MainFrameHeight;

            //GrMainShowRefillLog.Width = MainFrameWidth;
            //MessageBox.Show(GrMainShowRefillLog.Width.ToString());


            //MessageBox.Show("Current Page Size: " + WindowWidth + "/" + WindowHeight + " Main Frame Size: " + MainFrameWidth + "/" +
            //MainFrameHeight);
        }

        public void LoadRefillLog()
        {
            //var conn = new MySqlConnection(ConnectionString.ToString());
            //conn.Open();
            //var cmd =
            //    new MySqlCommand(
            //        "Select Seal,Model,Organization,Data from Cartridges", conn);
            //var adp = new MySqlDataAdapter(cmd);
            //var ds = new DataSet();
            //adp.Fill(ds, "LoadDataBinding");
            //DgRefillLog.DataContext = ds;
            //conn.Close();

            MySqlConnection connection = new MySqlConnection(ConnectionString.ToString());
            const string sql =
                "SELECT Refill.Order, Cartridges.Seal, Cartridges.Organization, Cartridges.Model, Refill.Accepted, Refill.Filled, Refill.Replaced, Refill.Note " +
                "FROM Refill, Cartridges WHERE Cartridges.Seal = Refill.Seal";
            try
            {
                connection.Open();
                MySqlCommand cmd = new MySqlCommand(sql, connection);
                _adp = new MySqlDataAdapter(cmd);
                _ds = new DataSet();
                _adp.Fill(_ds, "LoadDataBinding");
                DgRefillLog.DataContext = _ds;
                connection.Close();
                MessageBox.Show("Данные обновлены!");
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.ToString());
                connection.Close();
            }
            finally
            {
                if (connection != null)
                    connection.Close();
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
    }
}