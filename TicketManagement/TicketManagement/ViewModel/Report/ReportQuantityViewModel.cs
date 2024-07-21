using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using TicketManagement.Models;
using TicketManagement.Repositories;
using TicketManagement.View.Report;

namespace TicketManagement.ViewModel.Report
{
    public class ReportQuantityViewModel : BaseViewModel
    {
        private DateTime _DateStart;

        public DateTime DateStart
        {
            get { return _DateStart; }
            set { _DateStart = value; OnPropertyChanged(); }
        }
        private DateTime _DateEnd;

        public DateTime DateEnd
        {
            get { return _DateEnd; }
            set { _DateEnd = value; OnPropertyChanged(); }
        }
        public Ticketrepository TicketRepository { get; set; }
        public ICommand ExportCommand { get; set; }
        public ICommand CancelCommand { get; set; }
        private int option;
        public ReportQuantityViewModel(int id)
        {
            TicketRepository = Ticketrepository.GetInstance();
            DateStart = DateTime.Now;
            DateEnd = DateTime.Now;
            option = id;
            ExportCommand = new RelayCommand<Window>((w) => true, (w) => ExecuteExport(w));
            CancelCommand = new RelayCommand<Window>((w) => true, (w) => w.Close());
        }

        private async void ExecuteExport(Window w)
        {
            ReportWindowView report = new ReportWindowView();
            List<Models.ReportByLocation> reportByLocations = new List<Models.ReportByLocation>();
            List<ReportByVehicle> reportByVehicles = new List<ReportByVehicle>();
            int Total = await TicketRepository.getRevenue(0, 0, DateStart, DateEnd);
            if (option == 2)
            {
                reportByVehicles = await TicketRepository.getReportQuantityVehicle(DateStart, DateEnd);
                report.ReportQuantityVehicle(reportByVehicles, Total, DateStart.ToString("dd/MM/yyyy"), DateEnd.ToString("dd/MM/yyyy"));
                w.Close();
            }
            else if (option == 3)
            {
                reportByLocations = await TicketRepository.getReportQuantityLocation(DateStart, DateEnd);
                report.ReportQuantityLocation(reportByLocations, Total, DateStart.ToString("dd/MM/yyyy"), DateEnd.ToString("dd/MM/yyyy"));
                w.Close();
            }
            report.Show();
        }
    }
}
