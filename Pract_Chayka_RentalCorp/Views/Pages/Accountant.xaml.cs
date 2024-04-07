using Npgsql;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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
    /// Логика взаимодействия для Accountant.xaml
    /// </summary>
    public partial class Accountant : Page
    {
        public Accountant()
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
            dbCmd.CommandText = $"Select carmodel.name,rental.startdate,rental.enddate,rental.cost,rentalstatus.name,clients.name,clients.surname,clients.patronymic \r\nfrom Rental\r\njoin cars\r\non cars.idcar=rental.idcar\r\njoin carmodel\r\non carmodel.idmodel=cars.idmodel\r\njoin clients\r\non clients.idclient=rental.idclient\r\njoin rentalstatus\r\non rentalstatus.idrentalstatus = rental.idrentalstatus;";
            NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(dbCmd);
            DataTable dataTable = new DataTable();
            adapter.Fill(dataTable);

            DatagridRental.ItemsSource = dataTable.DefaultView;
        }
    }
}
