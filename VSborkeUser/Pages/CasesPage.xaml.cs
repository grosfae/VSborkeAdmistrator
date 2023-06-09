using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    /// Логика взаимодействия для CasesPage.xaml
    /// </summary>
    public partial class CasesPage : Page
    {
        int maxPage = 0;
        public CasesPage()
        {
            InitializeComponent();
            CbManufacturer.ItemsSource = App.DB.Manufacturer.OrderBy(x => x.Name).ToList();
            CbPrimaryColor.ItemsSource = App.DB.PrimaryColor.ToList();
            CbTypeRGB.ItemsSource = App.DB.TypeRGB.ToList();
            

            CbAccess.IsChecked = true;

        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            TbStartPrice.Tag = $"от {App.DB.ComputerCase.Min(x => x.Price)}";
            TbEndPrice.Tag = $"до {App.DB.ComputerCase.Max(x => x.Price)}";
            TbCpuHeightStart.Tag = $"от {App.DB.ComputerCase.Min(x => x.MaxHeightCPUCooler)}";
            TbCpuHeightEnd.Tag = $"до {App.DB.ComputerCase.Max(x => x.MaxHeightCPUCooler)}";
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
            IEnumerable<ComputerCase> filterCase = App.DB.ComputerCase.Where(x => x.IsCustom == false);
            if (CbSort.SelectedIndex == 0)
            {
                filterCase = filterCase.OrderBy(x => x.PriceDiscount).ToList();
            }
            else if (CbSort.SelectedIndex == 1)
            {
                filterCase = filterCase.OrderByDescending(x => x.PriceDiscount).ToList();
            }
            foreach (var child in ChecksumAccess_Collection.Children)
            {
                var check = child as CheckBox;
                if (check.IsChecked == true)
                {
                    foreach (ComputerCase pc in filterCase)
                    {
                        filterCase = filterCase.Where(x =>
                        x.IsAccessable && CbAccess.IsChecked == true ||
                        x.IsAccessable == false && CbNoneAccess.IsChecked == true
                        ).ToList();
                    }
                }
            }

            if (CbManufacturer.SelectedIndex != -1)
            {
                filterCase = filterCase.Where(x => x.Manufacturer == CbManufacturer.SelectedItem).ToList();
            }
            if (CbPrimaryColor.SelectedIndex != -1)
            {
                filterCase = filterCase.Where(x => x.PrimaryColor == CbPrimaryColor.SelectedItem).ToList();
            }
            if (CbTypeRGB.SelectedIndex != -1)
            {
                filterCase = filterCase.Where(x => x.TypeRGB == CbTypeRGB.SelectedItem).ToList();
            }
            foreach (var child in Checksum_Collection.Children)
            {
                var check = child as CheckBox;
                if (check.IsChecked == true)
                {
                    foreach (ComputerCase pc in filterCase)
                    {
                        filterCase = filterCase.Where(x =>
                        x.EAtx && CbEatx.IsChecked == true ||
                        x.FlexAtx && CbFlex.IsChecked == true ||
                        x.MicroAtx && CbMicro.IsChecked == true ||
                        x.MiniDtx && CbMiniDtx.IsChecked == true ||
                        x.MiniItx && CbMiniItx.IsChecked == true ||
                        x.SsiCeb && CbSsiCeb.IsChecked == true ||
                        x.SsiEeb && CbSsiEeb.IsChecked == true ||
                        x.StandartAtx && CbStandart.IsChecked == true ||
                        x.ThinMiniItx && CbThin.IsChecked == true ||
                        x.XlAtx && CbXl.IsChecked == true).ToList();
                    }
                }
            }
            foreach (var childTower in ChecksumTower_Collection.Children)
            {
                var checkTower = childTower as CheckBox;
                if (checkTower.IsChecked == true)
                {
                    foreach (ComputerCase pcTower in filterCase)
                    {
                        filterCase = filterCase.Where(x =>
                        x.FormFactor.Name == "Desktop" && CbDesktop.IsChecked == true ||
                        x.FormFactor.Name == "Full-Tower" && CbFullTower.IsChecked == true ||
                        x.FormFactor.Name == "Mid-Tower" && CbMidTower.IsChecked == true ||
                        x.FormFactor.Name == "Mini-Tower" && CbMiniTower.IsChecked == true ||
                        x.FormFactor.Name == "Slim" && CbSlim.IsChecked == true ||
                        x.FormFactor.Name == "Super/Ultra-Tower" && CbSuperTower.IsChecked == true ||
                        x.FormFactor.Name == "Компактный (SFF)" && CbSff.IsChecked == true ||
                        x.FormFactor.Name == "Нестандартный" && CbNoneStandart.IsChecked == true ||
                        x.FormFactor.Name == "Открытый корпус" && CbOpenCase.IsChecked == true ||
                        x.FormFactor.Name == "Открытый стенд" && CbOpenStand.IsChecked == true).ToList();
                    }
                }
            }

            foreach (var childPower in Power_Collection.Children)
            {
                var checkPower = childPower as CheckBox;
                if (checkPower.IsChecked == true)
                {
                    foreach (ComputerCase pcPower in filterCase)
                    {
                        filterCase = filterCase.Where(x =>
                        x.AlignmentPowerBlock.Name == "Нижнее" && CbDowner.IsChecked == true ||
                        x.AlignmentPowerBlock.Name == "Верхнее" && CbUpper.IsChecked == true ||
                        x.AlignmentPowerBlock.Name == "Внешнее" && CbOutsider.IsChecked == true ||
                        x.AlignmentPowerBlock.Name == "Скрытое" && CbHider.IsChecked == true ||
                        x.AlignmentPowerBlock.Name == "Регулируемое" && CbConfigure.IsChecked == true).ToList();
                    }
                }
            }
            foreach (var childWindowMaterial in WindowsMaterial_Collection.Children)
            {
                var checkWindowMaterial = childWindowMaterial as CheckBox;
                if (checkWindowMaterial.IsChecked == true)
                {
                    foreach (ComputerCase pcWindowMaterial in filterCase)
                    {
                        filterCase = filterCase.Where(x =>
                        x.WindowMaterialId == 2 && CbAkrill.IsChecked == true ||
                        x.WindowMaterialId == 3 && CbTemperedGlass.IsChecked == true).ToList();
                    }
                }
            }

            foreach (var childWindowAlignment in WindowAlignment_Collection.Children)
            {
                var checkWindowAlignmentl = childWindowAlignment as CheckBox;
                if (checkWindowAlignmentl.IsChecked == true)
                {
                    foreach (ComputerCase pcWWindowAlignment in filterCase)
                    {
                        filterCase = filterCase.Where(x =>
                        x.WindowAlignment.Name == "Нет" && CbNoneWindow.IsChecked == true ||
                        x.WindowAlignment.Name == "С двух сторон" && CbDoubleWindow.IsChecked == true ||
                        x.WindowAlignment.Name == "Слева" && CbLeftWindow.IsChecked == true ||
                        x.WindowAlignment.Name == "Справа" && CbRightWindow.IsChecked == true).ToList();
                    }
                }
            }

            if (TbStartPrice.Text.Length > 0)
            {
                filterCase = filterCase.Where(x => x.Price >= Convert.ToInt64(TbStartPrice.Text)).ToList();
            }
            if (TbEndPrice.Text.Length > 0)
            {
                filterCase = filterCase.Where(x => x.Price <= Convert.ToInt64(TbEndPrice.Text)).ToList();
            }
            if (TbCpuHeightStart.Text.Length > 0)
            {
                filterCase = filterCase.Where(x => x.MaxHeightCPUCooler >= Convert.ToInt64(TbCpuHeightStart.Text)).ToList();
            }
            if (TbCpuHeightEnd.Text.Length > 0)
            {
                filterCase = filterCase.Where(x => x.MaxHeightCPUCooler <= Convert.ToInt64(TbCpuHeightEnd.Text)).ToList();
            }
            if (TbSearch.Text.Length > 0)
            {
                filterCase = filterCase.Where(x => x.FullName.ToLower().Contains(TbSearch.Text.ToLower()));
            }

            
            if (filterCase.Count() > count)
            {
                if (filterCase.Count() % count > 0)
                {
                    maxPage = (filterCase.Count() / count) + 1;
                }
                else
                {
                    maxPage = filterCase.Count() / count;
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

            filterCase = filterCase.Skip(count * numberPage).Take(count).ToList();
            LvCases.ItemsSource = filterCase.ToList();
            GeneratePageNumbers();

            if (filterCase.Count() == 0)
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

        private void PriceGraphBtn_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var selectedCase = (sender as TextBlock).DataContext as ComputerCase;
            var dialog = new PriceChartsWindow(selectedCase);
            dialog.ShowDialog();
        }

        private void ResetBtn_Click(object sender, RoutedEventArgs e)
        {
            numberPage = 0;
            fakePage = 1;

            CbSort.SelectedIndex = 0;
            TbSearch.Text = String.Empty;

            CbAccess.IsChecked = true;
            CbNoneAccess.IsChecked = false;

            TbStartPrice.Text = String.Empty;
            TbEndPrice.Text = String.Empty;

            TbCpuHeightStart.Text = String.Empty;
            TbCpuHeightEnd.Text = String.Empty;

            CbManufacturer.SelectedItem = null;
            CbPrimaryColor.SelectedItem = null;
            CbTypeRGB.SelectedItem = null;

            CbNoneWindow.IsChecked = false;
            CbDoubleWindow.IsChecked = false;
            CbLeftWindow.IsChecked = false;
            CbRightWindow.IsChecked = false;

            CbDowner.IsChecked = false;
            CbUpper.IsChecked = false;
            CbOutsider.IsChecked = false;
            CbHider.IsChecked = false;
            CbConfigure.IsChecked = false;

            CbAkrill.IsChecked = false;
            CbTemperedGlass.IsChecked = false;

            CbEatx.IsChecked = false;
            CbFlex.IsChecked = false;
            CbMicro.IsChecked = false;
            CbMiniDtx.IsChecked = false;
            CbMiniItx.IsChecked = false;
            CbSsiCeb.IsChecked = false;
            CbSsiEeb.IsChecked = false;
            CbStandart.IsChecked = false;
            CbThin.IsChecked = false;
            CbXl.IsChecked = false;

            CbDesktop.IsChecked = false;
            CbFullTower.IsChecked = false;
            CbMidTower.IsChecked = false;
            CbMiniTower.IsChecked = false;
            CbSlim.IsChecked = false;
            CbSuperTower.IsChecked = false;
            CbSff.IsChecked = false;
            CbNoneStandart.IsChecked = false;
            CbOpenCase.IsChecked = false;
            CbOpenStand.IsChecked = false;
            Refresh();
        }

        private void TbLinkName_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var selectedCase = (sender as TextBlock).DataContext as ComputerCase;
            NavigationService.Navigate(new ViewCasePage(selectedCase));
        }

        private void BuyBtn_Click(object sender, RoutedEventArgs e)
        {
            var selectedCase = (sender as Button).DataContext as ComputerCase;
            NavigationService.Navigate(new OrderOfferPage(selectedCase));
        }

        private void DeFavouriteBtn_Click(object sender, RoutedEventArgs e)
        {
            var selectedCase = (sender as Button).DataContext as ComputerCase;
            var favourite = App.DB.Favourite.SingleOrDefault(x => x.UserId == App.LoggedUser.Id & x.ComputerCaseId == selectedCase.Id);
            App.DB.Favourite.Remove(favourite);
            App.DB.SaveChanges();
            Refresh();
        }

        private void FavouriteBtn_Click(object sender, RoutedEventArgs e)
        {
            var selectedCase = (sender as Button).DataContext as ComputerCase;

            App.DB.Favourite.Add(new Favourite()
            {
                ComputerCaseId = selectedCase.Id,
                UserId = App.LoggedUser.Id
            });
            App.DB.SaveChanges();
            Refresh();
        }

        private void ApplyBtn_Click(object sender, RoutedEventArgs e)
        {
            numberPage = 0;
            fakePage = 1;
            Refresh();
        }

        private void CbAllMotherboard_Checked(object sender, RoutedEventArgs e)
        {
            CbEatx.IsChecked = true;
            CbFlex.IsChecked = true;
            CbMicro.IsChecked = true;
            CbMiniDtx.IsChecked = true;
            CbMiniItx.IsChecked = true;
            CbSsiCeb.IsChecked = true;
            CbSsiEeb.IsChecked = true;
            CbStandart.IsChecked = true;
            CbThin.IsChecked = true;
            CbXl.IsChecked = true;
        }

        private void CbAllMotherboardOfOne_Checked(object sender, RoutedEventArgs e)
        {
            if (CbEatx.IsChecked == true & CbFlex.IsChecked == true &
                CbMicro.IsChecked == true & CbMiniDtx.IsChecked == true &
                CbMiniItx.IsChecked == true & CbSsiCeb.IsChecked == true &
                CbSsiEeb.IsChecked == true & CbStandart.IsChecked == true &
                CbThin.IsChecked == true & CbXl.IsChecked == true)
                CbAllMotherboard.IsChecked = true;


        }
        private void CbAllMotherboardOfOne_Unchecked(object sender, RoutedEventArgs e)
        {
            if (CbEatx.IsChecked == false || CbFlex.IsChecked == false ||
                CbMicro.IsChecked == false || CbMiniDtx.IsChecked == false ||
                CbMiniItx.IsChecked == false || CbSsiCeb.IsChecked == false ||
                CbSsiEeb.IsChecked == false || CbStandart.IsChecked == false ||
                CbThin.IsChecked == false || CbXl.IsChecked == false)
                CbAllMotherboard.IsChecked = false;
        }

        private void CbAllMotherboard_Unchecked(object sender, RoutedEventArgs e)
        {
            if (CbEatx.IsChecked == false || CbFlex.IsChecked == false ||
                CbMicro.IsChecked == false || CbMiniDtx.IsChecked == false ||
                CbMiniItx.IsChecked == false || CbSsiCeb.IsChecked == false ||
                CbSsiEeb.IsChecked == false || CbStandart.IsChecked == false ||
                CbThin.IsChecked == false || CbXl.IsChecked == false)
            {
                CbAllMotherboard.IsChecked = false;
            }
            else
            {
                CbEatx.IsChecked = false;
                CbFlex.IsChecked = false;
                CbMicro.IsChecked = false;
                CbMiniDtx.IsChecked = false;
                CbMiniItx.IsChecked = false;
                CbSsiCeb.IsChecked = false;
                CbSsiEeb.IsChecked = false;
                CbStandart.IsChecked = false;
                CbThin.IsChecked = false;
                CbXl.IsChecked = false;
            }
        }

        private void CbAllCaseForm_Checked(object sender, RoutedEventArgs e)
        {
            CbDesktop.IsChecked = true;
            CbFullTower.IsChecked = true;
            CbMidTower.IsChecked = true;
            CbMiniTower.IsChecked = true;
            CbSlim.IsChecked = true;
            CbSuperTower.IsChecked = true;
            CbSff.IsChecked = true;
            CbNoneStandart.IsChecked = true;
            CbOpenCase.IsChecked = true;
            CbOpenStand.IsChecked = true;
        }

        private void CbAllCaseForm_Unchecked(object sender, RoutedEventArgs e)
        {
            if (CbDesktop.IsChecked == false || CbFullTower.IsChecked == false ||
                CbMidTower.IsChecked == false || CbMiniTower.IsChecked == false ||
                CbSlim.IsChecked == false || CbSuperTower.IsChecked == false ||
                CbSff.IsChecked == false || CbNoneStandart.IsChecked == false ||
                CbOpenCase.IsChecked == false || CbOpenStand.IsChecked == false)
            {
                CbAllCaseForm.IsChecked = false;
            }
            else
            {
                CbDesktop.IsChecked = false;
                CbFullTower.IsChecked = false;
                CbMidTower.IsChecked = false;
                CbMiniTower.IsChecked = false;
                CbSlim.IsChecked = false;
                CbSuperTower.IsChecked = false;
                CbSff.IsChecked = false;
                CbNoneStandart.IsChecked = false;
                CbOpenCase.IsChecked = false;
                CbOpenStand.IsChecked = false;
            }
        }
        private void CbAllCaseFormsOfOne_Checked(object sender, RoutedEventArgs e)
        {
            if (CbDesktop.IsChecked == true & CbFullTower.IsChecked == true &
                CbMidTower.IsChecked == true & CbMiniTower.IsChecked == true &
                CbSlim.IsChecked == true & CbSuperTower.IsChecked == true &
                CbSff.IsChecked == true & CbNoneStandart.IsChecked == true &
                CbOpenCase.IsChecked == true & CbOpenStand.IsChecked == true)
                CbAllCaseForm.IsChecked = true;

        }
        private void CbAllCaseFormsOfOne_Unchecked(object sender, RoutedEventArgs e)
        {
            if (CbDesktop.IsChecked == false || CbFullTower.IsChecked == false ||
                CbMidTower.IsChecked == false || CbMiniTower.IsChecked == false ||
                CbSlim.IsChecked == false || CbSuperTower.IsChecked == false ||
                CbSff.IsChecked == false || CbNoneStandart.IsChecked == false ||
                CbOpenCase.IsChecked == false || CbOpenStand.IsChecked == false)
                CbAllCaseForm.IsChecked = false;

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

        private void BtnManufacturerClear_Click(object sender, RoutedEventArgs e)
        {
            CbManufacturer.SelectedItem = null;
        }

        private void BtnPrimaryColorClear_Click(object sender, RoutedEventArgs e)
        {
            CbPrimaryColor.SelectedItem = null;
        }

        private void BtnTypeRGBClear_Click(object sender, RoutedEventArgs e)
        {
            CbTypeRGB.SelectedItem = null;
        }

        private void CbManufacturer_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CbManufacturer.SelectedItem != null)
                BtnManufacturerClear.Visibility = Visibility.Visible;
            else
                BtnManufacturerClear.Visibility = Visibility.Collapsed;
        }

        private void CbPrimaryColor_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CbPrimaryColor.SelectedItem != null)
                BtnPrimaryColorClear.Visibility = Visibility.Visible;
            else
                BtnPrimaryColorClear.Visibility = Visibility.Collapsed;
        }

        private void CbTypeRGB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CbTypeRGB.SelectedItem != null)
                BtnTypeRGBClear.Visibility = Visibility.Visible;
            else
                BtnTypeRGBClear.Visibility = Visibility.Collapsed;
        }



        private void TbStartPrice_LostFocus(object sender, RoutedEventArgs e)
        {
            if (TbStartPrice.Text == String.Empty)
            {
                return;
            }
            int value = int.Parse(TbStartPrice.Text);
            if (value < App.DB.ComputerCase.Min(x => x.Price))
            {
                value = App.DB.ComputerCase.Min(x => x.Price);
                TbStartPrice.Text = value.ToString();
            }
            if (value > App.DB.ComputerCase.Max(x => x.Price))
            {
                value = App.DB.ComputerCase.Max(x => x.Price);
                TbStartPrice.Text = value.ToString();
            }
            if (TbEndPrice.Text != String.Empty & TbStartPrice.Text != String.Empty)
            {
                if (int.Parse(TbEndPrice.Text) < int.Parse(TbStartPrice.Text))
                {
                    TbEndPrice.Text = App.DB.ComputerCase.Max(x => x.Price).ToString();
                    TbStartPrice.Text = App.DB.ComputerCase.Min(x => x.Price).ToString();
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
            if (value < App.DB.ComputerCase.Min(x => x.Price))
            {
                value = App.DB.ComputerCase.Min(x => x.Price);
                TbEndPrice.Text = value.ToString();
            }
            if (value > App.DB.ComputerCase.Max(x => x.Price))
            {
                value = App.DB.ComputerCase.Max(x => x.Price);
                TbEndPrice.Text = value.ToString();
            }
            if (TbEndPrice.Text != String.Empty & TbStartPrice.Text != String.Empty)
            {
                if (int.Parse(TbEndPrice.Text) < int.Parse(TbStartPrice.Text))
                {
                    TbEndPrice.Text = App.DB.ComputerCase.Max(x => x.Price).ToString();
                    TbStartPrice.Text = App.DB.ComputerCase.Min(x => x.Price).ToString();
                }
            }

        }

        private void TbCpuHeightEnd_LostFocus(object sender, RoutedEventArgs e)
        {
            if (TbCpuHeightEnd.Text == String.Empty)
            {
                return;
            }
            int value = int.Parse(TbCpuHeightEnd.Text);
            if (value < App.DB.ComputerCase.Min(x => x.MaxHeightCPUCooler))
            {
                value = App.DB.ComputerCase.Min(x => x.MaxHeightCPUCooler);
                TbCpuHeightEnd.Text = value.ToString();
            }
            if (value > App.DB.ComputerCase.Max(x => x.MaxHeightCPUCooler))
            {
                value = App.DB.ComputerCase.Max(x => x.MaxHeightCPUCooler);
                TbCpuHeightEnd.Text = value.ToString();
            }
            if (TbCpuHeightEnd.Text != String.Empty & TbCpuHeightStart.Text != String.Empty)
            {
                if (int.Parse(TbCpuHeightEnd.Text) < int.Parse(TbCpuHeightStart.Text))
                {
                    TbCpuHeightEnd.Text = App.DB.ComputerCase.Max(x => x.MaxHeightCPUCooler).ToString();
                    TbCpuHeightStart.Text = App.DB.ComputerCase.Min(x => x.MaxHeightCPUCooler).ToString();
                }
            }
        }

        private void TbCpuHeightStart_LostFocus(object sender, RoutedEventArgs e)
        {
            if (TbCpuHeightStart.Text == String.Empty)
            {
                return;
            }
            int value = int.Parse(TbCpuHeightStart.Text);
            if (value < App.DB.ComputerCase.Min(x => x.MaxHeightCPUCooler))
            {
                value = App.DB.ComputerCase.Min(x => x.MaxHeightCPUCooler);
                TbCpuHeightStart.Text = value.ToString();
            }
            if (value > App.DB.ComputerCase.Max(x => x.MaxHeightCPUCooler))
            {
                value = App.DB.ComputerCase.Max(x => x.MaxHeightCPUCooler);
                TbCpuHeightStart.Text = value.ToString();
            }
            if (TbCpuHeightEnd.Text != String.Empty & TbCpuHeightStart.Text != String.Empty)
            {
                if (int.Parse(TbCpuHeightEnd.Text) < int.Parse(TbCpuHeightStart.Text))
                {
                    TbCpuHeightEnd.Text = App.DB.ComputerCase.Max(x => x.MaxHeightCPUCooler).ToString();
                    TbCpuHeightStart.Text = App.DB.ComputerCase.Min(x => x.MaxHeightCPUCooler).ToString();
                }
            }
        }

        private void SearchIcon_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            numberPage = 0;
            fakePage = 1;
            Refresh();
        }


    }
}
