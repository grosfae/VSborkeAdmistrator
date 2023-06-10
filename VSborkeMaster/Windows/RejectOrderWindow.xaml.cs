using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;
using VSborkeMaster.Components;
using VSborkeMaster.Pages;

namespace VSborkeMaster.Windows
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
            if (contextOrder.ReasonReject != null)
            {
                CancelOrderBtn.Visibility = Visibility.Collapsed;
                TbReason.IsEnabled= false;
            }
            
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

        private void CancelOrderBtn_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(contextOrder.ReasonReject))
            {
                CustomMessageBox.Show("Введите причину отмены", CustomMessageBox.CustomMessageBoxTitle.Предупреждение, CustomMessageBox.CustomMessageBoxButton.Ok, CustomMessageBox.CustomMessageBoxButton.Нет);
                return;
            }
            DialogResult quest = CustomMessageBox.Show("Вы действительно хотите сохранить изменения?", CustomMessageBox.CustomMessageBoxTitle.Подтверждение, CustomMessageBox.CustomMessageBoxButton.Да, CustomMessageBox.CustomMessageBoxButton.Нет);
            if (quest == System.Windows.Forms.DialogResult.Yes)
            {
                contextOrder.IsReject = true;
                contextOrder.IsRejectedMaster = true;
                contextOrder.StatusId = 7;
                contextOrder.ComputerCase.Count += contextOrder.GeneralCount;
                App.DB.SaveChanges();
                CustomMessageBox.Show("Заказ отменен!", CustomMessageBox.CustomMessageBoxTitle.Успешно, CustomMessageBox.CustomMessageBoxButton.Ok, CustomMessageBox.CustomMessageBoxButton.Нет);

                App.RejectOrder = true;
                this.Close();
            }
        }
    }
}
