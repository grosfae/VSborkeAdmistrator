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
    /// Логика взаимодействия для ProfilePage.xaml
    /// </summary>
    public partial class ProfilePage : Page
    {
        public ProfilePage()
        {
            InitializeComponent();
            TbName.Text = App.LoggedUser.Name;
            TbSurname.Text = App.LoggedUser.Surname;
            TbPatronymic.Text = App.LoggedUser.Patronymic;
            TbAddress.Text = App.LoggedUser.Address;
            TbEmail.Text = App.LoggedUser.Email;
            TbPhone.Text = App.LoggedUser.Phone;
            TbGender.Text = App.LoggedUser.Gender.Name;
            TbRole.Text = App.LoggedUser.Role.Name;

            int progress = 0;
            if(TbName.Text != String.Empty)
            {
                progress += 17;
            }
            if (TbSurname.Text != String.Empty)
            {
                progress += 17;
            }
            if (TbGender.Text != String.Empty)
            {
                progress += 17;
            }
            if (TbPhone.Text != String.Empty)
            {
                progress += 17;
            }
            if (TbEmail.Text != String.Empty)
            {
                progress += 17;
            }
            if (TbAddress.Text != String.Empty)
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
            
        }

        private void SearchIcon_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

        }
    }
}
