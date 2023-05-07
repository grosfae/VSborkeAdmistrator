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
using System.Windows.Shapes;
using VSborkeAdmistrator.Components;

namespace VSborkeAdmistrator.Windows
{
    /// <summary>
    /// Логика взаимодействия для RejectOrderWindow.xaml
    /// </summary>
    public partial class RejectOrderWindow : Window
    {
        Order contextOrder;
        public RejectOrderWindow(Order order)
        {
            InitializeComponent();
            contextOrder = order;
            DataContext= contextOrder;
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
    }
}
