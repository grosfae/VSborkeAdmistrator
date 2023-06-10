using System;
using System.Collections.Generic;
using System.IO;
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
using VSborkeDeliveryMan.Components;

namespace VSborkeDeliveryMan.Pages
{
    /// <summary>
    /// Логика взаимодействия для ProfilePage.xaml
    /// </summary>
    public partial class ProfilePage : Page
    {
        User contextUser;
        public ProfilePage(User user)
        {
            InitializeComponent();
            contextUser = user;
            DataContext = contextUser;

            int progress = 0;
            if (contextUser.Name != String.Empty)
            {
                progress += 17;
            }
            if (contextUser.Surname != String.Empty)
            {
                progress += 17;
            }
            if (contextUser.Gender != null)
            {
                progress += 17;
            }
            if (contextUser.Phone != String.Empty)
            {
                progress += 17;
            }
            if (contextUser.Email != String.Empty)
            {
                progress += 17;
            }
            if (contextUser.Address != String.Empty)
            {
                progress += 17;
            }
            if (progress > 100)
            {
                progress = 100;
            }
            TbCountProgress.Text = $"{progress}%";
            PbProfileProgress.Value = progress;
        }

        private void ProfileEditBtn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new ProfileEditPage(contextUser));
        }

        private void SearchIcon_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

        }

        public void ProfileInfoProgress()
        {
            int progress = 0;
            if (contextUser.Name != String.Empty)
            {
                progress += 17;
            }
            if (contextUser.Surname != String.Empty)
            {
                progress += 17;
            }
            if (contextUser.Gender != null)
            {
                progress += 17;
            }
            if (contextUser.Phone != String.Empty)
            {
                progress += 17;
            }
            if (contextUser.Email != String.Empty)
            {
                progress += 17;
            }
            if (contextUser.Address != String.Empty)
            {
                progress += 17;
            }
            if (progress > 100)
            {
                progress = 100;
            }
            TbCountProgress.Text = $"{progress}%";
            PbProfileProgress.Value = progress;
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            ProfileInfoProgress();
        }


    }
}
