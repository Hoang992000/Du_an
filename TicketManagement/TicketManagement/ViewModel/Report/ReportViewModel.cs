using LiveCharts;
using LiveCharts.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using TicketManagement.Models;
using TicketManagement.Repositories;
using TicketManagement.View.Report;

namespace TicketManagement.ViewModel.Report
{
    public class ListReportModel
    {
        public int STT { get; set; }
        public string Description { get; set; }
    }
    public class ReportViewModel : BaseViewModel
    {
        private ObservableCollection<ListReportModel> _ListReport;
        public ObservableCollection<ListReportModel> ListReport { get => _ListReport; set { _ListReport = value; OnPropertyChanged(); } }
        public ICommand StartCommand { get; set; }

        public ReportRepository reportRepository;
        private SeriesCollection _seriesCollection;
        public SeriesCollection SeriesCollection
        {
            get { return _seriesCollection; }
            set { _seriesCollection = value; OnPropertyChanged(); }
        }
        private List<string> _labels;
        public List<string> Labels
        {
            get { return _labels; }
            set { _labels = value; OnPropertyChanged(); }
        }
        private Func<double, string> _yFormatter;
        public Func<double, string> YFormatter
        {
            get { return _yFormatter; }
            set { _yFormatter = value; OnPropertyChanged(); }
        }
        public ReportViewModel()
        {
            Task.Run(Initialize);

            StartCommand = new RelayCommand<ListReportModel>((p) => true, (p) => ExecuteReport(p));
        }
        private async void Initialize()
        {
            IsExecuting = true;
            reportRepository = ReportRepository.GetInstance();
            ListReport = new ObservableCollection<ListReportModel>
            {
                new ListReportModel()
                {
                    STT = 1,
                    Description = "Thống kê chi tiết vé",
                },
                new ListReportModel()
                {
                    STT = 2,
                    Description = "Thống kê số lượng theo phương tiện",
                },
                new ListReportModel()
                {
                    STT = 3,
                    Description = "Thống kê số lượng theo địa điểm",
                }
            };
            List<ChartData> chartData = await reportRepository.GetChartDatasAsync();
            var Chartvalue = new ChartValues<double>();
            foreach (var item in chartData)
            {
                Chartvalue.Add(item.Revenue);
            }
            App.Current.Dispatcher.Invoke(() =>
            {
                SeriesCollection = new SeriesCollection
            {
                new LineSeries
                {
                    Title = "Doanh thu",
                    Values = Chartvalue
                }
            };
                Labels = new List<string> { "T1", "T2", "T3", "T4", "T5", "T6", "T7", "T8", "T9", "T10", "T11", "T12" };
                YFormatter = value => value.ToString("C0", new System.Globalization.CultureInfo("vi-VN"));
            });
            IsExecuting = false;
        }
        private void ExecuteReport(ListReportModel report)
        {
            switch (report.STT)
            {
                case 1:
                    View.Report.ReportByLocation reportByLocation = new View.Report.ReportByLocation();
                    reportByLocation.ShowDialog();
                    break;
                case 2:
                    ReportQuantityView reportByQuantity = new ReportQuantityView(report.STT);
                    reportByQuantity.ShowDialog();
                    break;
                case 3:
                    ReportQuantityView reportByQuantity2 = new ReportQuantityView(report.STT);
                    reportByQuantity2.ShowDialog();
                    break;
            }
        }
    }
}
