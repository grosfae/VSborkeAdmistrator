using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
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
            
            countCase = int.Parse(TbCountCase.Text);
            TbCountUnit.Text = $"{countCase} шт. x";

            pricePerUnitStock = contextComputerCase.PriceDiscount;
            TbPricePerUnit.Text = String.Format("{0:#,#} ₽", pricePerUnitStock);

            pricePerUnitDiscount = contextComputerCase.PriceDiscount;
            TbPricePerUnitDiscount.Text = String.Format("{0:#,#} ₽", pricePerUnitDiscount);

            finallyPrice = countCase * pricePerUnitDiscount;
            TbFinallyPrice.Text = String.Format("ИТОГО: {0} ₽", finallyPrice);

            delivery = 2;
            TbDelivery.Text = String.Format("Сроки доставки: {0} недели", delivery);

        }
        int countCase;
        int pricePerUnitStock;
        int pricePerUnitDiscount;
        int DiscountForCount;
        int countToConstruct;

        int finallyPrice;
        int delivery;

        int deliveryProgress = 0;

        private void CountMinusBtn_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if(countCase == 1)
            {
                return;
            }
            else
            {
                countCase--;
            }
            RefreshCount();

            
            
        }

        private void RefreshCount()
        {
            
            
            if(countCase <= 0 )
            {
                countCase = 1;
            }
            if(countCase < 10)
            {
                DiscountForCount = 0;
            }
            if (countCase >= 10 & countCase < 20)
            {
                DiscountForCount = 5;
                
            }
            if (countCase >= 20 & countCase < 30)
            {
                DiscountForCount = 10;
            }
            if (countCase >= 40 & countCase < 50)
            {
                DiscountForCount = 20;
            }
            if (countCase >= 50 & countCase < 60)
            {
                DiscountForCount = 25;
            }
            if (countCase >= 60)
            {
                DiscountForCount = 35;
            }

            
            if (countCase > contextComputerCase.Count)
            {
                countToConstruct = countCase - contextComputerCase.Count;
                
                delivery = 1;
                TbDelivery.Text = String.Format("Сроки доставки: {0} месяц", delivery);
                TbCountToConstruct.Text = String.Format("+{0} корпусов к изготовлению", countToConstruct);
            }
            else
            {
                countToConstruct = 0;
                delivery = 2;
                TbDelivery.Text = String.Format("Сроки доставки: {0} недели", delivery);
                TbCountToConstruct.Text = String.Empty;
            }

            
            TbCountCase.Text = countCase.ToString();
            TbCountUnit.Text = $"{countCase} шт. x";

            TbDiscount.Text = String.Format("Скидка за количество: {0}%", DiscountForCount);
            pricePerUnitDiscount = pricePerUnitStock * (100 - DiscountForCount) / 100;
            TbPricePerUnitDiscount.Text = String.Format("{0:#,#} ₽", pricePerUnitDiscount);


            finallyPrice = countCase * pricePerUnitDiscount;
            TbFinallyPrice.Text = String.Format("ИТОГО: {0} ₽", finallyPrice);

            if(DiscountForCount == 0)
            {
                TbDiscount.Visibility = Visibility.Collapsed;
                TbPricePerUnit.Visibility= Visibility.Collapsed;
            }
            else
            {
                TbDiscount.Visibility = Visibility.Visible;
                TbPricePerUnit.Visibility = Visibility.Visible;
            }
        }
        private void CountPlusBtn_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (countCase < 100)
            {
                countCase++;
            }
            else
            {
                return;
            }
            RefreshCount();

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
        private void TbCountCase_LostFocus(object sender, RoutedEventArgs e)
        {
            if (int.Parse(TbCountCase.Text) > 100)
            {
                countCase = 100;
                RefreshCount();
            }
            else
            {
                countCase = int.Parse(TbCountCase.Text);
                RefreshCount();
            }
            
            
        }

        private void TbPhone_TextChanged(object sender, TextChangedEventArgs e)
        {
            int progress = 0;
            if (TbPhone.Text.Length > 0)
            {
                BorderFirstStage.Background = new SolidColorBrush(Color.FromRgb(56,129,239));
                TbFirstStage.Foreground = Brushes.White;
                progress = 100;
            }
            if (TbPhone.Text.Length == 0)
            {
                BorderFirstStage.Background = Brushes.White;
                TbFirstStage.Foreground = new SolidColorBrush(Color.FromRgb(56, 129, 239));
                progress = 0;
            }
            PbFirst.Value = progress;

        }

        private void DeliveryProgress()
        {
            int progress = 0;
            if (TbAddress.Text.Length > 0)
            {
                progress += 25;
            }
            else if (TbAddress.Text.Length == 0)
            {
                progress -= 25;
            }
            if (TbApartment.Text.Length > 0)
            {                
                progress += 25;
            }
            else if (TbApartment.Text.Length == 0)
            {              
                progress -= 25;
            }
            if(CbDate.SelectedItem != null)
            {
                progress += 25;
            }
            else if (CbDate.SelectedItem == null)
            {
                progress -= 25;
            }
            if(CbTime.SelectedItem != null)
            {
                progress += 25;
            }
            else if (CbTime.SelectedItem == null)
            {
                progress -= 25;
            }
            if (progress >= 25)
            {
                BorderSecondStage.Background = new SolidColorBrush(Color.FromRgb(56, 129, 239));
                TbSecondStage.Foreground = Brushes.White;
            }
            else
            {
                BorderSecondStage.Background = Brushes.White;
                TbSecondStage.Foreground = new SolidColorBrush(Color.FromRgb(56, 129, 239));
            }
            PbSecond.Value = progress;
        }

        private void TbAddress_TextChanged(object sender, TextChangedEventArgs e)
        {
            int progress = 0;
            if (TbAddress.Text.Length > 0 & TbAddress.Text.Length < 2)
            {
                progress += 25;
            }
            else if(TbAddress.Text.Length == 0)
            {
                progress -= 25;
            }
            if (progress >= 25)
            {
                BorderSecondStage.Background = new SolidColorBrush(Color.FromRgb(56, 129, 239));
                TbSecondStage.Foreground = Brushes.White;
            }
            else
            {
                BorderSecondStage.Background = Brushes.White;
                TbSecondStage.Foreground = new SolidColorBrush(Color.FromRgb(56, 129, 239));
            }
            PbSecond.Value += progress;
        }

        private void TbApartment_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (TbApartment.Text.Length > 0)
            {
                deliveryProgress += 25;
            }
            else if (TbApartment.Text.Length == 0)
            {
                deliveryProgress -= 25;
            }
            if (deliveryProgress >= 25)
            {
                BorderSecondStage.Background = new SolidColorBrush(Color.FromRgb(56, 129, 239));
                TbSecondStage.Foreground = Brushes.White;
            }
            else
            {
                BorderSecondStage.Background = Brushes.White;
                TbSecondStage.Foreground = new SolidColorBrush(Color.FromRgb(56, 129, 239));
            }
            PbSecond.Value = deliveryProgress;
        }
    }
}
