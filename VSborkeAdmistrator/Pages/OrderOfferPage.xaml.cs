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
    /// Логика взаимодействия для OrderOfferPage.xaml
    /// </summary>
    public partial class OrderOfferPage : Page
    {
        ComputerCase contextComputerCase;
        public OrderOfferPage(ComputerCase computerCase)
        {
            InitializeComponent();
            contextComputerCase = computerCase;
            DataContext = contextComputerCase;

            
        }

        private void TbPhone_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void TbAdress_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void TbApartment_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
