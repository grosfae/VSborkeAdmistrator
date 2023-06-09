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
    /// Логика взаимодействия для ProfilePage.xaml
    /// </summary>
    public partial class ProfilePage : Page
    {
        User contextUser;
        public ProfilePage(User user)
        {
            InitializeComponent();
            contextUser = user;
            DataContext = contextUser;

            int progress = 0;
            if(contextUser.Name != String.Empty)
            {
                progress += 17;
            }
            if (contextUser.Surname != String.Empty)
            {
                progress += 17;
            }
            if (contextUser.Gender != null)
            {
                progress += 17;
            }
            if (contextUser.Phone != String.Empty)
            {
                progress += 17;
            }
            if (contextUser.Email != String.Empty)
            {
                progress += 17;
            }
            if (contextUser.Address != String.Empty)
            {
                progress += 17;
            }
            if (progress > 100)
            {
                progress = 100;
            }
            TbCountProgress.Text = $"{progress}%";
            PbProfileProgress.Value = progress;

            var dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
            dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick);
            dispatcherTimer.Interval = new TimeSpan(0, 0, 30);
            dispatcherTimer.Start();
        }

        private void ProfileEditBtn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new ProfileEditPage(contextUser));
        }

        private void SearchIcon_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

        }

        public void ProfileInfoProgress()
        {
            int progress = 0;
            if (contextUser.Name != String.Empty)
            {
                progress += 17;
            }
            if (contextUser.Surname != String.Empty)
            {
                progress += 17;
            }
            if (contextUser.Gender != null)
            {
                progress += 17;
            }
            if (contextUser.Phone != String.Empty)
            {
                progress += 17;
            }
            if (contextUser.Email != String.Empty)
            {
                progress += 17;
            }
            if (contextUser.Address != String.Empty)
            {
                progress += 17;
            }
            if (progress > 100)
            {
                progress = 100;
            }
            TbCountProgress.Text = $"{progress}%";
            PbProfileProgress.Value = progress;
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            ProfileInfoProgress();
            LvOrders.ItemsSource = App.DB.Order.Where(x => x.UserId == contextUser.Id).ToList();
        }

        private void StBtnDeleteFromHistory_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var selectedOrder = (sender as TextBlock).DataContext as Order;
            selectedOrder.DeleteFromHistory = true;
            App.DB.SaveChanges();
        }

        private void TbLinkReview_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var selectedCase = (sender as TextBlock).DataContext as ComputerCase;
            NavigationService.Navigate(new ViewCasePage(selectedCase));
        }

        private void CommentViewBtn_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var selectedCase = (sender as TextBlock).DataContext as ComputerCase;
            NavigationService.Navigate(new ViewCasePage(selectedCase));
        }
        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            // Updating the Label which displays the current second

            Refresh();

        }

        private void Refresh()
        {
            App.DB = new VSborkeBaseEntities();
            IEnumerable<Order> filterOrder = contextUser.Order.Where(x => x.DeleteFromHistory != true);
            if(OpenedOrderBtn.IsChecked == true)
            {
                filterOrder = filterOrder.Where(x => x.StatusId >= 1 & x.StatusId < 6 );
            }
            if (FinishedOrderBtn.IsChecked == true)
            {
                filterOrder = filterOrder.Where(x => x.StatusId == 6);
            }
            if (CanceledOrderBtn.IsChecked == true)
            {
                filterOrder = filterOrder.Where(x => x.StatusId == 7);
            }
            LvOrders.ItemsSource = filterOrder;
        }

        private void OpenedOrderBtn_Checked(object sender, RoutedEventArgs e)
        {
            FinishedOrderBtn.IsChecked = false;
            CanceledOrderBtn.IsChecked = false;
            Refresh();
        }

        private void AcceptedOrderBtn_Checked(object sender, RoutedEventArgs e)
        {
            OpenedOrderBtn.IsChecked = false;
            CanceledOrderBtn.IsChecked = false;
            Refresh();
        }

        private void CanceledOrderBtn_Checked(object sender, RoutedEventArgs e)
        {
            FinishedOrderBtn.IsChecked = false;
            OpenedOrderBtn.IsChecked = false;
            Refresh();
        }

        private void AllOrderBtn_Click(object sender, RoutedEventArgs e)
        {
            FinishedOrderBtn.IsChecked = false;
            OpenedOrderBtn.IsChecked = false;
            CanceledOrderBtn.IsChecked = false;
            StartDate.Text = String.Empty;
            TbSearch.Text = String.Empty;
            Refresh();
        }

        private void StBtnReject_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DialogResult quest = CustomMessageBox.Show("Вы действительно хотите отменить заказ?", CustomMessageBox.CustomMessageBoxTitle.Подтверждение, CustomMessageBox.CustomMessageBoxButton.Да, CustomMessageBox.CustomMessageBoxButton.Нет);
            if (quest == DialogResult.Yes)
            {
                var selectedOrder = (sender as StackPanel).DataContext as Order;
                selectedOrder.Status = App.DB.Status.FirstOrDefault(x => x.Id == 7);
                selectedOrder.IsReject = true;
                selectedOrder.ReasonReject = $"Заказ отменен пользователем {DateTime.Now}.";
                CustomMessageBox.Show("Заказ успешно отменен", CustomMessageBox.CustomMessageBoxTitle.Успешно, CustomMessageBox.CustomMessageBoxButton.Да, CustomMessageBox.CustomMessageBoxButton.Нет);
                App.DB.SaveChanges();
                Refresh();
            }
        }
    }
}
