using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using VSborkeAdmistrator.Components;
using VSborkeAdmistrator.Windows;

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

            if(contextComputerCase.Id == 0)
            {
                contextComputerCase.CableManagementBackSide = false;
                contextComputerCase.CardReader = false;
                contextComputerCase.ThinMiniItx = false;
                contextComputerCase.MiniItx = false;
                contextComputerCase.MiniDtx = false;
                contextComputerCase.EAtx = false;
                contextComputerCase.StandartAtx = false;
                contextComputerCase.CutCPUCooler = false;
                contextComputerCase.DustFilter = false;
                contextComputerCase.FlexAtx = false;
                contextComputerCase.IsAccessable = false;
                contextComputerCase.IsAntiVibration = false;
                contextComputerCase.IsCustom = false;
                contextComputerCase.MicroAtx = false;
                contextComputerCase.RGB = false;
                contextComputerCase.SpecialDesign = false;
                contextComputerCase.XlAtx = false;
                contextComputerCase.WindowOnSide = false;
                contextComputerCase.IsDelete = false;
                contextComputerCase.SupportLiquidCooling = false;
                contextComputerCase.SsiCeb = false;
                contextComputerCase.SsiEeb = false;
            }

            

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
            CbManufacturer.ItemsSource= App.DB.Manufacturer.OrderBy(x => x.Name).ToList();
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
            CbConnectorRGB.ItemsSource= App.DB.ConnectorRGB.ToList();

        }
        

        private void MainImageBtn_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new Microsoft.Win32.OpenFileDialog()
            {
                Filter = "*.jpg|*.jpg|*.png|*.png|*.jpeg|*.jpeg"
            };
            if (dialog.ShowDialog().GetValueOrDefault())
            {
                contextComputerCase.MainImage = File.ReadAllBytes(dialog.FileName);
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
            if (contextComputerCase.Price <= 0 )
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

        private void AdditionImgAdd_Click(object sender, RoutedEventArgs e)
        {
            if (contextComputerCase.Id == 0)
            {
                CustomMessageBox.Show("Сохраните данные о корпусе перед добавлением доп. изображений", CustomMessageBox.CustomMessageBoxTitle.Warning, CustomMessageBox.CustomMessageBoxButton.Ok, CustomMessageBox.CustomMessageBoxButton.Нет);
                return;
            }
            AdditionComputerCaseImage casePhoto = new AdditionComputerCaseImage();
            var dialog = new Microsoft.Win32.OpenFileDialog()
            {
                Filter = "*.jpg|*.jpg|*.png|*.png|*.jpeg|*.jpeg"
            };
            if (dialog.ShowDialog().GetValueOrDefault())
            {
                casePhoto.AdditionImage = File.ReadAllBytes(dialog.FileName);
                casePhoto.ComputerCaseId = contextComputerCase.Id;
                App.DB.AdditionComputerCaseImage.Add(casePhoto);
                App.DB.SaveChanges();
                LvAdditionImages.ItemsSource = App.DB.AdditionComputerCaseImage.Where(x => x.ComputerCaseId == contextComputerCase.Id).ToList();
            }
        }

        private void AdditionImgDelete_Click(object sender, RoutedEventArgs e)
        {
            var selectedPhoto = LvAdditionImages.SelectedItem as AdditionComputerCaseImage;
            if (selectedPhoto == null)
            {
                CustomMessageBox.Show("Выберите дополнительное изображение", CustomMessageBox.CustomMessageBoxTitle.Warning, CustomMessageBox.CustomMessageBoxButton.Ok, CustomMessageBox.CustomMessageBoxButton.Нет);
                return;
            }
            App.DB.AdditionComputerCaseImage.Remove(selectedPhoto);
            App.DB.SaveChanges();
            LvAdditionImages.ItemsSource = App.DB.AdditionComputerCaseImage.Where(x => x.ComputerCaseId == contextComputerCase.Id).ToList();
        }

        private void CbWindowOnSide_Checked(object sender, RoutedEventArgs e)
        {
            CbAlignmentWindow.IsEnabled = true;
            CbMaterialWindow.IsEnabled = true;
        }

        private void CbWindowOnSide_Unchecked(object sender, RoutedEventArgs e)
        {
            CbAlignmentWindow.IsEnabled = false;
            CbMaterialWindow.IsEnabled = false;
            CbAlignmentWindow.SelectedIndex = 0;
            CbMaterialWindow.SelectedIndex = 0;
        }

        private void CbSuppLiquid_Checked(object sender, RoutedEventArgs e)
        {
            CbBackLiquid.IsEnabled = true;
            CbBottomLiquid.IsEnabled = true;
            CbFrontLiquid.IsEnabled = true;
        }

        private void CbSuppLiquid_Unchecked(object sender, RoutedEventArgs e)
        {
            CbBackLiquid.IsEnabled = false;
            CbBottomLiquid.IsEnabled = false;
            CbFrontLiquid.IsEnabled = false;
            CbFrontLiquid.SelectedIndex = 0;
            CbBackLiquid.SelectedIndex = 0;
            CbBottomLiquid.SelectedIndex = 0;
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            if (contextComputerCase.Id == 0)
            {
                if (CbWindowOnSide.IsChecked == false)
                {
                    CbAlignmentWindow.IsEnabled = false;
                    CbMaterialWindow.IsEnabled = false;
                    CbAlignmentWindow.SelectedIndex = 0;
                    CbMaterialWindow.SelectedIndex = 0;
                }

                if (CbSuppLiquid.IsChecked == false)
                {
                    CbBackLiquid.IsEnabled = false;
                    CbBottomLiquid.IsEnabled = false;
                    CbFrontLiquid.IsEnabled = false;
                    CbFrontLiquid.SelectedIndex = 0;
                    CbBackLiquid.SelectedIndex = 0;
                    CbBottomLiquid.SelectedIndex = 0;
                }

                if (CbRGB.IsChecked == false)
                {
                    CbColorRGB.IsEnabled = false;
                    CbSourceRGB.IsEnabled = false;
                    CbTypeManagmentRGB.IsEnabled = false;
                    CbTypeRGB.IsEnabled = false;
                    CbConnectorRGB.IsEnabled = false;

                    CbColorRGB.SelectedIndex = 0;
                    CbSourceRGB.SelectedIndex = 0;
                    CbTypeManagmentRGB.SelectedIndex = 0;
                    CbTypeRGB.SelectedIndex = 0;
                    CbConnectorRGB.SelectedIndex = 0;
                }
            }
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            CbColorRGB.IsEnabled = true;
            CbSourceRGB.IsEnabled = true;
            CbTypeManagmentRGB.IsEnabled = true;
            CbTypeRGB.IsEnabled = true;
            CbConnectorRGB.IsEnabled = true;
        }

        private void CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            CbColorRGB.IsEnabled = false;
            CbSourceRGB.IsEnabled = false;
            CbTypeManagmentRGB.IsEnabled = false;
            CbTypeRGB.IsEnabled = false;
            CbConnectorRGB.IsEnabled = false;

            CbColorRGB.SelectedIndex= 0;
            CbSourceRGB.SelectedIndex= 0;
            CbTypeManagmentRGB.SelectedIndex= 0;
            CbTypeRGB.SelectedIndex= 0;
            CbConnectorRGB.SelectedIndex = 0;
        }

        int numberPage = 0;
        int count = 4;

        private void Update()
        {
            IEnumerable<AdditionComputerCaseImage> caseImagesList = App.DB.AdditionComputerCaseImage.Where(x => x.ComputerCaseId == contextComputerCase.Id);
            caseImagesList = caseImagesList.Skip(count * numberPage).Take(count);
            LvAdditionImages.ItemsSource = caseImagesList;
        }
    }
}
