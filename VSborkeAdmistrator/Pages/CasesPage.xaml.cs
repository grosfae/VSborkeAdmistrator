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
using VSborkeAdmistrator.Components;

namespace VSborkeAdmistrator.Pages
{
    /// <summary>
    /// Логика взаимодействия для CasesPage.xaml
    /// </summary>
    public partial class CasesPage : Page
    {
        public CasesPage()
        {
            InitializeComponent();
            LvUsers.ItemsSource = App.DB.ComputerCase.ToList();

        }
        private void BanBtn_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var selectedUser = (sender as Image).DataContext as User;
            selectedUser.IsBanned = true;
            Refresh();
        }

        private void ReadMoreBtn_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var selectedUser = (sender as Image).DataContext as User;

            NavigationService.Navigate(new ProfilePage(selectedUser));
        }

        private void CbGender_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Refresh();
        }

        private void StartDate_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            Refresh();
        }

        private void Refresh()
        {
            
        }

        private void CbSort_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Refresh();
        }


        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            Refresh();
        }

        private void TbSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            Refresh();
        }

        private void СbBanned_Checked(object sender, RoutedEventArgs e)
        {
            Refresh();
        }

        private void ResetBtn_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void NoBanBtn_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var selectedUser = (sender as Image).DataContext as User;
            selectedUser.IsBanned = false;
            Refresh();
        }

        private void CbRole_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Refresh();
        }

        private void TbLinkName_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var selectedCase = (sender as TextBlock).DataContext as ComputerCase;
            NavigationService.Navigate(new ComputerCaseEditPage(selectedCase));
        }

        private void BuyBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void NotificBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void EditBtn_Click(object sender, RoutedEventArgs e)
        {
            var selectedCase = (sender as Button).DataContext as ComputerCase;
            NavigationService.Navigate(new ComputerCaseEditPage(selectedCase));
        }

        private void DeFavouriteBtn_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
