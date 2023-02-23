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
            LvUsers.ItemsSource = App.DB.User.ToList();
            CbGender.ItemsSource = App.DB.Gender.ToList();
            TbCounter.Text = App.DB.User.Count().ToString();
        }

        private void BanBtn_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var selectedUser = (sender as Image).DataContext as User;
        }

        private void ReadMoreBtn_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var selectedUser = (sender as Image).DataContext as User;

            NavigationService.Navigate(new ProfilePage(selectedUser));
        }

        private void CbGender_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void StartDate_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        public void Refresh()
        {
            IEnumerable<User> filterUser =  App.DB.User.ToList();
            if (CbSort.SelectedIndex == 1)
                filterUser = filterUser.OrderBy(x => x.Id);
            else if (CbSort.SelectedIndex == 2)
                filterUser =  filterUser.OrderByDescending(x => x.Id);
        }

        private void CbSort_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
