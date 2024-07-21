using LiveCharts;
using LiveCharts.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using TicketManagement.Models;
using TicketManagement.Repositories;

namespace TicketManagement.ViewModel.Home
{
    public class HomeViewModel : BaseViewModel
    {
        private ObservableCollection<ReportModel> _ListReport;
        private int _TotalTicket = 0;
        private int _Location = 0;
        public ObservableCollection<ReportModel> ListReport { get => _ListReport; set { _ListReport = value; OnPropertyChanged(); } }
        public int TotalTicket { get => _TotalTicket; set { _TotalTicket = value; OnPropertyChanged(); } }
        public int Location { get => _Location; set { _Location = value; OnPropertyChanged(); } }
        private int _Revenue;

        public int Revenue
        {
            get { return _Revenue; }
            set { _Revenue = value; OnPropertyChanged(); }
        }

        public HomeRepository homeRepository;
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
        public ReportRepository reportRepository;
        public Ticketrepository ticketRepository;
        public HomeViewModel()
        {
            reportRepository = ReportRepository.GetInstance();
            ticketRepository = Ticketrepository.GetInstance();
            Task.Run(Initialize);
        }
        private async void Initialize()
        {
            IsExecuting = true;
            homeRepository = HomeRepository.GetInstance();
            //ListReport = new ObservableCollection<ReportModel>(await homeRepository.GetHomeData());
            TotalTicket = await homeRepository.GetTotalTicket();
            Location = await homeRepository.GetTotalOcation();
            DateTime now = DateTime.Now;
            Revenue = await ticketRepository.getRevenue(0, 0, now, now);
            List<HomeChartData> homeChartDatas = await reportRepository.GetHomeChartDatasAsync();
            var Chartvalue1 = new ChartValues<int>();
            var Chartvalue2 = new ChartValues<int>();
            foreach (var item in homeChartDatas)
            {
                Chartvalue2.Add(item.Paid);
            }
            foreach (var item in homeChartDatas)
            {
                Chartvalue1.Add(item.UnPaid);
            }
            App.Current.Dispatcher.Invoke(() =>
            {
                SeriesCollection = new SeriesCollection
            {
                new StackedColumnSeries
                {
                    Title = "Đã thanh toán:",
                    Values = Chartvalue2
                },
                new StackedColumnSeries
                {
                    Title = "Chưa thanh toán:",
                    Values = Chartvalue1
                }
            };
                List<string> locationname = new List<string>();
                homeChartDatas.ForEach(x => locationname.Add(x.LocationName));
                Labels = new List<string>(locationname);

            });
            IsExecuting = false;
        }
    }
}
