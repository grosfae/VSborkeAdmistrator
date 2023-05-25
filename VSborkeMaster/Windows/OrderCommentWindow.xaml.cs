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
using System.Windows.Shapes;
using VSborkeMaster.Components;

namespace VSborkeMaster.Windows
{
    /// <summary>
    /// Логика взаимодействия для OrderCommentWindow.xaml
    /// </summary>
    public partial class OrderCommentWindow : Window
    {
        Order contextOrder;
        public OrderCommentWindow(Order order)
        {
            InitializeComponent();
            contextOrder = order;
            DataContext = contextOrder;
            Header.MouseLeftButtonDown += new MouseButtonEventHandler(Window_MouseDown);
        }
        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void CloseButton_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }

        private void BackBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
