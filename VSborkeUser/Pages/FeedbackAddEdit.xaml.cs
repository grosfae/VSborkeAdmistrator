using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
    /// Логика взаимодействия для FeedbackAddEdit.xaml
    /// </summary>
    public partial class FeedbackAddEdit : Page
    {
        FeedBack contextFeedback;
        ComputerCase contextComputerCase;
        public int progress = 0;
        public FeedbackAddEdit(FeedBack feedBack, ComputerCase computerCase)
        {
            InitializeComponent();
            contextFeedback = feedBack;
            DataContext = contextFeedback;
            contextComputerCase = computerCase;
        }

        private void ReviewSaveBtn_Click(object sender, RoutedEventArgs e)
        {
            string errorMessage = "";
            if (contextFeedback.GeneralStars == 0)
            {
                errorMessage += "Выберите оценку\n";
            }
            
            if (string.IsNullOrWhiteSpace(errorMessage) == false)
            {
                CustomMessageBox.Show(errorMessage, CustomMessageBox.CustomMessageBoxTitle.Предупреждение, CustomMessageBox.CustomMessageBoxButton.Ok, CustomMessageBox.CustomMessageBoxButton.Нет);
                return;
            }
            if (contextFeedback.Id == 0)
            {
                contextFeedback.ComputerCaseId = contextComputerCase.Id;
                contextFeedback.DateOfReview= DateTime.Now;
                contextFeedback.UserId = App.LoggedUser.Id;
                App.DB.FeedBack.Add(contextFeedback);
            }
            if (TbAddition.Text.Length > 0)
            {
                contextFeedback.DateOfAddition= DateTime.Now;
            }
            App.DB.SaveChanges();
            CustomMessageBox.Show("Отзыв сохранен!", CustomMessageBox.CustomMessageBoxTitle.Успешно, CustomMessageBox.CustomMessageBoxButton.Ok, CustomMessageBox.CustomMessageBoxButton.Нет);
            NavigationService.GoBack();
        }

        private void TbOneStar_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            TbOneStar.Text = @""; 
            TbTwoStar.Text = @""; 
            TbThreeStar.Text = @"";
            TbFourStar.Text = @""; 
            TbFiveStar.Text = @"";
            contextFeedback.GeneralStars = 1;

            if(contextFeedback.GeneralStars != 0)
            {
                CbMark.IsChecked= true;
                progress = 40;
            }
            if(TbAdvantage.Text.Length > 0)
            {
                CbAdventages.IsChecked= true;
                progress += 20;
            }

            if (TbDisadvantage.Text.Length > 0)
            {
                CbDisadventages.IsChecked = true;
                progress += 20;
            }
            if (TbComment.Text.Length > 0)
            {
                CbComment.IsChecked = true;
                progress += 20;
            }
            PbReviewProgress.Value = progress;
            TbCountProgress.Text = $"{progress}%";

        }

        private void TbTwoStar_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            TbOneStar.Text = @"";
            TbTwoStar.Text = @"";
            TbThreeStar.Text = @"";
            TbFourStar.Text = @"";
            TbFiveStar.Text = @"";
            contextFeedback.GeneralStars = 2;
            if (contextFeedback.GeneralStars != 0)
            {
                CbMark.IsChecked = true;
                progress = 40;
            }
            if (TbAdvantage.Text.Length > 0)
            {
                CbAdventages.IsChecked = true;
                progress += 20;
            }

            if (TbDisadvantage.Text.Length > 0)
            {
                CbDisadventages.IsChecked = true;
                progress += 20;
            }
            if (TbComment.Text.Length > 0)
            {
                CbComment.IsChecked = true;
                progress += 20;
            }
            PbReviewProgress.Value = progress;
            TbCountProgress.Text = $"{progress}%";
        }

        private void TbThreeStar_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            TbOneStar.Text = @"";
            TbTwoStar.Text = @"";
            TbThreeStar.Text = @"";
            TbFourStar.Text = @"";
            TbFiveStar.Text = @"";
            contextFeedback.GeneralStars = 3;
            if (contextFeedback.GeneralStars != 0)
            {
                CbMark.IsChecked = true;
                progress = 40;
            }
            if (TbAdvantage.Text.Length > 0)
            {
                CbAdventages.IsChecked = true;
                progress += 20;
            }

            if (TbDisadvantage.Text.Length > 0)
            {
                CbDisadventages.IsChecked = true;
                progress += 20;
            }
            if (TbComment.Text.Length > 0)
            {
                CbComment.IsChecked = true;
                progress += 20;
            }
            PbReviewProgress.Value = progress;
            TbCountProgress.Text = $"{progress}%";
        }

        private void TbFourStar_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            TbOneStar.Text = @"";
            TbTwoStar.Text = @"";
            TbThreeStar.Text = @"";
            TbFourStar.Text = @"";
            TbFiveStar.Text = @"";
            contextFeedback.GeneralStars = 4;
            if (contextFeedback.GeneralStars != 0)
            {
                CbMark.IsChecked = true;
                progress = 40;
            }
            if (TbAdvantage.Text.Length > 0)
            {
                CbAdventages.IsChecked = true;
                progress += 20;
            }

            if (TbDisadvantage.Text.Length > 0)
            {
                CbDisadventages.IsChecked = true;
                progress += 20;
            }
            if (TbComment.Text.Length > 0)
            {
                CbComment.IsChecked = true;
                progress += 20;
            }
            PbReviewProgress.Value = progress;
            TbCountProgress.Text = $"{progress}%";
        }

        private void TbFiveStar_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            TbOneStar.Text = @""; 
            TbTwoStar.Text = @""; 
            TbThreeStar.Text = @"";
            TbFourStar.Text = @""; 
            TbFiveStar.Text = @"";
            contextFeedback.GeneralStars = 5;
            if (contextFeedback.GeneralStars != 0)
            {
                CbMark.IsChecked = true;
                progress = 40;
            }
            if (TbAdvantage.Text.Length > 0)
            {
                CbAdventages.IsChecked = true;
                progress += 20;
            }

            if (TbDisadvantage.Text.Length > 0)
            {
                CbDisadventages.IsChecked = true;
                progress += 20;
            }
            if (TbComment.Text.Length > 0)
            {
                CbComment.IsChecked = true;
                progress += 20;
            }
            PbReviewProgress.Value = progress;
            TbCountProgress.Text = $"{progress}%";
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            if(contextFeedback.Id == 0)
            {
                SpAddition.Visibility = Visibility.Collapsed;
            }
            if (contextFeedback.Id != 0)
            {
                TbAdvantage.IsEnabled = false;
                TbDisadvantage.IsEnabled = false;
                TbComment.IsEnabled = false;
            }
            if (contextFeedback.GeneralStars == 1)
            {
                TbOneStar.Text = @"";
                TbTwoStar.Text = @"";
                TbThreeStar.Text = @"";
                TbFourStar.Text = @"";
                TbFiveStar.Text = @"";
                CbMark.IsChecked = true;
                progress = 40;
            }
            if (contextFeedback.GeneralStars == 2)
            {
                TbOneStar.Text = @"";
                TbTwoStar.Text = @"";
                TbThreeStar.Text = @"";
                TbFourStar.Text = @"";
                TbFiveStar.Text = @"";
                CbMark.IsChecked = true;
                progress = 40;
            }
            if (contextFeedback.GeneralStars == 3)
            {
                TbOneStar.Text = @"";
                TbTwoStar.Text = @"";
                TbThreeStar.Text = @"";
                TbFourStar.Text = @"";
                TbFiveStar.Text = @"";
                CbMark.IsChecked = true;
                progress = 40;
            }
            if (contextFeedback.GeneralStars == 4)
            {
                TbOneStar.Text = @"";
                TbTwoStar.Text = @"";
                TbThreeStar.Text = @"";
                TbFourStar.Text = @"";
                TbFiveStar.Text = @"";
                CbMark.IsChecked = true;
                progress = 40;
            }
            if (contextFeedback.GeneralStars == 5)
            {
                TbOneStar.Text = @"";
                TbTwoStar.Text = @"";
                TbThreeStar.Text = @"";
                TbFourStar.Text = @"";
                TbFiveStar.Text = @"";
                CbMark.IsChecked = true;
                progress = 40;
            }
            if (TbAdvantage.Text.Length > 0)
            {
                CbAdventages.IsChecked = true;
                progress += 20;
            }

            if (TbDisadvantage.Text.Length > 0)
            {
                CbDisadventages.IsChecked = true;
                progress += 20;
            }
            if (TbComment.Text.Length > 0)
            {
                CbComment.IsChecked = true;
                progress += 20;
            }
            PbReviewProgress.Value = progress;
            TbCountProgress.Text = $"{progress}%";
        }

        private void BackBtn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void TbAdvantage_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (TbAdvantage.Text.Length > 0 & CbAdventages.IsChecked != true)
            {
                CbAdventages.IsChecked = true;
                progress += 20;
            }
            if (TbAdvantage.Text.Length == 0)
            {
                CbAdventages.IsChecked = false;
                progress -= 20;
            }
            PbReviewProgress.Value = progress;
            TbCountProgress.Text = $"{progress}%";
        }

        private void TbDisadvantage_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (TbDisadvantage.Text.Length > 0 & CbDisadventages.IsChecked != true)
            {
                CbDisadventages.IsChecked = true;
                progress += 20;
            }
            if (TbDisadvantage.Text.Length == 0)
            {
                CbDisadventages.IsChecked = false;
                progress -= 20;
            }
            PbReviewProgress.Value = progress;
            TbCountProgress.Text = $"{progress}%";
        }

        private void TbComment_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (TbComment.Text.Length > 0 & CbComment.IsChecked != true)
            {
                CbComment.IsChecked = true;
                progress += 20;
            }
            if (TbComment.Text.Length == 0)
            {
                CbComment.IsChecked = false;
                progress -= 20;
            }
            PbReviewProgress.Value = progress;
            TbCountProgress.Text = $"{progress}%";
        }
    }
}
