using LiveCharts;
using LiveCharts.Wpf;
using System;
using System.Collections.Generic;
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
using VSborkeStorager.Components;
using VSborkeStorager.Windows;

namespace VSborkeStorager.Pages
{
    /// <summary>
    /// Логика взаимодействия для ViewCasePage.xaml
    /// </summary>
    public partial class ViewCasePage : Page
    {

        ComputerCase contextComputerCase;

        int maxPage = 0;

        public ViewCasePage(ComputerCase computerCase)
        {
            InitializeComponent();
            contextComputerCase = computerCase;
            DataContext = contextComputerCase;


            CbSortReview.SelectedIndex = 0;


            LvAnalogue.ItemsSource = App.DB.ComputerCase.Where(x => x.Id != contextComputerCase.Id & x.EAtx == contextComputerCase.EAtx & x.FlexAtx == contextComputerCase.FlexAtx
            & x.MicroAtx == contextComputerCase.MicroAtx & x.MiniDtx == contextComputerCase.MiniDtx & x.MiniItx == contextComputerCase.MiniItx & x.SsiCeb == contextComputerCase.SsiCeb
            & x.SsiEeb == contextComputerCase.SsiEeb & x.StandartAtx == contextComputerCase.StandartAtx & x.ThinMiniItx == contextComputerCase.ThinMiniItx & x.XlAtx == contextComputerCase.XlAtx
            ).ToList().Take(2);
            LvAdditionImages.ItemsSource = contextComputerCase.AdditionComputerCaseImage.ToList();

            if (App.DB.ComputerCase.Where(x => x.Id != contextComputerCase.Id & x.EAtx == contextComputerCase.EAtx & x.FlexAtx == contextComputerCase.FlexAtx
            & x.MicroAtx == contextComputerCase.MicroAtx & x.MiniDtx == contextComputerCase.MiniDtx & x.MiniItx == contextComputerCase.MiniItx & x.SsiCeb == contextComputerCase.SsiCeb
            & x.SsiEeb == contextComputerCase.SsiEeb & x.StandartAtx == contextComputerCase.StandartAtx & x.ThinMiniItx == contextComputerCase.ThinMiniItx & x.XlAtx == contextComputerCase.XlAtx
            ).FirstOrDefault() != null)
            {
                LvAnalogue.Visibility = Visibility.Visible;
                TbNoneAnalog.Visibility = Visibility.Collapsed;
            }
            else
            {
                LvAnalogue.Visibility = Visibility.Collapsed;
                TbNoneAnalog.Visibility = Visibility.Visible;
            }
            RbAll.IsChecked = true;
            LbReview.ItemsSource = contextComputerCase.FeedBack.ToList();

        }
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            
            maxPage = contextComputerCase.FeedBack.Count();

            FrameForChart.Navigate(new PriceGraphMiniPage(contextComputerCase));
            FeedbackMarkRefresh();
            Update();
        }
        int numberPage = 0;
        int count = 5;

        int fakePage = 1;
        private void BtnFirstPage_Click(object sender, RoutedEventArgs e)
        {
            numberPage = 0;
            Update();
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
            Update();
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
                Update();
            }
            GeneratePageNumbers();
        }

        private void BtnLastPage_Click(object sender, RoutedEventArgs e)
        {
            numberPage = maxPage - 1;
            fakePage = maxPage;
            Update();
            GeneratePageNumbers();
        }
        private void Update()
        {

            IEnumerable<FeedBack> partsList = App.DB.FeedBack.Where(x => x.ComputerCaseId == contextComputerCase.Id);
            if (CbSortReview.SelectedIndex == 0)
            {
                partsList = partsList.OrderByDescending(x => x.DateOfReview).ToList();
            }
            if (CbSortReview.SelectedIndex == 1)
            {
                partsList = partsList.OrderByDescending(x => x.GeneralStars).ToList();
            }
            if (RbAll.IsChecked == true)
            {

            }
            else if (RbFive.IsChecked == true)
            {
                partsList = partsList.Where(x => x.GeneralStars == 5).ToList();
            }
            else if (RbFour.IsChecked == true)
            {
                partsList = partsList.Where(x => x.GeneralStars == 4).ToList();
            }
            else if (RbThree.IsChecked == true)
            {
                partsList = partsList.Where(x => x.GeneralStars == 3).ToList();
            }
            else if (RbTwo.IsChecked == true)
            {
                partsList = partsList.Where(x => x.GeneralStars == 2).ToList();
            }
            else if (RbOne.IsChecked == true)
            {
                partsList = partsList.Where(x => x.GeneralStars == 1).ToList();
            }

            if (partsList.Count() > count)
            {
                if (partsList.Count() % count > 0)
                {
                    maxPage = (partsList.Count() / count) + 1;
                }
                else
                {
                    maxPage = partsList.Count() / count;
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
            if (TbSearch.Text.Length > 0)
            {
                partsList = partsList.Where(x => x.Advantage.ToLower().Contains(TbSearch.Text.ToLower()) || x.Disadvantages.ToLower().Contains(TbSearch.Text.ToLower()) || x.Comment.ToLower().Contains(TbSearch.Text.ToLower()));
            }
            partsList = partsList.Skip(count * numberPage).Take(count).ToList();
            LbReview.ItemsSource = partsList;
            GeneratePageNumbers();

            if(partsList.Count() == 0)
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
            Update();



        }
        private void CancelBtn_Click(object sender, RoutedEventArgs e)
        {
            
            NavigationService.GoBack();
        }
        private void TextBox_PreviewTextInput_1(object sender, TextCompositionEventArgs e)
        {
            if (Regex.IsMatch(e.Text, @"[0-9]") == false)
            {
                e.Handled = true;
            }
        }

        private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (Regex.IsMatch(e.Text, @"[0-9.]") == false)
            {
                e.Handled = true;
            }
        }

        private void TextBox_PreviewTextInputABC(object sender, TextCompositionEventArgs e)
        {
            if (Regex.IsMatch(e.Text, @"[A-zА-я,]") == false)
            {
                e.Handled = true;
            }
        }

        private void BuyBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void NotificBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ButAnalogueBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void LvAdditionImages_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedImage = LvAdditionImages.SelectedItem as AdditionComputerCaseImage;
            var c = selectedImage.AdditionImage;

            var image = new BitmapImage();
            using (var mem = new MemoryStream(c))
            {
                mem.Position = 0;
                image.BeginInit();
                image.CreateOptions = BitmapCreateOptions.PreservePixelFormat;
                image.CacheOption = BitmapCacheOption.OnLoad;
                image.UriSource = null;
                image.StreamSource = mem;
                image.EndInit();
            }
            image.Freeze();
            MainImage.Source = image;

        }
        public SeriesCollection SeriesCollection { get; set; }

        public string[] Labels { get; set; }

        public Func<double, string> YFormatter { get; set; }


        private void FeedbackMarkRefresh()
        {
            double FeedbackCount = App.DB.FeedBack.Where(x => x.ComputerCaseId == contextComputerCase.Id).Count();
            TbReviewCount.Text = $"{FeedbackCount} отзывов";
            if (FeedbackCount == 0)
            {
                return;
            }
            double FiveCount = App.DB.FeedBack.Where(x => x.ComputerCaseId == contextComputerCase.Id & x.GeneralStars == 5).Count();
            double FourCount = App.DB.FeedBack.Where(x => x.ComputerCaseId == contextComputerCase.Id & x.GeneralStars == 4).Count();
            double ThreeCount = App.DB.FeedBack.Where(x => x.ComputerCaseId == contextComputerCase.Id & x.GeneralStars == 3).Count();
            double TwoCount = App.DB.FeedBack.Where(x => x.ComputerCaseId == contextComputerCase.Id & x.GeneralStars == 2).Count();
            double OneCount = App.DB.FeedBack.Where(x => x.ComputerCaseId == contextComputerCase.Id & x.GeneralStars == 1).Count();

            PbFive.Value = FiveCount / FeedbackCount * 100;
            PbFour.Value = FourCount / FeedbackCount * 100;
            PbThree.Value = ThreeCount / FeedbackCount * 100;
            PbTwo.Value = TwoCount / FeedbackCount * 100;
            PbOne.Value = OneCount / FeedbackCount * 100;

            double FiveMark = FiveCount * 5 / FeedbackCount;
            double FourMark = FourCount * 4 / FeedbackCount;
            double ThreeMark = ThreeCount * 3 / FeedbackCount;
            double TwoMark = TwoCount * 2 / FeedbackCount;
            double OneMark = OneCount * 1 / FeedbackCount;

            double result = Math.Round(FiveMark + FourMark + ThreeMark + TwoMark + OneMark, 2, MidpointRounding.AwayFromZero);

            TbGeneralCaseScore.Text = result.ToString();

            if (result == 1)
            {
                TbGeneralCaseStars.Text = @"    ";
            }
            if (result >= 1.50 & result < 2)
            {
                TbGeneralCaseStars.Text = @"    ";
            }
            if (result == 2)
            {
                TbGeneralCaseStars.Text = @"    ";
            }
            if (result >= 2.50 & result < 3)
            {
                TbGeneralCaseStars.Text = @"    ";
            }
            if (result == 3)
            {
                TbGeneralCaseStars.Text = @"    ";
            }
            if (result >= 3.50 & result < 4)
            {
                TbGeneralCaseStars.Text = @"    ";
            }
            if (result == 4)
            {
                TbGeneralCaseStars.Text = @"    ";
            }
            if (result >= 4.50 & result < 5)
            {
                TbGeneralCaseStars.Text = @"    ";
            }
            if (result == 5)
            {
                TbGeneralCaseStars.Text = @"    ";
            }


        }

        private void SpecBtn_Click(object sender, RoutedEventArgs e)
        {
            PbDescription.Visibility = Visibility.Visible;
            PbSpecs.Visibility = Visibility.Visible;
            SpReviews.Visibility = Visibility.Collapsed;
        }

        private void ReviewBtn_Click(object sender, RoutedEventArgs e)
        {
            PbDescription.Visibility = Visibility.Collapsed;
            PbSpecs.Visibility = Visibility.Collapsed;
            SpReviews.Visibility = Visibility.Visible;
        }


        private void TbNameLink_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var selectedCase = (sender as TextBlock).DataContext as ComputerCase;
            NavigationService.Navigate(new ViewCasePage(selectedCase));
        }

        private void RbAll_Checked(object sender, RoutedEventArgs e)
        {
            Update();
        }


        private void PriceGraphBtn_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var dialog = new PriceChartsWindow(contextComputerCase);
            dialog.ShowDialog();
        }

        private void CbSortReview_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Update();
        }

        private void TbSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            Update();
        }

        private void ResetBtn_Click(object sender, RoutedEventArgs e)
        {
            CbSortReview.SelectedIndex= 0;
            RbAll.IsChecked = true;
            TbSearch.Clear();
        }
    }

}
