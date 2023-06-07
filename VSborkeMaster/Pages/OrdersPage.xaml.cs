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
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using VSborkeMaster.Components;
using VSborkeMaster.Windows;


namespace VSborkeMaster.Pages
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
            var dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
            dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick);
            dispatcherTimer.Interval = new TimeSpan(0, 0, 30);
            dispatcherTimer.Start();

            var TimerForReject = new System.Windows.Threading.DispatcherTimer();
            TimerForReject.Tick += new EventHandler(TimerForReject_Tick);
            TimerForReject.Interval = new TimeSpan(0, 0, 1);
            TimerForReject.Start();
        }
        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            // Updating the Label which displays the current second

            Refresh();

            var expiredOrder = App.DB.Order.FirstOrDefault(x => x.DateForConstruct < DateTime.Now & x.StatusId <= 3 & x.IsAcceptedOperator == true);

            if (expiredOrder != null)
            {
                expiredOrder.IsReject = true;
                expiredOrder.Status = App.DB.Status.FirstOrDefault(x => x.Id == 7);
                expiredOrder.ReasonReject = $"Необходимо было изготовить к {expiredOrder.DateForConstruct}.\nЗаказ отменен за истечением срока изготовления.";
                App.DB.SaveChanges();

            }
        }
        private void TimerForReject_Tick(object sender, EventArgs e)
        {
            // Updating the Label which displays the current second
            if (App.RejectOrder == true)
            {
                Refresh();
                App.RejectOrder = false;
            }

        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            maxPage = App.DB.Order.Count();
            if (App.DB.Order.Count() != 0)
            {
                TbStartPrice.Tag = $"от {App.DB.Order.Where(x => x.IsAcceptedOperator == true).Min(x => x.FinallyPrice)}";
                TbEndPrice.Tag = $"до {App.DB.Order.Where(x => x.IsAcceptedOperator == true).Max(x => x.FinallyPrice)}";
                TbStartCount.Tag = $"от {App.DB.Order.Where(x => x.IsAcceptedOperator == true).Min(x => x.GeneralCount)}";
                TbEndCount.Tag = $"до {App.DB.Order.Where(x => x.IsAcceptedOperator == true).Max(x => x.GeneralCount)}";
            }
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
            App.DB = new VSborkeBaseEntities();
            IEnumerable<Order> filterOrder = App.DB.Order.Where(x => x.IsAcceptedOperator == true & x.StatusId != 4 & x.StatusId != 5 & x.StatusId != 6 );
            if (CbSort.SelectedIndex == 0)
            {
                filterOrder = filterOrder.OrderBy(x => x.FinallyPrice).ToList();
            }
            else if (CbSort.SelectedIndex == 1)
            {
                filterOrder = filterOrder.OrderByDescending(x => x.FinallyPrice).ToList();
            }
            foreach (var child in ChecksumStatus_Collection.Children)
            {
                var check = child as CheckBox;
                if (check.IsChecked == true)
                {
                    foreach (Order pc in filterOrder)
                    {
                        filterOrder = filterOrder.Where(x =>
                        x.StatusId == 1 && CbInProcessing.IsChecked == true ||
                        x.StatusId == 2 && CbInAccept.IsChecked == true ||
                        x.StatusId == 3 && CbInProgress.IsChecked == true).ToList();
                    }
                }
            }
            if (StartDate.Text.Length > 0)
            {
                filterOrder = filterOrder.Where(x => x.OrderDate >= StartDate.SelectedDate);
            }
            if (EndDate.Text.Length > 0)
            {
                filterOrder = filterOrder.Where(x => x.OrderDate <= EndDate.SelectedDate);
            }
            if (TbStartPrice.Text.Length > 0)
            {
                filterOrder = filterOrder.Where(x => x.FinallyPrice >= Convert.ToInt64(TbStartPrice.Text)).ToList();
            }
            if (TbEndPrice.Text.Length > 0)
            {
                filterOrder = filterOrder.Where(x => x.FinallyPrice <= Convert.ToInt64(TbEndPrice.Text)).ToList();
            }
            if (TbStartCount.Text.Length > 0)
            {
                filterOrder = filterOrder.Where(x => x.FinallyPrice >= Convert.ToInt64(TbStartCount.Text)).ToList();
            }
            if (TbEndCount.Text.Length > 0)
            {
                filterOrder = filterOrder.Where(x => x.FinallyPrice <= Convert.ToInt64(TbEndCount.Text)).ToList();
            }
            if (TbSearch.Text.Length > 0)
            {
                filterOrder = filterOrder.Where(x => x.ComputerCase.FullName.Contains(TbSearch.Text.ToLower()));
            }
            if (filterOrder.Count() > count)
            {
                if (filterOrder.Count() % count > 0)
                {
                    maxPage = (filterOrder.Count() / count) + 1;
                }
                else
                {
                    maxPage = filterOrder.Count() / count;
                }
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

        private void ApplyBtn_Click(object sender, RoutedEventArgs e)
        {
            numberPage = 0;
            fakePage = 1;
            Refresh();
        }

        private void ResetBtn_Click(object sender, RoutedEventArgs e)
        {
            numberPage = 0;
            fakePage = 1;

            CbSort.SelectedIndex = 0;
            TbSearch.Text = String.Empty;



            TbStartPrice.Text = String.Empty;
            TbEndPrice.Text = String.Empty;

            StartDate.Text = String.Empty;
            EndDate.Text = String.Empty;

            TbStartCount.Text = String.Empty;
            TbEndCount.Text = String.Empty;

            Refresh();
        }

        private void SearchIcon_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            numberPage = 0;
            fakePage = 1;
            Refresh();
        }

        private void AcceptBtnSt_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var selectedOrder = (sender as StackPanel).DataContext as Order;
            selectedOrder.Status = App.DB.Status.FirstOrDefault(x => x.Id == 3);
            selectedOrder.IsAcceptedMaster = true;
            App.DB.SaveChanges();
            Refresh();
        }

        private void RejectBtnSt_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var selectedOrder = (sender as StackPanel).DataContext as Order;
            var dialog = new RejectOrderWindow(selectedOrder);
            dialog.ShowDialog();
        }

        private void TbStartPrice_LostFocus(object sender, RoutedEventArgs e)
        {
            if (TbStartPrice.Text == String.Empty)
            {
                return;
            }
            int value = int.Parse(TbStartPrice.Text);
            if (value < App.DB.Order.Where(x => x.IsAcceptedOperator == true).Min(x => x.FinallyPrice))
            {
                value = App.DB.Order.Where(x => x.IsAcceptedOperator == true).Min(x => x.FinallyPrice);
                TbStartPrice.Text = value.ToString();
            }
            if (value > App.DB.Order.Where(x => x.IsAcceptedOperator == true).Max(x => x.FinallyPrice))
            {
                value = App.DB.Order.Where(x => x.IsAcceptedOperator == true).Max(x => x.FinallyPrice);
                TbStartPrice.Text = value.ToString();
            }
            if (TbEndPrice.Text != String.Empty & TbStartPrice.Text != String.Empty)
            {
                if (int.Parse(TbEndPrice.Text) < int.Parse(TbStartPrice.Text))
                {
                    TbEndPrice.Text = App.DB.Order.Where(x => x.IsAcceptedOperator == true).Max(x => x.FinallyPrice).ToString();
                    TbStartPrice.Text = App.DB.Order.Where(x => x.IsAcceptedOperator == true).Min(x => x.FinallyPrice).ToString();
                }
            }
        }

        private void TbEndPrice_LostFocus(object sender, RoutedEventArgs e)
        {
            if (TbEndPrice.Text == String.Empty)
            {
                return;
            }
            int value = int.Parse(TbEndPrice.Text);
            if (value < App.DB.Order.Where(x => x.IsAcceptedOperator == true).Min(x => x.FinallyPrice))
            {
                value = App.DB.Order.Where(x => x.IsAcceptedOperator == true).Min(x => x.FinallyPrice);
                TbEndPrice.Text = value.ToString();
            }
            if (value > App.DB.Order.Where(x => x.IsAcceptedOperator == true).Max(x => x.FinallyPrice))
            {
                value = App.DB.Order.Where(x => x.IsAcceptedOperator == true).Max(x => x.FinallyPrice);
                TbEndPrice.Text = value.ToString();
            }
            if (TbEndPrice.Text != String.Empty & TbStartPrice.Text != String.Empty)
            {
                if (int.Parse(TbEndPrice.Text) < int.Parse(TbStartPrice.Text))
                {
                    TbEndPrice.Text = App.DB.Order.Where(x => x.IsAcceptedOperator == true).Max(x => x.FinallyPrice).ToString();
                    TbStartPrice.Text = App.DB.Order.Where(x => x.IsAcceptedOperator == true).Min(x => x.FinallyPrice).ToString();
                }
            }

        }

        private void Numbers_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (Regex.IsMatch(e.Text, @"[0-9]") == false)
            {
                e.Handled = true;
            }

        }

        private void ForSpaces_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space)
            {
                e.Handled = true;
            }
        }

        private void TbStartCount_LostFocus(object sender, RoutedEventArgs e)
        {
            if (TbStartCount.Text == String.Empty)
            {
                return;
            }
            int value = int.Parse(TbStartCount.Text);
            if (value < App.DB.Order.Where(x => x.IsAcceptedOperator == true).Min(x => x.GeneralCount))
            {
                value = App.DB.Order.Where(x => x.IsAcceptedOperator == true).Min(x => x.GeneralCount);
                TbStartCount.Text = value.ToString();
            }
            if (value > App.DB.Order.Where(x => x.IsAcceptedOperator == true).Max(x => x.GeneralCount))
            {
                value = App.DB.Order.Where(x => x.IsAcceptedOperator == true).Max(x => x.GeneralCount);
                TbStartCount.Text = value.ToString();
            }
            if (TbEndCount.Text != String.Empty & TbStartCount.Text != String.Empty)
            {
                if (int.Parse(TbEndCount.Text) < int.Parse(TbStartCount.Text))
                {
                    TbEndCount.Text = App.DB.Order.Where(x => x.IsAcceptedOperator == true).Max(x => x.GeneralCount).ToString();
                    TbStartCount.Text = App.DB.Order.Where(x => x.IsAcceptedOperator == true).Min(x => x.GeneralCount).ToString();
                }
            }
        }

        private void TbEndCount_LostFocus(object sender, RoutedEventArgs e)
        {
            if (TbEndCount.Text == String.Empty)
            {
                return;
            }
            int value = int.Parse(TbEndCount.Text);
            if (value < App.DB.Order.Where(x => x.IsAcceptedOperator == true).Min(x => x.GeneralCount))
            {
                value = App.DB.Order.Where(x => x.IsAcceptedOperator == true).Min(x => x.GeneralCount);
                TbEndCount.Text = value.ToString();
            }
            if (value > App.DB.Order.Where(x => x.IsAcceptedOperator == true).Max(x => x.GeneralCount))
            {
                value = App.DB.Order.Where(x => x.IsAcceptedOperator == true).Max(x => x.GeneralCount);
                TbEndCount.Text = value.ToString();
            }
            if (TbEndCount.Text != String.Empty & TbStartCount.Text != String.Empty)
            {
                if (int.Parse(TbEndCount.Text) < int.Parse(TbStartCount.Text))
                {
                    TbEndCount.Text = App.DB.Order.Where(x => x.IsAcceptedOperator == true).Max(x => x.GeneralCount).ToString();
                    TbStartCount.Text = App.DB.Order.Where(x => x.IsAcceptedOperator == true).Min(x => x.GeneralCount).ToString();
                }
            }
        }

        private void TbLinkName_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var selectedOrder = (sender as TextBlock).DataContext as Order;
            NavigationService.Navigate(new ViewCasePage(selectedOrder.ComputerCase));
        }

        private void StUser_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var selectedOrder = (sender as StackPanel).DataContext as Order;
            NavigationService.Navigate(new ProfilePage(selectedOrder.User));
        }

        private void CommentViewBtn_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var selectedOrder = (sender as TextBlock).DataContext as Order;
            var dialog = new OrderCommentWindow(selectedOrder);
            dialog.ShowDialog();
        }

        private void ReasonViewBtn_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var selectedOrder = (sender as StackPanel).DataContext as Order;
            var dialog = new RejectOrderWindow(selectedOrder);
            dialog.ShowDialog();
        }
    }
}
