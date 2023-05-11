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

            

        }
        int countCase;
        int pricePerUnitStock;
        int pricePerUnitDiscount;
        int DiscountForCount;
        int countToConstruct;

        private void TbPhone_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void TbAdress_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void TbApartment_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

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
            TbCountCase.Text = countCase.ToString();
            TbCountUnit.Text = $"{countCase} шт. x";
            if (countCase < 10)
            {
                TbPricePerUnit.Visibility = Visibility.Collapsed;
                TbDiscount.Visibility = Visibility.Collapsed;
            }
            if (countCase >= 10 & countCase < 20)
            {
                DiscountForCount = 5;
                TbPricePerUnit.Visibility = Visibility.Visible;
                TbDiscount.Visibility = Visibility.Visible;
            }
            if (countCase >= 20 & countCase < 30)
            {
                DiscountForCount = 10;
            }
            if (countCase >= 40 & countCase < 50)
            {
                DiscountForCount = 25;
            }
            if (countCase >= 50 & countCase < 60)
            {
                DiscountForCount = 30;
            }
            if (countCase >= 60)
            {
                DiscountForCount = 35;
            }

            if (countCase > contextComputerCase.Count)
            {
                countToConstruct = countCase - contextComputerCase.Count;
                TbCountToConstruct.Text = String.Format("+{0} корпусов к изготовлению", countToConstruct);
                TbCountToConstruct.Visibility = Visibility.Visible;
            }
            else
            {
                TbCountToConstruct.Visibility = Visibility.Collapsed;
            }

            TbDiscount.Text = String.Format("Скидка за количество: {0}%", DiscountForCount);
            pricePerUnitDiscount = pricePerUnitStock * (100 - DiscountForCount) / 100;
            TbPricePerUnitDiscount.Text = String.Format("{0:#,#} ₽", pricePerUnitDiscount);
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
            TbCountCase.Text = countCase.ToString();
            TbCountUnit.Text = $"{countCase} шт. x";
            if (countCase < 10)
            {
                TbPricePerUnit.Visibility = Visibility.Collapsed;
                TbDiscount.Visibility = Visibility.Collapsed;
            }
            if (countCase >= 10 & countCase < 20)
            {
                DiscountForCount = 10;
                TbPricePerUnit.Visibility = Visibility.Visible;
                TbDiscount.Visibility = Visibility.Visible;
            }
            if (countCase >= 20 & countCase < 30)
            {
                DiscountForCount = 10;
            }
            if (countCase >= 40 & countCase < 50)
            {
                DiscountForCount = 25;
            }
            if (countCase >= 50 & countCase < 60)
            {
                DiscountForCount = 30;
            }
            if (countCase >= 60)
            {
                DiscountForCount = 35;
            }

            if (countCase > contextComputerCase.Count)
            {
                countToConstruct = countCase - contextComputerCase.Count;
                TbCountToConstruct.Text = String.Format("+{0} корпусов к изготовлению", countToConstruct);
                TbCountToConstruct.Visibility= Visibility.Visible;
            }
            else
            {
                TbCountToConstruct.Visibility = Visibility.Collapsed;
            }

            TbDiscount.Text = String.Format("Скидка за количество: {0}%", DiscountForCount);
            pricePerUnitDiscount = pricePerUnitStock * (100 - DiscountForCount) / 100;
            TbPricePerUnitDiscount.Text = String.Format("{0:#,#} ₽", pricePerUnitDiscount);
        }

        private void Numbers_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (Regex.IsMatch(e.Text, @"[0-9]") == false)
            {
                e.Handled = true;
            }
        }

        private void TbCountCase_LostFocus(object sender, RoutedEventArgs e)
        {
            
        }
    }
}
