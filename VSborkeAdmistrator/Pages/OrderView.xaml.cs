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
using VSborkeAdmistrator.Components;

namespace VSborkeAdmistrator.Pages
{
    /// <summary>
    /// Логика взаимодействия для OrderView.xaml
    /// </summary>
    public partial class OrderView : Page
    {
        Order contextOrder;
        public OrderView(Order order)
        {
            InitializeComponent();
            contextOrder= order;
            DataContext= contextOrder;
        }

        private void TbLinkName_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void AcceptBtnSt_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void RejectBtnSt_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

        }
    }
}
