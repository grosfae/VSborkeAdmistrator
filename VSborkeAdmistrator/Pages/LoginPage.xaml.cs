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
    /// Логика взаимодействия для LoginPage.xaml
    /// </summary>
    public partial class LoginPage : Page
    {
        public LoginPage()
        {
            InitializeComponent();
            if (Properties.Settings.Default.Email != null)
                TbEmail.Text = Properties.Settings.Default.Email;
            if (Properties.Settings.Default.Password != null)
                PbPassword.Password = Properties.Settings.Default.Password;
        }

        private void EnterBtn_Click(object sender, RoutedEventArgs e)
        {
            var user = App.DB.User.FirstOrDefault(x => x.Email == TbEmail.Text);
            if (TbEmail.Text == String.Empty & PbPassword.Password == String.Empty)
            {
                MessageBox.Show("Все поля должны быть заполнены");
                return;
            }
            if (TbEmail.Text == String.Empty)
            {
                MessageBox.Show("Электронная почта должна быть заполена");
                return;
            }
            if (user == null)
            {
                MessageBox.Show("Логин неверный");
                return;
            }
            if (user.Password != PbPassword.Password)
            {
                MessageBox.Show("Пароль неверный");
                return;
            }
            if (user.IsBanned == true)
            {
                MessageBox.Show("Пользователь заблокирован");
                return;
            }
            if (SaveCb.IsChecked == true)
            {
                Properties.Settings.Default.Email = TbEmail.Text;
                Properties.Settings.Default.Password = PbPassword.Password;
                Properties.Settings.Default.Save();
            }
            else
            {
                Properties.Settings.Default.Email = null;
                Properties.Settings.Default.Password = null;
                Properties.Settings.Default.Save();
            }
            App.LoggedUser = user;
            NavigationService.Navigate(new MenuPage());
        }
    }
}
