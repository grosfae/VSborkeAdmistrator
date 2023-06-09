using LiveCharts.Wpf;
using LiveCharts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using VSborkeUser.Components;

namespace VSborkeUser.Windows
{
    /// <summary>
    /// Логика взаимодействия для PriceChartsWindow.xaml
    /// </summary>
    public partial class PriceChartsWindow : Window
    {
        ComputerCase contextComputerCase;
        public PriceChartsWindow(ComputerCase computerCase)
        {
            InitializeComponent();
            contextComputerCase = computerCase;
            DataContext = contextComputerCase;
            Header.MouseLeftButtonDown += new MouseButtonEventHandler(Window_MouseDown);
        }
        public SeriesCollection SeriesCollection { get; set; }

        public string[] Labels { get; set; }

        public Func<double, string> YFormatter { get; set; }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            TbName.Text = contextComputerCase.NameForWindows;
            TbCost.Text = $"{contextComputerCase.PriceDiscount} ₽";
            TbPriceRange.Text = contextComputerCase.PriceRange;
            IEnumerable<PriceHistory> priceHistories = App.DB.PriceHistory.Where(x => x.ComputerCaseId == contextComputerCase.Id).OrderBy(x => x.DateHistory);
            int buf = 0;
            double[] MyPrice = new double[App.DB.PriceHistory.Where(x => x.ComputerCaseId == contextComputerCase.Id).Count()];
            DateTime[] MyDates = new DateTime[App.DB.PriceHistory.Where(x => x.ComputerCaseId == contextComputerCase.Id).Count()];
            string[] labels = new string[App.DB.PriceHistory.Where(x => x.ComputerCaseId == contextComputerCase.Id).Count()];

            foreach (var pcf in priceHistories)
            {
                MyPrice[buf] = pcf.Price;
                MyDates[buf] = pcf.DateHistory;
                labels[buf] = pcf.DateHistory.ToString("d MMMM yyyy", new System.Globalization.CultureInfo("ru-RU"));
                buf++;
            }

            SeriesCollection = new SeriesCollection
            {
                new LineSeries
                {
                    Title = "Цена",
                    Stroke = new SolidColorBrush(Color.FromRgb(56, 129, 239)),
                    Values = new ChartValues<double> (MyPrice)
                },

            };


            Labels = labels;
            YFormatter = value => value.ToString("C");

            DataContext = this;

        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void CloseButton_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }

    }
}
