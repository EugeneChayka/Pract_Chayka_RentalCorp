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
using Npgsql;

namespace Pract_Chayka_RentalCorp.Views.Pages
{
    /// <summary>
    /// Логика взаимодействия для Autho.xaml
    /// </summary>
    public partial class Autho : Page
    {
        public Autho()
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
            if(dbCon.State==ConnectionState.Closed)
            dbCon.Open();
        }

        
        private void btnEnter_Click(object sender, RoutedEventArgs e)
        {
            try {
                dbCmd = new NpgsqlCommand();
                dbCmd.Connection = dbCon;
                dbCmd.CommandText = $"Select employees.idPost from employees join autho on employees.idemp=autho.idemp where autho.Login = '{txtbLogin.Text}' and autho.password ='{pswbPassword.Password}'";
                object result = dbCmd.ExecuteScalar();
                if(result != null)
                {
                    int idPost = (int)result;

                    switch (idPost)
                    {
                        case 1:
                            NavigationService.Navigate(new Views.Pages.Admin());
                            break;

                        case 2:
                            NavigationService.Navigate(new Views.Pages.Director());
                            break;
                        case 3:
                            NavigationService.Navigate(new Views.Pages.Manager());
                            break;

                        case 4:
                            NavigationService.Navigate(new Views.Pages.Accountant());
                            break;
                    }
                    dbCon.Close();
                }
                else 
               MessageBox.Show("Нет пользователя с такими данными");
            } catch (Exception ex) { MessageBox.Show($"Ошибка: {ex.Message}"); }
            
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            connection();
        }
    }
}
