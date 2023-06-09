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

namespace VSborkeStorager.Windows
{
    /// <summary>
    /// Логика взаимодействия для CustomMessageBox.xaml
    /// </summary>
    public partial class CustomMessageBox : Window
    {
        public CustomMessageBox()
        {
            InitializeComponent();
            Header.MouseLeftButtonDown += new MouseButtonEventHandler(Window_MouseDown);

        }

        static CustomMessageBox customMessageBox;
        static DialogResult result = System.Windows.Forms.DialogResult.No;
        public enum CustomMessageBoxButton
        {
            Ok,
            Нет,
            Да,
            Отмена,
            Confirm
        }
        public enum CustomMessageBoxTitle
        {
            Error,
            Warning,
            Подтверждение,
            Предупреждение,
            Успешно
        }
        public static DialogResult Show(string message, CustomMessageBoxTitle title, CustomMessageBoxButton btnOk, CustomMessageBoxButton btnNo)
        {
            customMessageBox = new CustomMessageBox();
            customMessageBox.txtMessage.Text = message;
            customMessageBox.OkBtn.Content = customMessageBox.GetMessageButton(btnOk);
            customMessageBox.CancelBtn.Content = customMessageBox.GetMessageButton(btnNo);
            customMessageBox.txtTitle.Text = customMessageBox.GetTitle(title);

            switch (title)
            {
                case CustomMessageBoxTitle.Error:
                    customMessageBox.iconMsg.Source = new BitmapImage(new Uri("pack://application:,,,/VSborkeStorager;component/Resources/MessageBoxImages/AlertMessage.png"));
                    break;

                case CustomMessageBoxTitle.Warning:
                    customMessageBox.iconMsg.Source = new BitmapImage(new Uri("pack://application:,,,/VSborkeStorager;component/Resources/MessageBoxImages/WarningMessage.png"));
                    customMessageBox.CancelBtn.Visibility = Visibility.Collapsed;
                    customMessageBox.OkBtn.SetValue(Grid.ColumnSpanProperty, 2);
                    break;
                case CustomMessageBoxTitle.Подтверждение:
                    customMessageBox.iconMsg.Source = new BitmapImage(new Uri("pack://application:,,,/VSborkeStorager;component/Resources/MessageBoxImages/QuestionMessage.png"));
                    break;
                case CustomMessageBoxTitle.Предупреждение:
                    customMessageBox.iconMsg.Visibility = Visibility.Collapsed;
                    customMessageBox.CancelBtn.Visibility = Visibility.Collapsed;
                    customMessageBox.txtMessage.HorizontalAlignment = System.Windows.HorizontalAlignment.Center;
                    customMessageBox.txtMessage.SetValue(Grid.ColumnProperty, 0);
                    customMessageBox.txtMessage.SetValue(Grid.ColumnSpanProperty, 2);
                    customMessageBox.OkBtn.SetValue(Grid.ColumnSpanProperty, 2);
                    break;
                case CustomMessageBoxTitle.Успешно:
                    customMessageBox.iconMsg.Source = new BitmapImage(new Uri("pack://application:,,,/VSborkeStorager;component/Resources/MessageBoxImages/SuccessMessage.png"));
                    customMessageBox.CancelBtn.Visibility = Visibility.Collapsed;
                    customMessageBox.OkBtn.SetValue(Grid.ColumnSpanProperty, 2);
                    break;
            }
            customMessageBox.ShowDialog();
            return result;
        }
        public string GetTitle(CustomMessageBoxTitle value)
        {
            return Enum.GetName(typeof(CustomMessageBoxTitle), value);
        }

        public string GetMessageButton(CustomMessageBoxButton value)
        {
            return Enum.GetName(typeof(CustomMessageBoxButton), value);
        }

        private void CloseButton_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            result = System.Windows.Forms.DialogResult.No;
            customMessageBox.Close();
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void OkBtn_Click(object sender, RoutedEventArgs e)
        {
            result = System.Windows.Forms.DialogResult.Yes;
            customMessageBox.Close();
        }

        private void CancelBtn_Click(object sender, RoutedEventArgs e)
        {
            result = System.Windows.Forms.DialogResult.No;
            customMessageBox.Close();
        }
    }
}
