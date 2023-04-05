using System;
using System.Collections.Generic;
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
    /// Логика взаимодействия для CasesPage.xaml
    /// </summary>
    public partial class CasesPage : Page
    {
        public CasesPage()
        {
            InitializeComponent();
            TbStartPrice.Tag = $"от {App.DB.ComputerCase.Min(x => x.Price)}";
            TbEndPrice.Tag = $"до {App.DB.ComputerCase.Max(x => x.Price)}";
            CbManufacturer.ItemsSource = App.DB.Manufacturer.ToList();
            CbPrimaryColor.ItemsSource = App.DB.PrimaryColor.ToList();
            CbTypeRGB.ItemsSource = App.DB.TypeRGB.ToList();


        }

        private void Refresh()
        {
            IEnumerable<ComputerCase> filterCase = App.DB.ComputerCase.Where(x => x.IsCustom == false);
            if (CbAccess.IsChecked == true & CbNoneAccess.IsChecked == true)
            {

            }
            else
            {
                if (CbAccess.IsChecked == true)
                {
                    filterCase = filterCase.Where(x => x.IsAccessable == true).ToList();
                }
                if (CbNoneAccess.IsChecked == true)
                {
                    filterCase = filterCase.Where(x => x.IsAccessable == false).ToList();
                }
            }
            if (CbDeleted.IsChecked == true)
            {
                filterCase = filterCase.Where(x => x.IsDelete == true).ToList();
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

            if (CbEatx.IsChecked == true)
            {
                filterCase = filterCase.Where(x => x.EAtx == true).ToList();
            }
            if (CbFlex.IsChecked == true)
            {
                filterCase = filterCase.Where(x => x.FlexAtx == true).ToList();
            }
            if (CbMicro.IsChecked == true)
            {
                filterCase = filterCase.Where(x => x.MicroAtx == true).ToList();
            }
            if (CbMiniDtx.IsChecked == true)
            {
                filterCase = filterCase.Where(x => x.MiniDtx == true).ToList();
            }
            if (CbMiniItx.IsChecked == true)
            {
                filterCase = filterCase.Where(x => x.MiniItx == true).ToList();
            }
            if (CbSsiCeb.IsChecked == true)
            {
                filterCase = filterCase.Where(x => x.SsiCeb == true).ToList();
            }
            if (CbSsiEeb.IsChecked == true)
            {
                filterCase = filterCase.Where(x => x.SsiEeb == true).ToList();
            }
            if (CbStandart.IsChecked == true)
            {
                filterCase = filterCase.Where(x => x.StandartAtx == true).ToList();
            }
            if (CbThin.IsChecked == true)
            {
                filterCase = filterCase.Where(x => x.ThinMiniItx == true).ToList();
            }
            if (CbXl.IsChecked == true)
            {
                filterCase = filterCase.Where(x => x.XlAtx == true).ToList();
            }

            if (CbDesktop.IsChecked == true)
            {
                filterCase = filterCase.Where(x => x.FormFactor.Name == "Desktop").ToList();
            }
            if (CbMidTower.IsChecked == true)
            {
                filterCase = filterCase.Where(x => x.FormFactor.Name == "Mid-Tower").ToList();
            }
            if (CbFullTower.IsChecked == true)
            {
                filterCase =  filterCase.Where(x => x.FormFactor.Name == "Full-Tower").ToList();
            }
            if (CbMiniTower.IsChecked == true)            {
                filterCase = filterCase.Where(x => x.FormFactor.Name == "Mini-Tower").ToList();
            }
            if (CbSlim.IsChecked == true)
            {
                filterCase = filterCase.Where(x => x.FormFactor.Name == "Slim").ToList();
            }
            if (CbSuperTower.IsChecked == true)
            {
                filterCase = filterCase.Where(x => x.FormFactor.Name == "Super/Ultra-Tower").ToList();
            }
            if (CbMiniTower.IsChecked == true)
            {
                filterCase = filterCase.Where(x => x.FormFactor.Name == "Mini-Tower").ToList();
            }
            LvCases.ItemsSource = filterCase.ToList();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            Refresh();
        }


        private void ResetBtn_Click(object sender, RoutedEventArgs e)
        {
            CbAccess.IsChecked = true;
            CbNoneAccess.IsChecked = false;
            CbDeleted.IsChecked = false;

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
        }

        private void TbLinkName_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var selectedCase = (sender as TextBlock).DataContext as ComputerCase;
            NavigationService.Navigate(new ComputerCaseEditPage(selectedCase));
        }

        private void BuyBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void NotificBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void EditBtn_Click(object sender, RoutedEventArgs e)
        {
            var selectedCase = (sender as Button).DataContext as ComputerCase;
            NavigationService.Navigate(new ComputerCaseEditPage(selectedCase));
        }

        private void DeFavouriteBtn_Click(object sender, RoutedEventArgs e)
        {
            var selectedCase = (sender as Button).DataContext as ComputerCase;
            var favourite = App.DB.Favourite.SingleOrDefault(x => x.UserId == App.LoggedUser.Id & x.ComputerCaseId == selectedCase.Id);
            App.DB.Favourite.Remove(favourite);
            App.DB.SaveChanges();
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

        }

        private void ApplyBtn_Click(object sender, RoutedEventArgs e)
        {
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
            if(CbEatx.IsChecked == true & CbFlex.IsChecked == true & 
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
    }
}
