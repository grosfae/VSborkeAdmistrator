using Microsoft.Win32;
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

namespace VSborkeAdmistrator.Pages
{
    /// <summary>
    /// Логика взаимодействия для ComputerCaseEditPage.xaml
    /// </summary>
    public partial class ComputerCaseEditPage : Page
    {
        ComputerCase contextComputerCase;
        public ComputerCaseEditPage(ComputerCase computerCase)
        {
            InitializeComponent();
            contextComputerCase = computerCase;
            DataContext = contextComputerCase;
            LvAdditionImages.ItemsSource = App.DB.AdditionComputerCaseImage.Where(x => x.ComputerCaseId == contextComputerCase.Id).ToList();

            CbAlignmentPowerBlock.ItemsSource = App.DB.AlignmentPowerBlock.ToList();
            CbAlignmentWindow.ItemsSource = App.DB.WindowAlignment.ToList();
            CbBackCooler.ItemsSource = App.DB.SupportBackCooler.ToList();
            CbBackLiquid.ItemsSource = App.DB.BackLiquidCooling.ToList();
            CbBottomCooler.ItemsSource = App.DB.SupportBottomCooler.ToList();
            CbBottomLiquid.ItemsSource = App.DB.BottomLiquidCooling.ToList();
            CbColorRGB.ItemsSource = App.DB.ColorRGB.ToList();
            CbCoolerInside.ItemsSource = App.DB.CoolerInside.ToList();
            CbFormFactor.ItemsSource = App.DB.FormFactor.ToList();
            CbFormPowerBlock.ItemsSource = App.DB.PowerBlockStandartSupport.ToList();
            CbFrontCooler.ItemsSource = App.DB.SupportFrontCooler.ToList();
            CbFrontLiquid.ItemsSource = App.DB.FrontLiquidCooling.ToList();
            CbFrontPanelMaterial.ItemsSource = App.DB.FrontPanelMaterial.ToList();
            CbHorizontalAddonsSlot.ItemsSource = App.DB.HorizontalAddonSlot.ToList();
            CbIoAlignment.ItemsSource = App.DB.IOPanelAlignment.ToList();
            CbIoConnectors.ItemsSource = App.DB.IOPanel.ToList();
            CbManufacturer.ItemsSource= App.DB.Manufacturer.ToList();
            CbMaterialSet.ItemsSource = App.DB.MaterialSet.ToList();
            CbMaterialWindow.ItemsSource = App.DB.WindowMaterial.ToList();
            CbOrientationMotherboard.ItemsSource = App.DB.OrientationMotherboard.ToList();
            CbPrimaryColor.ItemsSource = App.DB.PrimaryColor.ToList();
            CbSecondaryColor.ItemsSource = App.DB.SecondColor.ToList();
            CbSideCooler.ItemsSource = App.DB.SupportSideCooler.ToList();
            CbSidePanelFixation.ItemsSource = App.DB.SidePanelFixation.ToList();
            CbSlotHDD.ItemsSource = App.DB.SlotHDD.ToList();
            CbSlotSSD.ItemsSource = App.DB.SlotSSD.ToList();
            CbSlotXHDD.ItemsSource = App.DB.SlotXHDD.ToList();
            CbSourceRGB.ItemsSource = App.DB.SourceRGB.ToList();
            CbTypeManagmentRGB.ItemsSource = App.DB.TypeManagmentRGB.ToList();
            CbTypeRGB.ItemsSource = App.DB.TypeRGB.ToList();
            CbTopCooler.ItemsSource = App.DB.SupportTopCooler.ToList();
            CbVerticalAddonSlot.ItemsSource = App.DB.VerticalAddonSlot.ToList();
            
        }
        

        private void MainImageBtn_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new OpenFileDialog();
            if (dialog.ShowDialog().GetValueOrDefault())
            {
                contextComputerCase.MainImage = File.ReadAllBytes(dialog.FileName);
                DataContext = null;
                DataContext = contextComputerCase;
            }
        }

        private void LeftBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void RightBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void SaveBtn_Click(object sender, RoutedEventArgs e)
        {

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
    }
}
