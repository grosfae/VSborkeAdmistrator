using Microsoft.Win32;
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
using VSborkeAdmistrator.Components;

namespace VSborkeAdmistrator.Pages
{
    /// <summary>
    /// Логика взаимодействия для ProfileEditPage.xaml
    /// </summary>
    public partial class ProfileEditPage : Page
    {
        User contextUser;
        public ProfileEditPage(User user)
        {
            InitializeComponent();
            CbGender.ItemsSource = App.DB.Gender.ToList();
            contextUser = user;
            DataContext = contextUser;
        }

        private void ProfileSaveBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ProfileCancelBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ProfileEditImageBtn_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new OpenFileDialog();
            if (dialog.ShowDialog().GetValueOrDefault())
            {
                contextUser.ProfileImage = File.ReadAllBytes(dialog.FileName);
                DataContext = null;
                DataContext = contextUser;
            }
        }
    }
}
