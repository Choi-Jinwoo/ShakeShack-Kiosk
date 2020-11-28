using LiveCharts;
using LiveCharts.Wpf;
using ShakeShack_Kiosk.Model;
using ShakeShack_Kiosk.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using System.Windows.Threading;

namespace ShakeShack_Kiosk.Controls
{
    /// <summary>
    /// StatControl.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class StatControl : UserControl, INotifyPropertyChanged
    {
        private OrderHistoryViewModel orderHistoryViewModel = OrderHistoryViewModel.Instance;
        private TableViewModel tableViewModel = TableViewModel.Instance;
        private List<ChartItem> chartItems = new List<ChartItem>();


        private enum GraphType
        {
            PRICE,
            COUNT,
        }

        public List<string> Labels { get; set; } = new List<string>();
        public Func<double, string> YFormatter { get; set; } = value => value.ToString("C");

        private SeriesCollection seriesCollection { get; set; }
        public SeriesCollection SeriesCollection
        {
            get => seriesCollection;
            set
            {
                seriesCollection = value;
                OnPropertyChanged("SeriesCollection");
            }
        }
        public SeriesCollection SalesSeriesCollection { get; set; }

        public StatControl()
        {
            InitializeComponent();

            spTotalSales.DataContext = orderHistoryViewModel;
            dgFoodTable.ItemsSource = orderHistoryViewModel.GetFoodChartItemByTable(tableViewModel.tables[0].Number);
            dgCategoryTable.ItemsSource = orderHistoryViewModel.GetCategoryChartItemsByTable(tableViewModel.tables[0].Number);
            cbTable.ItemsSource = tableViewModel.tables;
            tbTodayTotalSales.Text = $"{orderHistoryViewModel.GetTodayTotalSales()}원";
            DataContext = this;

            chartItems = orderHistoryViewModel.GetFoodChartItems();

            UpdateGraph(GraphType.COUNT);
            SalesSeriesCollection = new SeriesCollection();
            List<SalesByTime> salesByTimeList = orderHistoryViewModel.GetChartItemsByTime();

            LineSeries lineSeries = new LineSeries
            {
                Title = "시간별 매출",
                Values = new ChartValues<int>(),
            };
            foreach (SalesByTime salesByTime in salesByTimeList)
            {
                Labels.Add(salesByTime.Name);
                lineSeries.Values.Add(salesByTime.Sales);
            }

            SalesSeriesCollection.Add(lineSeries);

            cbTable.SelectedIndex = 0;
        }

        private void UpdateGraph(GraphType type)
        {
            SeriesCollection = new SeriesCollection();

            foreach (ChartItem foodChartItem in chartItems)
            {
                int value = 0;

                if (type == GraphType.PRICE)
                {
                    value = foodChartItem.Price;
                }
                else if (type == GraphType.COUNT)
                {
                    value = foodChartItem.Count;
                }

                SeriesCollection.Add(new PieSeries()
                {
                    Title = foodChartItem.Name,
                    Values = new ChartValues<int> { value },
                    DataLabels = true,
                });
            }
        }

        private void btnAllTotalSales_Click(object sender, RoutedEventArgs e)
        {
            orderHistoryViewModel.SetAllPayemntSales();
        }

        private void btnCashTotalSales_Click(object sender, RoutedEventArgs e)
        {
            orderHistoryViewModel.SetCashCashPayment();
        }

        private void btnCardTotalSales_Click(object sender, RoutedEventArgs e)
        {
            orderHistoryViewModel.SetCardPaymentSales();
        }

        private void btnCategoryStat_Click(object sender, RoutedEventArgs e)
        {
            chartItems = orderHistoryViewModel.GetCategoryChartItems();
            UpdateGraph(GraphType.PRICE);
        }

        private void btnMenuStat_Click(object sender, RoutedEventArgs e)
        {
            chartItems = orderHistoryViewModel.GetFoodChartItems();
            UpdateGraph(GraphType.PRICE);
        }

        private void btnPriceGraph_Click(object sender, RoutedEventArgs e)
        {
            UpdateGraph(GraphType.PRICE);
        }

        private void btnCountGraph_Click(object sender, RoutedEventArgs e)
        {
            UpdateGraph(GraphType.COUNT);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

        private void cbTable_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbTable.SelectedIndex == -1) return;

            DiningTable table = (DiningTable)cbTable.SelectedItem;
            dgFoodTable.ItemsSource = orderHistoryViewModel.GetFoodChartItemByTable(table.Number);
            dgCategoryTable.ItemsSource = orderHistoryViewModel.GetCategoryChartItemsByTable(table.Number);
        }
    }
}
