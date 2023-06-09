using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using VSborkeUser.Windows;

namespace VSborkeUser.Pages
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
            {
                TbEmail.Text = Properties.Settings.Default.Email;
            }
            if (Properties.Settings.Default.Password != null)
            {
                PbPassword.Password = Properties.Settings.Default.Password;
            }
            
        }

        private void EnterBtn_Click(object sender, RoutedEventArgs e)
        {
            var user = App.DB.User.FirstOrDefault(x => x.Email == TbEmail.Text);
            if (TbEmail.Text == String.Empty & PbPassword.Password == String.Empty)
            {
                CustomMessageBox.Show("Все поля должны быть заполнены", CustomMessageBox.CustomMessageBoxTitle.Warning, CustomMessageBox.CustomMessageBoxButton.Ok, CustomMessageBox.CustomMessageBoxButton.Нет);
                return;
            }
            if (TbEmail.Text == String.Empty)
            {
                CustomMessageBox.Show("Электронная почта должна быть заполена", CustomMessageBox.CustomMessageBoxTitle.Warning, CustomMessageBox.CustomMessageBoxButton.Ok, CustomMessageBox.CustomMessageBoxButton.Нет);
                return;
            }
            if (PbPassword.Password == String.Empty)
            {
                CustomMessageBox.Show("Пароль должен быть заполнен", CustomMessageBox.CustomMessageBoxTitle.Warning, CustomMessageBox.CustomMessageBoxButton.Ok, CustomMessageBox.CustomMessageBoxButton.Нет);
                return;
            }
            if (user == null)
            {
                CustomMessageBox.Show("Такого пользователя не существует", CustomMessageBox.CustomMessageBoxTitle.Warning, CustomMessageBox.CustomMessageBoxButton.Ok, CustomMessageBox.CustomMessageBoxButton.Нет);
                return;
            }
            if (user.Role.Name != "Пользователь")
            {
                CustomMessageBox.Show("В эту учетную запись нельзя войти", CustomMessageBox.CustomMessageBoxTitle.Warning, CustomMessageBox.CustomMessageBoxButton.Ok, CustomMessageBox.CustomMessageBoxButton.Нет);
                return;
            }
            if (user.Password != PbPassword.Password)
            {
                CustomMessageBox.Show("Пароль неверный", CustomMessageBox.CustomMessageBoxTitle.Warning, CustomMessageBox.CustomMessageBoxButton.Ok, CustomMessageBox.CustomMessageBoxButton.Нет);
                return;
            }
            if (user.IsBanned == true)
            {
                CustomMessageBox.Show("Пользователь заблокирован", CustomMessageBox.CustomMessageBoxTitle.Warning, CustomMessageBox.CustomMessageBoxButton.Ok, CustomMessageBox.CustomMessageBoxButton.Нет);
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
