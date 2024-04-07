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
    /// Логика взаимодействия для DataEmp.xaml
    /// </summary>
    public partial class DataEmp : Page
    {
        public DataEmp()
        {
            InitializeComponent();
        }
        string dbStrConnection = "Server=localhost;Port=5432;User Id=postgres;Password=123;Database=CarRentalCompany";
        NpgsqlConnection dbCon;
        NpgsqlCommand dbCmd;
        string oldname;
        string oldsurname;
        string oldpatronymic;
        string oldidPost;
        string oldhiredate;
        private void connection()
        {
            dbCon = new NpgsqlConnection();
            dbCon.ConnectionString = dbStrConnection;
            if (dbCon.State == ConnectionState.Closed)
                dbCon.Open();
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                DataRowView row = (DataRowView)datagridEmp.SelectedItem;
                string name = row["name"].ToString();
                string surname = row["surname"].ToString();
                string patronymic = row["patronymic"].ToString();
                string idpost = row["idpost"].ToString();
                string hiredate = row["hiredate"].ToString();
                dbCmd = new NpgsqlCommand();
                dbCmd.Connection = dbCon;
                dbCmd.CommandText = $"INSERT INTO employees (Name, Surname, Patronymic, idPost, hiredate) VALUES ('{name}', '{surname}', '{patronymic}', '{idpost}', '{hiredate}')";

                dbCmd.ExecuteNonQuery();

                dbCmd.CommandText = "Select name, surname, patronymic, idPost, hiredate from employees";
                NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(dbCmd);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);
                datagridEmp.ItemsSource = dataTable.DefaultView;
            }
            catch (Exception ex) { MessageBox.Show($"Ошибка: {ex.Message}"); }
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (datagridEmp.SelectedItem != null)
                {
                    DataRowView row = (DataRowView)datagridEmp.SelectedItem;
                    string name = row["name"].ToString();
                    string surname = row["surname"].ToString();
                    string patronymic = row["patronymic"].ToString();
                    string idpost = row["idpost"].ToString();
                    string hiredate = row["hiredate"].ToString();
                    dbCmd = new NpgsqlCommand();
                    dbCmd.Connection = dbCon;
                    dbCmd.CommandText = $"UPDATE employees SET name ='{name}', surname='{surname}', Patronymic='{patronymic}', idpost='{idpost}', hiredate='{hiredate}' where name='{oldname}' and surname='{oldsurname}' and patronymic='{oldpatronymic}' and idpost ='{oldidPost}' and hiredate ='{oldhiredate}'";
                    dbCmd.ExecuteNonQuery();

                    dbCmd.CommandText = "Select name, surname, patronymic, idpost, hiredate from employees";
                    NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(dbCmd);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);
                    datagridEmp.ItemsSource = dataTable.DefaultView;
                }
            }
            catch (Exception ex) { MessageBox.Show($"Ошибка: {ex.Message}"); }
        }

        private void btnDel_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (datagridEmp.SelectedItem != null)
                {
                    DataRowView row = (DataRowView)datagridEmp.SelectedItem;
                    string name = row["name"].ToString();
                    string surname = row["surname"].ToString();
                    string patronymic = row["patronymic"].ToString();
                    string idpost = row["idpost"].ToString();
                    string hiredate = row["hiredate"].ToString();
                    dbCmd = new NpgsqlCommand();
                    dbCmd.Connection = dbCon;
                    dbCmd.CommandText = $"DELETE FROM employees WHERE Name = '{name}' AND Surname = '{surname}' AND Patronymic = '{patronymic}' AND idpost = '{idpost}' AND hiredate = '{hiredate}'";
                    dbCmd.ExecuteNonQuery();
                    dbCmd.CommandText = $"Select name,surname,patronymic,idpost,hiredate from employees";

                    NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(dbCmd);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);
                    datagridEmp.ItemsSource = dataTable.DefaultView;
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
            object selectedItem = datagridEmp.SelectedItem;
            if (selectedItem is DataRowView)
            {
                DataRowView row = (DataRowView)datagridEmp.SelectedItem;
                oldname = row["name"].ToString();
                oldsurname = row["surname"].ToString();
                oldpatronymic = row["patronymic"].ToString();
                oldidPost = row["idpost"].ToString();
                oldhiredate = row["hiredate"].ToString();
            }
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            connection();
            dbCmd = new NpgsqlCommand();
            dbCmd.Connection = dbCon;
            dbCmd.CommandText = $"Select name,surname,patronymic,idpost,hiredate from employees;";
            NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(dbCmd);
            DataTable dataTable = new DataTable();
            adapter.Fill(dataTable);

            datagridEmp.ItemsSource = dataTable.DefaultView;
        }
    }
}
