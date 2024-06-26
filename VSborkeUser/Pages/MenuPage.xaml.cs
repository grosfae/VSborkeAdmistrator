﻿using System;
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
using VSborkeUser.Components;

namespace VSborkeUser.Pages
{
    /// <summary>
    /// Логика взаимодействия для MenuPage.xaml
    /// </summary>
    public partial class MenuPage : Page
    {
        public MenuPage()
        {
            InitializeComponent();
        }
        
        private void ProfileBtn_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new ProfilePage(App.LoggedUser));
        }

        private void AccountBtn_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void ComputerListBtn_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new CasesPage());
        }

        private void ConfigBtn_Click(object sender, RoutedEventArgs e)
        {
           
        }

        private void InfoShowBtn_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void ExitBtn_Click(object sender, RoutedEventArgs e)
        {
            App.LoggedUser = null;
            NavigationService.Navigate(new LoginPage());
        }

        private void FavouriteListBtn_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new FavouriteCaseList());
        }
    }
}
