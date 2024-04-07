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
using System.Xml.Linq;

namespace Pract_Chayka_RentalCorp.Views.Pages
{
    /// <summary>
    /// Логика взаимодействия для DataClients.xaml
    /// </summary>
    public partial class DataClients : Page
    {
        public DataClients()
        {
            InitializeComponent();
        }
        string dbStrConnection = "Server=localhost;Port=5432;User Id=postgres;Password=123;Database=CarRentalCompany";
        NpgsqlConnection dbCon;
        NpgsqlCommand dbCmd;
        string oldname;
        string oldsurname;
        string oldpatronymic;
        string oldphonenumber;
        string oldemail;
        private void connection()
        {
            dbCon = new NpgsqlConnection();
            dbCon.ConnectionString = dbStrConnection;
            if (dbCon.State == ConnectionState.Closed)
                dbCon.Open();
        }
        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            try {
                DataRowView row = (DataRowView)datagridClients.SelectedItem;
                string name = row["name"].ToString();
                string surname = row["surname"].ToString();
                string patronymic = row["patronymic"].ToString();
                string phonenumber = row["phonenumber"].ToString();
                string email = row["email"].ToString();
                dbCmd = new NpgsqlCommand();
                dbCmd.Connection = dbCon;
                dbCmd.CommandText = $"INSERT INTO Clients (Name, Surname, Patronymic, PhoneNumber, Email) VALUES ('{name}', '{surname}', '{patronymic}', '{phonenumber}', '{email}')";


                dbCmd.ExecuteNonQuery();


                dbCmd.CommandText = "Select name, surname, patronymic, phonenumber, email from Clients";
                NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(dbCmd);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);
                datagridClients.ItemsSource = dataTable.DefaultView;
            }
            catch (Exception ex) { MessageBox.Show($"Ошибка: {ex.Message}"); }
        
            
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            try {
                if (datagridClients.SelectedItem != null)
                {
                    DataRowView row = (DataRowView)datagridClients.SelectedItem;
                    string name = row["name"].ToString();
                    string surname = row["surname"].ToString();
                    string patronymic = row["patronymic"].ToString();
                    string phonenumber = row["phonenumber"].ToString();
                    string email = row["email"].ToString();
                    dbCmd = new NpgsqlCommand();
                    dbCmd.Connection = dbCon;
                    dbCmd.CommandText = $"UPDATE Clients SET name ='{name}', surname='{surname}', Patronymic='{patronymic}', PhoneNumber='{phonenumber}', Email='{email}' where name='{oldname}' and surname='{oldsurname}' and patronymic='{oldpatronymic}' and phonenumber='{oldphonenumber}' and email ='{oldemail}'";
                    dbCmd.ExecuteNonQuery();

                    dbCmd.CommandText = "Select name, surname, patronymic, phonenumber, email from Clients";
                    NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(dbCmd);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);
                    datagridClients.ItemsSource = dataTable.DefaultView;
                }
            } catch (Exception ex) { MessageBox.Show($"Ошибка: {ex.Message}"); }
           
        }

        private void btnDel_Click(object sender, RoutedEventArgs e)
        {
            try {
                if (datagridClients.SelectedItem != null)
                {
                    DataRowView row = (DataRowView)datagridClients.SelectedItem;
                    string name = row["name"].ToString();
                    string surname = row["surname"].ToString();
                    string patronymic = row["patronymic"].ToString();
                    string phonenumber = row["phonenumber"].ToString();
                    string email = row["email"].ToString();

                    dbCmd = new NpgsqlCommand();
                    dbCmd.Connection = dbCon;
                    dbCmd.CommandText = $"DELETE FROM Clients WHERE Name = '{name}' AND Surname = '{surname}' AND Patronymic = '{patronymic}' AND phonenumber = '{phonenumber}' AND Email = '{email}'";
                    dbCmd.ExecuteNonQuery();
                    dbCmd.CommandText = $"Select name,surname,patronymic,phonenumber,email from Clients";

                    NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(dbCmd);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);
                    datagridClients.ItemsSource = dataTable.DefaultView;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}");
            }
                
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            connection();
            dbCmd = new NpgsqlCommand();
            dbCmd.Connection = dbCon;
            dbCmd.CommandText = $"Select name,surname,patronymic,phonenumber,email from Clients";
            NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(dbCmd);
            DataTable dataTable = new DataTable();
            adapter.Fill(dataTable);

            datagridClients.ItemsSource = dataTable.DefaultView;
        }

        private void datagridClients_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
          
            object selectedItem = datagridClients.SelectedItem;
            if (selectedItem is DataRowView)
            {
                
                DataRowView row = (DataRowView)datagridClients.SelectedItem;
                oldname = row["name"].ToString();
                oldsurname = row["surname"].ToString();
                oldpatronymic = row["patronymic"].ToString();
                oldphonenumber = row["phonenumber"].ToString();
                oldemail = row["email"].ToString();
            }
           
        }

        private void btnback_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}
