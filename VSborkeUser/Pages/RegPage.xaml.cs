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
using VSborkeUser.Components;
using VSborkeUser.Windows;

namespace VSborkeUser.Pages
{
    /// <summary>
    /// Логика взаимодействия для RegPage.xaml
    /// </summary>
    public partial class RegPage : Page
    {
        public RegPage()
        {
            InitializeComponent();
            CbGender.ItemsSource = App.DB.Gender.ToList();

        }

        private void BackBtn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void RegBtn_Click(object sender, RoutedEventArgs e)
        {
            var user = App.DB.User.FirstOrDefault(x => x.Email == TbEmail.Text);
            if (CbGender.SelectedItem == null || TbSurname.Text == String.Empty || TbName.Text == String.Empty || TbEmail.Text == String.Empty || PbPassword.Password == String.Empty || PbSecondPassword.Password == String.Empty)
            {
                CustomMessageBox.Show("Все обязательные поля (*) должны быть заполнены", CustomMessageBox.CustomMessageBoxTitle.Warning, CustomMessageBox.CustomMessageBoxButton.Ok, CustomMessageBox.CustomMessageBoxButton.Нет);
                return;
            }
            if (user != null)
            {
                CustomMessageBox.Show("На эту почту уже есть учетная запись", CustomMessageBox.CustomMessageBoxTitle.Warning, CustomMessageBox.CustomMessageBoxButton.Ok, CustomMessageBox.CustomMessageBoxButton.Нет);
                return;
            }
            if (PbPassword.Password != PbSecondPassword.Password)
            {
                CustomMessageBox.Show("Вы повторили пароль неверно", CustomMessageBox.CustomMessageBoxTitle.Warning, CustomMessageBox.CustomMessageBoxButton.Ok, CustomMessageBox.CustomMessageBoxButton.Нет);
                return;
            }
            App.DB.User.Add(new User() 
            {
                Name = TbName.Text,
                Surname = TbSurname.Text,
                Patronymic = TbPatronymic.Text,
                Email = TbEmail.Text,
                Password = PbPassword.Password,
                Phone = TbPhone.Text,
                Gender = CbGender.SelectedItem as Gender,
                DateRegistration = DateTime.Now,
                IsLogged = false,
                Role = App.DB.Role.FirstOrDefault(x => x.Id == 6)
            });
            App.DB.SaveChanges();
            CustomMessageBox.Show("Новый пользователь зарегистрирован!", CustomMessageBox.CustomMessageBoxTitle.Успешно, CustomMessageBox.CustomMessageBoxButton.Ok, CustomMessageBox.CustomMessageBoxButton.Нет);
            NavigationService.Navigate(new LoginPage());
        }
    }
}
