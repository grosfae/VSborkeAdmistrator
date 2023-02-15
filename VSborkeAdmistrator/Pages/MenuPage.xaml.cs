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

namespace VSborkeAdmistrator.Pages
{
    /// <summary>
    /// Логика взаимодействия для MenuPage.xaml
    /// </summary>
    public partial class MenuPage : Page
    {
        public MenuPage()
        {
            InitializeComponent();
        }
        private void ProfileBtn_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            MainFrame.Navigate(new ProfilePage());
        }

        private void AccountListBtn_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void ComputerListBtn_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void InfoShowBtn_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void ConfigBtn_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void ExitBtn_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            App.LoggedUser = null;
            NavigationService.Navigate(new LoginPage());
        }

        private void ProfileBtn_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
