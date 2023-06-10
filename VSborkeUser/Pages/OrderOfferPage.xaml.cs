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
using VSborkeUser.Components;
using VSborkeUser.Windows;

namespace VSborkeUser.Pages
{
    /// <summary>
    /// Логика взаимодействия для OrderOfferPage.xaml
    /// </summary>
    public partial class OrderOfferPage : Page
    {
        ComputerCase contextComputerCase;
        int deliveryProgress = 0;
        public OrderOfferPage(ComputerCase computerCase)
        {
            InitializeComponent();
            contextComputerCase = computerCase;
            DataContext = contextComputerCase;
            
            if(App.LoggedUser.Phone != String.Empty)
            {
                TbPhone.Text = App.LoggedUser.Phone;
            }
            if (App.LoggedUser.Email != String.Empty)
            {
                TbEmail.Text = App.LoggedUser.Email;
            }
            if (App.LoggedUser.Address != String.Empty)
            {
                TbAddress.Text = App.LoggedUser.Address;
            }
            countCase = int.Parse(TbCountCase.Text);
            TbCountUnit.Text = $"{countCase} шт. x";

            pricePerUnitStock = contextComputerCase.PriceDiscount;
            TbPricePerUnit.Text = String.Format("{0:#,#} ₽", pricePerUnitStock);

            pricePerUnitDiscount = contextComputerCase.PriceDiscount;
            TbPricePerUnitDiscount.Text = String.Format("{0:#,#} ₽", pricePerUnitDiscount);

            delivery = 2;
            TbDelivery.Text = String.Format("Сроки доставки: {0} недели", delivery);

            DeliveryCostRefresh();
            finallyPrice = countCase * pricePerUnitDiscount + deliveryCost;
            TbFinallyPrice.Text = String.Format("ИТОГО: {0} ₽", finallyPrice);

            

            for(int i = 1; i< 100; i++)
            {
                CbFloors.Items.Add(i);
            }

            for (int i = 0; i < 7; i++)
            {
                CbDate.Items.Add(DateTime.Now.AddDays(14 + i).Date.ToString("d"));
            }

            CbTime.Items.Add("10:00 - 13:00");
            CbTime.Items.Add("14:00 - 16:00");
            CbTime.Items.Add("16:00 - 18:00");
            CbTime.Items.Add("18:00 - 20:00");

            RefreshCount();

        }
        int countCase;
        int pricePerUnitStock;
        int pricePerUnitDiscount;
        int DiscountForCount;
        int countToConstruct;

        int finallyPrice;
        int delivery;

        int deliveryCost;

       
        bool secondAddress = false;
        bool secondApartment = false;
        bool dateSelected = false;
        bool timeSelected = false;
        bool UpFloor = false;
        bool privateHouse = false;
        bool liftForOrder = false;

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
                CbDate.Items.Clear();
                for (int i = 0; i < 7; i++)
                {
                    CbDate.Items.Add(DateTime.Now.AddDays(31 + i).Date.ToString("d"));
                }

            }
            else
            {
                countToConstruct = 0;
                delivery = 2;
                TbDelivery.Text = String.Format("Сроки доставки: {0} недели", delivery);
                TbCountToConstruct.Text = String.Empty;
                CbDate.Items.Clear();
                for (int i = 0; i < 7; i++)
                {
                    CbDate.Items.Add(DateTime.Now.AddDays(14 + i).Date.ToString("d"));
                }
            }


            if (CbDate.SelectedItem != null & dateSelected != true)
            {
                dateSelected = true;
                deliveryProgress += 25;
            }
            else if (CbDate.SelectedItem == null & dateSelected == true)
            {
                dateSelected = false;
                deliveryProgress -= 25;
            }


            TbCountCase.Text = countCase.ToString();
            TbCountUnit.Text = $"{countCase} шт. x";

            TbDiscount.Text = String.Format("Скидка за количество: {0}%", DiscountForCount);
            pricePerUnitDiscount = pricePerUnitStock * (100 - DiscountForCount) / 100;
            TbPricePerUnitDiscount.Text = String.Format("{0:#,#} ₽", pricePerUnitDiscount);


            DeliveryCostRefresh();
            finallyPrice = countCase * pricePerUnitDiscount;
            finallyPrice += deliveryCost;
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
        private void DeliveryCostRefresh()
        {
            deliveryCost = 0;
            int floors = 0;
            if (CbFloors.SelectedItem != null)
            {
                floors = int.Parse(CbFloors.SelectedItem.ToString());
            }
            int floorsSumm = 0;
            int deliverySumm = 0;
            if (delivery == 2)
            {
                deliverySumm += 200;
            }
            if (delivery == 1)
            {
                deliverySumm += 400;
            }
            if(floors >= 6)
            {
                floorsSumm = floors * 50 - 250;
                deliverySumm += floorsSumm;
            }
            if (CbLift.IsChecked == true)
            {
                deliverySumm -= floorsSumm;
            }
            deliveryCost = deliverySumm;
            TbDeliveryPrice.Text = String.Format("Стоимость доставки: {0} ₽", deliveryCost);


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

        

        private void TbAddress_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (TbAddress.Text.Length > 0 & secondAddress != true)
            {
                secondAddress = true;
                deliveryProgress += 25;
            }
            else if (TbAddress.Text.Length == 0 & secondAddress == true)
            {
                secondAddress = false;
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

            if (deliveryProgress == 100)
            {
                BorderThirdStage.Background = new SolidColorBrush(Color.FromRgb(56, 129, 239));
                TbThirdStage.Foreground = Brushes.White;
                PbThird.Value = 100;
            }
            else
            {
                BorderThirdStage.Background = Brushes.White;
                TbThirdStage.Foreground = new SolidColorBrush(Color.FromRgb(56, 129, 239));
                PbThird.Value = 0;
            }
            DeliveryCostRefresh();
        }

        private void TbApartment_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (TbApartment.Text.Length > 0 & secondApartment != true)
            {
                secondApartment = true;
                deliveryProgress += 25;
            }
            else if (TbApartment.Text.Length == 0 & secondApartment == true)
            {
                secondApartment = false;
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
            if (deliveryProgress == 100)
            {
                BorderThirdStage.Background = new SolidColorBrush(Color.FromRgb(56, 129, 239));
                TbThirdStage.Foreground = Brushes.White;
                PbThird.Value = 100;
            }
            else
            {
                BorderThirdStage.Background = Brushes.White;
                TbThirdStage.Foreground = new SolidColorBrush(Color.FromRgb(56, 129, 239));
                PbThird.Value = 0;
            }
            DeliveryCostRefresh();
        }

        private void CbUpToFloor_Checked(object sender, RoutedEventArgs e)
        {
            CbFloors.Visibility= Visibility.Visible;
            DeliveryCostRefresh();
            UpFloor = true;
        }

        private void CbUpToFloor_Unchecked(object sender, RoutedEventArgs e)
        {
            CbFloors.Visibility = Visibility.Collapsed;
            CbFloors.SelectedItem= null;
            UpFloor = false;
            DeliveryCostRefresh();
        }

        private void CbDate_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CbDate.SelectedItem != null & dateSelected != true)
            {
                dateSelected = true;
                deliveryProgress += 25;
            }
            else if (CbDate.SelectedItem == null & dateSelected == true)
            {
                dateSelected = false;
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
            if (deliveryProgress == 100)
            {
                BorderThirdStage.Background = new SolidColorBrush(Color.FromRgb(56, 129, 239));
                TbThirdStage.Foreground = Brushes.White;
                PbThird.Value = 100;
            }
            else
            {
                BorderThirdStage.Background = Brushes.White;
                TbThirdStage.Foreground = new SolidColorBrush(Color.FromRgb(56, 129, 239));
                PbThird.Value = 0;
            }
        }

        private void CbTime_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CbTime.SelectedItem != null & timeSelected != true)
            {
                timeSelected = true;
                deliveryProgress += 25;
            }
            else if(CbTime.SelectedItem == null & timeSelected == true)
            {
                timeSelected= false;
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
            if (deliveryProgress == 100)
            {
                BorderThirdStage.Background = new SolidColorBrush(Color.FromRgb(56, 129, 239));
                TbThirdStage.Foreground = Brushes.White;
                PbThird.Value = 100;
            }
            else
            {
                BorderThirdStage.Background = Brushes.White;
                TbThirdStage.Foreground = new SolidColorBrush(Color.FromRgb(56, 129, 239));
                PbThird.Value = 0;
            }
        }

        private void CancelOrder_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void DealOrderBtn_Click(object sender, RoutedEventArgs e)
        {
            string errorMessage = "";
            if (string.IsNullOrWhiteSpace(TbPhone.Text))
            {
                errorMessage += "Заполните номер телефона\n";
            }
            if (string.IsNullOrWhiteSpace(TbAddress.Text))
            {
                errorMessage += "Заполните адрес для доставки\n";
            }
            if (string.IsNullOrWhiteSpace(TbApartment.Text))
            {
                errorMessage += "Заполните номер помещения\n";
            }
            if (CbDate.SelectedItem == null)
            {
                errorMessage += "Выберите дату доставки\n";
            }
            if (CbTime.SelectedItem == null)
            {
                errorMessage += "Выберите время доставки\n";
            }
            if (UpFloor == true & CbFloors.SelectedItem == null)
            {
                errorMessage += "Выберите этаж для доставки\n";
            }

            if (string.IsNullOrWhiteSpace(errorMessage) == false)
            {
                CustomMessageBox.Show(errorMessage, CustomMessageBox.CustomMessageBoxTitle.Предупреждение, CustomMessageBox.CustomMessageBoxButton.Ok, CustomMessageBox.CustomMessageBoxButton.Нет);
                return;
            }

            string comment;
            string floorNumber;
            if(string.IsNullOrWhiteSpace(TbCommentOrder.Text))
            {
                comment = null;
            }
            else
            {
                comment = TbCommentOrder.Text;
            }

            if (CbFloors.SelectedItem == null)
            {
                floorNumber = null;
            }
            else
            {
                floorNumber = CbFloors.SelectedItem.ToString();
            }
            DateTime dateForPocket;
            DateTime dateForConstruct;
            if (countToConstruct != 0)
            {
                dateForConstruct = DateTime.Parse(CbDate.SelectedItem.ToString()) - TimeSpan.FromDays(10);
                dateForPocket = DateTime.Parse(CbDate.SelectedItem.ToString()) - TimeSpan.FromDays(8);
            }
            else
            {
                dateForConstruct = DateTime.Parse(CbDate.SelectedItem.ToString()) - TimeSpan.FromDays(6);
                dateForPocket = DateTime.Parse(CbDate.SelectedItem.ToString()) - TimeSpan.FromDays(5);
            }

            float totalWeight = contextComputerCase.Weight * countCase;
            
            
            if(contextComputerCase.Count - countCase <= 0)
            {
                contextComputerCase.Count = 0;
            }
            else
            {
                contextComputerCase.Count = contextComputerCase.Count - countCase;
            }

            App.DB.Order.Add(new Order()
            {
                UserId = App.LoggedUser.Id,
                OrderDate = DateTime.Now,
                FinallyPrice = finallyPrice,
                GeneralCount = countCase,
                CountInAccessable = countCase - countToConstruct,
                CountForCreate = countToConstruct,
                PricePerUnit = pricePerUnitDiscount,
                PricePerUnitStock = contextComputerCase.PriceDiscount,
                Discount = DiscountForCount,
                DeliveryPrice = deliveryCost,
                TotalWeight = totalWeight,
                ComputerCaseId = contextComputerCase.Id,
                Status = App.DB.Status.FirstOrDefault(x => x.Id == 1),
                CommentOrder = comment,
                Phone = TbPhone.Text,
                Email = TbEmail.Text,
                Address = TbAddress.Text,
                FlatNumber = TbApartment.Text,
                PrivateHome = privateHouse,
                UpToFloor = UpFloor,
                Floor = floorNumber,
                LiftForFullOrder = liftForOrder,
                DateDelivery = DateTime.Parse(CbDate.SelectedItem.ToString()),
                TimeDelivery = CbTime.SelectedItem.ToString(),
                DateForConstruct = dateForConstruct,
                DateForPocket = dateForPocket


            });

            App.DB.SaveChanges();
            CustomMessageBox.Show("Заказ оформлен!", CustomMessageBox.CustomMessageBoxTitle.Успешно, CustomMessageBox.CustomMessageBoxButton.Ok, CustomMessageBox.CustomMessageBoxButton.Нет);
            NavigationService.GoBack();
        }

        private void CbLift_Checked(object sender, RoutedEventArgs e)
        {
            liftForOrder = true;
            DeliveryCostRefresh();
            RefreshCount();
            
        }

        private void CbLift_Unchecked(object sender, RoutedEventArgs e)
        {
            liftForOrder = false;
            DeliveryCostRefresh();
            RefreshCount();
        }

        private void CbFloors_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DeliveryCostRefresh();
            RefreshCount();
        }

        private void CbHouse_Checked(object sender, RoutedEventArgs e)
        {
            privateHouse = true;
        }

        private void CbHouse_Unchecked(object sender, RoutedEventArgs e)
        {
            privateHouse = false;
        }
    }
}
