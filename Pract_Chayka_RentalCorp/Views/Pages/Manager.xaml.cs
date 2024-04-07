using System;
using System.Collections.Generic;
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
    /// Логика взаимодействия для Manager.xaml
    /// </summary>
    public partial class Manager : Page
    {
        public Manager()
        {
            InitializeComponent();
        }

        private void btnRental_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Views.Pages.DataRental());
        }

        private void btnClient_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Views.Pages.DataClients());
        }
    }
}
