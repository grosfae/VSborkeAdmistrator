using LiveCharts.Wpf;
using LiveCharts;
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
using System.Xml.Linq;
using VSborkeMaster.Components;

namespace VSborkeMaster.Pages
{
    /// <summary>
    /// Логика взаимодействия для PriceGraphMiniPage.xaml
    /// </summary>
    public partial class PriceGraphMiniPage : Page
    {
        ComputerCase contextComputerCase;
        public PriceGraphMiniPage(ComputerCase computerCase)
        {
            InitializeComponent();
            contextComputerCase = computerCase;
            DataContext = contextComputerCase;
        }

        public SeriesCollection SeriesCollection { get; set; }

        public string[] Labels { get; set; }

        public Func<double, string> YFormatter { get; set; }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
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
    }
}
