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
using System.Windows.Shapes;
using VSborkeAdmistrator.Components;
using WpfDrawing.Charts;

namespace VSborkeAdmistrator.Windows
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
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
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
            

            IEnumerable<PriceHistory> priceHistories = App.DB.PriceHistory.Where(x => x.ComputerCaseId == contextComputerCase.Id).OrderBy(x =>x.DateHistory);
            
            foreach(PriceHistory pc in priceHistories)
            {
                chart.AddValue(pc.Price);
            }
        }

    }
}
