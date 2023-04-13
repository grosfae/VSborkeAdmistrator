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
using VSborkeAdmistrator.Components;
using VSborkeAdmistrator.Windows;
using WpfDrawing.Charts;
using static System.Net.Mime.MediaTypeNames;
using static System.Net.WebRequestMethods;

namespace VSborkeAdmistrator.Pages
{
    /// <summary>
    /// Логика взаимодействия для ViewCasePage.xaml
    /// </summary>
    public partial class ViewCasePage : Page
    {
        ComputerCase contextComputerCase;
        public ViewCasePage(ComputerCase computerCase)
        {
            InitializeComponent();
            contextComputerCase = computerCase;
            DataContext = contextComputerCase;

            

            LvAnalogue.ItemsSource = App.DB.ComputerCase.Where(x => x.Id != contextComputerCase.Id & x.EAtx == contextComputerCase.EAtx & x.FlexAtx == contextComputerCase.FlexAtx
            & x.MicroAtx == contextComputerCase.MicroAtx & x.MiniDtx == contextComputerCase.MiniDtx & x.MiniItx == contextComputerCase.MiniItx & x.SsiCeb == contextComputerCase.SsiCeb 
            & x.SsiEeb == contextComputerCase.SsiEeb & x.StandartAtx == contextComputerCase.StandartAtx & x.ThinMiniItx == contextComputerCase.ThinMiniItx & x.XlAtx == contextComputerCase.XlAtx
            ).ToList().Take(2);
            LvAdditionImages.ItemsSource = App.DB.AdditionComputerCaseImage.Where(x => x.ComputerCaseId == contextComputerCase.Id).ToList();

            
        }

        private void MainImageBtn_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new Microsoft.Win32.OpenFileDialog()
            {
                Filter = "*.jpg|*.jpg|*.png|*.png|*.jpeg|*.jpeg"
            };
            if (dialog.ShowDialog().GetValueOrDefault())
            {
                contextComputerCase.MainImage = System.IO.File.ReadAllBytes(dialog.FileName);
                DataContext = null;
                DataContext = contextComputerCase;
            }
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
        

        private void DeFavouriteBtn_Click(object sender, RoutedEventArgs e)
        {
            var favourite = App.DB.Favourite.SingleOrDefault(x => x.UserId == App.LoggedUser.Id & x.ComputerCaseId == contextComputerCase.Id);
            App.DB.Favourite.Remove(favourite);
            App.DB.SaveChanges();
            DeFavouriteBtn.Visibility = Visibility.Collapsed;
            FavouriteBtn.Visibility = Visibility.Visible;
        }

        private void FavouriteBtn_Click(object sender, RoutedEventArgs e)
        {

            App.DB.Favourite.Add(new Favourite()
            {
                ComputerCaseId = contextComputerCase.Id,
                UserId = App.LoggedUser.Id
            });
            App.DB.SaveChanges();
            FavouriteBtn.Visibility = Visibility.Collapsed;
            DeFavouriteBtn.Visibility= Visibility.Visible;
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

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            // Удаляем прежний график.
            GridForChart.Children.OfType<Canvas>().ToList().ForEach(p => GridForChart.Children.Remove(p));

            Chart chart = null;

            // Создаём новый график выбранного вида.

            chart = new LineChart();

            // Добавляем новую диаграмму на поле контейнера для графиков.
            GridForChart.Children.Add(chart.ChartBackground);

            // Принудительно обновляем размеры контейнера для графика.
            GridForChart.UpdateLayout();

            // Создаём график.
            CreateChart(chart);
        }

        private void CreateChart(Chart chart)
        {
            chart.Clear();
            IEnumerable<PriceHistory> priceHistories = App.DB.PriceHistory.Where(x => x.ComputerCaseId == contextComputerCase.Id).OrderBy(x => x.DateHistory);

            foreach (PriceHistory pc in priceHistories)
            {
                chart.AddValue(pc.Price);
            }
        }

        private void SpecBtn_Click(object sender, RoutedEventArgs e)
        {
            PbDescription.Visibility = Visibility.Visible;
            PbSpecs.Visibility = Visibility.Visible;
        }

        private void ReviewBtn_Click(object sender, RoutedEventArgs e)
        {
            PbDescription.Visibility = Visibility.Collapsed;
            PbSpecs.Visibility = Visibility.Collapsed;
        }

        private void AnalogBtn_Click(object sender, RoutedEventArgs e)
        {
            PbDescription.Visibility = Visibility.Collapsed;
            PbSpecs.Visibility = Visibility.Collapsed;
        }
    }

}
