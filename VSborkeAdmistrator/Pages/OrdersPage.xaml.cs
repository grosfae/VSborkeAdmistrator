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
    /// Логика взаимодействия для OrdersPage.xaml
    /// </summary>
    public partial class OrdersPage : Page
    {
        int maxPage = 0;
        public OrdersPage()
        {
            InitializeComponent();
        }
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            maxPage = App.DB.Order.Count(x => x.IsReject == true);
            Refresh();
        }
        int numberPage = 0;
        int count = 10;

        int fakePage = 1;

        private void BtnFirstPage_Click(object sender, RoutedEventArgs e)
        {
            numberPage = 0;
            Refresh();
            fakePage = 1;
            GeneratePageNumbers();
        }

        private void BtnPreviousPage_Click(object sender, RoutedEventArgs e)
        {
            numberPage--;
            fakePage--;
            if (numberPage < 0)
                numberPage = 0;
            if (fakePage < 1)
                fakePage = 1;
            Refresh();
            GeneratePageNumbers();
        }

        private void BtnNextPage_Click(object sender, RoutedEventArgs e)
        {
            numberPage++;
            fakePage++;
            if (numberPage == maxPage)
            {
                numberPage = maxPage - 1;
                fakePage--;
            }

            if (fakePage < maxPage + 1)
            {
                Refresh();
            }
            GeneratePageNumbers();
        }

        private void BtnLastPage_Click(object sender, RoutedEventArgs e)
        {
            numberPage = maxPage - 1;
            fakePage = maxPage;
            Refresh();
            GeneratePageNumbers();
        }
        private void Refresh()
        {
            IEnumerable<Order> filterOrder = App.DB.Order;
            if (CbSort.SelectedIndex == 0)
            {
                filterOrder = filterOrder.OrderBy(x => x.FinallyPrice).ToList();
            }
            else if (CbSort.SelectedIndex == 1)
            {
                filterOrder = filterOrder.OrderByDescending(x => x.FinallyPrice).ToList();
            }

            if (TbSearch.Text.Length > 0)
            {
                filterOrder = filterOrder.Where(x => x.ComputerCase.FullName.Contains(TbSearch.Text.ToLower()));
            }
            if (filterOrder.Count() > count)
            {
                maxPage = filterOrder.Count() / count;
            }
            else
            {
                maxPage = 1;
            }
            if (fakePage > maxPage)
            {
                fakePage = maxPage;
            }

            filterOrder = filterOrder.Skip(count * numberPage).Take(count).ToList();
            LvOrders.ItemsSource = filterOrder.ToList();
            GeneratePageNumbers();

            if (filterOrder.Count() == 0)
            {
                BoxImageGrid.Visibility = Visibility.Visible;
            }
            else
            {
                BoxImageGrid.Visibility = Visibility.Collapsed;
            }
        }

        private void GeneratePageNumbers()
        {
            SPanelPages.Children.Clear();
            for (int i = 1; i <= maxPage; i++)
            {
                RadioButton btn = new RadioButton();
                btn.Content = i;
                btn.Margin = new Thickness(0, 0, 0, 0);
                btn.Click += PageButton_Click;
                Style style = this.FindResource("RadioPage") as Style;
                btn.Style = style;
                SPanelPages.Children.Add(btn);

                if (int.Parse(btn.Content.ToString()) == fakePage)
                {
                    btn.IsChecked = true;
                }
            }
        }
        private void PageButton_Click(object sender, RoutedEventArgs e)
        {
            var b = sender as RadioButton;
            string c = b.Content.ToString();
            int a = int.Parse(c) - 1;
            numberPage = a;
            fakePage = int.Parse(c);
            Refresh();

        }
        private void ResetBtn_Click(object sender, RoutedEventArgs e)
        {
            TbSearch.Text = String.Empty;

            Refresh();
        }

        private void SearchIcon_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Refresh();
        }

        private void TbLinkReview_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

        }
    }
}
