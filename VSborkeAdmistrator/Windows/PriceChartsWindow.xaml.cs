﻿using System;
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
        private void ChartsButton_Click(object sender, RoutedEventArgs e)
        {
            // Удаляем прежний график.
            GridForChart.Children.OfType<Canvas>().ToList().ForEach(p => GridForChart.Children.Remove(p));

            Chart chart = null;

            Button button = sender as Button;

            // Создаём новый график выбранного вида.
            switch (button.Name)
            {
                case "BarsButton":
                    if ((chart is BarChart) == false)
                    {
                        chart = new BarChart();
                    }

                    break;
                case "LineButton":
                    if ((chart is LineChart) == false)
                    {
                        chart = new LineChart();
                    }

                    break;
                case "PieButton":
                    if ((chart is PieChart) == false)
                    {
                        chart = new PieChart();
                    }

                    break;
            }

            // Добавляем новую диаграмму на поле контейнера для графиков.
            GridForChart.Children.Add(chart.ChartBackground);

            // Принудительно обновляем размеры контейнера для графика.
            GridForChart.UpdateLayout();

            // Создаём график.
            CreateChart(chart);

        }

        private static void CreateChart(Chart chart)
        {
            chart.Clear();

            Random random = new Random();

            for (int i = 0; i < random.Next(1, 25); i++)
            {
                chart.AddValue(random.Next(0, 2001));
            }
        }

    }
}
