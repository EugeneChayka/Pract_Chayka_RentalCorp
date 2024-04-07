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
    /// Логика взаимодействия для DataRental.xaml
    /// </summary>
    public partial class DataRental : Page
    {
        public DataRental()
        {
            InitializeComponent();
        }
        string dbStrConnection = "Server=localhost;Port=5432;User Id=postgres;Password=123;Database=CarRentalCompany";
        NpgsqlConnection dbCon;
        NpgsqlCommand dbCmd;
        string oldidcar;
        string oldstartdate;
        string oldenddate;
        string oldcost;
        string oldidrentalstatus;
        string oldidclient;
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
            dbCmd.CommandText = $"Select idcar,startdate,enddate,cost,idrentalstatus,idclient from Rental;";
            NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(dbCmd);
            DataTable dataTable = new DataTable();
            adapter.Fill(dataTable);

            datagridRental.ItemsSource = dataTable.DefaultView;
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                DataRowView row = (DataRowView)datagridRental.SelectedItem;
                string idcar = row["idcar"].ToString();
                string startdate = row["startdate"].ToString();
                string enddate = row["enddate"].ToString();
                string cost = row["cost"].ToString();
                string idrentalstatus = row["idrentalstatus"].ToString();
                string idclient = row["idclient"].ToString();
                dbCmd = new NpgsqlCommand();
                dbCmd.Connection = dbCon;
                dbCmd.CommandText = $"INSERT INTO rental (idcar, startdate, enddate, cost, idrentalstatus,idclient) VALUES ('{idcar}', '{startdate}', '{enddate}', '{cost}', '{idrentalstatus}','{idclient}')";

                dbCmd.ExecuteNonQuery();

                dbCmd.CommandText = $"Select idcar,startdate,enddate,cost,idrentalstatus,idclient from Rental;";
                NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(dbCmd);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);
                datagridRental.ItemsSource = dataTable.DefaultView;
            }
            catch (Exception ex) { MessageBox.Show($"Ошибка: {ex.Message}"); }
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (datagridRental.SelectedItem != null)
                {
                    DataRowView row = (DataRowView)datagridRental.SelectedItem;
                    string idcar = row["idcar"].ToString();
                    string startdate = row["startdate"].ToString();
                    string enddate = row["enddate"].ToString();
                    string cost = row["cost"].ToString();
                    string idrentalstatus = row["idrentalstatus"].ToString();
                    string idclient = row["idclient"].ToString();
                    dbCmd = new NpgsqlCommand();
                    dbCmd.Connection = dbCon;
                    dbCmd.CommandText = $"UPDATE rental SET idcar ='{idcar}', startdate='{startdate}', enddate='{enddate}', cost='{cost}', idrentalstatus='{idrentalstatus}',idclient='{idclient}' where idcar ='{oldidcar}', startdate='{oldstartdate}', enddate='{oldenddate}', cost='{oldcost}', idrentalstatus='{oldidrentalstatus}',idclient='{oldidclient}'";
                    dbCmd.ExecuteNonQuery();

                    dbCmd.CommandText = $"Select idcar,startdate,enddate,cost,idrentalstatus,idclient from Rental;";
                    NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(dbCmd);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);
                    datagridRental.ItemsSource = dataTable.DefaultView;
                }
            }
            catch (Exception ex) { MessageBox.Show($"Ошибка: {ex.Message}"); }
        }

        private void btnDel_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (datagridRental.SelectedItem != null)
                {
                    DataRowView row = (DataRowView)datagridRental.SelectedItem;
                    string idcar = row["idcar"].ToString();
                    string startdate = row["startdate"].ToString();
                    string enddate = row["enddate"].ToString();
                    string cost = row["cost"].ToString();
                    string idrentalstatus = row["idrentalstatus"].ToString();
                    string idclient = row["idclient"].ToString();
                    dbCmd = new NpgsqlCommand();
                    dbCmd.Connection = dbCon;
                    dbCmd.CommandText = $"DELETE FROM rental WHERE idcar = '{idcar}' AND startdate = '{startdate}' AND enddate = '{enddate}' AND cost = '{cost}' AND idrentalstatus = '{idrentalstatus}' AND idclient='{idclient}'";
                    dbCmd.ExecuteNonQuery();

                    dbCmd.CommandText = $"Select idcar,startdate,enddate,cost,idrentalstatus,idclient from Rental;";
                    NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(dbCmd);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);
                    datagridRental.ItemsSource = dataTable.DefaultView;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}");
            }
        }

        private void btnback_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void datagridRental_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            object selectedItem = datagridRental.SelectedItem;
            if (selectedItem is DataRowView)
            {
               
                DataRowView row = (DataRowView)datagridRental.SelectedItem;
                oldidcar = row["idcar"].ToString();
                oldstartdate = row["startdate"].ToString();
                oldenddate = row["enddate"].ToString();
                oldcost = row["cost"].ToString();
                oldidrentalstatus = row["idrentalstatus"].ToString();
                oldidclient = row["idclient"].ToString();
            }
        }
    }
}
