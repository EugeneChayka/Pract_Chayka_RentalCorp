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
    /// Логика взаимодействия для DataCar.xaml
    /// </summary>
    public partial class DataCar : Page
    {
        public DataCar()
        {
            InitializeComponent();
        }

        string dbStrConnection = "Server=localhost;Port=5432;User Id=postgres;Password=123;Database=CarRentalCompany";
        NpgsqlConnection dbCon;
        NpgsqlCommand dbCmd;
        string oldidmodel;
        string oldyearrelease;
        string oldidcolor;
        string oldnumber;
        string oldidcarstatus;
        private void connection()
        {
            dbCon = new NpgsqlConnection();
            dbCon.ConnectionString = dbStrConnection;
            if (dbCon.State == ConnectionState.Closed)
                dbCon.Open();
        }
        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (datagridCar.SelectedItem != null)
                {
                    DataRowView row = (DataRowView)datagridCar.SelectedItem;
                    string idmodel = row["idmodel"].ToString();
                    string yearrelease = row["yearrelease"].ToString();
                    string idcolor = row["idcolor"].ToString();
                    string number = row["number"].ToString();
                    string idcarstatus = row["idcarstatus"].ToString();
                    dbCmd = new NpgsqlCommand();
                    dbCmd.Connection = dbCon;
                    dbCmd.CommandText = $"UPDATE cars SET idmodel ='{idmodel}', yearrelease='{yearrelease}', idcolor='{idcolor}', number='{number}', idcarstatus='{idcarstatus}' where idmodel='{oldidmodel}' and yearrelease='{oldyearrelease}' and idcolor='{oldidcolor}' and number='{oldnumber}' and idcarstatus ='{oldidcarstatus}'";
                    dbCmd.ExecuteNonQuery();

                    dbCmd.CommandText = "Select idmodel, yearrelease, idcolor, number, idcarstatus from cars";
                    NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(dbCmd);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);
                    datagridCar.ItemsSource = dataTable.DefaultView;
                }
            }
            catch (Exception ex) { MessageBox.Show($"Ошибка: {ex.Message}"); }
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                DataRowView row = (DataRowView)datagridCar.SelectedItem;
                string idmodel = row["idmodel"].ToString();
                string yearrelease = row["yearrelease"].ToString();
                string idcolor = row["idcolor"].ToString();
                string number = row["number"].ToString();
                string idcarstatus = row["idcarstatus"].ToString();
                dbCmd = new NpgsqlCommand();
                dbCmd.Connection = dbCon;
                dbCmd.CommandText = $"INSERT INTO cars (idmodel, yearrelease, idcolor, number, idcarstatus) VALUES ('{idmodel}', '{yearrelease}', '{idcolor}', '{number}', '{idcarstatus}')";
                dbCmd.ExecuteNonQuery();

                dbCmd.CommandText = "Select idmodel, yearrelease, idcolor, number, idcarstatus from cars";
                NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(dbCmd);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);
                datagridCar.ItemsSource = dataTable.DefaultView;
            }
            catch (Exception ex) { MessageBox.Show($"Ошибка: {ex.Message}"); }
        }

        private void btnDel_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (datagridCar.SelectedItem != null)
                {
                    DataRowView row = (DataRowView)datagridCar.SelectedItem;
                    string idmodel = row["idmodel"].ToString();
                    string yearrelease = row["yearrelease"].ToString();
                    string idcolor = row["idcolor"].ToString();
                    string number = row["number"].ToString();
                    string idcarstatus = row["idcarstatus"].ToString();
                    dbCmd = new NpgsqlCommand();
                    dbCmd.Connection = dbCon;
                    dbCmd.CommandText = $"DELETE FROM cars WHERE idmodel = '{idmodel}' AND yearrelease = '{yearrelease}' AND idcolor = '{idcolor}' AND number = '{number}' AND idcarstatus = '{idcarstatus}'";
                    dbCmd.ExecuteNonQuery();

                    dbCmd.CommandText = "Select idmodel, yearrelease, idcolor, number, idcarstatus from cars";
                    NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(dbCmd);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);
                    datagridCar.ItemsSource = dataTable.DefaultView;
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

        private void datagridClients_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            object selectedItem = datagridCar.SelectedItem;
            if (selectedItem is DataRowView)
            {

                DataRowView row = (DataRowView)datagridCar.SelectedItem;
                oldidmodel = row["idmodel"].ToString();
                oldyearrelease = row["yearrelease"].ToString();
                oldidcolor = row["idcolor"].ToString();
                oldnumber = row["number"].ToString();
                oldidcarstatus = row["idcarstatus"].ToString();
            }
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            connection();
            dbCmd = new NpgsqlCommand();
            dbCmd.Connection = dbCon;
            dbCmd.CommandText = $"Select idmodel, yearrelease, idcolor, number, idcarstatus from cars";
            NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(dbCmd);
            DataTable dataTable = new DataTable();
            adapter.Fill(dataTable);

            datagridCar.ItemsSource = dataTable.DefaultView;
        }
    }
}
