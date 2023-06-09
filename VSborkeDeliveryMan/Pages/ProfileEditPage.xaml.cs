using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
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
using VSborkeDeliveryMan.Components;
using VSborkeDeliveryMan.Windows;
using static System.Net.WebRequestMethods;

namespace VSborkeDeliveryMan.Pages
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
            string errorMessage = "";
            if (string.IsNullOrWhiteSpace(contextUser.Name))
            {
                errorMessage += "Введите ммя\n";
            }
            if (string.IsNullOrWhiteSpace(contextUser.Surname))
            {
                errorMessage += "Введите фамилию\n";
            }
            if (contextUser.Gender == null)
            {
                errorMessage += "Выберите пол\n";
            }

            if (string.IsNullOrWhiteSpace(errorMessage) == false)
            {
                CustomMessageBox.Show(errorMessage, CustomMessageBox.CustomMessageBoxTitle.Warning, CustomMessageBox.CustomMessageBoxButton.Ok, CustomMessageBox.CustomMessageBoxButton.Нет);
                return;
            }
            DialogResult quest = CustomMessageBox.Show("Вы действительно хотите сохранить изменения?", CustomMessageBox.CustomMessageBoxTitle.Подтверждение, CustomMessageBox.CustomMessageBoxButton.Да, CustomMessageBox.CustomMessageBoxButton.Нет);
            if (quest == DialogResult.Yes)
            {
                if (contextUser.Id == 0)
                {
                    App.DB.User.Add(contextUser);
                }
                App.DB.SaveChanges();
                NavigationService.GoBack();
            }
        }

        private void ProfileCancelBtn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void ProfileEditImageBtn_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new Microsoft.Win32.OpenFileDialog();
            {
                dialog.Filter = "*.png|*.png|*.jpg|*.jpg|*.jpeg|*.jpeg";
            };
            if (dialog.ShowDialog().GetValueOrDefault())
            {
                contextUser.ProfileImage = System.IO.File.ReadAllBytes(dialog.FileName);
                DataContext = null;
                DataContext = contextUser;
            }
        }

        private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (Regex.IsMatch(e.Text, @"[A-zА-я]") == false)
            {
                e.Handled = true;
            }
        }

        private void TextBox_PreviewTextInput_1(object sender, TextCompositionEventArgs e)
        {
            if (Regex.IsMatch(e.Text, @"[0-9]") == false)
            {
                e.Handled = true;
            }
        }
    }
}
