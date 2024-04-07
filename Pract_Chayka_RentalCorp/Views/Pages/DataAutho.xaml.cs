using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
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

namespace Pract_Chayka_RentalCorp.Views.Pages
{
    /// <summary>
    /// Логика взаимодействия для DataAutho.xaml
    /// </summary>
    public partial class DataAutho : Page
    {
        public DataAutho()
        {
            InitializeComponent();
        }
        string dbStrConnection = "Server=localhost;Port=5432;User Id=postgres;Password=123;Database=CarRentalCompany";
        NpgsqlConnection dbCon;
        NpgsqlCommand dbCmd;
        private void connection()
        {
            dbCon = new NpgsqlConnection();
            dbCon.ConnectionString = dbStrConnection;
            if (dbCon.State == ConnectionState.Closed)
                dbCon.Open();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            connection();
            dbCmd = new NpgsqlCommand();
            dbCmd.Connection = dbCon;
            dbCmd.CommandText = $"Select Login,password from Autho;";
            NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(dbCmd);
            DataTable dataTable = new DataTable();
            adapter.Fill(dataTable);

            datagridAutho.ItemsSource = dataTable.DefaultView;
        }

        private void btnback_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}
