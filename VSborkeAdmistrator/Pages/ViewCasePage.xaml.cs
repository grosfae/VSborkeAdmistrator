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

            

            LvAnalogue.ItemsSource = App.DB.ComputerCase.Where(x => x.Id != computerCase.Id ).ToList().Take(2);
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

        private void LeftBtn_Click(object sender, RoutedEventArgs e)
        {
            numberPage--;
            if (numberPage < 0)
                numberPage = 0;
            Update();
        }

        private void RightBtn_Click(object sender, RoutedEventArgs e)
        {
            numberPage++;
            if (LvAdditionImages.Items.Count < 4)
                numberPage--;
            Update();
        }
        private void SaveBtn_Click(object sender, RoutedEventArgs e)
        {
            string errorMessage = "";
            if (string.IsNullOrWhiteSpace(contextComputerCase.Name))
            {
                errorMessage += "Введите название\n";
            }
            if (string.IsNullOrWhiteSpace(contextComputerCase.Description))
            {
                errorMessage += "Введите описание\n";
            }
            if (contextComputerCase.Price <= 0)
            {
                errorMessage += "Введите корректную цену\n";
            }
            if (contextComputerCase.Manufacturer == null)
            {
                errorMessage += "Выберите бренд корпуса\n";
            }
            if (contextComputerCase.MainImage == null)
            {
                errorMessage += "Добавьте главное изображение\n";
            }
            if (contextComputerCase.Discount < 0 || contextComputerCase.Discount > 90)
            {
                errorMessage += "Введите корректную скидку\n";
            }
            if (contextComputerCase.FormFactor == null)
            {
                errorMessage += "Выберите форм-фактор корпуса\n";
            }
            if (contextComputerCase.Height <= 0)
            {
                errorMessage += "Введите высоту\n";
            }
            if (contextComputerCase.Width <= 0)
            {
                errorMessage += "Введите ширину\n";
            }
            if (contextComputerCase.Length <= 0)
            {
                errorMessage += "Введите длину\n";
            }
            if (contextComputerCase.Weight <= 0 || contextComputerCase.Weight > 50)
            {
                errorMessage += "Введите корректный вес\n";
            }
            if (contextComputerCase.MaterialSet == null)
            {
                errorMessage += "Выберите материалы корпуса\n";
            }
            if (contextComputerCase.MetalThickness < 0.4 || contextComputerCase.MetalThickness > 3)
            {
                errorMessage += "Введите корректную толщину металла\n";
            }
            if (contextComputerCase.FrontPanelMaterial == null)
            {
                errorMessage += "Выберите материал фронтальной панели\n";
            }
            if (contextComputerCase.EAtx == false & contextComputerCase.FlexAtx == false & contextComputerCase.MicroAtx == false & contextComputerCase.MiniDtx == false & contextComputerCase.MiniItx == false & contextComputerCase.SsiCeb == false & contextComputerCase.SsiEeb == false & contextComputerCase.StandartAtx == false & contextComputerCase.ThinMiniItx == false & contextComputerCase.XlAtx == false)
            {
                errorMessage += "Выберите форм-фактор поддерживаемых материнских плат\n";
            }
            if (contextComputerCase.OrientationMotherboard == null)
            {
                errorMessage += "Выберите ориентацию материнской платы\n";
            }
            if (contextComputerCase.PowerBlockStandartSupport == null)
            {
                errorMessage += "Выберите форм-фактор блока питания\n";
            }
            if (contextComputerCase.AlignmentPowerBlock == null)
            {
                errorMessage += "Выберите расположение блока питания\n";
            }
            if (contextComputerCase.MaxLengthPowerBlock <= 0)
            {
                errorMessage += "Введите максимальную длину блока питания\n";
            }
            if (contextComputerCase.HorizontalAddonSlot == null)
            {
                errorMessage += "Выберите горизонтальные слоты расширения\n";
            }
            if (contextComputerCase.VerticalAddonSlot == null)
            {
                errorMessage += "Выберите вертикальные слоты расширения\n";
            }
            if (contextComputerCase.MaxLengthVideocard <= 0)
            {
                errorMessage += "Введите максимальная длина видеокарты\n";
            }
            if (contextComputerCase.MaxHeightCPUCooler <= 0)
            {
                errorMessage += "Введите максимальную высоту кулера CPU\n";
            }
            if (contextComputerCase.SlotSSD == null)
            {
                errorMessage += "Выберите количество 2.5 отсеков накопителей\n";
            }
            if (contextComputerCase.SlotHDD == null)
            {
                errorMessage += "Выберите количество 3.5 отсеков накопителей\n";
            }
            if (contextComputerCase.SlotXHDD == null)
            {
                errorMessage += "Выберите количество 5.25 отсеков накопителей\n";
            }
            if (contextComputerCase.CoolerInside == null)
            {
                errorMessage += "Выберите количество вентиляторов в комплекте\n";
            }
            if (contextComputerCase.SupportFrontCooler == null)
            {
                errorMessage += "Выберите поддерживаемые фронтальные вентиляторы\n";
            }
            if (contextComputerCase.SupportBackCooler == null)
            {
                errorMessage += "Выберите поддерживаемые тыловые вентиляторы\n";
            }
            if (contextComputerCase.SupportTopCooler == null)
            {
                errorMessage += "Выберите поддерживаемые верхние вентиляторы\n";
            }
            if (contextComputerCase.SupportSideCooler == null)
            {
                errorMessage += "Выберите поддерживаемые боковые вентиляторы\n";
            }
            if (contextComputerCase.SupportBottomCooler == null)
            {
                errorMessage += "Выберите поддерживаемые нижние вентиляторы\n";
            }
            if (contextComputerCase.PrimaryColor == null)
            {
                errorMessage += "Выберите основной цвет\n";
            }
            if (contextComputerCase.SecondColor == null)
            {
                errorMessage += "Выберите дополнительный цвет\n";
            }
            if (contextComputerCase.IOPanel == null)
            {
                errorMessage += "Выберите разъемы передней панели\n";
            }
            if (contextComputerCase.IOPanelAlignment == null)
            {
                errorMessage += "Выберите расположение IO-панели\n";
            }
            if (contextComputerCase.SidePanelFixation == null)
            {
                errorMessage += "Выберите фиксацию боковых панелей\n";
            }
            if (contextComputerCase.DeliverySet == null)
            {
                errorMessage += "Введите комплект поставки\n";
            }
            if (string.IsNullOrWhiteSpace(errorMessage) == false)
            {
                CustomMessageBox.Show(errorMessage, CustomMessageBox.CustomMessageBoxTitle.Предупреждение, CustomMessageBox.CustomMessageBoxButton.Ok, CustomMessageBox.CustomMessageBoxButton.Нет);
                return;
            }
            if (contextComputerCase.Id == 0)
            {
                contextComputerCase.UserId = App.LoggedUser.Id;
                App.DB.ComputerCase.Add(contextComputerCase);
            }
            App.DB.SaveChanges();
            CustomMessageBox.Show("Сохранение успешно!", CustomMessageBox.CustomMessageBoxTitle.Успешно, CustomMessageBox.CustomMessageBoxButton.Ok, CustomMessageBox.CustomMessageBoxButton.Нет);
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
        

        int numberPage = 0;
        int count = 4;

        private void Update()
        {
            IEnumerable<AdditionComputerCaseImage> caseImagesList = App.DB.AdditionComputerCaseImage.Where(x => x.ComputerCaseId == contextComputerCase.Id);
            caseImagesList = caseImagesList.Skip(count * numberPage).Take(count);
            LvAdditionImages.ItemsSource = caseImagesList;
        }

        private void DeFavouriteBtn_Click(object sender, RoutedEventArgs e)
        {
            var favourite = App.DB.Favourite.SingleOrDefault(x => x.UserId == App.LoggedUser.Id & x.ComputerCaseId == contextComputerCase.Id);
            App.DB.Favourite.Remove(favourite);
        }

        private void FavouriteBtn_Click(object sender, RoutedEventArgs e)
        {

            App.DB.Favourite.Add(new Favourite()
            {
                ComputerCaseId = contextComputerCase.Id,
                UserId = App.LoggedUser.Id
            });
            App.DB.SaveChanges();
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

        private void SpecBtn_Checked(object sender, RoutedEventArgs e)
        {
            PbSpecifications.Visibility= Visibility.Visible;
        }

        private void ReviewBtn_Checked(object sender, RoutedEventArgs e)
        {
            PbReview.Visibility = Visibility.Visible;
        }

        private void SpecBtn_Unchecked(object sender, RoutedEventArgs e)
        {
            PbSpecifications.Visibility = Visibility.Hidden;
        }

        private void ReviewBtn_Unchecked(object sender, RoutedEventArgs e)
        {
            PbReview.Visibility = Visibility.Hidden;
        }
    }

}
