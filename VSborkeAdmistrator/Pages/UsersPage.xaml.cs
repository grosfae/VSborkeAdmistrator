using System;
using System.Collections.Generic;
using System.Data.Entity;
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
    /// Логика взаимодействия для UsersPage.xaml
    /// </summary>
    public partial class UsersPage : Page
    {
        public UsersPage()
        {
            InitializeComponent();
            LvUsers.ItemsSource = App.DB.User.Where(x => x.Id != App.LoggedUser.Id).ToList();
            CbGender.ItemsSource = App.DB.Gender.ToList();
            CbRole.ItemsSource = App.DB.Role.ToList();
            TbCounter.Text = App.DB.User.Count().ToString();

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
            IEnumerable<User> filterUser =  App.DB.User.Where(x => x.Id != App.LoggedUser.Id);
            if (CbSort.SelectedIndex == 0)
                filterUser = filterUser.ToList();
            else if (CbSort.SelectedIndex == 1)
                filterUser = filterUser.OrderBy(x => x.Id).ToList();
            else if (CbSort.SelectedIndex == 2)
                filterUser =  filterUser.OrderByDescending(x => x.Id).ToList();

            if(CbGender.SelectedIndex == 0)
            {
                filterUser = filterUser.Where(x => x.GenderId == 1).ToList();
            }
            else if (CbGender.SelectedIndex == 1)
            {
                filterUser = filterUser.Where(x => x.GenderId == 2).ToList();
            }

            if (CbRole.SelectedIndex != -1)
            {
                filterUser = filterUser.Where(x => x.Role == CbRole.SelectedItem).ToList();
            }

            if (TbSearch.Text.Length > 0)
            {
                filterUser = filterUser.Where(x => x.Name.ToLower().StartsWith(TbSearch.Text.ToLower()));
            }

            if (CbBanned.IsChecked == true)
            {
                filterUser = filterUser.Where(x => x.IsBanned == true).ToList();
            }
            else
            {
                filterUser = filterUser.Where(x => x.IsBanned == false || x.IsBanned == null).ToList();
                
            }

            if(StartDate.SelectedDate != null)
            {
                filterUser = filterUser.Where(x => x.DateRegistration >= StartDate.SelectedDate).ToList();
            }
            if (EndDate.SelectedDate != null)
            {
                filterUser = filterUser.Where(x => x.DateRegistration <= EndDate.SelectedDate).ToList();
            }
            LvUsers.ItemsSource = filterUser.ToList();
            TbCounter.Text = filterUser.Count().ToString();
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
            CbGender.SelectedItem= null;
            CbRole.SelectedItem = null;
            CbSort.SelectedItem= null;
            TbSearch.Text = string.Empty;
            StartDate.Text = string.Empty;
            EndDate.Text = string.Empty;
            CbBanned.IsChecked = false;
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
    }
}
